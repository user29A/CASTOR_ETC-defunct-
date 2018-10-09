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


#pragma once

using namespace Microsoft::Win32;

namespace CETC {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			array<String^>^ startargs = gcnew array<String^>(0);
			Form1::InitializeVars(startargs);
		}

		Form1(array<System::String ^>^ startargs)
		{
			InitializeComponent();
			Form1::InitializeVars(startargs);
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::DataVisualization::Charting::Chart^  Chart_Source;
	protected:





	private: System::Windows::Forms::DataVisualization::Charting::Chart^  Chart_Final;

	private: System::Windows::Forms::GroupBox^  SourceTypeGroupBox;

	private: System::Windows::Forms::RadioButton^  SourceStarRadBtn;
	private: System::Windows::Forms::ComboBox^  SourceStarDrop;


	private: System::Windows::Forms::RadioButton^  SourceGalaxyRadBtn;

	private: System::Windows::Forms::GroupBox^  ExtinctionGroupBox;
	private: System::Windows::Forms::ComboBox^  SourceGalaxyDrop;

	private: System::Windows::Forms::RadioButton^  SourceBlackbodyRadBtn;


	private: System::Windows::Forms::MaskedTextBox^  SourceBlackbodyTempTxt;
	private: System::Windows::Forms::ComboBox^  SourceAGNDrop;



	private: System::Windows::Forms::RadioButton^  SourceAGNRadBtn;


	private: System::Windows::Forms::GroupBox^  FilterGroupBox;
	private: System::Windows::Forms::RadioButton^  FiltergRadBtn;


	private: System::Windows::Forms::RadioButton^  FilterUVRadBtn;








	private: System::Windows::Forms::RadioButton^  FilterUVDarkRadBtn;
	private: System::Windows::Forms::RadioButton^  FilteruWideRadBtn;
	private: System::Windows::Forms::RadioButton^  FilteruRadBtn;
	private: System::Windows::Forms::Button^  EscBtn;

	private: System::Windows::Forms::RadioButton^  SourcePowerLawRadBtn;
	private: System::Windows::Forms::ComboBox^  SourcePowerLawDrop;
	private: System::Windows::Forms::GroupBox^  SourcePropertiesGroupBox;
	private: System::Windows::Forms::RadioButton^  DistanceRadBtn;


	private: System::Windows::Forms::RadioButton^  mvRadBtn;
	private: System::Windows::Forms::TextBox^  DistanceTxt;
	private: System::Windows::Forms::TextBox^  mvTxt;
	private: System::Windows::Forms::TextBox^  RadiusTxt;


	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::TextBox^  ExtinctionRvTxt;

	private: System::Windows::Forms::Label^  label4;
	private: System::Windows::Forms::RadioButton^  ExtinctionColumnDensityRadBtn;

	private: System::Windows::Forms::RadioButton^  ExtinctionAvRadBtn;
	private: System::Windows::Forms::RadioButton^  ExtinctionDistanceRadBtn;

	private: System::Windows::Forms::Button^  ExtinctionHelpBtn;
	private: System::Windows::Forms::TextBox^  ExtinctionColumnDensityTxt;

	private: System::Windows::Forms::TextBox^  ExtinctionAvTxt;
	private: System::Windows::Forms::Label^  TotalCountUVDarkLabel;
	private: System::Windows::Forms::Label^  TotalCountUVLabel;
	private: System::Windows::Forms::Label^  TotalCountuWideLabel;
	private: System::Windows::Forms::Label^  TotalCountuLabel;
	private: System::Windows::Forms::Label^  TotalCountgLabel;
	private: System::Windows::Forms::DataVisualization::Charting::Chart^  Chart_Filter;
private: System::Windows::Forms::Label^  label3;
private: System::Windows::Forms::Label^  PowerLawAlphaLabel;

private: System::Windows::Forms::TextBox^  PowerLawAlphaTxt;
private: System::Windows::Forms::TextBox^  PowerLawNormTxt;
private: System::Windows::Forms::TextBox^  RedShiftTxt;

private: System::Windows::Forms::Label^  label1;
private: System::Windows::Forms::Label^  label5;
private: System::Windows::Forms::TextBox^  mTxt;
private: System::Windows::Forms::CheckBox^  ShowLocalFlux;
private: System::Windows::Forms::Button^  GalaxyTypeHelpBtn;

private: System::Windows::Forms::Button^  AGNTypeHelpBtn;

private: System::Windows::Forms::Button^  SpectralTypeHelpBtn;
private: System::Windows::Forms::Label^  label6;
private: System::Windows::Forms::Label^  label7;
private: System::Windows::Forms::NumericUpDown^  SNTargetUpD;

private: System::Windows::Forms::Label^  SNTimegLabel;
private: System::Windows::Forms::Label^  SNTimeuLabel;
private: System::Windows::Forms::Label^  SNTimeuWideLabel;
private: System::Windows::Forms::Label^  SNTimeUVLabel;
private: System::Windows::Forms::Label^  SNTimeUVDarkLabel;
private: System::Windows::Forms::GroupBox^  DetectorGroupBox;

private: System::Windows::Forms::NumericUpDown^  PlateScaleUpD;

private: System::Windows::Forms::Label^  label8;
private: System::Windows::Forms::NumericUpDown^  FWHMUpD;

private: System::Windows::Forms::Label^  label9;
private: System::Windows::Forms::TextBox^  SersicReffTxt;

private: System::Windows::Forms::Label^  label10;
private: System::Windows::Forms::Label^  label11;
private: System::Windows::Forms::TextBox^  SersicRTxt;
private: System::Windows::Forms::TextBox^  SersicnTxt;

private: System::Windows::Forms::Label^  label12;
private: System::Windows::Forms::Button^  SNHelpBtn;
private: System::Windows::Forms::Label^  label13;
private: System::Windows::Forms::Label^  MaxCountgLabel;

private: System::Windows::Forms::Label^  MaxCountuLabel;

private: System::Windows::Forms::Label^  MaxCountuWideLabel;

private: System::Windows::Forms::Label^  MaxCountUVLabel;

private: System::Windows::Forms::Label^  MaxCountUVDarkLabel;



	private: System::ComponentModel::IContainer^  components;





	protected:

	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>


#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			System::Windows::Forms::DataVisualization::Charting::ChartArea^  chartArea1 = (gcnew System::Windows::Forms::DataVisualization::Charting::ChartArea());
			System::Windows::Forms::DataVisualization::Charting::Series^  series1 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Title^  title1 = (gcnew System::Windows::Forms::DataVisualization::Charting::Title());
			System::Windows::Forms::DataVisualization::Charting::ChartArea^  chartArea2 = (gcnew System::Windows::Forms::DataVisualization::Charting::ChartArea());
			System::Windows::Forms::DataVisualization::Charting::Series^  series2 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Title^  title2 = (gcnew System::Windows::Forms::DataVisualization::Charting::Title());
			System::Windows::Forms::DataVisualization::Charting::ChartArea^  chartArea3 = (gcnew System::Windows::Forms::DataVisualization::Charting::ChartArea());
			System::Windows::Forms::DataVisualization::Charting::Series^  series3 = (gcnew System::Windows::Forms::DataVisualization::Charting::Series());
			System::Windows::Forms::DataVisualization::Charting::Title^  title3 = (gcnew System::Windows::Forms::DataVisualization::Charting::Title());
			System::ComponentModel::ComponentResourceManager^  resources = (gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid));
			this->Chart_Source = (gcnew System::Windows::Forms::DataVisualization::Charting::Chart());
			this->Chart_Final = (gcnew System::Windows::Forms::DataVisualization::Charting::Chart());
			this->SourceTypeGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->GalaxyTypeHelpBtn = (gcnew System::Windows::Forms::Button());
			this->AGNTypeHelpBtn = (gcnew System::Windows::Forms::Button());
			this->SpectralTypeHelpBtn = (gcnew System::Windows::Forms::Button());
			this->SourcePowerLawDrop = (gcnew System::Windows::Forms::ComboBox());
			this->SourceAGNDrop = (gcnew System::Windows::Forms::ComboBox());
			this->SourceBlackbodyTempTxt = (gcnew System::Windows::Forms::MaskedTextBox());
			this->SourcePowerLawRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->SourceBlackbodyRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->SourceAGNRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->SourceGalaxyDrop = (gcnew System::Windows::Forms::ComboBox());
			this->SourceGalaxyRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->SourceStarDrop = (gcnew System::Windows::Forms::ComboBox());
			this->SourceStarRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->ExtinctionGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->ExtinctionColumnDensityTxt = (gcnew System::Windows::Forms::TextBox());
			this->ExtinctionAvTxt = (gcnew System::Windows::Forms::TextBox());
			this->ExtinctionHelpBtn = (gcnew System::Windows::Forms::Button());
			this->ExtinctionDistanceRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->ExtinctionColumnDensityRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->ExtinctionAvRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->ExtinctionRvTxt = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->FilterGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->label13 = (gcnew System::Windows::Forms::Label());
			this->MaxCountgLabel = (gcnew System::Windows::Forms::Label());
			this->MaxCountuLabel = (gcnew System::Windows::Forms::Label());
			this->MaxCountuWideLabel = (gcnew System::Windows::Forms::Label());
			this->MaxCountUVLabel = (gcnew System::Windows::Forms::Label());
			this->MaxCountUVDarkLabel = (gcnew System::Windows::Forms::Label());
			this->SNHelpBtn = (gcnew System::Windows::Forms::Button());
			this->SNTimegLabel = (gcnew System::Windows::Forms::Label());
			this->SNTimeuLabel = (gcnew System::Windows::Forms::Label());
			this->SNTimeuWideLabel = (gcnew System::Windows::Forms::Label());
			this->SNTimeUVLabel = (gcnew System::Windows::Forms::Label());
			this->SNTimeUVDarkLabel = (gcnew System::Windows::Forms::Label());
			this->SNTargetUpD = (gcnew System::Windows::Forms::NumericUpDown());
			this->label7 = (gcnew System::Windows::Forms::Label());
			this->label6 = (gcnew System::Windows::Forms::Label());
			this->TotalCountgLabel = (gcnew System::Windows::Forms::Label());
			this->FilteruRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->TotalCountuLabel = (gcnew System::Windows::Forms::Label());
			this->FilteruWideRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->TotalCountuWideLabel = (gcnew System::Windows::Forms::Label());
			this->FilterUVDarkRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->TotalCountUVLabel = (gcnew System::Windows::Forms::Label());
			this->FiltergRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->TotalCountUVDarkLabel = (gcnew System::Windows::Forms::Label());
			this->FilterUVRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->EscBtn = (gcnew System::Windows::Forms::Button());
			this->SourcePropertiesGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->SersicRTxt = (gcnew System::Windows::Forms::TextBox());
			this->SersicnTxt = (gcnew System::Windows::Forms::TextBox());
			this->label12 = (gcnew System::Windows::Forms::Label());
			this->SersicReffTxt = (gcnew System::Windows::Forms::TextBox());
			this->DistanceTxt = (gcnew System::Windows::Forms::TextBox());
			this->RedShiftTxt = (gcnew System::Windows::Forms::TextBox());
			this->label11 = (gcnew System::Windows::Forms::Label());
			this->label10 = (gcnew System::Windows::Forms::Label());
			this->PowerLawNormTxt = (gcnew System::Windows::Forms::TextBox());
			this->RadiusTxt = (gcnew System::Windows::Forms::TextBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->mTxt = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->PowerLawAlphaLabel = (gcnew System::Windows::Forms::Label());
			this->PowerLawAlphaTxt = (gcnew System::Windows::Forms::TextBox());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->mvTxt = (gcnew System::Windows::Forms::TextBox());
			this->mvRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->DistanceRadBtn = (gcnew System::Windows::Forms::RadioButton());
			this->Chart_Filter = (gcnew System::Windows::Forms::DataVisualization::Charting::Chart());
			this->ShowLocalFlux = (gcnew System::Windows::Forms::CheckBox());
			this->DetectorGroupBox = (gcnew System::Windows::Forms::GroupBox());
			this->FWHMUpD = (gcnew System::Windows::Forms::NumericUpDown());
			this->label9 = (gcnew System::Windows::Forms::Label());
			this->PlateScaleUpD = (gcnew System::Windows::Forms::NumericUpDown());
			this->label8 = (gcnew System::Windows::Forms::Label());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Source))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Final))->BeginInit();
			this->SourceTypeGroupBox->SuspendLayout();
			this->ExtinctionGroupBox->SuspendLayout();
			this->FilterGroupBox->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SNTargetUpD))->BeginInit();
			this->SourcePropertiesGroupBox->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Filter))->BeginInit();
			this->DetectorGroupBox->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->FWHMUpD))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->PlateScaleUpD))->BeginInit();
			this->SuspendLayout();
			// 
			// Chart_Source
			// 
			this->Chart_Source->BackColor = System::Drawing::Color::Transparent;
			chartArea1->AxisX->Title = L"Wavelength";
			chartArea1->AxisY->LabelStyle->Format = L"e1";
			chartArea1->BackColor = System::Drawing::Color::DimGray;
			chartArea1->BackGradientStyle = System::Windows::Forms::DataVisualization::Charting::GradientStyle::TopBottom;
			chartArea1->BackSecondaryColor = System::Drawing::Color::Silver;
			chartArea1->Name = L"ChartArea1";
			this->Chart_Source->ChartAreas->Add(chartArea1);
			this->Chart_Source->Location = System::Drawing::Point(12, 36);
			this->Chart_Source->Name = L"Chart_Source";
			series1->BorderColor = System::Drawing::Color::White;
			series1->ChartArea = L"ChartArea1";
			series1->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Line;
			series1->Color = System::Drawing::Color::Red;
			series1->Name = L"Series1";
			this->Chart_Source->Series->Add(series1);
			this->Chart_Source->Size = System::Drawing::Size(827, 258);
			this->Chart_Source->TabIndex = 0;
			this->Chart_Source->Text = L"chart1";
			title1->Font = (gcnew System::Drawing::Font(L"Garamond", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			title1->Name = L"Title1";
			title1->Text = L"Local Source Flux (Source * Extinction * Distance)";
			title1->TextOrientation = System::Windows::Forms::DataVisualization::Charting::TextOrientation::Horizontal;
			this->Chart_Source->Titles->Add(title1);
			// 
			// Chart_Final
			// 
			this->Chart_Final->BackColor = System::Drawing::Color::Transparent;
			chartArea2->AxisX->Title = L"Wavelength";
			chartArea2->AxisY->LabelStyle->Format = L"e1";
			chartArea2->BackColor = System::Drawing::Color::DimGray;
			chartArea2->BackGradientStyle = System::Windows::Forms::DataVisualization::Charting::GradientStyle::TopBottom;
			chartArea2->BackSecondaryColor = System::Drawing::Color::Silver;
			chartArea2->Name = L"ChartArea1";
			this->Chart_Final->ChartAreas->Add(chartArea2);
			this->Chart_Final->Location = System::Drawing::Point(12, 564);
			this->Chart_Final->Name = L"Chart_Final";
			series2->BorderColor = System::Drawing::Color::White;
			series2->ChartArea = L"ChartArea1";
			series2->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Line;
			series2->Color = System::Drawing::Color::Red;
			series2->Name = L"Series1";
			this->Chart_Final->Series->Add(series2);
			this->Chart_Final->Size = System::Drawing::Size(827, 258);
			this->Chart_Final->TabIndex = 4;
			this->Chart_Final->Text = L"chart1";
			title2->Font = (gcnew System::Drawing::Font(L"Garamond", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			title2->Name = L"Title1";
			title2->Text = L"Final Throughput";
			title2->TextOrientation = System::Windows::Forms::DataVisualization::Charting::TextOrientation::Horizontal;
			this->Chart_Final->Titles->Add(title2);
			// 
			// SourceTypeGroupBox
			// 
			this->SourceTypeGroupBox->BackColor = System::Drawing::Color::Silver;
			this->SourceTypeGroupBox->Controls->Add(this->GalaxyTypeHelpBtn);
			this->SourceTypeGroupBox->Controls->Add(this->AGNTypeHelpBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SpectralTypeHelpBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SourcePowerLawDrop);
			this->SourceTypeGroupBox->Controls->Add(this->SourceAGNDrop);
			this->SourceTypeGroupBox->Controls->Add(this->SourceBlackbodyTempTxt);
			this->SourceTypeGroupBox->Controls->Add(this->SourcePowerLawRadBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SourceBlackbodyRadBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SourceAGNRadBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SourceGalaxyDrop);
			this->SourceTypeGroupBox->Controls->Add(this->SourceGalaxyRadBtn);
			this->SourceTypeGroupBox->Controls->Add(this->SourceStarDrop);
			this->SourceTypeGroupBox->Controls->Add(this->SourceStarRadBtn);
			this->SourceTypeGroupBox->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->SourceTypeGroupBox->Location = System::Drawing::Point(845, 12);
			this->SourceTypeGroupBox->Name = L"SourceTypeGroupBox";
			this->SourceTypeGroupBox->Size = System::Drawing::Size(247, 165);
			this->SourceTypeGroupBox->TabIndex = 5;
			this->SourceTypeGroupBox->TabStop = false;
			this->SourceTypeGroupBox->Text = L"Source Type";
			// 
			// GalaxyTypeHelpBtn
			// 
			this->GalaxyTypeHelpBtn->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->GalaxyTypeHelpBtn->Location = System::Drawing::Point(217, 133);
			this->GalaxyTypeHelpBtn->Name = L"GalaxyTypeHelpBtn";
			this->GalaxyTypeHelpBtn->Size = System::Drawing::Size(24, 23);
			this->GalaxyTypeHelpBtn->TabIndex = 20;
			this->GalaxyTypeHelpBtn->Text = L"\?";
			this->GalaxyTypeHelpBtn->TextAlign = System::Drawing::ContentAlignment::MiddleRight;
			this->GalaxyTypeHelpBtn->UseVisualStyleBackColor = true;
			this->GalaxyTypeHelpBtn->Click += gcnew System::EventHandler(this, &Form1::GalaxyTypeHelpBtn_Click);
			// 
			// AGNTypeHelpBtn
			// 
			this->AGNTypeHelpBtn->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->AGNTypeHelpBtn->Location = System::Drawing::Point(217, 105);
			this->AGNTypeHelpBtn->Name = L"AGNTypeHelpBtn";
			this->AGNTypeHelpBtn->Size = System::Drawing::Size(24, 23);
			this->AGNTypeHelpBtn->TabIndex = 20;
			this->AGNTypeHelpBtn->Text = L"\?";
			this->AGNTypeHelpBtn->TextAlign = System::Drawing::ContentAlignment::MiddleRight;
			this->AGNTypeHelpBtn->UseVisualStyleBackColor = true;
			this->AGNTypeHelpBtn->Click += gcnew System::EventHandler(this, &Form1::AGNTypeHelpBtn_Click);
			// 
			// SpectralTypeHelpBtn
			// 
			this->SpectralTypeHelpBtn->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->SpectralTypeHelpBtn->Location = System::Drawing::Point(217, 50);
			this->SpectralTypeHelpBtn->Name = L"SpectralTypeHelpBtn";
			this->SpectralTypeHelpBtn->Size = System::Drawing::Size(24, 23);
			this->SpectralTypeHelpBtn->TabIndex = 20;
			this->SpectralTypeHelpBtn->Text = L"\?";
			this->SpectralTypeHelpBtn->TextAlign = System::Drawing::ContentAlignment::MiddleRight;
			this->SpectralTypeHelpBtn->UseVisualStyleBackColor = true;
			this->SpectralTypeHelpBtn->Click += gcnew System::EventHandler(this, &Form1::SpectralTypeHelpBtn_Click);
			// 
			// SourcePowerLawDrop
			// 
			this->SourcePowerLawDrop->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->SourcePowerLawDrop->Enabled = false;
			this->SourcePowerLawDrop->FormattingEnabled = true;
			this->SourcePowerLawDrop->Items->AddRange(gcnew cli::array< System::Object^  >(2) { L"f(nu)", L"f(lambda)" });
			this->SourcePowerLawDrop->Location = System::Drawing::Point(123, 77);
			this->SourcePowerLawDrop->Name = L"SourcePowerLawDrop";
			this->SourcePowerLawDrop->Size = System::Drawing::Size(88, 26);
			this->SourcePowerLawDrop->TabIndex = 14;
			this->SourcePowerLawDrop->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::SourcePowerLawDrop_SelectedIndexChanged);
			// 
			// SourceAGNDrop
			// 
			this->SourceAGNDrop->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->SourceAGNDrop->Enabled = false;
			this->SourceAGNDrop->FormattingEnabled = true;
			this->SourceAGNDrop->Items->AddRange(gcnew cli::array< System::Object^  >(5) {
				L"Liner (norm: v=12.5)", L"NGC-1068 (norm: v=10.3)",
					L"QSO (norm: b=12.5)", L"Seyfert1 (norm: b=12.5)", L"Seyfert2 (norm: v=12.5)"
			});
			this->SourceAGNDrop->Location = System::Drawing::Point(123, 104);
			this->SourceAGNDrop->Name = L"SourceAGNDrop";
			this->SourceAGNDrop->Size = System::Drawing::Size(88, 26);
			this->SourceAGNDrop->TabIndex = 12;
			this->SourceAGNDrop->DropDown += gcnew System::EventHandler(this, &Form1::SourceAGNDrop_DropDown);
			this->SourceAGNDrop->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::SourceAGNDrop_SelectedIndexChanged);
			this->SourceAGNDrop->DropDownClosed += gcnew System::EventHandler(this, &Form1::SourceAGNDrop_DropDownClosed);
			// 
			// SourceBlackbodyTempTxt
			// 
			this->SourceBlackbodyTempTxt->Enabled = false;
			this->SourceBlackbodyTempTxt->Location = System::Drawing::Point(123, 22);
			this->SourceBlackbodyTempTxt->Mask = L"0000000K";
			this->SourceBlackbodyTempTxt->Name = L"SourceBlackbodyTempTxt";
			this->SourceBlackbodyTempTxt->Size = System::Drawing::Size(88, 25);
			this->SourceBlackbodyTempTxt->TabIndex = 7;
			this->SourceBlackbodyTempTxt->Text = L"50000";
			this->SourceBlackbodyTempTxt->TextChanged += gcnew System::EventHandler(this, &Form1::SourceBlackbodyTempTxt_TextChanged);
			this->SourceBlackbodyTempTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::SourceBlackbodyTempTxt_KeyDown);
			// 
			// SourcePowerLawRadBtn
			// 
			this->SourcePowerLawRadBtn->AutoSize = true;
			this->SourcePowerLawRadBtn->Location = System::Drawing::Point(6, 78);
			this->SourcePowerLawRadBtn->Name = L"SourcePowerLawRadBtn";
			this->SourcePowerLawRadBtn->Size = System::Drawing::Size(101, 22);
			this->SourcePowerLawRadBtn->TabIndex = 13;
			this->SourcePowerLawRadBtn->TabStop = true;
			this->SourcePowerLawRadBtn->Text = L"Power Law:";
			this->SourcePowerLawRadBtn->UseVisualStyleBackColor = true;
			this->SourcePowerLawRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::SourcePowerLawRadBtn_CheckedChanged);
			// 
			// SourceBlackbodyRadBtn
			// 
			this->SourceBlackbodyRadBtn->AutoSize = true;
			this->SourceBlackbodyRadBtn->Location = System::Drawing::Point(6, 23);
			this->SourceBlackbodyRadBtn->Name = L"SourceBlackbodyRadBtn";
			this->SourceBlackbodyRadBtn->Size = System::Drawing::Size(97, 22);
			this->SourceBlackbodyRadBtn->TabIndex = 9;
			this->SourceBlackbodyRadBtn->TabStop = true;
			this->SourceBlackbodyRadBtn->Text = L"Blackbody:";
			this->SourceBlackbodyRadBtn->UseVisualStyleBackColor = true;
			this->SourceBlackbodyRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::SourceBlackbodyRadBtn_CheckedChanged);
			// 
			// SourceAGNRadBtn
			// 
			this->SourceAGNRadBtn->AutoSize = true;
			this->SourceAGNRadBtn->Location = System::Drawing::Point(6, 105);
			this->SourceAGNRadBtn->Name = L"SourceAGNRadBtn";
			this->SourceAGNRadBtn->Size = System::Drawing::Size(101, 22);
			this->SourceAGNRadBtn->TabIndex = 11;
			this->SourceAGNRadBtn->TabStop = true;
			this->SourceAGNRadBtn->Text = L"AGN Type:";
			this->SourceAGNRadBtn->UseVisualStyleBackColor = true;
			this->SourceAGNRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::SourceAGNRadBtn_CheckedChanged);
			// 
			// SourceGalaxyDrop
			// 
			this->SourceGalaxyDrop->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->SourceGalaxyDrop->Enabled = false;
			this->SourceGalaxyDrop->FormattingEnabled = true;
			this->SourceGalaxyDrop->Items->AddRange(gcnew cli::array< System::Object^  >(12) {
				L"Elliptical (norm: v=12.5)", L"Bulge (norm: v=12.5)",
					L"S0 (norm: v=12.5)", L"Sa (norm: v=12.5)", L"Sb (norm: v=12.5)", L"Sc (norm: v=12.5)", L"Starburst1: e(b-v)<0.1 (norm: v=12.5)",
					L"Starburst2: 0.11< e(b-v)<0.21 (norm: v=12.5)", L"Starburst3: 0.25< e(b-v)<0.35 (norm: v=12.5)", L"Starburst4: 0.39< e(b-v)<0.50 (norm: v=12.5)",
					L"Starburst5: 0.51< e(b-v)<0.60 (norm: v=12.5)", L"Starburst6: 0.61< e(b-v)<0.70 (norm: v=12.5)"
			});
			this->SourceGalaxyDrop->Location = System::Drawing::Point(123, 132);
			this->SourceGalaxyDrop->Name = L"SourceGalaxyDrop";
			this->SourceGalaxyDrop->Size = System::Drawing::Size(88, 26);
			this->SourceGalaxyDrop->TabIndex = 8;
			this->SourceGalaxyDrop->DropDown += gcnew System::EventHandler(this, &Form1::SourceGalaxyDrop_DropDown);
			this->SourceGalaxyDrop->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::SourceGalaxyDrop_SelectedIndexChanged);
			this->SourceGalaxyDrop->DropDownClosed += gcnew System::EventHandler(this, &Form1::SourceGalaxyDrop_DropDownClosed);
			// 
			// SourceGalaxyRadBtn
			// 
			this->SourceGalaxyRadBtn->AutoSize = true;
			this->SourceGalaxyRadBtn->Location = System::Drawing::Point(6, 133);
			this->SourceGalaxyRadBtn->Name = L"SourceGalaxyRadBtn";
			this->SourceGalaxyRadBtn->Size = System::Drawing::Size(110, 22);
			this->SourceGalaxyRadBtn->TabIndex = 7;
			this->SourceGalaxyRadBtn->TabStop = true;
			this->SourceGalaxyRadBtn->Text = L"Galaxy Type:";
			this->SourceGalaxyRadBtn->UseVisualStyleBackColor = true;
			this->SourceGalaxyRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::SourceGalaxyRadBtn_CheckedChanged);
			// 
			// SourceStarDrop
			// 
			this->SourceStarDrop->DropDownStyle = System::Windows::Forms::ComboBoxStyle::DropDownList;
			this->SourceStarDrop->Enabled = false;
			this->SourceStarDrop->FormattingEnabled = true;
			this->SourceStarDrop->Items->AddRange(gcnew cli::array< System::Object^  >(40) {
				L"O5V", L"O6V", L"O8V", L"B0V", L"B3V", L"B5V",
					L"B8V", L"A0V", L"A5V", L"F0V", L"F5V", L"G0V", L"G5V", L"K0V", L"K5V", L"M0V", L"M2V", L"M5V", L"B0III", L"B5III", L"G0III",
					L"G5III", L"K0III", L"K5III", L"M0III", L"O5I", L"O6I", L"O8I", L"B0I", L"B5I", L"A0I", L"A5I", L"F0I", L"F5I", L"G0I", L"G5I",
					L"K0I", L"K5I", L"M0I", L"M2I"
			});
			this->SourceStarDrop->Location = System::Drawing::Point(123, 49);
			this->SourceStarDrop->Name = L"SourceStarDrop";
			this->SourceStarDrop->Size = System::Drawing::Size(88, 26);
			this->SourceStarDrop->TabIndex = 6;
			this->SourceStarDrop->SelectedIndexChanged += gcnew System::EventHandler(this, &Form1::SourceStarDrop_SelectedIndexChanged);
			// 
			// SourceStarRadBtn
			// 
			this->SourceStarRadBtn->AutoSize = true;
			this->SourceStarRadBtn->Location = System::Drawing::Point(6, 50);
			this->SourceStarRadBtn->Name = L"SourceStarRadBtn";
			this->SourceStarRadBtn->Size = System::Drawing::Size(117, 22);
			this->SourceStarRadBtn->TabIndex = 0;
			this->SourceStarRadBtn->TabStop = true;
			this->SourceStarRadBtn->Text = L"Spectral Type:";
			this->SourceStarRadBtn->UseVisualStyleBackColor = true;
			this->SourceStarRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::SourceStarRadBtn_CheckedChanged);
			// 
			// ExtinctionGroupBox
			// 
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionColumnDensityTxt);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionAvTxt);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionHelpBtn);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionDistanceRadBtn);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionColumnDensityRadBtn);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionAvRadBtn);
			this->ExtinctionGroupBox->Controls->Add(this->ExtinctionRvTxt);
			this->ExtinctionGroupBox->Controls->Add(this->label4);
			this->ExtinctionGroupBox->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->ExtinctionGroupBox->Location = System::Drawing::Point(845, 183);
			this->ExtinctionGroupBox->Name = L"ExtinctionGroupBox";
			this->ExtinctionGroupBox->Size = System::Drawing::Size(301, 140);
			this->ExtinctionGroupBox->TabIndex = 6;
			this->ExtinctionGroupBox->TabStop = false;
			this->ExtinctionGroupBox->Text = L"Exctinction";
			// 
			// ExtinctionColumnDensityTxt
			// 
			this->ExtinctionColumnDensityTxt->Enabled = false;
			this->ExtinctionColumnDensityTxt->Location = System::Drawing::Point(253, 80);
			this->ExtinctionColumnDensityTxt->Name = L"ExtinctionColumnDensityTxt";
			this->ExtinctionColumnDensityTxt->Size = System::Drawing::Size(41, 25);
			this->ExtinctionColumnDensityTxt->TabIndex = 20;
			this->ExtinctionColumnDensityTxt->Text = L"1";
			this->ExtinctionColumnDensityTxt->TextChanged += gcnew System::EventHandler(this, &Form1::ExtinctionColumnDensityTxt_TextChanged);
			this->ExtinctionColumnDensityTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::ExtinctionColumnDensityTxt_KeyDown);
			// 
			// ExtinctionAvTxt
			// 
			this->ExtinctionAvTxt->Enabled = false;
			this->ExtinctionAvTxt->Location = System::Drawing::Point(253, 52);
			this->ExtinctionAvTxt->Name = L"ExtinctionAvTxt";
			this->ExtinctionAvTxt->Size = System::Drawing::Size(41, 25);
			this->ExtinctionAvTxt->TabIndex = 17;
			this->ExtinctionAvTxt->Text = L"1";
			this->ExtinctionAvTxt->TextChanged += gcnew System::EventHandler(this, &Form1::ExtinctionAvTxt_TextChanged);
			this->ExtinctionAvTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::ExtinctionAvTxt_KeyDown);
			// 
			// ExtinctionHelpBtn
			// 
			this->ExtinctionHelpBtn->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->ExtinctionHelpBtn->Location = System::Drawing::Point(271, 15);
			this->ExtinctionHelpBtn->Name = L"ExtinctionHelpBtn";
			this->ExtinctionHelpBtn->Size = System::Drawing::Size(24, 23);
			this->ExtinctionHelpBtn->TabIndex = 19;
			this->ExtinctionHelpBtn->Text = L"\?";
			this->ExtinctionHelpBtn->TextAlign = System::Drawing::ContentAlignment::MiddleRight;
			this->ExtinctionHelpBtn->UseVisualStyleBackColor = true;
			this->ExtinctionHelpBtn->Click += gcnew System::EventHandler(this, &Form1::ExtinctionHelpBtn_Click);
			// 
			// ExtinctionDistanceRadBtn
			// 
			this->ExtinctionDistanceRadBtn->AutoSize = true;
			this->ExtinctionDistanceRadBtn->Location = System::Drawing::Point(9, 109);
			this->ExtinctionDistanceRadBtn->Name = L"ExtinctionDistanceRadBtn";
			this->ExtinctionDistanceRadBtn->Size = System::Drawing::Size(82, 22);
			this->ExtinctionDistanceRadBtn->TabIndex = 18;
			this->ExtinctionDistanceRadBtn->Text = L"Distance";
			this->ExtinctionDistanceRadBtn->UseVisualStyleBackColor = true;
			this->ExtinctionDistanceRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::ExtinctionDistanceRadBtn_CheckedChanged);
			// 
			// ExtinctionColumnDensityRadBtn
			// 
			this->ExtinctionColumnDensityRadBtn->AutoSize = true;
			this->ExtinctionColumnDensityRadBtn->Location = System::Drawing::Point(9, 81);
			this->ExtinctionColumnDensityRadBtn->Name = L"ExtinctionColumnDensityRadBtn";
			this->ExtinctionColumnDensityRadBtn->Size = System::Drawing::Size(238, 22);
			this->ExtinctionColumnDensityRadBtn->TabIndex = 17;
			this->ExtinctionColumnDensityRadBtn->Text = L"Column Density (10^21 / cm^2):";
			this->ExtinctionColumnDensityRadBtn->UseVisualStyleBackColor = true;
			this->ExtinctionColumnDensityRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::ExtinctionColumnDensityRadBtn_CheckedChanged);
			// 
			// ExtinctionAvRadBtn
			// 
			this->ExtinctionAvRadBtn->AutoSize = true;
			this->ExtinctionAvRadBtn->Location = System::Drawing::Point(9, 53);
			this->ExtinctionAvRadBtn->Name = L"ExtinctionAvRadBtn";
			this->ExtinctionAvRadBtn->Size = System::Drawing::Size(201, 22);
			this->ExtinctionAvRadBtn->TabIndex = 14;
			this->ExtinctionAvRadBtn->Text = L"Visual Band Extinction Av:";
			this->ExtinctionAvRadBtn->UseVisualStyleBackColor = true;
			this->ExtinctionAvRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::ExtinctionAvRadBtn_CheckedChanged);
			// 
			// ExtinctionRvTxt
			// 
			this->ExtinctionRvTxt->Location = System::Drawing::Point(38, 22);
			this->ExtinctionRvTxt->Name = L"ExtinctionRvTxt";
			this->ExtinctionRvTxt->Size = System::Drawing::Size(41, 25);
			this->ExtinctionRvTxt->TabIndex = 16;
			this->ExtinctionRvTxt->Text = L"3.1";
			this->ExtinctionRvTxt->TextChanged += gcnew System::EventHandler(this, &Form1::ExtinctionRvTxt_TextChanged);
			this->ExtinctionRvTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::ExtinctionRvTxt_KeyDown);
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(6, 25);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(30, 18);
			this->label4->TabIndex = 15;
			this->label4->Text = L"Rv:";
			// 
			// FilterGroupBox
			// 
			this->FilterGroupBox->Controls->Add(this->label13);
			this->FilterGroupBox->Controls->Add(this->MaxCountgLabel);
			this->FilterGroupBox->Controls->Add(this->MaxCountuLabel);
			this->FilterGroupBox->Controls->Add(this->MaxCountuWideLabel);
			this->FilterGroupBox->Controls->Add(this->MaxCountUVLabel);
			this->FilterGroupBox->Controls->Add(this->MaxCountUVDarkLabel);
			this->FilterGroupBox->Controls->Add(this->SNHelpBtn);
			this->FilterGroupBox->Controls->Add(this->SNTimegLabel);
			this->FilterGroupBox->Controls->Add(this->SNTimeuLabel);
			this->FilterGroupBox->Controls->Add(this->SNTimeuWideLabel);
			this->FilterGroupBox->Controls->Add(this->SNTimeUVLabel);
			this->FilterGroupBox->Controls->Add(this->SNTimeUVDarkLabel);
			this->FilterGroupBox->Controls->Add(this->SNTargetUpD);
			this->FilterGroupBox->Controls->Add(this->label7);
			this->FilterGroupBox->Controls->Add(this->label6);
			this->FilterGroupBox->Controls->Add(this->TotalCountgLabel);
			this->FilterGroupBox->Controls->Add(this->FilteruRadBtn);
			this->FilterGroupBox->Controls->Add(this->TotalCountuLabel);
			this->FilterGroupBox->Controls->Add(this->FilteruWideRadBtn);
			this->FilterGroupBox->Controls->Add(this->TotalCountuWideLabel);
			this->FilterGroupBox->Controls->Add(this->FilterUVDarkRadBtn);
			this->FilterGroupBox->Controls->Add(this->TotalCountUVLabel);
			this->FilterGroupBox->Controls->Add(this->FiltergRadBtn);
			this->FilterGroupBox->Controls->Add(this->TotalCountUVDarkLabel);
			this->FilterGroupBox->Controls->Add(this->FilterUVRadBtn);
			this->FilterGroupBox->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->FilterGroupBox->Location = System::Drawing::Point(845, 329);
			this->FilterGroupBox->Name = L"FilterGroupBox";
			this->FilterGroupBox->Size = System::Drawing::Size(542, 186);
			this->FilterGroupBox->TabIndex = 9;
			this->FilterGroupBox->TabStop = false;
			this->FilterGroupBox->Text = L"Filter";
			// 
			// label13
			// 
			this->label13->AutoSize = true;
			this->label13->Font = (gcnew System::Drawing::Font(L"Garamond", 12, static_cast<System::Drawing::FontStyle>((System::Drawing::FontStyle::Bold | System::Drawing::FontStyle::Underline)),
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			this->label13->Location = System::Drawing::Point(204, 19);
			this->label13->Name = L"label13";
			this->label13->Size = System::Drawing::Size(72, 18);
			this->label13->TabIndex = 27;
			this->label13->Text = L"Max CPS";
			// 
			// MaxCountgLabel
			// 
			this->MaxCountgLabel->AutoSize = true;
			this->MaxCountgLabel->Location = System::Drawing::Point(203, 156);
			this->MaxCountgLabel->Name = L"MaxCountgLabel";
			this->MaxCountgLabel->Size = System::Drawing::Size(44, 18);
			this->MaxCountgLabel->TabIndex = 30;
			this->MaxCountgLabel->Text = L"count";
			// 
			// MaxCountuLabel
			// 
			this->MaxCountuLabel->AutoSize = true;
			this->MaxCountuLabel->Location = System::Drawing::Point(203, 129);
			this->MaxCountuLabel->Name = L"MaxCountuLabel";
			this->MaxCountuLabel->Size = System::Drawing::Size(44, 18);
			this->MaxCountuLabel->TabIndex = 29;
			this->MaxCountuLabel->Text = L"count";
			// 
			// MaxCountuWideLabel
			// 
			this->MaxCountuWideLabel->AutoSize = true;
			this->MaxCountuWideLabel->Location = System::Drawing::Point(203, 100);
			this->MaxCountuWideLabel->Name = L"MaxCountuWideLabel";
			this->MaxCountuWideLabel->Size = System::Drawing::Size(44, 18);
			this->MaxCountuWideLabel->TabIndex = 28;
			this->MaxCountuWideLabel->Text = L"count";
			// 
			// MaxCountUVLabel
			// 
			this->MaxCountUVLabel->AutoSize = true;
			this->MaxCountUVLabel->Location = System::Drawing::Point(203, 72);
			this->MaxCountUVLabel->Name = L"MaxCountUVLabel";
			this->MaxCountUVLabel->Size = System::Drawing::Size(44, 18);
			this->MaxCountUVLabel->TabIndex = 26;
			this->MaxCountUVLabel->Text = L"count";
			// 
			// MaxCountUVDarkLabel
			// 
			this->MaxCountUVDarkLabel->AutoSize = true;
			this->MaxCountUVDarkLabel->Location = System::Drawing::Point(203, 44);
			this->MaxCountUVDarkLabel->Name = L"MaxCountUVDarkLabel";
			this->MaxCountUVDarkLabel->Size = System::Drawing::Size(44, 18);
			this->MaxCountUVDarkLabel->TabIndex = 25;
			this->MaxCountUVDarkLabel->Text = L"count";
			// 
			// SNHelpBtn
			// 
			this->SNHelpBtn->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->SNHelpBtn->Location = System::Drawing::Point(514, 14);
			this->SNHelpBtn->Name = L"SNHelpBtn";
			this->SNHelpBtn->Size = System::Drawing::Size(24, 23);
			this->SNHelpBtn->TabIndex = 20;
			this->SNHelpBtn->Text = L"\?";
			this->SNHelpBtn->TextAlign = System::Drawing::ContentAlignment::MiddleRight;
			this->SNHelpBtn->UseVisualStyleBackColor = true;
			this->SNHelpBtn->Click += gcnew System::EventHandler(this, &Form1::SNHelpBtn_Click);
			// 
			// SNTimegLabel
			// 
			this->SNTimegLabel->AutoSize = true;
			this->SNTimegLabel->Location = System::Drawing::Point(367, 158);
			this->SNTimegLabel->Name = L"SNTimegLabel";
			this->SNTimegLabel->Size = System::Drawing::Size(36, 18);
			this->SNTimegLabel->TabIndex = 24;
			this->SNTimegLabel->Text = L"time";
			// 
			// SNTimeuLabel
			// 
			this->SNTimeuLabel->AutoSize = true;
			this->SNTimeuLabel->Location = System::Drawing::Point(367, 131);
			this->SNTimeuLabel->Name = L"SNTimeuLabel";
			this->SNTimeuLabel->Size = System::Drawing::Size(36, 18);
			this->SNTimeuLabel->TabIndex = 23;
			this->SNTimeuLabel->Text = L"time";
			// 
			// SNTimeuWideLabel
			// 
			this->SNTimeuWideLabel->AutoSize = true;
			this->SNTimeuWideLabel->Location = System::Drawing::Point(367, 102);
			this->SNTimeuWideLabel->Name = L"SNTimeuWideLabel";
			this->SNTimeuWideLabel->Size = System::Drawing::Size(36, 18);
			this->SNTimeuWideLabel->TabIndex = 22;
			this->SNTimeuWideLabel->Text = L"time";
			// 
			// SNTimeUVLabel
			// 
			this->SNTimeUVLabel->AutoSize = true;
			this->SNTimeUVLabel->Location = System::Drawing::Point(367, 74);
			this->SNTimeUVLabel->Name = L"SNTimeUVLabel";
			this->SNTimeUVLabel->Size = System::Drawing::Size(36, 18);
			this->SNTimeUVLabel->TabIndex = 21;
			this->SNTimeUVLabel->Text = L"time";
			// 
			// SNTimeUVDarkLabel
			// 
			this->SNTimeUVDarkLabel->AutoSize = true;
			this->SNTimeUVDarkLabel->Location = System::Drawing::Point(367, 46);
			this->SNTimeUVDarkLabel->Name = L"SNTimeUVDarkLabel";
			this->SNTimeUVDarkLabel->Size = System::Drawing::Size(36, 18);
			this->SNTimeUVDarkLabel->TabIndex = 20;
			this->SNTimeUVDarkLabel->Text = L"time";
			// 
			// SNTargetUpD
			// 
			this->SNTargetUpD->Increment = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 0 });
			this->SNTargetUpD->Location = System::Drawing::Point(460, 18);
			this->SNTargetUpD->Maximum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 1000, 0, 0, 0 });
			this->SNTargetUpD->Name = L"SNTargetUpD";
			this->SNTargetUpD->Size = System::Drawing::Size(52, 25);
			this->SNTargetUpD->TabIndex = 19;
			this->SNTargetUpD->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 0 });
			this->SNTargetUpD->ValueChanged += gcnew System::EventHandler(this, &Form1::SNTargetUpD_ValueChanged);
			// 
			// label7
			// 
			this->label7->AutoSize = true;
			this->label7->Font = (gcnew System::Drawing::Font(L"Garamond", 12, static_cast<System::Drawing::FontStyle>((System::Drawing::FontStyle::Bold | System::Drawing::FontStyle::Underline)),
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			this->label7->Location = System::Drawing::Point(364, 19);
			this->label7->Name = L"label7";
			this->label7->Size = System::Drawing::Size(97, 18);
			this->label7->TabIndex = 16;
			this->label7->Text = L"Time for SN:";
			// 
			// label6
			// 
			this->label6->AutoSize = true;
			this->label6->Font = (gcnew System::Drawing::Font(L"Garamond", 12, static_cast<System::Drawing::FontStyle>((System::Drawing::FontStyle::Bold | System::Drawing::FontStyle::Underline)),
				System::Drawing::GraphicsUnit::Point, static_cast<System::Byte>(0)));
			this->label6->Location = System::Drawing::Point(283, 19);
			this->label6->Name = L"label6";
			this->label6->Size = System::Drawing::Size(77, 18);
			this->label6->TabIndex = 16;
			this->label6->Text = L"Total CPS";
			// 
			// TotalCountgLabel
			// 
			this->TotalCountgLabel->AutoSize = true;
			this->TotalCountgLabel->Location = System::Drawing::Point(282, 156);
			this->TotalCountgLabel->Name = L"TotalCountgLabel";
			this->TotalCountgLabel->Size = System::Drawing::Size(44, 18);
			this->TotalCountgLabel->TabIndex = 18;
			this->TotalCountgLabel->Text = L"count";
			// 
			// FilteruRadBtn
			// 
			this->FilteruRadBtn->AutoSize = true;
			this->FilteruRadBtn->Location = System::Drawing::Point(6, 125);
			this->FilteruRadBtn->Name = L"FilteruRadBtn";
			this->FilteruRadBtn->Size = System::Drawing::Size(149, 22);
			this->FilteruRadBtn->TabIndex = 5;
			this->FilteruRadBtn->TabStop = true;
			this->FilteruRadBtn->Text = L"u (300nm - 400nm)";
			this->FilteruRadBtn->UseVisualStyleBackColor = true;
			this->FilteruRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::FilteruRadBtn_CheckedChanged);
			// 
			// TotalCountuLabel
			// 
			this->TotalCountuLabel->AutoSize = true;
			this->TotalCountuLabel->Location = System::Drawing::Point(282, 129);
			this->TotalCountuLabel->Name = L"TotalCountuLabel";
			this->TotalCountuLabel->Size = System::Drawing::Size(44, 18);
			this->TotalCountuLabel->TabIndex = 17;
			this->TotalCountuLabel->Text = L"count";
			// 
			// FilteruWideRadBtn
			// 
			this->FilteruWideRadBtn->AutoSize = true;
			this->FilteruWideRadBtn->Location = System::Drawing::Point(6, 97);
			this->FilteruWideRadBtn->Name = L"FilteruWideRadBtn";
			this->FilteruWideRadBtn->Size = System::Drawing::Size(186, 22);
			this->FilteruWideRadBtn->TabIndex = 4;
			this->FilteruWideRadBtn->TabStop = true;
			this->FilteruWideRadBtn->Text = L"u Wide (260nm - 400nm)";
			this->FilteruWideRadBtn->UseVisualStyleBackColor = true;
			this->FilteruWideRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::FilteruWideRadBtn_CheckedChanged);
			// 
			// TotalCountuWideLabel
			// 
			this->TotalCountuWideLabel->AutoSize = true;
			this->TotalCountuWideLabel->Location = System::Drawing::Point(282, 100);
			this->TotalCountuWideLabel->Name = L"TotalCountuWideLabel";
			this->TotalCountuWideLabel->Size = System::Drawing::Size(44, 18);
			this->TotalCountuWideLabel->TabIndex = 16;
			this->TotalCountuWideLabel->Text = L"count";
			// 
			// FilterUVDarkRadBtn
			// 
			this->FilterUVDarkRadBtn->AutoSize = true;
			this->FilterUVDarkRadBtn->Location = System::Drawing::Point(6, 41);
			this->FilterUVDarkRadBtn->Name = L"FilterUVDarkRadBtn";
			this->FilterUVDarkRadBtn->Size = System::Drawing::Size(199, 22);
			this->FilterUVDarkRadBtn->TabIndex = 3;
			this->FilterUVDarkRadBtn->TabStop = true;
			this->FilterUVDarkRadBtn->Text = L"UV Dark (135nm - 260nm)";
			this->FilterUVDarkRadBtn->UseVisualStyleBackColor = true;
			this->FilterUVDarkRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::FilterUVDarkRadBtn_CheckedChanged);
			// 
			// TotalCountUVLabel
			// 
			this->TotalCountUVLabel->AutoSize = true;
			this->TotalCountUVLabel->Location = System::Drawing::Point(282, 72);
			this->TotalCountUVLabel->Name = L"TotalCountUVLabel";
			this->TotalCountUVLabel->Size = System::Drawing::Size(44, 18);
			this->TotalCountUVLabel->TabIndex = 15;
			this->TotalCountUVLabel->Text = L"count";
			// 
			// FiltergRadBtn
			// 
			this->FiltergRadBtn->AutoSize = true;
			this->FiltergRadBtn->Location = System::Drawing::Point(6, 153);
			this->FiltergRadBtn->Name = L"FiltergRadBtn";
			this->FiltergRadBtn->Size = System::Drawing::Size(148, 22);
			this->FiltergRadBtn->TabIndex = 2;
			this->FiltergRadBtn->TabStop = true;
			this->FiltergRadBtn->Text = L"g (440nm - 550nm)";
			this->FiltergRadBtn->UseVisualStyleBackColor = true;
			this->FiltergRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::FiltergRadBtn_CheckedChanged);
			// 
			// TotalCountUVDarkLabel
			// 
			this->TotalCountUVDarkLabel->AutoSize = true;
			this->TotalCountUVDarkLabel->Location = System::Drawing::Point(282, 44);
			this->TotalCountUVDarkLabel->Name = L"TotalCountUVDarkLabel";
			this->TotalCountUVDarkLabel->Size = System::Drawing::Size(44, 18);
			this->TotalCountUVDarkLabel->TabIndex = 14;
			this->TotalCountUVDarkLabel->Text = L"count";
			// 
			// FilterUVRadBtn
			// 
			this->FilterUVRadBtn->AutoSize = true;
			this->FilterUVRadBtn->Location = System::Drawing::Point(6, 69);
			this->FilterUVRadBtn->Name = L"FilterUVRadBtn";
			this->FilterUVRadBtn->Size = System::Drawing::Size(163, 22);
			this->FilterUVRadBtn->TabIndex = 0;
			this->FilterUVRadBtn->TabStop = true;
			this->FilterUVRadBtn->Text = L"UV (150nm - 300nm)";
			this->FilterUVRadBtn->UseVisualStyleBackColor = true;
			this->FilterUVRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::FilterUVRadBtn_CheckedChanged);
			// 
			// EscBtn
			// 
			this->EscBtn->DialogResult = System::Windows::Forms::DialogResult::Cancel;
			this->EscBtn->Location = System::Drawing::Point(219, 182);
			this->EscBtn->Name = L"EscBtn";
			this->EscBtn->Size = System::Drawing::Size(75, 23);
			this->EscBtn->TabIndex = 12;
			this->EscBtn->Text = L"Escape";
			this->EscBtn->UseVisualStyleBackColor = true;
			this->EscBtn->Click += gcnew System::EventHandler(this, &Form1::EscBtn_Click);
			// 
			// SourcePropertiesGroupBox
			// 
			this->SourcePropertiesGroupBox->Controls->Add(this->SersicRTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->SersicnTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->label12);
			this->SourcePropertiesGroupBox->Controls->Add(this->SersicReffTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->DistanceTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->RedShiftTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->label11);
			this->SourcePropertiesGroupBox->Controls->Add(this->label10);
			this->SourcePropertiesGroupBox->Controls->Add(this->PowerLawNormTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->RadiusTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->label1);
			this->SourcePropertiesGroupBox->Controls->Add(this->label5);
			this->SourcePropertiesGroupBox->Controls->Add(this->mTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->label3);
			this->SourcePropertiesGroupBox->Controls->Add(this->PowerLawAlphaLabel);
			this->SourcePropertiesGroupBox->Controls->Add(this->PowerLawAlphaTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->label2);
			this->SourcePropertiesGroupBox->Controls->Add(this->mvTxt);
			this->SourcePropertiesGroupBox->Controls->Add(this->mvRadBtn);
			this->SourcePropertiesGroupBox->Controls->Add(this->DistanceRadBtn);
			this->SourcePropertiesGroupBox->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->SourcePropertiesGroupBox->Location = System::Drawing::Point(1098, 12);
			this->SourcePropertiesGroupBox->Name = L"SourcePropertiesGroupBox";
			this->SourcePropertiesGroupBox->Size = System::Drawing::Size(336, 165);
			this->SourcePropertiesGroupBox->TabIndex = 13;
			this->SourcePropertiesGroupBox->TabStop = false;
			this->SourcePropertiesGroupBox->Text = L"Source Properties";
			// 
			// SersicRTxt
			// 
			this->SersicRTxt->Location = System::Drawing::Point(286, 132);
			this->SersicRTxt->Name = L"SersicRTxt";
			this->SersicRTxt->Size = System::Drawing::Size(45, 25);
			this->SersicRTxt->TabIndex = 24;
			this->SersicRTxt->Text = L"20";
			// 
			// SersicnTxt
			// 
			this->SersicnTxt->Location = System::Drawing::Point(125, 132);
			this->SersicnTxt->Name = L"SersicnTxt";
			this->SersicnTxt->Size = System::Drawing::Size(45, 25);
			this->SersicnTxt->TabIndex = 28;
			this->SersicnTxt->Text = L"1";
			// 
			// label12
			// 
			this->label12->AutoSize = true;
			this->label12->Location = System::Drawing::Point(6, 135);
			this->label12->Name = L"label12";
			this->label12->Size = System::Drawing::Size(106, 18);
			this->label12->TabIndex = 27;
			this->label12->Text = L"Sersic Factor n:";
			// 
			// SersicReffTxt
			// 
			this->SersicReffTxt->Location = System::Drawing::Point(218, 133);
			this->SersicReffTxt->Name = L"SersicReffTxt";
			this->SersicReffTxt->Size = System::Drawing::Size(45, 25);
			this->SersicReffTxt->TabIndex = 26;
			this->SersicReffTxt->Text = L"12";
			// 
			// DistanceTxt
			// 
			this->DistanceTxt->Location = System::Drawing::Point(125, 24);
			this->DistanceTxt->Name = L"DistanceTxt";
			this->DistanceTxt->Size = System::Drawing::Size(45, 25);
			this->DistanceTxt->TabIndex = 14;
			this->DistanceTxt->Text = L"0.5";
			this->DistanceTxt->TextChanged += gcnew System::EventHandler(this, &Form1::DistanceTxt_TextChanged);
			this->DistanceTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::DistanceTxt_KeyDown);
			// 
			// RedShiftTxt
			// 
			this->RedShiftTxt->Location = System::Drawing::Point(286, 107);
			this->RedShiftTxt->Name = L"RedShiftTxt";
			this->RedShiftTxt->Size = System::Drawing::Size(45, 25);
			this->RedShiftTxt->TabIndex = 22;
			this->RedShiftTxt->Text = L"0";
			this->RedShiftTxt->TextChanged += gcnew System::EventHandler(this, &Form1::RedShiftTxt_TextChanged);
			this->RedShiftTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::RedShiftTxt_KeyDown);
			// 
			// label11
			// 
			this->label11->AutoSize = true;
			this->label11->Location = System::Drawing::Point(264, 135);
			this->label11->Name = L"label11";
			this->label11->Size = System::Drawing::Size(25, 18);
			this->label11->TabIndex = 23;
			this->label11->Text = L"Io:";
			// 
			// label10
			// 
			this->label10->AutoSize = true;
			this->label10->Location = System::Drawing::Point(173, 135);
			this->label10->Name = L"label10";
			this->label10->Size = System::Drawing::Size(47, 18);
			this->label10->TabIndex = 25;
			this->label10->Text = L"R_eff:";
			// 
			// PowerLawNormTxt
			// 
			this->PowerLawNormTxt->Location = System::Drawing::Point(286, 80);
			this->PowerLawNormTxt->Name = L"PowerLawNormTxt";
			this->PowerLawNormTxt->Size = System::Drawing::Size(45, 25);
			this->PowerLawNormTxt->TabIndex = 18;
			this->PowerLawNormTxt->Text = L"1";
			this->PowerLawNormTxt->TextChanged += gcnew System::EventHandler(this, &Form1::PowerLawNormTxt_TextChanged);
			this->PowerLawNormTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::PowerLawNormTxt_KeyDown);
			// 
			// RadiusTxt
			// 
			this->RadiusTxt->Location = System::Drawing::Point(286, 26);
			this->RadiusTxt->Name = L"RadiusTxt";
			this->RadiusTxt->Size = System::Drawing::Size(45, 25);
			this->RadiusTxt->TabIndex = 16;
			this->RadiusTxt->Text = L"1";
			this->RadiusTxt->TextChanged += gcnew System::EventHandler(this, &Form1::RadiusTxt_TextChanged);
			this->RadiusTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::RadiusTxt_KeyDown);
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(218, 110);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(71, 18);
			this->label1->TabIndex = 21;
			this->label1->Text = L"Red Shift:";
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(6, 108);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(39, 18);
			this->label5->TabIndex = 19;
			this->label5->Text = L"Mag:";
			// 
			// mTxt
			// 
			this->mTxt->Location = System::Drawing::Point(125, 105);
			this->mTxt->Name = L"mTxt";
			this->mTxt->Size = System::Drawing::Size(45, 25);
			this->mTxt->TabIndex = 20;
			this->mTxt->Text = L"12.5";
			this->mTxt->TextChanged += gcnew System::EventHandler(this, &Form1::mTxt_TextChanged);
			this->mTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::mTxt_KeyDown);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(240, 81);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(49, 18);
			this->label3->TabIndex = 17;
			this->label3->Text = L"Norm:";
			// 
			// PowerLawAlphaLabel
			// 
			this->PowerLawAlphaLabel->AutoSize = true;
			this->PowerLawAlphaLabel->Location = System::Drawing::Point(6, 81);
			this->PowerLawAlphaLabel->Name = L"PowerLawAlphaLabel";
			this->PowerLawAlphaLabel->Size = System::Drawing::Size(70, 18);
			this->PowerLawAlphaLabel->TabIndex = 15;
			this->PowerLawAlphaLabel->Text = L"Alpha nu:";
			// 
			// PowerLawAlphaTxt
			// 
			this->PowerLawAlphaTxt->Location = System::Drawing::Point(125, 78);
			this->PowerLawAlphaTxt->Name = L"PowerLawAlphaTxt";
			this->PowerLawAlphaTxt->Size = System::Drawing::Size(45, 25);
			this->PowerLawAlphaTxt->TabIndex = 15;
			this->PowerLawAlphaTxt->Text = L"1";
			this->PowerLawAlphaTxt->TextChanged += gcnew System::EventHandler(this, &Form1::PowerLawAlphaTxt_TextChanged);
			this->PowerLawAlphaTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::PowerLawAlphaTxt_KeyDown);
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(186, 29);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(103, 18);
			this->label2->TabIndex = 14;
			this->label2->Text = L"Radius (RSun):";
			// 
			// mvTxt
			// 
			this->mvTxt->Location = System::Drawing::Point(125, 51);
			this->mvTxt->Name = L"mvTxt";
			this->mvTxt->Size = System::Drawing::Size(45, 25);
			this->mvTxt->TabIndex = 15;
			this->mvTxt->Text = L"0.5";
			this->mvTxt->TextChanged += gcnew System::EventHandler(this, &Form1::mvTxt_TextChanged);
			this->mvTxt->KeyDown += gcnew System::Windows::Forms::KeyEventHandler(this, &Form1::mvTxt_KeyDown);
			// 
			// mvRadBtn
			// 
			this->mvRadBtn->AutoSize = true;
			this->mvRadBtn->Location = System::Drawing::Point(6, 52);
			this->mvRadBtn->Name = L"mvRadBtn";
			this->mvRadBtn->Size = System::Drawing::Size(50, 22);
			this->mvRadBtn->TabIndex = 12;
			this->mvRadBtn->TabStop = true;
			this->mvRadBtn->Text = L"mv:";
			this->mvRadBtn->UseVisualStyleBackColor = true;
			this->mvRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::mvRadBtn_CheckedChanged);
			// 
			// DistanceRadBtn
			// 
			this->DistanceRadBtn->AutoSize = true;
			this->DistanceRadBtn->Location = System::Drawing::Point(6, 27);
			this->DistanceRadBtn->Name = L"DistanceRadBtn";
			this->DistanceRadBtn->Size = System::Drawing::Size(123, 22);
			this->DistanceRadBtn->TabIndex = 10;
			this->DistanceRadBtn->TabStop = true;
			this->DistanceRadBtn->Text = L"Distance (kpc):";
			this->DistanceRadBtn->UseVisualStyleBackColor = true;
			this->DistanceRadBtn->CheckedChanged += gcnew System::EventHandler(this, &Form1::DistanceRadBtn_CheckedChanged);
			// 
			// Chart_Filter
			// 
			this->Chart_Filter->BackColor = System::Drawing::Color::Transparent;
			chartArea3->AxisX->Title = L"Wavelength";
			chartArea3->AxisY->LabelStyle->Format = L"e1";
			chartArea3->BackColor = System::Drawing::Color::DimGray;
			chartArea3->BackGradientStyle = System::Windows::Forms::DataVisualization::Charting::GradientStyle::TopBottom;
			chartArea3->BackSecondaryColor = System::Drawing::Color::Silver;
			chartArea3->Name = L"ChartArea1";
			this->Chart_Filter->ChartAreas->Add(chartArea3);
			this->Chart_Filter->Location = System::Drawing::Point(12, 300);
			this->Chart_Filter->Name = L"Chart_Filter";
			series3->BorderColor = System::Drawing::Color::White;
			series3->ChartArea = L"ChartArea1";
			series3->ChartType = System::Windows::Forms::DataVisualization::Charting::SeriesChartType::Line;
			series3->Color = System::Drawing::Color::Red;
			series3->Name = L"Series1";
			this->Chart_Filter->Series->Add(series3);
			this->Chart_Filter->Size = System::Drawing::Size(827, 258);
			this->Chart_Filter->TabIndex = 14;
			this->Chart_Filter->Text = L"chart1";
			title3->Font = (gcnew System::Drawing::Font(L"Garamond", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			title3->Name = L"Title1";
			title3->Text = L"Mirror * Filter * QE";
			title3->TextOrientation = System::Windows::Forms::DataVisualization::Charting::TextOrientation::Horizontal;
			this->Chart_Filter->Titles->Add(title3);
			// 
			// ShowLocalFlux
			// 
			this->ShowLocalFlux->AutoSize = true;
			this->ShowLocalFlux->Checked = true;
			this->ShowLocalFlux->CheckState = System::Windows::Forms::CheckState::Checked;
			this->ShowLocalFlux->Location = System::Drawing::Point(122, 46);
			this->ShowLocalFlux->Name = L"ShowLocalFlux";
			this->ShowLocalFlux->Size = System::Drawing::Size(52, 17);
			this->ShowLocalFlux->TabIndex = 15;
			this->ShowLocalFlux->Text = L"Local";
			this->ShowLocalFlux->UseVisualStyleBackColor = true;
			this->ShowLocalFlux->CheckedChanged += gcnew System::EventHandler(this, &Form1::ShowLocalFlux_CheckedChanged);
			// 
			// DetectorGroupBox
			// 
			this->DetectorGroupBox->Controls->Add(this->FWHMUpD);
			this->DetectorGroupBox->Controls->Add(this->label9);
			this->DetectorGroupBox->Controls->Add(this->PlateScaleUpD);
			this->DetectorGroupBox->Controls->Add(this->label8);
			this->DetectorGroupBox->Font = (gcnew System::Drawing::Font(L"Garamond", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(0)));
			this->DetectorGroupBox->Location = System::Drawing::Point(1152, 183);
			this->DetectorGroupBox->Name = L"DetectorGroupBox";
			this->DetectorGroupBox->Size = System::Drawing::Size(248, 79);
			this->DetectorGroupBox->TabIndex = 16;
			this->DetectorGroupBox->TabStop = false;
			this->DetectorGroupBox->Text = L"Detector && Optics";
			// 
			// FWHMUpD
			// 
			this->FWHMUpD->DecimalPlaces = 2;
			this->FWHMUpD->Increment = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 131072 });
			this->FWHMUpD->Location = System::Drawing::Point(174, 45);
			this->FWHMUpD->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 131072 });
			this->FWHMUpD->Name = L"FWHMUpD";
			this->FWHMUpD->Size = System::Drawing::Size(67, 25);
			this->FWHMUpD->TabIndex = 3;
			this->FWHMUpD->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) { 15, 0, 0, 131072 });
			this->FWHMUpD->ValueChanged += gcnew System::EventHandler(this, &Form1::FWHMUpD_ValueChanged);
			// 
			// label9
			// 
			this->label9->AutoSize = true;
			this->label9->Location = System::Drawing::Point(6, 49);
			this->label9->Name = L"label9";
			this->label9->Size = System::Drawing::Size(109, 18);
			this->label9->TabIndex = 2;
			this->label9->Text = L"FWHM (arcsec)";
			// 
			// PlateScaleUpD
			// 
			this->PlateScaleUpD->DecimalPlaces = 2;
			this->PlateScaleUpD->Increment = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 131072 });
			this->PlateScaleUpD->Location = System::Drawing::Point(174, 19);
			this->PlateScaleUpD->Minimum = System::Decimal(gcnew cli::array< System::Int32 >(4) { 5, 0, 0, 131072 });
			this->PlateScaleUpD->Name = L"PlateScaleUpD";
			this->PlateScaleUpD->Size = System::Drawing::Size(67, 25);
			this->PlateScaleUpD->TabIndex = 1;
			this->PlateScaleUpD->Value = System::Decimal(gcnew cli::array< System::Int32 >(4) { 1, 0, 0, 65536 });
			this->PlateScaleUpD->ValueChanged += gcnew System::EventHandler(this, &Form1::PlateScaleUpD_ValueChanged);
			// 
			// label8
			// 
			this->label8->AutoSize = true;
			this->label8->Location = System::Drawing::Point(6, 21);
			this->label8->Name = L"label8";
			this->label8->Size = System::Drawing::Size(168, 18);
			this->label8->TabIndex = 0;
			this->label8->Text = L"Plate Scale (arcsec/pixel)";
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->BackColor = System::Drawing::Color::Silver;
			this->CancelButton = this->EscBtn;
			this->ClientSize = System::Drawing::Size(1446, 842);
			this->Controls->Add(this->DetectorGroupBox);
			this->Controls->Add(this->ShowLocalFlux);
			this->Controls->Add(this->Chart_Source);
			this->Controls->Add(this->SourcePropertiesGroupBox);
			this->Controls->Add(this->FilterGroupBox);
			this->Controls->Add(this->ExtinctionGroupBox);
			this->Controls->Add(this->SourceTypeGroupBox);
			this->Controls->Add(this->Chart_Final);
			this->Controls->Add(this->EscBtn);
			this->Controls->Add(this->Chart_Filter);
			this->Icon = (cli::safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->Name = L"Form1";
			this->StartPosition = System::Windows::Forms::FormStartPosition::CenterScreen;
			this->Text = L"CASTOR ETC";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Source))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Final))->EndInit();
			this->SourceTypeGroupBox->ResumeLayout(false);
			this->SourceTypeGroupBox->PerformLayout();
			this->ExtinctionGroupBox->ResumeLayout(false);
			this->ExtinctionGroupBox->PerformLayout();
			this->FilterGroupBox->ResumeLayout(false);
			this->FilterGroupBox->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->SNTargetUpD))->EndInit();
			this->SourcePropertiesGroupBox->ResumeLayout(false);
			this->SourcePropertiesGroupBox->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->Chart_Filter))->EndInit();
			this->DetectorGroupBox->ResumeLayout(false);
			this->DetectorGroupBox->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->FWHMUpD))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->PlateScaleUpD))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion

void InitializeVars(array<String^>^ startargs);

void ThroughPutETC();
void FilterUpDate();
void SourceBlackBody();
void SourcePowerLaw();
void SourceSpectralType();
void SourceGalaxy();
void SourceAGN();
double FV_SURFACE;
double NORM_MAG;
double a_lambda(double lambda);//lambda in nm
double b_lambda(double lambda);//lambda in nm
void CheckTextToNum(TextBox^ textbox);

array<String^>^ STARTARGS;
int SESSION = 0;
bool FIRSTSLOAD = false;
bool COMPUTATION_EXISTS = false;
bool ISPOINTSOURCE = true;
int LAMBDASTEP = 1;//lambda steps in 1nm increments
array<double>^ LAMBDA_NM;//lambda is 120nm to 1100nm in 1nm steps giving 1100 - 120 + 1 = 981 elements
int NELEMENTS = 1100 - 120 + 1;
double PLOT_MIN = 120;
double PLOT_MAX = 620;
array<double>^ SOURCE_FLUX = gcnew array<double>(NELEMENTS);//vector for source flux curve
array<double>^ SOURCE_FLUX_LOCAL = gcnew array<double>(NELEMENTS);//vector for source flux curve
array<double>^ EXTINCTION = gcnew array<double>(NELEMENTS);//vector for extintion curve
array<double>^ DETECTOR_QE = gcnew array<double>(NELEMENTS);//vector for quantum efficiency curve
array<double>^ MIRROR_REFL = gcnew array<double>(NELEMENTS);//vector for mirror reflectivity curve
array<double>^ BACKGROUND = gcnew array<double>(NELEMENTS);//vector for background curve
array<double, 2>^ FILTERS = gcnew array<double, 2>(NELEMENTS, 5);//uvdark, uv, uwide, u, g  --vector array for filters
array<double, 2>^ FINAL_FLUX_FILTERS = gcnew array<double, 2>(NELEMENTS, 5);//uvdark, uv, uwide, u, g  --vector array for final fluxes each filter
array<double, 2>^ FINAL_FLUX_FILTERS_BG = gcnew array<double, 2>(NELEMENTS, 5);//uvdark, uv, uwide, u, g  --vector array for final fluxes each filter backgrounds
array<double>^ FINAL_COUNTS;
array<double>^ FINAL_MAXS;
array<double>^ FINAL_COUNTS_BG;
array<double>^ SN_TIME;

double MIRROR_EFF_AREA; //mirror effective collection area, set at Form1_Load

int SELECTED_FILTER = 0;

String^ CASTOR_DIRECTORY = "C:\\CASTOR_ETC\\";
String^ LAMBDA_NM_FILENAME = CASTOR_DIRECTORY + "Lambda\\lambda_nm.fits";
String^ QE_FILENAME = CASTOR_DIRECTORY + "QE\\QE.fits";
String^ MIRROR_REFL_FILENAME = CASTOR_DIRECTORY + "Reflectivity\\reflectivity.fits";
String^ MIRROR_EFF_AREA_FILENAME = CASTOR_DIRECTORY + "Area\\area.txt";
String^ BACKGROUND_FILENAME = CASTOR_DIRECTORY + "Background\\background.fits";
array<String^>^ FILTER_FILENAMES = gcnew array<String^>(5);//uvdark, uv, uwide, u, g,
String^ FILTER_uvdark_FILENAME = CASTOR_DIRECTORY + "Filters\\uv_dark_135_260.fits";
String^ FILTER_uv_FILENAME = CASTOR_DIRECTORY + "Filters\\uv_150_300.fits";
String^ FILTER_uwide_FILENAME = CASTOR_DIRECTORY + "Filters\\u_wide_260_400.fits";
String^ FILTER_u_FILENAME = CASTOR_DIRECTORY + "Filters\\u_300_400.fits";
String^ FILTER_g_FILENAME = CASTOR_DIRECTORY + "Filters\\g_440_550.fits";

double h = 6.626068E-34; //Js
double c = 2.9979E8; //m/s
double K = 1.38065E-23; //J/K
double Rsun = 6.96E8; //m
double SR2KPC = 2.25534E-11; //solar radii to kpc
double KPC2M = 3.08568025E19; //kpc to meters

void SetReg(System::String^ ProgramName, System::String^ KeyName, System::Object^ KeyValue)
{
	RegistryKey^ User = Registry::CurrentUser;
	RegistryKey^ SW = User->OpenSubKey("Software", true);
	RegistryKey^ AstroWerks = SW->CreateSubKey("AstroWerks");
	RegistryKey^ SUBKEY = AstroWerks->CreateSubKey(ProgramName);
	SUBKEY->SetValue(KeyName, KeyValue);
}

Object^ GetReg(System::String^ ProgramName, System::String^ KeyName)
{
	RegistryKey^ User = Registry::CurrentUser;
	RegistryKey^ SW = User->OpenSubKey("Software", true);
	RegistryKey^ AstroWerks = SW->CreateSubKey("AstroWerks");
	RegistryKey^ SUBKEY = AstroWerks->CreateSubKey(ProgramName);
	Object ^ result = SUBKEY->GetValue(KeyName);
	return result;
}

private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceStarRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceGalaxyRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceStarDrop_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceGalaxyDrop_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceBlackbodyRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceBlackbodyTempTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceAGNDrop_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceAGNRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FilterUVRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FiltergRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FilteruRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FilterUVDarkRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FilteruWideRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void EscBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourcePowerLawRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourcePowerLawDrop_SelectedIndexChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void DistanceRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void DistanceTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void mvTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void RadiusTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionRvTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionHelpBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceBlackbodyTempTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void mvRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceGalaxyDrop_DropDown(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceGalaxyDrop_DropDownClosed(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceAGNDrop_DropDown(System::Object^  sender, System::EventArgs^  e);
private: System::Void SourceAGNDrop_DropDownClosed(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionAvRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionColumnDensityRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionDistanceRadBtn_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionAvTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ExtinctionColumnDensityTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void ShowLocalFlux_CheckedChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void PowerLawAlphaTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void PowerLawNormTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void mTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void RedShiftTxt_TextChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SpectralTypeHelpBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void AGNTypeHelpBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void GalaxyTypeHelpBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void RedShiftTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void mTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void mvTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void DistanceTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void RadiusTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void PowerLawAlphaTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void PowerLawNormTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void ExtinctionAvTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void ExtinctionColumnDensityTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void ExtinctionRvTxt_KeyDown(System::Object^  sender, System::Windows::Forms::KeyEventArgs^  e);
private: System::Void SNTargetUpD_ValueChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void SNHelpBtn_Click(System::Object^  sender, System::EventArgs^  e);
private: System::Void PlateScaleUpD_ValueChanged(System::Object^  sender, System::EventArgs^  e);
private: System::Void FWHMUpD_ValueChanged(System::Object^  sender, System::EventArgs^  e);
};
}
