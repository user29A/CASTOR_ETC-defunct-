/*Copyright 2018 Joseph Edwin Postma

Contact email: joepostma@live.ca

This file is part of CETC.

CETC is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

CETC is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

See http://www.gnu.org/licenses/ for a copy of the
GNU General Public License. */


#include "Form1.h"

using namespace CETC;


void Form1::ThroughPutETC()
{
	FINAL_COUNTS = gcnew array<double>(5);
	FINAL_MAXS = gcnew array<double>(5);
	FINAL_COUNTS_BG = gcnew array<double>(5);
	SN_TIME = gcnew array<double>(5);
	SOURCE_FLUX = gcnew array<double>(NELEMENTS);
	SOURCE_FLUX_LOCAL = gcnew array<double>(NELEMENTS);
	double FWHM = (double)FWHMUpD->Value;//arcsec
	double pscale = (double)PlateScaleUpD->Value;//arcsec/pixel
	double sigma = FWHM / 2.355 / pscale;//pixels

	BACKGROUND = JPFits::FitsFile::ReadImageVectorOnly(BACKGROUND_FILENAME, nullptr);
	//BackGround is in log values; needs to be converted to SI and to appropriate pixel area, from (erg/cm^2/s/A/arcsec^2) to (J/m^2/s/m/Npix^2)
	//= 1x10^-7 (J/erg) / 1x10^-4 (m^2/cm^2) / 1x10^-10 (m/A) * pscale^2 (arcsec^2/pixel^2) = 1e7 * pscale^2
	//and to 5 sigma??
	double fac = 1e7 * pscale * pscale * (5 * sigma) * (5 * sigma);
	for (int i = 0; i < LAMBDA_NM->Length; i++)
		BACKGROUND[i] = Math::Pow(10, BACKGROUND[i]) * fac;

	#pragma region Sources and Source Flux Computation
	if (SourceBlackbodyRadBtn->Checked)
		SourceBlackBody();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

	if (SourcePowerLawRadBtn->Checked)
		SourcePowerLaw();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

	if (SourceStarRadBtn->Checked)
		SourceSpectralType();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)
	
	if (SourceGalaxyRadBtn->Checked)
		SourceGalaxy();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

	if (SourceAGNRadBtn->Checked)
		SourceAGN();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)
	#pragma endregion

	#pragma region Extinction Computation
	double Av;
	if (ExtinctionAvRadBtn->Checked)
		Av = Convert::ToDouble(ExtinctionAvTxt->Text);
	if (ExtinctionColumnDensityRadBtn->Checked)
		Av = Convert::ToDouble(ExtinctionColumnDensityTxt->Text);
	if (ExtinctionDistanceRadBtn->Checked)
	{
		double distance = ::Convert::ToDouble(DistanceTxt->Text);
		Av = distance * 1.6;
	}

	double Rv = Convert::ToDouble(ExtinctionRvTxt->Text);

	for (int i = 0; i < LAMBDA_NM->Length; i++)
	{
		double Alambda = (a_lambda(LAMBDA_NM[i]) + b_lambda(LAMBDA_NM[i]) / Rv) * Av;
		EXTINCTION[i] = Math::Pow(10, -0.4*Alambda);
		SOURCE_FLUX_LOCAL[i] *= EXTINCTION[i];
	}
	#pragma endregion

	//plot source flux * extinction * distance
	Chart_Source->Series[0]->Points->Clear();
	if (ShowLocalFlux->Checked)
	{
		Chart_Source->Titles[0]->Text = "Local Source Flux (Source * Extinction * Distance)";
		for (int i = 0; i < LAMBDA_NM->Length; i++)
			Chart_Source->Series[0]->Points->AddXY(LAMBDA_NM[i], SOURCE_FLUX_LOCAL[i]);
	}
	else
	{
		Chart_Source->Titles[0]->Text = "Source Flux";
		for (int i = 0; i < LAMBDA_NM->Length; i++)
			Chart_Source->Series[0]->Points->AddXY(LAMBDA_NM[i], SOURCE_FLUX[i]);
	}
	Chart_Source->ChartAreas[0]->AxisX->Minimum = PLOT_MIN;
	Chart_Source->ChartAreas[0]->AxisX->Maximum = PLOT_MAX;

	#pragma region Final Filter Flux Computation
	double SN_target = (double)SNTargetUpD->Value;
	for (int i = 0; i < FILTER_FILENAMES->Length; i++)
	{
		for (int j = 0; j < LAMBDA_NM->Length; j++)
		{
			double lambda_m = LAMBDA_NM[j] * 1e-9;

			FINAL_FLUX_FILTERS[j, i] = (SOURCE_FLUX_LOCAL[j] * lambda_m/(h*c)) * EXTINCTION[j] * DETECTOR_QE[j] * FILTERS[j, i] * MIRROR_REFL[j] * MIRROR_REFL[j] * MIRROR_EFF_AREA * 1e-9;
			FINAL_FLUX_FILTERS_BG[j, i] = (BACKGROUND[j] * lambda_m/(h*c)) * DETECTOR_QE[j] * FILTERS[j, i] * MIRROR_REFL[j] * MIRROR_REFL[j] * MIRROR_EFF_AREA * 1e-9;

			FINAL_COUNTS[i] += FINAL_FLUX_FILTERS[j, i] * LAMBDASTEP;
			FINAL_COUNTS_BG[i] += FINAL_FLUX_FILTERS_BG[j, i] * LAMBDASTEP;
		}

		if (ISPOINTSOURCE)//blackbody, spectral type, power law
		{
			double A = FINAL_COUNTS[i] / (2 * Math::PI*sigma*sigma);
			double sum = 0;
			int n = 200;
			double tn_2 = double(2 * n * 2 * n);
			double t_s2 = 2 * sigma * sigma;
			double tnp1_2 = double((2 * n + 1) * (2 * n + 1));
			#pragma omp parallel for reduction(+:sum)
			for (int x = -n; x <= n; x++)
				for (int y = -n; y <= n; y++)
					sum += A * Math::Exp(-(x*x + y*y) / tn_2 / t_s2);

			FINAL_MAXS[i] = sum / tnp1_2;
		}
		else//galaxy, AGN
		{

		}

		SN_TIME[i] = SN_target * SN_target * (1 + FINAL_COUNTS_BG[i] / FINAL_COUNTS[i]) / FINAL_COUNTS[i];
	}

	#pragma endregion

	COMPUTATION_EXISTS = true;

	//uvdark, uv, uwide, u, g
	TotalCountUVDarkLabel->Text = FINAL_COUNTS[0].ToString("e2");
	TotalCountUVLabel->Text = FINAL_COUNTS[1].ToString("e2");
	TotalCountuWideLabel->Text = FINAL_COUNTS[2].ToString("e2");
	TotalCountuLabel->Text = FINAL_COUNTS[3].ToString("e2");
	TotalCountgLabel->Text = FINAL_COUNTS[4].ToString("e2");

	//uvdark, uv, uwide, u, g
	MaxCountUVDarkLabel->Text = FINAL_MAXS[0].ToString("e2");
	MaxCountUVLabel->Text = FINAL_MAXS[1].ToString("e2");
	MaxCountuWideLabel->Text = FINAL_MAXS[2].ToString("e2");
	MaxCountuLabel->Text = FINAL_MAXS[3].ToString("e2");
	MaxCountgLabel->Text = FINAL_MAXS[4].ToString("e2");
	
	//uvdark, uv, uwide, u, g
	SNTimeUVDarkLabel->Text = SN_TIME[0].ToString("e2");
	SNTimeUVLabel->Text = SN_TIME[1].ToString("e2");
	SNTimeuWideLabel->Text = SN_TIME[2].ToString("e2");
	SNTimeuLabel->Text = SN_TIME[3].ToString("e2");
	SNTimegLabel->Text = SN_TIME[4].ToString("e2");

	Chart_Filter->Series[0]->Points->Clear();
	Chart_Final->Series[0]->Points->Clear();
	for (int i = 0; i < LAMBDA_NM->Length; i++)
	{
		Chart_Filter->Series[0]->Points->AddXY(LAMBDA_NM[i], FILTERS[i, SELECTED_FILTER]);
		Chart_Final->Series[0]->Points->AddXY(LAMBDA_NM[i], FINAL_FLUX_FILTERS[i, SELECTED_FILTER]);
	}
	Chart_Filter->ChartAreas[0]->AxisX->Minimum = PLOT_MIN;
	Chart_Filter->ChartAreas[0]->AxisX->Maximum = PLOT_MAX;

	Chart_Final->ChartAreas[0]->AxisX->Minimum = PLOT_MIN;
	Chart_Final->ChartAreas[0]->AxisX->Maximum = PLOT_MAX;
}

