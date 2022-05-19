using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace CASTOR_ETC
{
	public partial class Form1 : Form
	{
		Label[] totalcountlabels = new Label[5];
		Label[] maxcountlabels = new Label[5];
		Label[] bgcountlabels = new Label[5];
		Label[] msntimelabels = new Label[5];
		Label[] tsntimelabels = new Label[5];

		double FV_SURFACE;
		double NORM_MAG;

		string[] STARTARGS;
		int SESSION = 0;
		bool FIRSTSLOAD = false;
		bool COMPUTATION_EXISTS = false;
		bool ISPOINTSOURCE = true;
		int LAMBDASTEP = 1;//lambda steps in 1nm increments
		double[] LAMBDA_NM;//lambda is 120nm to 1100nm in 1nm steps giving 1100 - 120 + 1 = 981 elements
		static int NELEMENTS = 1100 - 120 + 1;
		double PLOT_MIN = 120;
		double PLOT_MAX = 620;
		double[] SOURCE_FLUX = new double[NELEMENTS];//vector for source flux curve
		double[] SOURCE_FLUX_LOCAL = new double[NELEMENTS];//vector for source flux curve
		double[] EXTINCTION = new double[NELEMENTS];//vector for extintion curve
		double[] DETECTOR_QE = new double[NELEMENTS];//vector for quantum efficiency curve
		double[] MIRROR_REFL = new double[NELEMENTS];//vector for mirror reflectivity curve
		double[] BACKGROUND = new double[NELEMENTS];//vector for background curve
		double[,] FILTERS = new double[NELEMENTS, 5];//uvdark, uv, uwide, u, g  --vector array for filters
		double[,] FINAL_FLUX_FILTERS = new double[NELEMENTS, 5];//uvdark, uv, uwide, u, g  --vector array for final fluxes each filter
		double[,] FINAL_FLUX_FILTERS_BG = new double[NELEMENTS, 5];//uvdark, uv, uwide, u, g  --vector array for final fluxes each filter backgrounds
		double[] FINAL_COUNTS;
		double[] FINAL_COUNTS_MAXPIX;
		double[] FINAL_COUNTS_BG;
		double[] SN_TIME_MCPS;
		double[] SN_TIME_TCPS;

		int APERTURENPIX = 0;

		//double MIRROR_AREA; //mirror effective collection area, set at Form1_Load

		int SELECTED_FILTER = 0;

		static string CASTOR_DIRECTORY = "C:\\CASTOR_ETC\\";
		string LAMBDA_NM_FILENAME = CASTOR_DIRECTORY + "Lambda\\lambda_nm.fits";
		string QE_FILENAME = CASTOR_DIRECTORY + "QE\\QE.fits";
		string MIRROR_REFL_FILENAME = CASTOR_DIRECTORY + "Reflectivity\\reflectivity.fits";
		//string MIRROR_AREA_FILENAME = CASTOR_DIRECTORY + "Area\\area.txt";
		string BACKGROUND_FILENAME = CASTOR_DIRECTORY + "Background\\background.fits";
		string[] FILTER_FILENAMES = new string[5];//uvdark, uv, uwide, u, g,
		string[] FILTER_NAMES;//uvdark, uv, uwide, u, g,
		string FILTER_uvdark_FILENAME = CASTOR_DIRECTORY + "Filters\\uv_dark_135_260.fits";
		string FILTER_uv_FILENAME = CASTOR_DIRECTORY + "Filters\\uv_150_300.fits";
		string FILTER_uwide_FILENAME = CASTOR_DIRECTORY + "Filters\\u_wide_260_400.fits";
		string FILTER_u_FILENAME = CASTOR_DIRECTORY + "Filters\\u_300_400.fits";
		string FILTER_g_FILENAME = CASTOR_DIRECTORY + "Filters\\g_440_550.fits";

		double h = 6.626068E-34; //Js
		double c = 2.9979E8; //m/s
		double K = 1.38065E-23; //J/K
		//double Rsun = 6.96E8; //m
		double SR2KPC = 2.25534E-11; //solar radii to kpc
		double KPC2M = 3.08568025E19; //kpc to meters

		public Form1()
		{
			InitializeComponent();
			string[] startargs = new string[0];
			InitializeVars(startargs);			
		}

		void SetReg(System.String ProgramName, System.String KeyName, System.Object KeyValue)
		{
			RegistryKey User = Registry.CurrentUser;
			RegistryKey SW = User.OpenSubKey("Software", true);
			RegistryKey AstroWerks = SW.CreateSubKey("AstroWerks");
			RegistryKey SUBKEY = AstroWerks.CreateSubKey(ProgramName);
			SUBKEY.SetValue(KeyName, KeyValue);
		}

		Object GetReg(System.String ProgramName, System.String KeyName)
		{
			RegistryKey User = Registry.CurrentUser;
			RegistryKey SW = User.OpenSubKey("Software", true);
			RegistryKey AstroWerks = SW.CreateSubKey("AstroWerks");
			RegistryKey SUBKEY = AstroWerks.CreateSubKey(ProgramName);
			Object result = SUBKEY.GetValue(KeyName);
			return result;
		}

		void ThroughPutETC()
		{
			if (STARTARGS.Length > 0)
				return;

			FINAL_COUNTS = new double[FILTER_FILENAMES.Length];
			FINAL_COUNTS_MAXPIX = new double[FILTER_FILENAMES.Length];
			FINAL_COUNTS_BG = new double[FILTER_FILENAMES.Length];
			SN_TIME_MCPS = new double[FILTER_FILENAMES.Length];
			SN_TIME_TCPS = new double[FILTER_FILENAMES.Length];
			SOURCE_FLUX = new double[NELEMENTS];
			SOURCE_FLUX_LOCAL = new double[NELEMENTS];
			double psfFWHM = (double)FWHMUpD.Value;//arcsec
			double platescale = (double)PlateScaleUpD.Value;//arcsec/pixel
			int apertureHWpix = (int)Math.Round(5 * psfFWHM / 2.355 / platescale);//pixels
			double mirrorarea = (double)MirrorAreaUpD.Value;
			APERTURENPIX = 0;
			if (ISPOINTSOURCE)
			{
				for (int i = -apertureHWpix; i <= apertureHWpix; i++)
					for (int j = -apertureHWpix; j <= apertureHWpix; j++)
						if (i * i + j * j <= apertureHWpix * apertureHWpix)
							APERTURENPIX++;

				toolTip1.SetToolTip(label16, APERTURENPIX.ToString() + " total pixels in total count aperture (+-5 sigma radius of PSF)");
			}
			else
			{
				APERTURENPIX = 1;
				toolTip1.SetToolTip(label16, APERTURENPIX.ToString() + " pixel in count values");
			}

			double RN = (double)ReadNoiseUpD.Value;
			double DR = ((double)DarkRateUpD.Value) / 60;
			double CR = 0;//cosmic ray rate

			double SersicN = (double)(SersicnUpD.Value);
			double SersicReff = (double)(SersicReffUpD.Value);//arcsec
			double SersicIeff = Convert.ToDouble(mIeTxt.Text);

			int n = 50;
			double tn_2 = (double)(2 * n * 2 * n);
			double psfsigma = psfFWHM / 2.355 / platescale;//pixels
			double t_s2 = 2 * psfsigma * psfsigma;
			double tnp1_2 = (double)((2 * n + 1) * (2 * n + 1));

			BACKGROUND = JPFITS.FITSImage.ReadImageVectorOnly(BACKGROUND_FILENAME, null, false);
			//BackGround is in log values; needs to be converted to SI and to appropriate pixel area, from (erg/cm2/s/A/arcsec2) to (J/m2/s/m/Npix2)
			//= 1x10-7 (J/erg) / 1x10-4 (m2/cm2) / 1x10-10 (m/A) * pscale2 (arcsec2/pixel2) = 1e7 * pscale2
			double fac = 1e7 * platescale * platescale;
			for (int i = 0; i < LAMBDA_NM.Length; i++)
				BACKGROUND[i] = Math.Pow(10, BACKGROUND[i]) * fac;

			#region Sources and Source Flux Computation
			if (SourceBlackbodyRadBtn.Checked)
				SourceBlackBody();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

			if (SourcePowerLawRadBtn.Checked)
				SourcePowerLaw();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

			if (SourceStarRadBtn.Checked)
				SourceSpectralType();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

			if (SourceGalaxyRadBtn.Checked)
				SourceGalaxy();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)

			if (SourceAGNRadBtn.Checked)
				SourceAGN();//sets SOURCE_FLUX and SOURCE_FLUX_LOCAL (before extinction)
			#endregion

			#region Extinction Computation
			double Av = 1;
			if (ExtinctionAvRadBtn.Checked)
				Av = Convert.ToDouble(ExtinctionAvTxt.Text);
			if (ExtinctionColumnDensityRadBtn.Checked)
				Av = Convert.ToDouble(ExtinctionColumnDensityTxt.Text);
			if (ExtinctionDistanceRadBtn.Checked)
				Av = Convert.ToDouble(DistanceTxt.Text) * 1.6;

			double Rv = Convert.ToDouble(ExtinctionRvTxt.Text);

			for (int i = 0; i < LAMBDA_NM.Length; i++)
			{
				double Alambda = (a_lambda(LAMBDA_NM[i]) + b_lambda(LAMBDA_NM[i]) / Rv) * Av;
				EXTINCTION[i] = Math.Pow(10, -0.4 * Alambda);
				SOURCE_FLUX_LOCAL[i] *= EXTINCTION[i];//extinction is computed into the local source flux here, not below at the filter flux computation//
			}
			#endregion			

			#region Final Filter Flux Computation
			double SN_target = (double)SNTargetUpD.Value;
			for (int i = 0; i < FILTER_FILENAMES.Length; i++)
			{
				for (int j = 0; j < LAMBDA_NM.Length; j++)
				{
					double lambda_m = LAMBDA_NM[j] * 1e-9;

					FINAL_FLUX_FILTERS[j, i] = (SOURCE_FLUX_LOCAL[j] * lambda_m / (h * c)) * DETECTOR_QE[j] * FILTERS[j, i] * MIRROR_REFL[j] * MIRROR_REFL[j] * mirrorarea * 1e-9;
					FINAL_FLUX_FILTERS_BG[j, i] = (BACKGROUND[j] * lambda_m / (h * c)) * DETECTOR_QE[j] * FILTERS[j, i] * MIRROR_REFL[j] * MIRROR_REFL[j] * mirrorarea * 1e-9;

					FINAL_COUNTS[i] += FINAL_FLUX_FILTERS[j, i] * LAMBDASTEP;
					FINAL_COUNTS_BG[i] += FINAL_FLUX_FILTERS_BG[j, i] * LAMBDASTEP;
				}

				if (ISPOINTSOURCE)//blackbody, spectral type, power law, AGN
				{
					double A = FINAL_COUNTS[i] / (2 * Math.PI * psfsigma * psfsigma);

					double sum = 0;
					//#pragma omp parallel for reduction (+: sum)
					for (int x = -n; x <= n; x++)
						for (int y = -n; y <= n; y++)
							sum += Math.Exp(-(double)(x * x + y * y) / tn_2 / t_s2);

					FINAL_COUNTS_MAXPIX[i] = sum * A / tnp1_2;
				}
				else//galaxy
				{
					//compute Sersic function, get max value

					double Re = (double)(SersicReffUpD.Value);//arcsec
					double nser = (double)(SersicnUpD.Value);
					double b = 1.9992 * n - 0.3271;

					double Ie = FINAL_COUNTS[i] * Math.Pow(b, 2 * nser) / alglib.gammafunction(2 * nser) / Re / Re / 2 / Math.PI / nser / Math.Exp(b);
					double I0 = Ie * Math.Exp(b);

					Ie = Ie / platescale / platescale;//from /arcsec2 to /pixel2
					I0 = I0 / platescale / platescale;//from /arcsec2 to /pixel2

					FINAL_COUNTS_MAXPIX[i] = I0;
					FINAL_COUNTS[i] = Ie;//change final counts here, which are total counts for a point source, to the counts at Re since the SN at Re is what we want
				}

				//SN_TIME_MCPS[i] = SN_target * SN_target * (1 + FINAL_COUNTS_BG[i] / FINAL_COUNTS_MAXPIX[i]) / FINAL_COUNTS_MAXPIX[i];
				//SN_TIME_TCPS[i] = SN_target * SN_target * (1 + FINAL_COUNTS_BG[i] * (double)(APERTURENPIX) / FINAL_COUNTS[i]) / FINAL_COUNTS[i];

				double a = FINAL_COUNTS_MAXPIX[i] * FINAL_COUNTS_MAXPIX[i] / SN_target / SN_target;
				double r = FINAL_COUNTS_MAXPIX[i] + FINAL_COUNTS_BG[i] + DR + CR;

				SN_TIME_MCPS[i] = 0.5 / a * (r + Math.Sqrt(r * r + 4 * a * RN));

				a = FINAL_COUNTS[i] * FINAL_COUNTS[i] / SN_target / SN_target;
				r = FINAL_COUNTS[i] + (FINAL_COUNTS_BG[i] + DR + CR) * (double)(APERTURENPIX);

				SN_TIME_TCPS[i] = 0.5 / a * (r + Math.Sqrt(r * r + 4 * a * RN * (double)(APERTURENPIX)));
			}
			#endregion

			COMPUTATION_EXISTS = true;

			FormatLabels();

			//plot source flux * extinction * distance
			Chart_Source.Series[0].Points.Clear();
			if (ShowLocalFlux.Checked)
			{
				Chart_Source.Titles[0].Text = "Local Source Flux (Source * Extinction * Distance)";
				for (int i = 0; i < LAMBDA_NM.Length; i++)
					Chart_Source.Series[0].Points.AddXY(LAMBDA_NM[i], SOURCE_FLUX_LOCAL[i]);
			}
			else
			{
				Chart_Source.Titles[0].Text = "Source Flux";
				for (int i = 0; i < LAMBDA_NM.Length; i++)
					Chart_Source.Series[0].Points.AddXY(LAMBDA_NM[i], SOURCE_FLUX[i]);
			}
			Chart_Source.ChartAreas[0].AxisX.Minimum = PLOT_MIN;
			Chart_Source.ChartAreas[0].AxisX.Maximum = PLOT_MAX;

			Chart_Filter.Series[0].Points.Clear();
			Chart_Final.Series.Clear();
			Chart_Final.Series.Add("Source");
			Chart_Final.Series["Source"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			Chart_Final.Series["Source"].BorderWidth = 1;
			Chart_Final.Series["Source"].Color = System.Drawing.Color.Red;
			Chart_Final.Series.Add("Background");
			Chart_Final.Series["Background"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			Chart_Final.Series["Background"].BorderWidth = 3;
			Chart_Final.Series["Background"].Color = System.Drawing.Color.Black;
			for (int i = 0; i < LAMBDA_NM.Length; i++)
			{
				Chart_Filter.Series[0].Points.AddXY(LAMBDA_NM[i], FILTERS[i, SELECTED_FILTER]);
				Chart_Final.Series["Source"].Points.AddXY(LAMBDA_NM[i], FINAL_FLUX_FILTERS[i, SELECTED_FILTER]);
				Chart_Final.Series["Background"].Points.AddXY(LAMBDA_NM[i], FINAL_FLUX_FILTERS_BG[i, SELECTED_FILTER] * (double)(APERTURENPIX));
			}

			Chart_Filter.ChartAreas[0].AxisX.Minimum = PLOT_MIN;
			Chart_Filter.ChartAreas[0].AxisX.Maximum = PLOT_MAX;

			Chart_Final.ChartAreas[0].AxisX.Minimum = PLOT_MIN;
			Chart_Final.ChartAreas[0].AxisX.Maximum = PLOT_MAX;

			Chart_Final.Legends.Clear();
			Chart_Final.Legends.Add("legend");
			Chart_Final.Legends["legend"].DockedToChartArea = Chart_Final.ChartAreas[0].Name;
		}

		void FilterGroupBox_Enter(System.Object sender, System.EventArgs e) { }

		void InitializeVars(string[] startargs)
		{
			STARTARGS = startargs;
		}

		void Form1_Load(System.Object sender, System.EventArgs e)
		{
			FIRSTSLOAD = true;

			SourceGalaxyDrop.SelectedIndex = Convert.ToInt32(GetReg("CETC", "SourceGalaxyDropIndex"));
			SourceStarDrop.SelectedIndex = Convert.ToInt32(GetReg("CETC", "SourceStarDropIndex"));
			SourceBlackbodyTempTxt.Text = (string)GetReg("CETC", "SourceBlackbodyTempTxt");
			FilterUVRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "FilterUVRadBtnChckd"));
			FilteruRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "FilteruRadBtnChckd"));
			FiltergRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "FiltergRadBtnChckd"));
			FilterUVDarkRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "FilterUVDarkRadBtnChckd"));
			FilteruWideRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "FilterUWideRadBtnChckd"));
			SourceStarRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "SourceStarRadBtn"));
			SourceGalaxyRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "SourceGalaxyRadBtn"));
			SourceBlackbodyRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "SourceBlackbodyRadBtn"));
			SourceAGNRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "SourceAGNRadBtn"));
			SourceAGNDrop.SelectedIndex = Convert.ToInt32(GetReg("CETC", "SourceAGNDropIndex"));
			SourcePowerLawRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "SourcePowerLawRadBtn"));
			SourcePowerLawDrop.SelectedIndex = Convert.ToInt32(GetReg("CETC", "SourcePowerLawDropIndex"));
			DistanceRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "DistanceRadBtn"));
			mvRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "mvRadBtn"));
			ExtinctionColumnDensityTxt.Text = (string)GetReg("CETC", "ExtinctionColumnDensityTxt");
			ExtinctionAvTxt.Text = (string)GetReg("CETC", "ExtinctionAvTxt");
			ExtinctionRvTxt.Text = (string)GetReg("CETC", "ExtinctionRvTxt");
			ExtinctionColumnDensityRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "ExtinctionColumnDensityRadBtn"));
			ExtinctionDistanceRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "ExtinctionDistanceRadBtn"));
			ExtinctionAvRadBtn.Checked = Convert.ToBoolean(GetReg("CETC", "ExtinctionAvRadBtn"));
			RadiusTxt.Text = (string)GetReg("CETC", "RadiusTxt");
			mvTxt.Text = (string)GetReg("CETC", "mvTxt");
			DistanceTxt.Text = (string)GetReg("CETC", "DistanceTxt");
			RedShiftTxt.Text = (string)GetReg("CETC", "RedShiftTxt");
			mIeTxt.Text = (string)GetReg("CETC", "mTxt");
			PowerLawNormTxt.Text = (string)GetReg("CETC", "PowerLawNormTxt");
			PowerLawAlphaTxt.Text = (string)GetReg("CETC", "PowerLawAlphaTxt");
			SNTargetUpD.Value = Convert.ToDecimal(GetReg("CETC", "SNTargetUpD"));
			MirrorAreaUpD.Value = Convert.ToDecimal(GetReg("CETC", "MirrorArea"));
			PlateScaleUpD.Value = Convert.ToDecimal(GetReg("CETC", "PlateScaleUpD"));
			FWHMUpD.Value = Convert.ToDecimal(GetReg("CETC", "FWHMUpD"));
			SersicReffUpD.Value = Convert.ToDecimal(GetReg("CETC", "SersicReffUpD"));
			SersicnUpD.Value = Convert.ToDecimal(GetReg("CETC", "SersicnUpD"));
			ReadNoiseUpD.Value = Convert.ToDecimal(GetReg("CETC", "ReadNoiseUpD"));
			DarkRateUpD.Value = Convert.ToDecimal(GetReg("CETC", "DarkRateUpD"));

			LAMBDA_NM = JPFITS.FITSImage.ReadImageVectorOnly(LAMBDA_NM_FILENAME, null, false);

			DETECTOR_QE = JPFITS.FITSImage.ReadImageVectorOnly(QE_FILENAME, null, false);

			//BACKGROUND = JPFITS.FITSImage.ReadImageVectorOnly(BACKGROUND_FILENAME, null);
			//need to update background vector depending on pixel scale and convert to SI upon calculation...do in ThroughPutETC();

			MIRROR_REFL = JPFITS.FITSImage.ReadImageVectorOnly(MIRROR_REFL_FILENAME, null, false);

			/*System.IO.StreamReader sr = new System.IO.StreamReader(MIRROR_AREA_FILENAME);
			MIRROR_AREA = Convert.ToDouble(sr.ReadLine());
			sr.Close();*/

			FILTER_NAMES = new string[5] { "uvdark", "uv", "uwide", "u", "g" };//uvdark, uv, uwide, u, g,
																			   //uvdark, uv, uwide, u, g,
			FILTER_FILENAMES[0] = FILTER_uvdark_FILENAME;
			FILTER_FILENAMES[1] = FILTER_uv_FILENAME;
			FILTER_FILENAMES[2] = FILTER_uwide_FILENAME;
			FILTER_FILENAMES[3] = FILTER_u_FILENAME;
			FILTER_FILENAMES[4] = FILTER_g_FILENAME;
			for (int i = 0; i < FILTER_FILENAMES.Length; i++)
			{
				double[] filter = JPFITS.FITSImage.ReadImageVectorOnly(FILTER_FILENAMES[i], null, false);
				for (int j = 0; j < LAMBDA_NM.Length; j++)
					FILTERS[j, i] = filter[j];
			}

			FIRSTSLOAD = false;

			totalcountlabels[0] = TotalCountUVDarkLabel;
			totalcountlabels[1] = TotalCountUVLabel;
			totalcountlabels[2] = TotalCountuWideLabel;
			totalcountlabels[3] = TotalCountuLabel;
			totalcountlabels[4] = TotalCountgLabel;
			maxcountlabels[0] = MaxCountUVDarkLabel;
			maxcountlabels[1] = MaxCountUVLabel;
			maxcountlabels[2] = MaxCountuWideLabel;
			maxcountlabels[3] = MaxCountuLabel;
			maxcountlabels[4] = MaxCountgLabel;
			bgcountlabels[0] = BGCountUVDarkLabel;
			bgcountlabels[1] = BGCountUVLabel;
			bgcountlabels[2] = BGCountuWideLabel;
			bgcountlabels[3] = BGCountuLabel;
			bgcountlabels[4] = BGCountgLabel;
			msntimelabels[0] = MSNTimeUVDarkLabel;
			msntimelabels[1] = MSNTimeUVLabel;
			msntimelabels[2] = MSNTimeuWideLabel;
			msntimelabels[3] = MSNTimeuLabel;
			msntimelabels[4] = MSNTimegLabel;
			tsntimelabels[0] = TSNTimeUVDarkLabel;
			tsntimelabels[1] = TSNTimeUVLabel;
			tsntimelabels[2] = TSNTimeuWideLabel;
			tsntimelabels[3] = TSNTimeuLabel;
			tsntimelabels[4] = TSNTimegLabel;

			if (STARTARGS.Length > 0)//then this GUI been opened with a string argument for script execution
			{
				//Supress computation until all switches are set, unlike the GUI interface where switch changes
				//trigger the computation and graph updates etc.

				//read the argument file name and set all the parameters, then execute
				string inputfile = STARTARGS[0];

				System.IO.StreamReader sr = new System.IO.StreamReader(inputfile);
				string line = sr.ReadLine();

				if (line != "SESSION")
				{
					MessageBox.Show("Input file error at SESSION.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					SESSION = Convert.ToInt32(line);
				}
				catch
				{
					MessageBox.Show("Input file error at SESSION number: not an integer.  Stopping!");
					return;
				}
				//SESSION number now set

				line = sr.ReadLine();
				if (line != "SOURCE")
				{
					MessageBox.Show("Input file error at SOURCE.  Stopping!");
					return;
				}
				line = sr.ReadLine();  //ST | BB | GT | AGN | PL
				if (line == "ST")
					SourceStarRadBtn.Checked = true;
				else if (line == "BB")
					SourceBlackbodyRadBtn.Checked = true;
				else if (line == "GT")
					SourceGalaxyRadBtn.Checked = true;
				else if (line == "AGN")
					SourceAGNRadBtn.Checked = true;
				else if (line == "PL")
					SourcePowerLawRadBtn.Checked = true;
				else
				{
					MessageBox.Show("Input file error at SOURCE type: None of ST or BB or GT or AGN or PL.  Stopping!");
					return;
				}
				//source radio button now set

				line = sr.ReadLine();
				if (line != "BBTEMP")
				{
					MessageBox.Show("Input file error at BBTEMP.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at BBTEMP value: not a numeric value.  Stopping!");
					return;
				}
				SourceBlackbodyTempTxt.Text = line;
				//BB temperature set

				line = sr.ReadLine();
				if (line != "STYPE")
				{
					MessageBox.Show("Input file error at STYPE.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				int index = SourceStarDrop.FindString(line);
				if (index == -1)
				{
					MessageBox.Show("Input file error at Spectral Type value: not a spectral type.  Stopping!");
					return;
				}
				SourceStarDrop.SelectedIndex = index;
				//spectral type set

				line = sr.ReadLine();
				if (line != "ALPHA_NU")
				{
					MessageBox.Show("Input file error at ALPHA_NU.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToBoolean(line);
				}
				catch
				{
					MessageBox.Show("Input file error at Aplha_Nu value: not a bool.  Stopping!");
					return;
				}
				if (Convert.ToBoolean(line))
					SourcePowerLawDrop.SelectedIndex = 0;
				else
					SourcePowerLawDrop.SelectedIndex = 1;
				//alpha nu or alpha lambda set

				line = sr.ReadLine();
				if (line != "AGNTYPE")
				{
					MessageBox.Show("Input file error at AGNTYPE.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				index = SourceAGNDrop.FindString(line);
				if (index == -1)
				{
					MessageBox.Show("Input file error at AGN Type value: not an AGN type.  Stopping!");
					return;
				}
				SourceAGNDrop.SelectedIndex = index;
				//AGN type set

				line = sr.ReadLine();
				if (line != "GALTYPE")
				{
					MessageBox.Show("Input file error at GALTYPE.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				index = SourceGalaxyDrop.FindString(line);
				if (index == -1)
				{
					MessageBox.Show("Input file error at Galaxy Type value: not a Galaxy type.  Stopping!");
					return;
				}
				SourceGalaxyDrop.SelectedIndex = index;
				//Galaxy type set

				line = sr.ReadLine();
				if (line != "SOURCE_MV")
				{
					MessageBox.Show("Input file error at SOURCE_MV.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToBoolean(line);
				}
				catch
				{
					MessageBox.Show("Input file error at SOURCE_MV value: not a bool.  Stopping!");
					return;
				}
				if (Convert.ToBoolean(line))
				{
					mvRadBtn.Checked = true;
					mvRadBtn.Enabled = true;
					DistanceRadBtn.Checked = false;
					DistanceRadBtn.Enabled = false;
				}
				else
				{
					mvRadBtn.Checked = false;
					mvRadBtn.Enabled = false;
					DistanceRadBtn.Checked = true;
					DistanceRadBtn.Enabled = true;
				}
				//mvRadBtn and DistanceRadBtn set

				line = sr.ReadLine();
				if (line != "MV")
				{
					MessageBox.Show("Input file error at MV.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at MV value: not a numeric value.  Stopping!");
					return;
				}
				mvTxt.Text = line;
				//mvTxt value set

				line = sr.ReadLine();
				if (line != "DISTANCE")
				{
					MessageBox.Show("Input file error at DISTANCE.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at DISTANCE value: not a numeric value.  Stopping!");
					return;
				}
				DistanceTxt.Text = line;
				//Distance value set

				line = sr.ReadLine();
				if (line != "RADIUS")
				{
					MessageBox.Show("Input file error at RADIUS.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at RADIUS value: not a numeric value.  Stopping!");
					return;
				}
				RadiusTxt.Text = line;
				//Radius value set

				line = sr.ReadLine();
				if (line != "ALPHA")
				{
					MessageBox.Show("Input file error at ALPHA.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at ALPHA value: not a numeric value.  Stopping!");
					return;
				}
				PowerLawAlphaTxt.Text = line;
				//PowerLawAlpha value set

				line = sr.ReadLine();
				if (line != "NORM")
				{
					MessageBox.Show("Input file error at NORM.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at NORM value: not a numeric value.  Stopping!");
					return;
				}
				PowerLawNormTxt.Text = line;
				//PowerLawAlpha value set

				line = sr.ReadLine();
				if (line != "MAG")
				{
					MessageBox.Show("Input file error at MAG.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at MAG value: not a numeric value.  Stopping!");
					return;
				}
				mIeTxt.Text = line;
				//mTxt value set

				line = sr.ReadLine();
				if (line != "REDSHIFT")
				{
					MessageBox.Show("Input file error at REDSHIFT.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at REDSHIFT value: not a numeric value.  Stopping!");
					return;
				}
				RedShiftTxt.Text = line;
				//RedShiftTxt value set

				line = sr.ReadLine();
				if (line != "SERSICN")
				{
					MessageBox.Show("Input file error at SERSICN.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at SERSICN value: not a numeric value.  Stopping!");
					return;
				}
				SersicnUpD.Value = Convert.ToDecimal(line);
				//SersicnTxt value set

				line = sr.ReadLine();
				if (line != "REFF")
				{
					MessageBox.Show("Input file error at REFF.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at REFF value: not a numeric value.  Stopping!");
					return;
				}
				SersicReffUpD.Value = Convert.ToDecimal(line);
				//SersicReffTxt value set

				/*line = sr.ReadLine();
				if (line != "IEFF")
				{
					MessageBox.Show("Input file error at IEFF.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at IEFF value: not a numeric value.  Stopping!");
					return;
				}
				SersicIeffTxt.Text = line;*/
				//SersicIeffTxt value set

				line = sr.ReadLine();
				if (line != "EXTINCTION")
				{
					MessageBox.Show("Input file error at EXTINCTION.  Stopping!");
					return;
				}
				line = sr.ReadLine();  //VB | CD | D
				if (line == "VB")
					ExtinctionAvRadBtn.Checked = true;
				else if (line == "CD")
					ExtinctionColumnDensityRadBtn.Checked = true;
				else if (line == "D")
					ExtinctionDistanceRadBtn.Checked = true;
				else
				{
					MessageBox.Show("Input file error at SOURCE type: None of VB or CD or DL.  Stopping!");
					return;
				}
				//EXTINCTION radio button now set

				line = sr.ReadLine();
				if (line != "RV")
				{
					MessageBox.Show("Input file error at RV.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at RV value: not a numeric value.  Stopping!");
					return;
				}
				ExtinctionRvTxt.Text = line;
				//ExtinctionRvTxt value set

				line = sr.ReadLine();
				if (line != "AV")
				{
					MessageBox.Show("Input file error at AV.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at AV value: not a numeric value.  Stopping!");
					return;
				}
				ExtinctionAvTxt.Text = line;
				//ExtinctionAvTxt value set

				line = sr.ReadLine();
				if (line != "CD")
				{
					MessageBox.Show("Input file error at CD.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDouble(line);
				}
				catch
				{
					MessageBox.Show("Input file error at CD value: not a numeric value.  Stopping!");
					return;
				}
				ExtinctionColumnDensityTxt.Text = line;
				//ExtinctionColumnDensityTxt value set

				line = sr.ReadLine();
				if (line != "PLATESCALE")
				{
					MessageBox.Show("Input file error at PLATESCALE.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDecimal(line);
				}
				catch
				{
					MessageBox.Show("Input file error at PLATESCALE value: not a numeric value.  Stopping!");
					return;
				}
				PlateScaleUpD.Value = Convert.ToDecimal(line);
				//PlateScaleUpD value set

				line = sr.ReadLine();
				if (line != "FWHM")
				{
					MessageBox.Show("Input file error at FWHM.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDecimal(line);
				}
				catch
				{
					MessageBox.Show("Input file error at FWHM value: not a numeric value.  Stopping!");
					return;
				}
				FWHMUpD.Value = Convert.ToDecimal(line);
				//FWHMUpD value set

				line = sr.ReadLine();
				if (line != "FILTER")
				{
					MessageBox.Show("Input file error at FILTER.  Stopping!");
					return;
				}
				line = sr.ReadLine();  //uvd | uv | uw | u | g
				if (line == "uvd")
					FilterUVDarkRadBtn.Checked = true;
				else if (line == "uv")
					FilterUVRadBtn.Checked = true;
				else if (line == "uw")
					FilteruWideRadBtn.Checked = true;
				else if (line == "u")
					FilteruRadBtn.Checked = true;
				else if (line == "g")
					FiltergRadBtn.Checked = true;
				else
				{
					MessageBox.Show("Input file error at FILTER type: None of uvd or uv or uw or u or g.  Stopping!");
					return;
				}
				//FILTER radio button now set

				line = sr.ReadLine();
				if (line != "SN")
				{
					MessageBox.Show("Input file error at SN.  Stopping!");
					return;
				}
				line = sr.ReadLine();
				try
				{
					Convert.ToDecimal(line);
				}
				catch
				{
					MessageBox.Show("Input file error at SN value: not a numeric value.  Stopping!");
					return;
				}
				SNTargetUpD.Value = Convert.ToDecimal(line);
				//SNTargetUpD value set

				sr.Close();

				//reset startargs so that we can call ThroughPutETC()
				STARTARGS = new string[0];
				//now compute
				ThroughPutETC();

				//now save outputs
				SaveOutPuts();
			}
		}

		void SaveOutPuts()
		{
			string outdir = CASTOR_DIRECTORY + "Output\\" + SESSION.ToString() + "\\";
			System.IO.Directory.CreateDirectory(outdir);

			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(Chart_Source.Width, Chart_Source.Height);
			Chart_Source.DrawToBitmap(bmp, Chart_Source.DisplayRectangle);
			bmp.Save(outdir + "source_" + SESSION.ToString() + ".jpeg");

			bmp = new System.Drawing.Bitmap(Chart_Filter.Width, Chart_Filter.Height);
			Chart_Filter.DrawToBitmap(bmp, Chart_Filter.DisplayRectangle);
			bmp.Save(outdir + "filter_" + SESSION.ToString() + ".jpeg");

			bmp = new System.Drawing.Bitmap(Chart_Final.Width, Chart_Final.Height);
			Chart_Final.DrawToBitmap(bmp, Chart_Final.DisplayRectangle);
			bmp.Save(outdir + "final_" + SESSION.ToString() + ".jpeg");

			string[] labels = new string[11] { "lambda", "UVDark", "UV", "UVWide", "u", "g", "UVDarkBackground", "UVBackground", "UVWideBackground", "uBackground", "gBackground" };
			TypeCode[] tcodes = new TypeCode[11] { TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double, TypeCode.Double };
			int[] instances = new int[11] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
			string[] units = new string[11] { "nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm", "CPS/nm" };
			object[] dataobj = new object[11];
			dataobj[0] = (Object)LAMBDA_NM;
			double[] line = new double[FINAL_FLUX_FILTERS.GetLength(0)];
			for (int i = 0; i < FILTER_FILENAMES.Length; i++)
			{
				for (int j = 0; j < line.Length; j++)
					line[j] = FINAL_FLUX_FILTERS[j, i];
				dataobj[i + 1] = (Object)line;

				for (int j = 0; j < line.Length; j++)
					line[j] = FINAL_FLUX_FILTERS_BG[j, i] * (double)(APERTURENPIX);
				dataobj[i + 6] = (Object)line;
			}
			JPFITS.FITSBinTable.WriteExtension(outdir + "filterstable_" + SESSION.ToString() + ".fits", "FilterThrougputs", true, labels, tcodes, instances, units, null, null, null, dataobj);

			System.IO.StreamWriter sw = new System.IO.StreamWriter(outdir + "counts_" + SESSION.ToString() + ".txt");
			if (ISPOINTSOURCE)
				sw.WriteLine("Max Pixel CPS" + "	" + "Total CPS" + "	" + "Max CPS SN Time" + "	" + "Total CPS SN Time");
			else
				sw.WriteLine("Max Pixel CPS" + "	" + "Re CPS" + "	" + "Max CPS SN Time" + "	" + "Re CPS SN Time");
			sw.WriteLine(MaxCountUVDarkLabel.Text + "		" + TotalCountUVDarkLabel.Text + "		" + MSNTimeUVDarkLabel.Text + "		" + TSNTimeUVDarkLabel.Text);
			sw.WriteLine(MaxCountUVLabel.Text + "		" + TotalCountUVLabel.Text + "		" + MSNTimeUVLabel.Text + "		" + TSNTimeUVLabel.Text);
			sw.WriteLine(MaxCountuWideLabel.Text + "		" + TotalCountuWideLabel.Text + "		" + MSNTimeuWideLabel.Text + "		" + TSNTimeuWideLabel.Text);
			sw.WriteLine(MaxCountuLabel.Text + "		" + TotalCountuLabel.Text + "		" + MSNTimeuLabel.Text + "		" + TSNTimeuLabel.Text);
			sw.WriteLine(MaxCountgLabel.Text + "		" + TotalCountgLabel.Text + "		" + MSNTimegLabel.Text + "		" + TSNTimegLabel.Text);
			sw.Close();
		}

		void SaveOutputsBtn_Click(System.Object sender, System.EventArgs e)
		{
			Random r = new Random();
			SESSION = r.Next(100000);
			SaveOutPuts();

			string outdir = CASTOR_DIRECTORY + "Output\\" + SESSION.ToString() + "\\";
			System.Diagnostics.Process.Start("Explorer.exe", outdir);
		}

		void EscBtn_Click(System.Object sender, System.EventArgs e)
		{
			if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo) == DialogResult.No)
				return;

			Application.Exit();
		}

		void ShowLocalFlux_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (!COMPUTATION_EXISTS)
				return;

			if (ShowLocalFlux.Checked)
				Chart_Source.Titles[0].Text = "Local Source Flux (Source * Extinction * Distance)";
			else
				Chart_Source.Titles[0].Text = "Source Flux";

			Chart_Source.Series[0].Points.Clear();
			if (ShowLocalFlux.Checked)
				for (int i = 0; i < LAMBDA_NM.Length; i++)
					Chart_Source.Series[0].Points.AddXY(LAMBDA_NM[i], SOURCE_FLUX_LOCAL[i]);
			else
				for (int i = 0; i < LAMBDA_NM.Length; i++)
					Chart_Source.Series[0].Points.AddXY(LAMBDA_NM[i], SOURCE_FLUX[i]);
			Chart_Source.ChartAreas[0].AxisX.Minimum = PLOT_MIN;
			Chart_Source.ChartAreas[0].AxisX.Maximum = PLOT_MAX;
		}

		void SourceStarRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceStarRadBtn.Checked)
			{
				mvRadBtn.Enabled = true;
				SourceStarDrop.Enabled = true;
				DistanceRadBtn.Enabled = true;
				if (DistanceRadBtn.Checked)
					DistanceTxt.Enabled = true;
				else
					DistanceTxt.Enabled = false;
				mvRadBtn.Enabled = true;
				if (mvRadBtn.Checked)
					mvTxt.Enabled = true;
				else
					mvTxt.Enabled = false;
				SourceBlackbodyTempTxt.Enabled = false;
				RadiusTxt.Enabled = true;
				PowerLawAlphaTxt.Enabled = false;
				PowerLawNormTxt.Enabled = false;
				mIeTxt.Enabled = false;
				RedShiftTxt.Enabled = false;
				ISPOINTSOURCE = true;

				if (FIRSTSLOAD)
					return;

				ThroughPutETC();
			}
			else
			{
				SourceStarDrop.Enabled = false;
				mvRadBtn.Enabled = false;
			}

			SetReg("CETC", "SourceStarRadBtn", SourceStarRadBtn.Checked);
		}

		void SourceStarDrop_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SourceStarDropIndex", SourceStarDrop.SelectedIndex);

			ThroughPutETC();
		}

		void SourceAGNRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceAGNRadBtn.Checked)
			{
				SourceAGNDrop.Enabled = true;
				SourceAGNDrop_SelectedIndexChanged(sender, e);//trigger changes
				SersicReffUpD.Enabled = false;
				SersicnUpD.Enabled = false;
				RedShiftTxt.Enabled = true;
				mIeTxt.Enabled = true;
				mvRadBtn.Enabled = false;
				mvTxt.Enabled = false;
				PowerLawAlphaTxt.Enabled = false;
				PowerLawNormTxt.Enabled = false;
				DistanceRadBtn.Enabled = false;
				DistanceTxt.Enabled = false;
				RadiusTxt.Enabled = false;
				ISPOINTSOURCE = true;

				if (FIRSTSLOAD)
					return;
				ThroughPutETC();
			}
			else
			{
				SourceAGNDrop.Enabled = false;
				RedShiftTxt.Enabled = false;
				mIeTxt.Enabled = false;
			}

			SetReg("CETC", "SourceAGNRadBtn", SourceAGNRadBtn.Checked);
		}

		void SourceGalaxyRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceGalaxyRadBtn.Checked)
			{
				label5.Text = "V Mag:";//"Ie:";
				label6.Text = "ReCPS";
				toolTip1.SetToolTip(label6, "Re Counts per Second per Pixel");
				label22.Text = "ReCPS T";
				toolTip1.SetToolTip(label22, "Time to reach SN for Re Counts per Second (ReCPS)");
				SersicReffUpD.Enabled = true;
				SersicnUpD.Enabled = true;
				SourceGalaxyDrop.Enabled = true;
				RedShiftTxt.Enabled = true;
				mIeTxt.Enabled = true;
				mvRadBtn.Enabled = false;
				mvTxt.Enabled = false;
				PowerLawAlphaTxt.Enabled = false;
				PowerLawNormTxt.Enabled = false;
				DistanceRadBtn.Enabled = false;
				DistanceTxt.Enabled = false;
				RadiusTxt.Enabled = false;
				ISPOINTSOURCE = false;

				if (FIRSTSLOAD)
					return;
				ThroughPutETC();
			}
			else
			{
				label6.Text = "TotCPS";
				toolTip1.SetToolTip(label6, "Total Counts per Second");
				label22.Text = "TotCPS T";
				toolTip1.SetToolTip(label22, "Time to reach SN for Total Counts per Second (TotCPS)");
				SourceGalaxyDrop.Enabled = false;
				RedShiftTxt.Enabled = false;
				mIeTxt.Enabled = false;
			}

			SetReg("CETC", "SourceGalaxyRadBtn", SourceGalaxyRadBtn.Checked);
		}

		void SourceGalaxyDrop_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SourceGalaxyDropIndex", SourceGalaxyDrop.SelectedIndex);

			ThroughPutETC();
		}

		void SourceBlackbodyRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceBlackbodyRadBtn.Checked)
			{
				SourceBlackbodyTempTxt.Enabled = true;
				DistanceRadBtn.Enabled = true;
				DistanceRadBtn.Checked = true;
				RadiusTxt.Enabled = true;
				DistanceTxt.Enabled = true;
				mvRadBtn.Enabled = false;
				mvTxt.Enabled = false;
				PowerLawAlphaTxt.Enabled = false;
				PowerLawNormTxt.Enabled = false;
				mIeTxt.Enabled = false;
				RedShiftTxt.Enabled = false;
				ISPOINTSOURCE = true;

				if (FIRSTSLOAD)
					return;

				ThroughPutETC();
			}
			else
				SourceBlackbodyTempTxt.Enabled = false;

			SetReg("CETC", "SourceBlackbodyRadBtn", SourceBlackbodyRadBtn.Checked);
		}

		void DistanceRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (DistanceRadBtn.Checked)
			{
				DistanceTxt.Enabled = true;
				if (FIRSTSLOAD)
					return;
				ThroughPutETC();
			}
			else
				DistanceTxt.Enabled = false;

			SetReg("CETC", "DistanceRadBtn", DistanceRadBtn.Checked);
		}

		void mvRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (mvRadBtn.Checked)
			{
				mvTxt.Enabled = true;
				if (FIRSTSLOAD)
					return;
				ThroughPutETC();
			}
			else
				mvTxt.Enabled = false;

			SetReg("CETC", "mvRadBtn", mvRadBtn.Checked);
		}

		void DistanceTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "DistanceTxt", DistanceTxt.Text);
				ThroughPutETC();
			}
		}

		void RadiusTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "RadiusTxt", RadiusTxt.Text);
				ThroughPutETC();
			}
		}

		void PowerLawAlphaTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "PowerLawAlphaTxt", PowerLawAlphaTxt.Text);
				ThroughPutETC();
			}
		}

		void PowerLawNormTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "PowerLawNormTxt", PowerLawNormTxt.Text);
				ThroughPutETC();
			}
		}

		void ExtinctionAvTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "ExtinctionAvTxt", ExtinctionAvTxt.Text);
				ThroughPutETC();
			}
		}

		void ExtinctionColumnDensityTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "ExtinctionColumnDensityTxt", ExtinctionColumnDensityTxt.Text);
				ThroughPutETC();
			}
		}

		void ExtinctionRvTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "ExtinctionRvTxt", ExtinctionRvTxt.Text);
				ThroughPutETC();
			}
		}

		void mvTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "mvTxt", mvTxt.Text);
				ThroughPutETC();
			}
		}

		void mTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "mTxt", mIeTxt.Text);
				ThroughPutETC();
			}
		}

		void RedShiftTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				SetReg("CETC", "RedShiftTxt", RedShiftTxt.Text);
				ThroughPutETC();
			}
		}

		void SourceBlackbodyTempTxt_KeyDown(System.Object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = true;
				if (SourceBlackbodyTempTxt.Text == "")
					return;
				SetReg("CETC", "SourceBlackbodyTempTxt", SourceBlackbodyTempTxt.Text);
				if (SourceBlackbodyRadBtn.Checked)
					ThroughPutETC();
				else
					SourceBlackbodyRadBtn.Checked = true;
			}
		}

		void SourceBlackbodyTempTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceBlackbodyTempTxt.Text == "")
				return;
			try
			{
				Convert.ToDouble(SourceBlackbodyTempTxt.Text);
				SetReg("CETC", "SourceBlackbodyTempTxt", SourceBlackbodyTempTxt.Text);
			}
			catch
			{
				SourceBlackbodyTempTxt.Text = (string)GetReg("CETC", "SourceBlackbodyTempTxt");
			}
		}

		void SourcePowerLawRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (SourcePowerLawRadBtn.Checked)
			{
				SourcePowerLawDrop.Enabled = true;
				PowerLawAlphaTxt.Enabled = true;
				PowerLawNormTxt.Enabled = true;
				SourceGalaxyDrop.Enabled = false;
				RedShiftTxt.Enabled = false;
				mIeTxt.Enabled = false;
				mvRadBtn.Enabled = false;
				mvTxt.Enabled = false;
				DistanceRadBtn.Enabled = false;
				DistanceTxt.Enabled = false;
				RadiusTxt.Enabled = false;
				ISPOINTSOURCE = true;

				if (FIRSTSLOAD)
					return;
				ThroughPutETC();
			}
			else
			{
				SourcePowerLawDrop.Enabled = false;
				PowerLawAlphaTxt.Enabled = false;
				PowerLawNormTxt.Enabled = false;
			}

			SetReg("CETC", "SourcePowerLawRadBtn", SourcePowerLawRadBtn.Checked);
		}

		void SourcePowerLawDrop_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			if (SourcePowerLawDrop.SelectedIndex == 0)
				PowerLawAlphaLabel.Text = "Alpha nu:";
			else
				PowerLawAlphaLabel.Text = "Alpha lambda:";

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SourcePowerLawDropIndex", SourcePowerLawDrop.SelectedIndex);

			ThroughPutETC();
		}

		void FilterUpDate()
		{
			Chart_Filter.Series[0].Points.Clear();
			Chart_Final.Series.Clear();
			Chart_Final.Series.Add("Source");
			Chart_Final.Series["Source"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			Chart_Final.Series["Source"].BorderWidth = 3;
			Chart_Final.Series["Source"].Color = System.Drawing.Color.Red;
			Chart_Final.Series.Add("Background");
			Chart_Final.Series["Background"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
			Chart_Final.Series["Background"].BorderWidth = 1;
			Chart_Final.Series["Background"].Color = System.Drawing.Color.Black;
			for (int i = 0; i < LAMBDA_NM.Length; i++)
			{
				Chart_Filter.Series[0].Points.AddXY(LAMBDA_NM[i], FILTERS[i, SELECTED_FILTER]);
				Chart_Final.Series["Source"].Points.AddXY(LAMBDA_NM[i], FINAL_FLUX_FILTERS[i, SELECTED_FILTER]);
				Chart_Final.Series["Background"].Points.AddXY(LAMBDA_NM[i], FINAL_FLUX_FILTERS_BG[i, SELECTED_FILTER] * (double)(APERTURENPIX));
			}
		}

		void FilterUVDarkRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			SetReg("CETC", "FilterUVDarkRadBtnChckd", FilterUVDarkRadBtn.Checked);

			if (FilterUVDarkRadBtn.Checked)
			{
				SELECTED_FILTER = 0;
				if (!COMPUTATION_EXISTS)
					return;
				FilterUpDate();
			}
		}

		void FilterUVRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			SetReg("CETC", "FilterUVRadBtnChckd", FilterUVRadBtn.Checked);

			if (FilterUVRadBtn.Checked)
			{
				SELECTED_FILTER = 1;
				if (!COMPUTATION_EXISTS)
					return;
				FilterUpDate();
			}
		}

		void FilteruWideRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			SetReg("CETC", "FilterUWideRadBtnChckd", FilteruWideRadBtn.Checked);

			if (FilteruWideRadBtn.Checked)
			{
				SELECTED_FILTER = 2;
				if (!COMPUTATION_EXISTS)
					return;
				FilterUpDate();
			}
		}

		void FilteruRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			SetReg("CETC", "FilteruRadBtnChckd", FilteruRadBtn.Checked);

			if (FilteruRadBtn.Checked)
			{
				SELECTED_FILTER = 3;
				if (!COMPUTATION_EXISTS)
					return;
				FilterUpDate();
			}
		}

		void FiltergRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			SetReg("CETC", "FiltergRadBtnChckd", FiltergRadBtn.Checked);

			if (FiltergRadBtn.Checked)
			{
				SELECTED_FILTER = 4;
				if (!COMPUTATION_EXISTS)
					return;
				FilterUpDate();
			}
		}

		void CheckTextToNum(TextBox textbox)
		{
			if (textbox.Text == "." || textbox.Text == "")
				return;

			try
			{
				double var = Convert.ToDouble(textbox.Text);
				if (var < 0)
					textbox.Text = "0";
			}
			catch
			{
				textbox.Text = "0";
			}
		}

		void PowerLawAlphaTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "PowerLawAlphaTxt", PowerLawAlphaTxt.Text);
		}

		void PowerLawNormTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "PowerLawNormTxt", PowerLawNormTxt.Text);
		}

		void mTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "mTxt", mIeTxt.Text);
		}

		void RedShiftTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "RedShiftTxt", RedShiftTxt.Text);
		}

		void DistanceTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "DistanceTxt", DistanceTxt.Text);
		}

		void mvTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "mvTxt", mvTxt.Text);
		}

		void RadiusTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "RadiusTxt", RadiusTxt.Text);
		}

		void ExtinctionRvTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "ExtinctionRvTxt", ExtinctionRvTxt.Text);
		}

		void ExtinctionAvTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "ExtinctionAvTxt", ExtinctionAvTxt.Text);
		}

		void ExtinctionColumnDensityTxt_TextChanged(System.Object sender, System.EventArgs e)
		{
			CheckTextToNum((TextBox)sender);
			SetReg("CETC", "ExtinctionColumnDensityTxt", ExtinctionColumnDensityTxt.Text);
		}

		void ExtinctionAvRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (ExtinctionAvRadBtn.Checked)
			{
				ExtinctionAvTxt.Enabled = true;
				ExtinctionColumnDensityTxt.Enabled = false;
			}
			else
				ExtinctionAvTxt.Enabled = false;

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "ExtinctionAvRadBtn", ExtinctionAvRadBtn.Checked);

			ThroughPutETC();
		}

		void ExtinctionColumnDensityRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (ExtinctionColumnDensityRadBtn.Checked)
			{
				ExtinctionColumnDensityTxt.Enabled = true;
				ExtinctionAvTxt.Enabled = false;
			}
			else
				ExtinctionColumnDensityTxt.Enabled = false;

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "ExtinctionColumnDensityRadBtn", ExtinctionColumnDensityRadBtn.Checked);

			ThroughPutETC();
		}

		void ExtinctionDistanceRadBtn_CheckedChanged(System.Object sender, System.EventArgs e)
		{
			if (ExtinctionDistanceRadBtn.Checked)
			{
				ExtinctionAvTxt.Enabled = false;
				ExtinctionColumnDensityTxt.Enabled = false;
			}

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "ExtinctionDistanceRadBtn", ExtinctionDistanceRadBtn.Checked);

			ThroughPutETC();
		}

		void SourceGalaxyDrop_DropDown(System.Object sender, System.EventArgs e)
		{
			SourceGalaxyDrop.Width = 310;
		}

		void SourceGalaxyDrop_DropDownClosed(System.Object sender, System.EventArgs e)
		{
			SourceGalaxyDrop.Width = 88;
		}

		void SourceAGNDrop_DropDown(System.Object sender, System.EventArgs e)
		{
			SourceAGNDrop.Width = 200;
		}

		void SourceAGNDrop_DropDownClosed(System.Object sender, System.EventArgs e)
		{
			SourceAGNDrop.Width = 88;
		}

		void ExtinctionHelpBtn_Click(System.Object sender, System.EventArgs e)
		{
			string helpfile = CASTOR_DIRECTORY + "Help\\HelpExtinction.txt";
			System.IO.StreamReader sr = new System.IO.StreamReader(helpfile);
			string help = sr.ReadToEnd();
			sr.Close();
			MessageBox.Show(help, "Extinction Help...");
		}

		void SpectralTypeHelpBtn_Click(System.Object sender, System.EventArgs e)
		{
			string helpfile = CASTOR_DIRECTORY + "Help\\HelpSpectralType.txt";
			System.IO.StreamReader sr = new System.IO.StreamReader(helpfile);
			string help = sr.ReadToEnd();
			sr.Close();
			MessageBox.Show(help, "Spectral Type Help...");
		}

		void AGNTypeHelpBtn_Click(System.Object sender, System.EventArgs e)
		{
			string helpfile = CASTOR_DIRECTORY + "Help\\HelpAGN.txt";
			System.IO.StreamReader sr = new System.IO.StreamReader(helpfile);
			string help = sr.ReadToEnd();
			sr.Close();
			MessageBox.Show(help, "AGN Help...");
		}

		void GalaxyTypeHelpBtn_Click(System.Object sender, System.EventArgs e)
		{
			string helpfile = CASTOR_DIRECTORY + "Help\\HelpGalaxy.txt";
			System.IO.StreamReader sr = new System.IO.StreamReader(helpfile);
			string help = sr.ReadToEnd();
			sr.Close();
			MessageBox.Show(help, "Galaxy Help...");
		}

		void SNHelpBtn_Click(System.Object sender, System.EventArgs e)
		{
			string helpfile = CASTOR_DIRECTORY + "Help\\HelpSN.txt";
			System.IO.StreamReader sr = new System.IO.StreamReader(helpfile);
			string help = sr.ReadToEnd();
			sr.Close();
			MessageBox.Show(help, "SN Help...");
		}

		void SNTargetUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SNTargetUpD", SNTargetUpD.Value.ToString());
			ThroughPutETC();
		}

		void SersicReffUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SersicReffUpD", SersicReffUpD.Value.ToString());
			ThroughPutETC();
		}

		void SersicnUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SersicnUpD", SersicnUpD.Value.ToString());
			ThroughPutETC();
		}

		void MirrorAreaUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "MirrorArea", MirrorAreaUpD.Value.ToString());
			ThroughPutETC();
		}

		void PlateScaleUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			double platescale = (double)PlateScaleUpD.Value;//arcsec/pixel
			double psfFWHM = (double)FWHMUpD.Value;//arcsec
			FWHMpixelsTxt.Text = "(" + (Math.Round(psfFWHM / platescale, 2)).ToString() + "px)";
			int apertureHWpix = (int)Math.Round(5 * psfFWHM / 2.355 / platescale);//pixels
			APERTURENPIX = 0;
			if (ISPOINTSOURCE)
			{
				for (int i = -apertureHWpix; i <= apertureHWpix; i++)
					for (int j = -apertureHWpix; j <= apertureHWpix; j++)
						if (i * i + j * j <= apertureHWpix * apertureHWpix)
							APERTURENPIX++;
			}
			else
				APERTURENPIX = 1;

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "PlateScaleUpD", PlateScaleUpD.Value.ToString());
			ThroughPutETC();
		}

		void FWHMUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			double platescale = (double)PlateScaleUpD.Value;//arcsec/pixel
			double psfFWHM = (double)FWHMUpD.Value;//arcsec
			FWHMpixelsTxt.Text = "(" + (Math.Round(psfFWHM / platescale, 2)).ToString() + "px)";

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "FWHMUpD", FWHMUpD.Value.ToString());
			ThroughPutETC();
		}

		void ReadNoiseUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "ReadNoiseUpD", ReadNoiseUpD.Value.ToString());
			ThroughPutETC();
		}

		void DarkRateUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "DarkRateUpD", DarkRateUpD.Value.ToString());
			ThroughPutETC();
		}

		void SourceSigmaPixUpD_ValueChanged(System.Object sender, System.EventArgs e)
		{
			/*double platescale = (double)PlateScaleUpD.Value;//arcsec/pixel
			double psfFWHM = (double)FWHMUpD.Value;//arcsec
			double aperturerad = (double)SourceApertureRadiusUpD.Value;
			ApertureRadiusPixelsTxt.Text = " = " + (int(aperturerad / platescale)).ToString() + " pixel radius";
			double apertureHWarcsec = (double)SourceApertureRadiusUpD.Value;//arcsec
			int apertureHWpix = (int)Math.Round(5 * psfFWHM / 2.355 / platescale);//pixels                           //double psfSigmaPix = psfFWHM / 2.355 / platescale;//pixels int(apertureHWarcsec / platescale);//pix
			APERTURENPIX = 0;
			for (int i = -apertureHWpix; i <= apertureHWpix; i++)
				for (int j = -apertureHWpix; j <= apertureHWpix; j++)
					if (i*i + j * j <= apertureHWpix * apertureHWpix)
						APERTURENPIX++;
			ApertureRadiusPixelsTxt.Text += " (" + APERTURENPIX + " total pixels)";

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SourceApertureRadiusUpD", SourceApertureRadiusUpD.Value.ToString());
			ThroughPutETC();*/
		}

		double a_lambda(double lambda)//lambda in nm
		{
			double WaveNum = 1.0 / (lambda / 1000); //Wave Number in units of inverse micrometers
			double y, Fa;

			if (WaveNum < 3.3)
			{
				y = WaveNum - 1.82;
				return (1 + 0.17699 * y - 0.50447 * y * y - 0.02427 * y * y * y + 0.72085 * Math.Pow(y, 4) + 0.01979 * Math.Pow(y, 5) - 0.77530 * Math.Pow(y, 6) + 0.32999 * Math.Pow(y, 7));
			}
			if (WaveNum >= 3.3 && WaveNum < 5.9)
			{
				Fa = 0.0;
			}
			else
			{
				y = WaveNum - 5.9;
				Fa = -0.04473 * y * y - 0.009779 * y * y * y;
			}
			y = WaveNum - 4.67;
			return (1.752 - 0.316 * WaveNum - 0.104 / (y * y + 0.341) + Fa);
		}

		double b_lambda(double lambda)//lambda in nm
		{
			double WaveNum = 1.0 / (lambda / 1000); //Wave Number in units of inverse micrometers
			double y, Fb;

			if (WaveNum <= 3.3)
			{
				y = WaveNum - 1.82;
				return (1.41338 * y + 2.28305 * y * y + 1.07233 * y * y * y - 5.38434 * Math.Pow(y, 4) - 0.62251 * Math.Pow(y, 5) + 5.30260 * Math.Pow(y, 6) - 2.09002 * Math.Pow(y, 7));
			}
			if (WaveNum > 3.3 && WaveNum < 5.9)
			{
				Fb = 0.0;
			}
			else
			{
				y = WaveNum - 5.9;
				Fb = 0.2130 * y * y + 0.1207 * y * y * y;
			}
			y = WaveNum - 4.62;
			return (-3.090 + 1.825 * WaveNum + 1.206 / (y * y + 0.263) + Fb);
		}

		void SourceBlackBody()
		{
			double distance = Convert.ToDouble(DistanceTxt.Text);
			double radius = Convert.ToDouble(RadiusTxt.Text);
			double solidangle = Math.PI * (radius * SR2KPC) * (radius * SR2KPC) / (distance * distance);
			double BB_temp = Convert.ToDouble(SourceBlackbodyTempTxt.Text);
			double lambda;

			for (int i = 0; i < NELEMENTS; i++)
			{
				lambda = LAMBDA_NM[i] * 1e-9; //lambda in meters 
				SOURCE_FLUX[i] = 2 * h * c * c * Math.Pow(lambda, -5) * Math.Pow(Math.Exp((h * c) / (lambda * BB_temp * K)) - 1, -1);
				SOURCE_FLUX_LOCAL[i] = SOURCE_FLUX[i] * solidangle;
			}
		}

		void SourcePowerLaw()
		{
			double alpha = Convert.ToDouble(PowerLawAlphaTxt.Text);
			double norm = Convert.ToDouble(PowerLawNormTxt.Text);
			double lambda;

			if (SourcePowerLawDrop.SelectedIndex == 0)//f(nu)
			{
				for (int i = 0; i < NELEMENTS; i++)
				{
					lambda = LAMBDA_NM[i] * 1e-9; //lambda in meters
					SOURCE_FLUX[i] = c * norm * 1e-10 * (4.13322e-20 / (lambda * lambda)) * Math.Pow(lambda / 1.2398e-9, alpha);
					SOURCE_FLUX_LOCAL[i] = SOURCE_FLUX[i];
				}
			}

			if (SourcePowerLawDrop.SelectedIndex == 1)//f(lambda)
			{
				for (int i = 0; i < NELEMENTS; i++)
				{
					lambda = LAMBDA_NM[i] * 1e-9; //lambda in meters 
					SOURCE_FLUX[i] = norm * 1e-13 * 1e7 * Math.Pow(lambda / 300e-9, -alpha);
					SOURCE_FLUX_LOCAL[i] = SOURCE_FLUX[i];
				}
			}
		}

		void SourceSpectralType()
		{
			string spectype = (string)SourceStarDrop.SelectedItem;
			double[] kurucz_lambda = new double[0];
			double[] kurucz_source = new double[0];
			string Kuruczfile;

			if (spectype == "O5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_45000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g50");
				RadiusTxt.Text = "7.4";
				FV_SURFACE = 7.2463e8;
			}

			if (spectype == "O6V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_40000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "7.4";
				FV_SURFACE = 6.3521E8;
			}

			if (spectype == "O8V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_35000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "7.4";
				FV_SURFACE = 5.3639E8;
			}

			if (spectype == "B0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_30000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "6.64";
				FV_SURFACE = 4.0320E8;
			}

			if (spectype == "B3V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_19000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "3.25";
				FV_SURFACE = 1.7375E8;
			}

			if (spectype == "B5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_15000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "2.74";
				FV_SURFACE = 1.1849E8;
			}

			if (spectype == "B8V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_12000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "2.23";
				FV_SURFACE = 8.0542E7;
			}

			if (spectype == "A0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_9500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "1.87";
				FV_SURFACE = 4.9985E7;
			}

			if (spectype == "A5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_8250.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "1.69";
				FV_SURFACE = 3.3059E7;
			}

			if (spectype == "F0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_7250.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "1.51";
				FV_SURFACE = 1.9702E7;
			}

			if (spectype == "F5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_6500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "1.32";
				FV_SURFACE = 1.2481E7;
			}

			if (spectype == "G0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_6000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "1.08";
				FV_SURFACE = 8.7620E6;
			}

			if (spectype == "G5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_5750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "0.96";
				FV_SURFACE = 7.1766E6;
			}

			if (spectype == "K0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_5250.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "0.81";
				FV_SURFACE = 4.4845E6;
			}

			if (spectype == "K5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_4250.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "0.68";
				FV_SURFACE = 1.1103E6;
			}

			if (spectype == "M0V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "0.61";
				FV_SURFACE = 4.3687E5;
			}

			if (spectype == "M2V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "0.55";
				FV_SURFACE = 2.3601E5;
			}

			if (spectype == "M5V")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g50");
				RadiusTxt.Text = "0.49";
				FV_SURFACE = 2.4302E5;
			}

			if (spectype == "B0III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_29000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g35");
				RadiusTxt.Text = "3.0";
				FV_SURFACE = 3.9357E8;
			}

			if (spectype == "B5III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_15000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g35");
				RadiusTxt.Text = "3.0";
				FV_SURFACE = 1.1880E8;
			}

			if (spectype == "G0III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_5750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g30");
				RadiusTxt.Text = "5.0";
				FV_SURFACE = 7.2888E6;
			}

			if (spectype == "G5III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_5250.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g25");
				RadiusTxt.Text = "8.0";
				FV_SURFACE = 4.6069E6;
			}

			if (spectype == "K0III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_4750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g20");
				RadiusTxt.Text = "11.0";
				FV_SURFACE = 2.5875E6;
			}

			if (spectype == "K5III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_4000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g15");
				RadiusTxt.Text = "28.0";
				FV_SURFACE = 7.3310E5;
			}

			if (spectype == "M0III")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g15");
				RadiusTxt.Text = "28.0";
				FV_SURFACE = 4.0315E5;
			}

			if (spectype == "O5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_40000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 6.3521E8;
			}

			if (spectype == "O6I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_40000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g45");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 6.3521E8;
			}

			if (spectype == "O8I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_34000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g40");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 5.1313E8;
			}

			if (spectype == "B0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_26000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g30");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 3.2939E8;
			}

			if (spectype == "B5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_14000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g25");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 1.0584E8;
			}

			if (spectype == "A0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_9750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g20");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 5.2038E7;
			}

			if (spectype == "A5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_8500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g20");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 3.7259E7;
			}

			if (spectype == "F0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_7750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g20");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 2.7696E7;
			}

			if (spectype == "F5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_7000.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g15");
				RadiusTxt.Text = "60.0";
				FV_SURFACE = 1.8639E7;
			}

			if (spectype == "G0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_5500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g15");
				RadiusTxt.Text = "75.0";
				FV_SURFACE = 5.9486E6;
			}

			if (spectype == "G5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_4750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g10");
				RadiusTxt.Text = "95.0";
				FV_SURFACE = 2.6013E6;
			}

			if (spectype == "K0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_4500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g10");
				RadiusTxt.Text = "110.0";
				FV_SURFACE = 1.8243E6;
			}

			if (spectype == "K5I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g05");
				RadiusTxt.Text = "150.0";
				FV_SURFACE = 4.0596E5;
			}

			if (spectype == "M0I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3750.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g00");
				RadiusTxt.Text = "150.0";
				FV_SURFACE = 4.0151E5;
			}

			if (spectype == "M2I")
			{
				Kuruczfile = CASTOR_DIRECTORY + "SpectralType\\kp00_3500.fits";
				kurucz_lambda = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "WAVELENGTH");
				kurucz_source = JPFITS.FITSBinTable.GetExtensionEntry(Kuruczfile, "", "g00");
				RadiusTxt.Text = "150.0";
				FV_SURFACE = 1.8380E5;
			}

			//Kurucz profiles are tabulated in Angstroms - source vs. lambda in Angstroms
			//therefore need to resample at nm increments from 120 to 1100
			int i_LAMBDA_NM = 0;
			for (int i = 0; i < kurucz_lambda.Length; i++)
				if (kurucz_lambda[i] / 10 > LAMBDA_NM[i_LAMBDA_NM])
				{
					SOURCE_FLUX[i_LAMBDA_NM] = kurucz_source[i] * 1e7;  //convert from erg/s/cm3 . J/s/m3;
					i_LAMBDA_NM++;

					if (i_LAMBDA_NM == NELEMENTS)
						break;
				}

			double distance = Convert.ToDouble(DistanceTxt.Text);
			double radius = Convert.ToDouble(RadiusTxt.Text);

			if (mvRadBtn.Checked)
			{
				double magnitude = Convert.ToDouble(mvTxt.Text);
				radius = Convert.ToDouble(RadiusTxt.Text);
				distance = Math.Sqrt(Math.Pow(10, 0.4 * magnitude) * Math.Pow(3.085680E17 / 1.87, 2) / 4.9885E7 * Math.Pow(radius, 2) * FV_SURFACE) / KPC2M;
				DistanceTxt.Text = Math.Round(distance, 3).ToString();
				if (distance < 0.001)
					DistanceTxt.Text = "0.001";
			}

			double solidangle = Math.PI * (radius * SR2KPC) * (radius * SR2KPC) / (distance * distance);

			for (int i = 0; i < NELEMENTS; i++)
				SOURCE_FLUX_LOCAL[i] = solidangle * SOURCE_FLUX[i] / Math.PI;// divide by PI because spectrum input is surface flux not intensity: surface flux = PI*intensity
		}

		void SourceGalaxy()
		{
			string galtype = (string)SourceGalaxyDrop.SelectedItem;
			double[] gal_lambda;
			double[] gal_source;
			string galfile = "";

			if (galtype == "Bulge (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\bulge_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Elliptical (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\elliptical_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "S0 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\s0_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Sa (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\sa_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Sb (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\sb_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Sc (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\sc_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst1: e(b-v)<0.1 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb1_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst2: 0.11< e(b-v)<0.21 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb2_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst3: 0.25< e(b-v)<0.35 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb3_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst4: 0.39< e(b-v)<0.50 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb4_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst5: 0.51< e(b-v)<0.60 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb5_template.fits";
				NORM_MAG = 12.5;
			}

			if (galtype == "Starburst6: 0.61< e(b-v)<0.70 (norm: v=12.5)")
			{
				galfile = CASTOR_DIRECTORY + "Galaxies\\starb6_template.fits";
				NORM_MAG = 12.5;
			}

			gal_lambda = JPFITS.FITSBinTable.GetExtensionEntry(galfile, "", "WAVELENGTH");
			gal_source = JPFITS.FITSBinTable.GetExtensionEntry(galfile, "", "FLUX");
			double redshift = Convert.ToDouble(RedShiftTxt.Text);
			double m = Convert.ToDouble(mIeTxt.Text);

			//Galaxy profiles are tabulated in Angstroms - source vs. lambda in Angstroms
			//therefore need to resample at nm increments from 120 to 1100
			int i_LAMBDA_NM = 0;
			for (int i = 0; i < gal_lambda.Length; i++)
				if (gal_lambda[i] / 10 > LAMBDA_NM[i_LAMBDA_NM])
				{
					SOURCE_FLUX[i_LAMBDA_NM] = gal_source[i] * 1e7;//convert from erg/s/cm3 . J/s/M3;
					i_LAMBDA_NM++;

					if (i_LAMBDA_NM == NELEMENTS)
						break;
				}

			//Now shift the spectrum for the given red shift to get local source flux
			for (int i = 0; i < gal_lambda.Length; i++)
				gal_lambda[i] = gal_lambda[i] * (1 + redshift);

			//Galaxy profiles are tabulated in Angstroms - source vs. lambda in Angstroms
			//therefore need to resample at nm increments from 120 to 1100
			i_LAMBDA_NM = 0;
			for (int i = 0; i < gal_lambda.Length; i++)
				if (gal_lambda[i] / 10 > LAMBDA_NM[i_LAMBDA_NM])
				{
					SOURCE_FLUX_LOCAL[i_LAMBDA_NM] = gal_source[i] * 1e7;//convert from erg/s/cm3 . J/s/M3;
					i_LAMBDA_NM++;

					if (i_LAMBDA_NM == NELEMENTS)
						break;
				}

			//now redshift the local source flux and scale the magnitude
			for (int i = 0; i < NELEMENTS; i++)
				SOURCE_FLUX_LOCAL[i] = (SOURCE_FLUX_LOCAL[i] / (1 + redshift)) * (Math.Pow(10, -0.4 * (m - NORM_MAG)) / (1 + redshift));
		}

		void SourceAGN()
		{
			string AGNtype = (string)SourceAGNDrop.SelectedItem;
			double[] AGN_lambda;
			double[] AGN_source;
			string AGNfile = "";

			if (AGNtype == "Liner (norm: v=12.5)")
			{
				AGNfile = CASTOR_DIRECTORY + "AGN\\liner_template.fits";
				NORM_MAG = 12.5;
			}

			if (AGNtype == "NGC-1068 (norm: v=10.3)")
			{
				AGNfile = CASTOR_DIRECTORY + "AGN\\ngc1068_template.fits";
				NORM_MAG = 10.3;
			}

			if (AGNtype == "QSO (norm: b=12.5)")
			{
				AGNfile = CASTOR_DIRECTORY + "AGN\\qso_template.fits";
				NORM_MAG = 12.5;
			}

			if (AGNtype == "Seyfert1 (norm: b=12.5)")
			{
				AGNfile = CASTOR_DIRECTORY + "AGN\\seyfert1_template.fits";
				NORM_MAG = 12.5;
			}

			if (AGNtype == "Seyfert2 (norm: v=12.5)")
			{
				AGNfile = CASTOR_DIRECTORY + "AGN\\seyfert2_template.fits";
				NORM_MAG = 12.5;
			}

			AGN_lambda = JPFITS.FITSBinTable.GetExtensionEntry(AGNfile, "", "WAVELENGTH");
			AGN_source = JPFITS.FITSBinTable.GetExtensionEntry(AGNfile, "", "FLUX");
			double redshift = Convert.ToDouble(RedShiftTxt.Text);
			double m = Convert.ToDouble(mIeTxt.Text);

			//Galaxy profiles are tabulated in Angstroms - source vs. lambda in Angstroms
			//therefore need to resample at nm increments from 120 to 1100
			int i_LAMBDA_NM = 0;
			for (int i = 0; i < AGN_lambda.Length; i++)
				if (AGN_lambda[i] / 10 > LAMBDA_NM[i_LAMBDA_NM])
				{
					SOURCE_FLUX[i_LAMBDA_NM] = AGN_source[i] * 1e7;  //convert from erg/s/cm3 . J/s/M3;
					i_LAMBDA_NM++;

					if (i_LAMBDA_NM == NELEMENTS)
						break;
				}

			//Now shift the spectrum for the given red shift to get local source flux
			for (int i = 0; i < AGN_lambda.Length; i++)
				AGN_lambda[i] = AGN_lambda[i] * (1 + redshift);

			//Galaxy profiles are tabulated in Angstroms - source vs. lambda in Angstroms
			//therefore need to resample at nm increments from 120 to 1100
			i_LAMBDA_NM = 0;
			for (int i = 0; i < AGN_lambda.Length; i++)
				if (AGN_lambda[i] / 10 > LAMBDA_NM[i_LAMBDA_NM])
				{
					SOURCE_FLUX_LOCAL[i_LAMBDA_NM] = AGN_source[i] * 1e7;  //convert from erg/s/cm3 . J/s/M3;
					i_LAMBDA_NM++;

					if (i_LAMBDA_NM == NELEMENTS)
						break;
				}

			//now redshift the local source flux and scale the magnitude
			for (int i = 0; i < NELEMENTS; i++)
				SOURCE_FLUX_LOCAL[i] = (SOURCE_FLUX_LOCAL[i] / (1 + redshift)) * (Math.Pow(10, -0.4 * (m - NORM_MAG)) / (1 + redshift));
		}

		void SourceAGNDrop_SelectedIndexChanged(System.Object sender, System.EventArgs e)
		{
			if (SourceAGNDrop.SelectedIndex == 0 || SourceAGNDrop.SelectedIndex == 1 || SourceAGNDrop.SelectedIndex == 4)
				label5.Text = "V Mag:";
			else
				label5.Text = "B Mag:";

			if (FIRSTSLOAD)
				return;

			SetReg("CETC", "SourceAGNDropIndex", SourceAGNDrop.SelectedIndex);

			ThroughPutETC();
		}

		void FormatLabels()
		{
			//uvdark, uv, uwide, u, g
			TotalCountUVDarkLabel.Text = FINAL_COUNTS[0].ToString("e1");
			TotalCountUVLabel.Text = FINAL_COUNTS[1].ToString("e1");
			TotalCountuWideLabel.Text = FINAL_COUNTS[2].ToString("e1");
			TotalCountuLabel.Text = FINAL_COUNTS[3].ToString("e1");
			TotalCountgLabel.Text = FINAL_COUNTS[4].ToString("e1");

			//uvdark, uv, uwide, u, g
			MaxCountUVDarkLabel.Text = FINAL_COUNTS_MAXPIX[0].ToString("e1");
			MaxCountUVLabel.Text = FINAL_COUNTS_MAXPIX[1].ToString("e1");
			MaxCountuWideLabel.Text = FINAL_COUNTS_MAXPIX[2].ToString("e1");
			MaxCountuLabel.Text = FINAL_COUNTS_MAXPIX[3].ToString("e1");
			MaxCountgLabel.Text = FINAL_COUNTS_MAXPIX[4].ToString("e1");

			//uvdark, uv, uwide, u, g
			BGCountUVDarkLabel.Text = (/*(double)(APERTURENPIX) * */FINAL_COUNTS_BG[0]).ToString("e1");
			BGCountUVLabel.Text = (/*(double)(APERTURENPIX) * */FINAL_COUNTS_BG[1]).ToString("e1");
			BGCountuWideLabel.Text = (/*(double)(APERTURENPIX) * */FINAL_COUNTS_BG[2]).ToString("e1");
			BGCountuLabel.Text = (/*(double)(APERTURENPIX) * */FINAL_COUNTS_BG[3]).ToString("e1");
			BGCountgLabel.Text = (/*(double)(APERTURENPIX) * */FINAL_COUNTS_BG[4]).ToString("e1");

			//uvdark, uv, uwide, u, g
			MSNTimeUVDarkLabel.Text = SN_TIME_MCPS[0].ToString("e1");
			MSNTimeUVLabel.Text = SN_TIME_MCPS[1].ToString("e1");
			MSNTimeuWideLabel.Text = SN_TIME_MCPS[2].ToString("e1");
			MSNTimeuLabel.Text = SN_TIME_MCPS[3].ToString("e1");
			MSNTimegLabel.Text = SN_TIME_MCPS[4].ToString("e1");

			//uvdark, uv, uwide, u, g
			TSNTimeUVDarkLabel.Text = SN_TIME_TCPS[0].ToString("e1");
			TSNTimeUVLabel.Text = SN_TIME_TCPS[1].ToString("e1");
			TSNTimeuWideLabel.Text = SN_TIME_TCPS[2].ToString("e1");
			TSNTimeuLabel.Text = SN_TIME_TCPS[3].ToString("e1");
			TSNTimegLabel.Text = SN_TIME_TCPS[4].ToString("e1");

			for (int i = 0; i < totalcountlabels.Length; i++)
			{
				totalcountlabels[i].Text = totalcountlabels[i].Text.Substring(0, 5) + totalcountlabels[i].Text.Substring(6);
				maxcountlabels[i].Text = maxcountlabels[i].Text.Substring(0, 5) + maxcountlabels[i].Text.Substring(6);
				bgcountlabels[i].Text = bgcountlabels[i].Text.Substring(0, 5) + bgcountlabels[i].Text.Substring(6);
				msntimelabels[i].Text = msntimelabels[i].Text.Substring(0, 5) + msntimelabels[i].Text.Substring(6);
				tsntimelabels[i].Text = tsntimelabels[i].Text.Substring(0, 5) + tsntimelabels[i].Text.Substring(6);
			}
		}




	}
}
