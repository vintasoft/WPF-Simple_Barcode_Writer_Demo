using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using Vintasoft.WpfBarcode;
using Vintasoft.WpfBarcode.BarcodeInfo;
using Vintasoft.WpfBarcode.SymbologySubsets;
using Vintasoft.WpfBarcode.SymbologySubsets.GS1;
using Vintasoft.WpfBarcode.SymbologySubsets.RoyalMailMailmark;
using Vintasoft.WpfBarcode.GS1;
using Vintasoft.WpfBarcode.BarcodeStructure;

namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// MainWindow logic.
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Fields

        GS1ApplicationIdentifierValue[] _GS1ApplicationIdentifierValues;

        MailmarkCmdmValueItem _mailmarkCmdmValueItem = new MailmarkCmdmValueItem();

        PpnBarcodeValue _ppnBarcodeValue = new PpnBarcodeValue();

        bool _isInitialized = false;

        #endregion



        #region Constructor

        public MainWindow()
        {
            InitializeComponent();

            Title = "VintaSoft WPF Simple Barcode Writer Demo v" + BarcodeGlobalSettings.ProductVersion;

            barcodeWriter.BarcodeImageChanged += new EventHandler<BarcodeImageChangedEventArgs>(barcodeWriter_BarcodeImageChanged);

            barcodeWriter.BeginInit();

            barcodeGroupsTabPages.SelectionChanged += new SelectionChangedEventHandler(barcodeGroupsTabPages_SelectionChanged);

            // default GS1 value
            _GS1ApplicationIdentifierValues = new GS1ApplicationIdentifierValue[] { 
                new GS1ApplicationIdentifierValue(
                    GS1ApplicationIdentifiers.FindApplicationIdentifier("01"),
                    "0123456789012C")
            };

            // common
            barcodeValueTextBox.TextChanged += new TextChangedEventHandler(barcodeValueTextBox_TextChanged);
            barcodeValueTextBox.Text = "01234567";

            AddEnumValues(pixelFormatComboBox, typeof(BarcodeImagePixelFormat));
            pixelFormatComboBox.SelectionChanged += new SelectionChangedEventHandler(pixelFormatComboBox_SelectionChanged);
            pixelFormatComboBox.SelectedItem = BarcodeImagePixelFormat.Bgr24;

            minWidthNumericUpDown.ValueChanged += new EventHandler<EventArgs>(minWidthNumericUpDown_ValueChanged);
            paddingNumericUpDown.ValueChanged += new EventHandler<EventArgs>(paddingNumericUpDown_ValueChanged);
            widthAdjustNumericUpDown.ValueChanged += new EventHandler<EventArgs>(widthAdjustNumericUpDown_ValueChanged);

            foregroundColorPicker.SelectedColorChanged += new EventHandler(foregroundColorPicker_SelectedColorChanged);
            foregroundColorPicker.SelectedColor = Colors.Black;

            backgroundColorPicker.SelectedColorChanged += new EventHandler(backgroundColorPicker_SelectedColorChanged);
            backgroundColorPicker.SelectedColor = Colors.White;

            // linear
            linearBarcodeTypeComboBox.SelectionChanged += new SelectionChangedEventHandler(linearBarcodeTypeComboBox_SelectionChanged);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Code128);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.SSCC18);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.SwissPostParcel);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.FedExGround96);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.VicsBol);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.VicsScacPro);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Code16K);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Code93);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Code39);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.Code39Extended);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.Code32);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.VIN);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.PZN);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.NumlyNumber);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.DhlAwb);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Code11);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Codabar);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN13);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN13Plus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN13Plus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN13);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN13Plus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN13Plus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN8);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN8Plus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.EAN8Plus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN8);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN8Plus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.JAN8Plus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.EANVelocity);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISBN);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISBNPlus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISBNPlus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISMN);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISMNPlus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISMNPlus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISSN);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISSNPlus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISSNPlus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Interleaved2of5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.Interleaved2of5ChecksumISO16390);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.OPC);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.DeutschePostIdentcode);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.DeutschePostLeitcode);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.MSI);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.PatchCode);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Pharmacode);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RSS14);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RSS14Stacked);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RSSLimited);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RSSExpanded);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RSSExpandedStacked);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Standard2of5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.IATA2of5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Matrix2of5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Telepen);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCA);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCAPlus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCAPlus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCE);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCEPlus2);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.UPCEPlus5);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.AustralianPost);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.IntelligentMail);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Planet);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Postnet);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.DutchKIX);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.RoyalMail);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Mailmark4StateC);
            linearBarcodeTypeComboBox.Items.Add(BarcodeType.Mailmark4StateL);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataBar);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1_128);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataBarStacked);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataBarLimited);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataBarExpanded);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataBarExpandedStacked);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ITF14);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.ISBT128);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.HIBCLIC128);
            linearBarcodeTypeComboBox.Items.Add(BarcodeSymbologySubsets.HIBCLIC39);

            // sort supported barcode list
            object[] barcodes = new object[linearBarcodeTypeComboBox.Items.Count];
            linearBarcodeTypeComboBox.Items.CopyTo(barcodes, 0);
            string[] names = new string[barcodes.Length];
            for (int i = 0; i < barcodes.Length; i++)
                names[i] = barcodes[i].ToString();
            Array.Sort(names, barcodes);
            linearBarcodeTypeComboBox.Items.Clear();
            foreach (object barcode in barcodes)
                linearBarcodeTypeComboBox.Items.Add(barcode);


            // 2D
            twoDimensionalBarcodeComboBox.SelectionChanged += new SelectionChangedEventHandler(twoDimensionalBarcodeComboBox_SelectionChanged);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.Aztec);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.DataMatrix);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.PDF417);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.MicroPDF417);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.QR);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.MicroQR);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.MaxiCode);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeType.HanXinCode);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.GS1Aztec);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.GS1DataMatrix);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.GS1QR);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.MailmarkCmdmType7);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.MailmarkCmdmType9);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.MailmarkCmdmType29);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.PPN);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.XFACompressedAztec);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.XFACompressedDataMatrix);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.XFACompressedPDF417);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.XFACompressedQRCode);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.ISBT128DataMatrix);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.HIBCLICAztecCode);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.HIBCLICDataMatrix);
            twoDimensionalBarcodeComboBox.Items.Add(BarcodeSymbologySubsets.HIBCLICQRCode);


            linearBarcodeHeight.ValueChanged += new EventHandler<EventArgs>(linearBarcodeHeight_ValueChanged);

            valueAutoLetterSpacingCheckBox.Checked += new RoutedEventHandler(valueAutoLetterSpacingCheckBox_CheckedChanged);
            valueAutoLetterSpacingCheckBox.Unchecked += new RoutedEventHandler(valueAutoLetterSpacingCheckBox_CheckedChanged);

            valueVisibleCheckBox.Checked += new RoutedEventHandler(valueVisibleCheckBox_CheckedChanged);
            valueVisibleCheckBox.Unchecked += new RoutedEventHandler(valueVisibleCheckBox_CheckedChanged);

            valueGapNumericUpDown.ValueChanged += new EventHandler<EventArgs>(valueGapNumericUpDown_ValueChanged);

            fontFamilySelector.SelectedFamilyChanged += new EventHandler(fontFamilySelector_SelectedFamilyChanged);
            fontFamilySelector.SelectedFamily = new FontFamily("Courier New");

            valueFontSizeNumericUpDown.ValueChanged += new EventHandler<EventArgs>(valueFontSizeNumericUpDown_ValueChanged);

            // MSI checksum
            msiChecksumComboBox.Items.Add(MSIChecksumType.Mod10);
            msiChecksumComboBox.Items.Add(MSIChecksumType.Mod10Mod10);
            msiChecksumComboBox.Items.Add(MSIChecksumType.Mod11);
            msiChecksumComboBox.Items.Add(MSIChecksumType.Mod11Mod10);
            msiChecksumComboBox.SelectionChanged += new SelectionChangedEventHandler(msiChecksumComboBox_SelectionChanged);

            // Code 128 encoding mode
            code128ModeComboBox.Items.Add(Code128EncodingMode.Undefined);
            code128ModeComboBox.Items.Add(Code128EncodingMode.ModeA);
            code128ModeComboBox.Items.Add(Code128EncodingMode.ModeB);
            code128ModeComboBox.Items.Add(Code128EncodingMode.ModeC);
            code128ModeComboBox.SelectionChanged += new SelectionChangedEventHandler(code128ModeComboBox_SelectionChanged);

            // EAN subtypes
            AddEnumValues(eanSubtypeComboBox, typeof(EANSubtype));
            eanSubtypeComboBox.SelectionChanged += new SelectionChangedEventHandler(eanSubtypeComboBox_SelectionChanged);

            // RSS
            for (int i = 2; i <= 20; i += 2)
                rssExpandedStackedSegmentPerRow.Items.Add(i);
            rssLinkageFlag.Checked += new RoutedEventHandler(rssLinkageFlag_Checked);
            rssLinkageFlag.Unchecked += new RoutedEventHandler(rssLinkageFlag_Checked);
            rss14StackedOmni.Checked += new RoutedEventHandler(rss14StackedOmni_Checked);
            rss14StackedOmni.Unchecked += new RoutedEventHandler(rss14StackedOmni_Checked);
            rssExpandedStackedSegmentPerRow.SelectionChanged += new SelectionChangedEventHandler(rssExpandedStackedSegmentPerRow_SelectionChanged);

            // Postal
            postalADMiltiplierNumericUpDown.ValueChanged += new EventHandler<EventArgs>(postalADMiltiplierNumericUpDown_ValueChanged);
            AddEnumValues(australianPostCustomInfoComboBox, typeof(AustralianPostCustomerInfoFormat));
            australianPostCustomInfoComboBox.SelectionChanged += new SelectionChangedEventHandler(australianPostCustomInfoComboBox_SelectionChanged);


            // Aztec
            AddEnumValues(aztecSymbolComboBox, typeof(AztecSymbolType));
            aztecSymbolComboBox.SelectionChanged += new SelectionChangedEventHandler(aztecSymbolComboBox_SelectionChanged);
            for (int i = 0; i <= 32; i++)
                aztecLayersCountComboBox.Items.Add(i);
            aztecLayersCountComboBox.SelectionChanged += new SelectionChangedEventHandler(aztecLayersCountComboBox_SelectionChanged);

            aztecEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(aztecEncodingModeComboBox_SelectionChanged);
            aztecEncodingModeComboBox.Items.Add(AztecEncodingMode.Undefined);
            aztecEncodingModeComboBox.Items.Add(AztecEncodingMode.Text);
            aztecEncodingModeComboBox.Items.Add(AztecEncodingMode.Byte);

            aztecErrorCorrectionNumericUpDown.ValueChanged += new EventHandler<EventArgs>(aztecErrorCorrectionNumericUpDown_ValueChanged);

            // DataMatrix
            AddEnumValues(datamatrixEncodingModeComboBox, typeof(DataMatrixEncodingMode));
            datamatrixEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(datamatrixEncodingModeComboBox_SelectionChanged);

            AddEnumValues(datamatrixSymbolSizeComboBox, typeof(DataMatrixSymbolType));
            datamatrixSymbolSizeComboBox.SelectionChanged += new SelectionChangedEventHandler(datamatrixSymbolSizeComboBox_SelectionChanged);

            // QR Code
            AddEnumValues(qrEncodingModeComboBox, typeof(QREncodingMode));
            qrEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(qrEncodingModeComboBox_SelectionChanged);

            qrSymbolSizeComboBox.Items.Add(QRSymbolVersion.Undefined);
            for (int i = 1; i <= 40; i++)
                qrSymbolSizeComboBox.Items.Add((QRSymbolVersion)i);
            for (int i = 101; i <= 114; i++)
                qrSymbolSizeComboBox.Items.Add((QRSymbolVersion)i);
            qrSymbolSizeComboBox.SelectionChanged += new SelectionChangedEventHandler(qrSymbolSizeComboBox_SelectionChanged);
            qrDataMaskPatternComboBox.Items.Add("Auto");
            for (int i = 0; i < 8; i++)
                qrDataMaskPatternComboBox.Items.Add(i);
            qrDataMaskPatternComboBox.SelectedIndex = 0;
            qrDataMaskPatternComboBox.SelectionChanged += new SelectionChangedEventHandler(qrDataMaskPatternComboBox_SelectionChanged);

            AddEnumValues(qrECCLevelComboBox, typeof(QRErrorCorrectionLevel));
            qrECCLevelComboBox.SelectionChanged += new SelectionChangedEventHandler(qrECCLevelComboBox_SelectionChanged);

            // Han Xin Code
            AddEnumValues(hanXinCodeEncodingModeComboBox, typeof(HanXinCodeEncodingMode));
            hanXinCodeEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(hanXinCodeEncodingModeComboBox_SelectionChanged);

            hanXinCodeSymbolVersionComboBox.Items.Add(HanXinCodeSymbolVersion.Undefined);
            for (int i = 1; i <= 84; i++)
                hanXinCodeSymbolVersionComboBox.Items.Add((HanXinCodeSymbolVersion)i);
            hanXinCodeSymbolVersionComboBox.SelectionChanged += new SelectionChangedEventHandler(hanXinCodeSymbolSizeComboBox_SelectionChanged);

            AddEnumValues(hanXinCodeECCLevelComboBox, typeof(HanXinCodeErrorCorrectionLevel));
            hanXinCodeECCLevelComboBox.SelectionChanged += new SelectionChangedEventHandler(hanXinCodeECCLevelComboBox_SelectionChanged);


            // Micro QR Code
            AddEnumValues(microQrEncodingModeComboBox, typeof(QREncodingMode));
            microQrEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(microQrEncodingModeComboBox_SelectionChanged);

            microQrSymbolSizeComboBox.Items.Add(QRSymbolVersion.Undefined);
            microQrSymbolSizeComboBox.Items.Add(QRSymbolVersion.VersionM1);
            microQrSymbolSizeComboBox.Items.Add(QRSymbolVersion.VersionM2);
            microQrSymbolSizeComboBox.Items.Add(QRSymbolVersion.VersionM3);
            microQrSymbolSizeComboBox.Items.Add(QRSymbolVersion.VersionM4);
            microQrSymbolSizeComboBox.SelectionChanged += new SelectionChangedEventHandler(microQrSymbolSizeComboBox_SelectionChanged);

            microQrECCLevelComboBox.Items.Add(QRErrorCorrectionLevel.L);
            microQrECCLevelComboBox.Items.Add(QRErrorCorrectionLevel.M);
            microQrECCLevelComboBox.Items.Add(QRErrorCorrectionLevel.Q);
            microQrECCLevelComboBox.SelectionChanged += new SelectionChangedEventHandler(microQrECCLevelComboBox_SelectionChanged);

            microQrDataMaskPatternComboBox.Items.Add("Auto");
            for (int i = 0; i < 4; i++)
                microQrDataMaskPatternComboBox.Items.Add(i);
            microQrDataMaskPatternComboBox.SelectedIndex = 0;
            microQrDataMaskPatternComboBox.SelectionChanged += new SelectionChangedEventHandler(microQrDataMaskPatternComboBox_SelectionChanged);

            // MaxiCode
            maxiCodeResolutonNumericUpDown.ValueChanged += new EventHandler<EventArgs>(maxiCodeResolutonNumericUpDown_ValueChanged);

            maxiCodeEncodingModeComboBox.Items.Add(MaxiCodeEncodingMode.Mode2);
            maxiCodeEncodingModeComboBox.Items.Add(MaxiCodeEncodingMode.Mode3);
            maxiCodeEncodingModeComboBox.Items.Add(MaxiCodeEncodingMode.Mode4);
            maxiCodeEncodingModeComboBox.Items.Add(MaxiCodeEncodingMode.Mode5);
            maxiCodeEncodingModeComboBox.Items.Add(MaxiCodeEncodingMode.Mode6);
            maxiCodeEncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(maxiCodeEncodingModeComboBox_SelectionChanged);

            // PDF417
            AddEnumValues(pdf417EncodingModeComboBox, typeof(PDF417EncodingMode));
            pdf417EncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(pdf417EncodingModeComboBox_SelectionChanged);

            AddEnumValues(pdf417ErrorCorrectionComboBox, typeof(PDF417ErrorCorrectionLevel));
            pdf417ErrorCorrectionComboBox.SelectionChanged += new SelectionChangedEventHandler(pdf417ErrorCorrectionComboBox_SelectionChanged);

            pdf417RowsNumericUpDown.ValueChanged += new EventHandler<EventArgs>(pdf417RowsNumericUpDown_ValueChanged);

            pdf417ColsNumericUpDown.ValueChanged += new EventHandler<EventArgs>(pdf417ColsNumericUpDown_ValueChanged);

            pdf417RowHeightNumericUpDown.ValueChanged += new EventHandler<EventArgs>(pdf417RowHeightNumericUpDown_ValueChanged);

            pdf417CompactCheckBox.Checked += new RoutedEventHandler(pdf417CompactCheckBox_Checked);
            pdf417CompactCheckBox.Unchecked += new RoutedEventHandler(pdf417CompactCheckBox_Checked);

            // Micro PDF417
            AddEnumValues(microPdf417EncodingModeComboBox, typeof(PDF417EncodingMode));
            microPdf417EncodingModeComboBox.SelectionChanged += new SelectionChangedEventHandler(microPdf417EncodingModeComboBox_SelectionChanged);

            AddEnumValues(microPdf417SymbolSizeComboBox, typeof(MicroPDF417SymbolType));
            microPdf417SymbolSizeComboBox.SelectionChanged += new SelectionChangedEventHandler(microPdf417SymbolSizeComboBox_SelectionChanged);

            microPdf417ColumnsNumericUpDown.ValueChanged += new EventHandler<EventArgs>(microPdf417ColumnsNumericUpDown_ValueChanged);
            microPdf417RowHeightNumericUpDown.ValueChanged += new EventHandler<EventArgs>(microPdf417RowHeightNumericUpDown_ValueChanged);

            // PPN
            _ppnBarcodeValue.PharmacyProductNumber = "110375286414";
            _ppnBarcodeValue.BatchNumber = "12345ABCD";
            _ppnBarcodeValue.ExpiryDate = "150617";
            _ppnBarcodeValue.SerialNumber = "12345ABCDEF98765";

            // Code16K
            code16KRowsComboBox.Items.Add(0);
            for (int i = 2; i <= 16; i++)
                code16KRowsComboBox.Items.Add(i);
            code16KRowsComboBox.SelectionChanged += new SelectionChangedEventHandler(code16KRowsComboBox_SelectionChanged);

            code16KModeComboBox.Items.Add(Code128EncodingMode.Undefined);
            code16KModeComboBox.Items.Add(Code128EncodingMode.ModeA);
            code16KModeComboBox.Items.Add(Code128EncodingMode.ModeB);
            code16KModeComboBox.Items.Add(Code128EncodingMode.ModeC);
            code16KModeComboBox.Items.Add(Code128EncodingMode.Code16K_Mode3);
            code16KModeComboBox.Items.Add(Code128EncodingMode.Code16K_Mode4);
            code16KModeComboBox.Items.Add(Code128EncodingMode.Code16K_Mode5);
            code16KModeComboBox.Items.Add(Code128EncodingMode.Code16K_Mode6);
            code16KModeComboBox.SelectionChanged += new SelectionChangedEventHandler(code16KModeComboBox_SelectionChanged);

            // ECI
            EciCharacterEncoding[] eciCharacterEncodings = EciCharacterEncoding.GetEciCharacterEncodings();
            foreach (EciCharacterEncoding characterEncoding in eciCharacterEncodings)
            {
                encodingInfoComboBox.Items.Add(characterEncoding);
                if (characterEncoding.EciAssignmentNumber == 3)
                    encodingInfoComboBox.SelectedItem = characterEncoding;
            }
            encodingInfoComboBox.SelectionChanged += new SelectionChangedEventHandler(encodingInfoComboBox_SelectionChanged);



            encodingInfoCheckBox.Foreground = new SolidColorBrush(Color.FromRgb(220, 100, 0));
            encodingInfoCheckBox.Checked += new RoutedEventHandler(encodingInfoCheckBox_Checked);
            encodingInfoCheckBox.Unchecked += new RoutedEventHandler(encodingInfoCheckBox_Checked);

            useOptionalCheckSum.Checked += new RoutedEventHandler(useOptionalCheckSum_Checked);
            useOptionalCheckSum.Unchecked += new RoutedEventHandler(useOptionalCheckSum_Checked);

            enableTelepenNumericMode.Checked += new RoutedEventHandler(enableTelepenNumericMode_Checked);
            enableTelepenNumericMode.Unchecked += new RoutedEventHandler(enableTelepenNumericMode_Checked);

            barsRatioNumericUpDown.ValueChanged += new EventHandler<EventArgs>(barsRatioNumericUpDown_ValueChanged);

            linearBarcodeTypeComboBox.SelectedItem = BarcodeType.Code128;
            datamatrixSymbolSizeComboBox.SelectedItem = DataMatrixSymbolType.Undefined;
            twoDimensionalBarcodeComboBox.SelectedItem = BarcodeType.DataMatrix;
            qrEncodingModeComboBox.SelectedItem = QREncodingMode.Undefined;
            datamatrixEncodingModeComboBox.SelectedItem = DataMatrixEncodingMode.Undefined;
            aztecEncodingModeComboBox.SelectedIndex = 0;
            aztecSymbolComboBox.SelectedItem = AztecSymbolType.Undefined;
            australianPostCustomInfoComboBox.SelectedItem = AustralianPostCustomerInfoFormat.None;
            eanSubtypeComboBox.SelectedItem = EANSubtype.Undefined;
            msiChecksumComboBox.SelectedItem = MSIChecksumType.Mod10;
            code128ModeComboBox.SelectedItem = Code128EncodingMode.Undefined;
            code16KModeComboBox.SelectedItem = Code128EncodingMode.Undefined;
            microPdf417SymbolSizeComboBox.SelectedItem = MicroPDF417SymbolType.Undefined;
            microPdf417EncodingModeComboBox.SelectedItem = PDF417EncodingMode.Undefined;
            pdf417ErrorCorrectionComboBox.SelectedItem = PDF417ErrorCorrectionLevel.Undefined;
            pdf417EncodingModeComboBox.SelectedItem = PDF417EncodingMode.Undefined;
            maxiCodeEncodingModeComboBox.SelectedItem = MaxiCodeEncodingMode.Mode4;
            microQrECCLevelComboBox.SelectedItem = QRErrorCorrectionLevel.M;
            microQrSymbolSizeComboBox.SelectedItem = QRSymbolVersion.Undefined;
            microQrEncodingModeComboBox.SelectedItem = QREncodingMode.Undefined;
            qrECCLevelComboBox.SelectedItem = QRErrorCorrectionLevel.M;
            qrSymbolSizeComboBox.SelectedItem = QRSymbolVersion.Undefined;
            hanXinCodeEncodingModeComboBox.SelectedItem = HanXinCodeEncodingMode.Undefined;
            hanXinCodeECCLevelComboBox.SelectedItem = HanXinCodeErrorCorrectionLevel.L2;
            hanXinCodeSymbolVersionComboBox.SelectedItem = HanXinCodeSymbolVersion.Undefined;
            aztecLayersCountComboBox.SelectedIndex = 0;
            rssExpandedStackedSegmentPerRow.SelectedItem = 4;
            code16KRowsComboBox.SelectedIndex = 0;

            _isInitialized = true;

            barcodeWriter.EndInit();

            UpdateUI();
        }

        #endregion



        #region Properties

        BarcodeSymbologySubset _selectedBarcodeSubset = null;
        /// <summary>
        /// Gets or sets a selected barcode subset.
        /// </summary>
        public BarcodeSymbologySubset SelectedBarcodeSubset
        {
            get
            {
                return _selectedBarcodeSubset;
            }
            set
            {
                _selectedBarcodeSubset = value;
                UpdateUI();
            }
        }

        #endregion



        #region Methods

        #region MSI

        void msiChecksumComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.MSIChecksum = (MSIChecksumType)msiChecksumComboBox.SelectedItem;
        }

        #endregion

        #region Code128

        void code128ModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.Code128EncodingMode = (Code128EncodingMode)code128ModeComboBox.SelectedItem;
        }

        #endregion

        #region EAN

        void eanSubtypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        #endregion

        #region Telepen

        void enableTelepenNumericMode_Checked(object sender, RoutedEventArgs e)
        {
            barcodeWriter.Settings.EnableTelepenNumericMode = enableTelepenNumericMode.IsChecked.Value;
        }

        #endregion

        #region RSS

        void rssLinkageFlag_Checked(object sender, RoutedEventArgs e)
        {
            barcodeWriter.Settings.RSSLinkageFlag = rssLinkageFlag.IsChecked.Value;
        }

        void rss14StackedOmni_Checked(object sender, RoutedEventArgs e)
        {
            barcodeWriter.Settings.RSS14StackedOmnidirectional = rss14StackedOmni.IsChecked.Value;
        }

        void rssExpandedStackedSegmentPerRow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.RSSExpandedStackedSegmentPerRow = (int)rssExpandedStackedSegmentPerRow.SelectedItem;
        }

        #endregion

        #region Postal

        void postalADMiltiplierNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.PostBarcodesADMultiplier = postalADMiltiplierNumericUpDown.Value / 10.0;
        }

        void australianPostCustomInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.AustralianPostCustomerInfoFormat = (AustralianPostCustomerInfoFormat)australianPostCustomInfoComboBox.SelectedItem;
        }

        #endregion

        #region Aztec

        void aztecSymbolComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.AztecSymbol = (AztecSymbolType)aztecSymbolComboBox.SelectedItem;
        }

        void aztecLayersCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.AztecDataLayers = aztecLayersCountComboBox.SelectedIndex;
        }

        void aztecEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.AztecEncodingMode = (AztecEncodingMode)aztecEncodingModeComboBox.SelectedItem;
        }

        void aztecErrorCorrectionNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.AztecErrorCorrectionDataPercent = aztecErrorCorrectionNumericUpDown.Value;
        }

        #endregion

        #region DataMatrix

        void datamatrixEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.DataMatrixEncodingMode = (DataMatrixEncodingMode)datamatrixEncodingModeComboBox.SelectedItem;
        }

        void datamatrixSymbolSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.DataMatrixSymbol = (DataMatrixSymbolType)datamatrixSymbolSizeComboBox.SelectedItem;
        }

        #endregion

        #region Han Xin Code

        void hanXinCodeEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.HanXinCodeEncodingMode = (HanXinCodeEncodingMode)hanXinCodeEncodingModeComboBox.SelectedItem;
        }

        void hanXinCodeSymbolSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.HanXinCodeSymbol = (HanXinCodeSymbolVersion)hanXinCodeSymbolVersionComboBox.SelectedItem;
        }

        void hanXinCodeECCLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.HanXinCodeErrorCorrectionLevel = (HanXinCodeErrorCorrectionLevel)hanXinCodeECCLevelComboBox.SelectedItem;
        }

        #endregion

        #region QR

        void qrEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QREncodingMode = (QREncodingMode)qrEncodingModeComboBox.SelectedItem;
        }

        void qrSymbolSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QRSymbol = (QRSymbolVersion)qrSymbolSizeComboBox.SelectedItem;
        }

        void qrECCLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QRErrorCorrectionLevel = (QRErrorCorrectionLevel)qrECCLevelComboBox.SelectedItem;
        }

        void qrDataMaskPatternComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (qrDataMaskPatternComboBox.SelectedIndex == 0)
                barcodeWriter.Settings.QRMaskPattern = -1;
            else
                barcodeWriter.Settings.QRMaskPattern = (int)qrDataMaskPatternComboBox.SelectedItem;
        }

        #endregion

        #region MicroQR

        void microQrEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QREncodingMode = (QREncodingMode)microQrEncodingModeComboBox.SelectedItem;
        }

        void microQrSymbolSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QRSymbol = (QRSymbolVersion)microQrSymbolSizeComboBox.SelectedItem;
        }

        void microQrECCLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.QRErrorCorrectionLevel = (QRErrorCorrectionLevel)microQrECCLevelComboBox.SelectedItem;
        }

        void microQrDataMaskPatternComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (microQrDataMaskPatternComboBox.SelectedItem != null)
            {
                if (microQrDataMaskPatternComboBox.SelectedIndex == 0)
                    barcodeWriter.Settings.QRMaskPattern = -1;
                else
                    barcodeWriter.Settings.QRMaskPattern = (int)microQrDataMaskPatternComboBox.SelectedItem;
            }
        }

        #endregion

        #region MaxiCode

        void maxiCodeResolutonNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.MaxiCodeResolution = maxiCodeResolutonNumericUpDown.Value;
        }

        void maxiCodeEncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.MaxiCodeEncodingMode = (MaxiCodeEncodingMode)maxiCodeEncodingModeComboBox.SelectedItem;
        }

        #endregion

        #region PDF417

        void pdf417EncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.PDF417EncodingMode = (PDF417EncodingMode)pdf417EncodingModeComboBox.SelectedItem;
        }

        void pdf417ErrorCorrectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.PDF417ErrorCorrectionLevel = (PDF417ErrorCorrectionLevel)pdf417ErrorCorrectionComboBox.SelectedItem;
        }

        void pdf417RowsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.PDF417Rows = (int)pdf417RowsNumericUpDown.Value;
        }

        void pdf417ColsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.PDF417Columns = (int)pdf417ColsNumericUpDown.Value;
        }

        void pdf417RowHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.PDF417RowHeight = (int)pdf417RowHeightNumericUpDown.Value;
        }

        void pdf417CompactCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (pdf417CompactCheckBox.IsChecked == true)
                barcodeWriter.Settings.Barcode = BarcodeType.PDF417Compact;
            else
                barcodeWriter.Settings.Barcode = BarcodeType.PDF417;
        }

        #endregion

        #region Micro PDF417

        void microPdf417EncodingModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.MicroPDF417EncodingMode = (PDF417EncodingMode)microPdf417EncodingModeComboBox.SelectedItem;
        }

        void microPdf417SymbolSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.MicroPDF417Symbol = (MicroPDF417SymbolType)microPdf417SymbolSizeComboBox.SelectedItem;
        }

        void microPdf417ColumnsNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.MicroPDF417Columns = (int)microPdf417ColumnsNumericUpDown.Value;
        }

        void microPdf417RowHeightNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.MicroPDF417RowHeight = (int)microPdf417RowHeightNumericUpDown.Value;
        }

        #endregion

        #region Code 16K

        private void code16KRowsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.Code16KRows = (int)code16KRowsComboBox.SelectedItem;
        }

        private void code16KModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.Code16KEncodingMode = (Code128EncodingMode)code16KModeComboBox.SelectedItem;
        }

        #endregion

        #region Common

        private void barcodeDesign_Click(object sender, RoutedEventArgs e)
        {
            BarcodeStructureBase barcodeStructure = null;
            try
            {
                barcodeStructure = barcodeWriter.Writer.GetBarcodeStructure();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BarcodeDesignerWindow window = new BarcodeDesignerWindow();
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.Owner = this;
            window.Barcode = barcodeStructure;
            if (barcodeWriter.BarcodeRenderer != null)
                window.BarcodeRenderer = barcodeWriter.BarcodeRenderer;
            if (window.ShowDialog() == true)
            {
                barcodeWriter.BarcodeRenderer = window.BarcodeRenderer;
            }
        }

        private void resetDesign_Click(object sender, RoutedEventArgs e)
        {
            barcodeWriter.BarcodeRenderer = null;
        }

        void barcodeWriter_BarcodeImageChanged(object sender, BarcodeImageChangedEventArgs e)
        {
            imageSizeLabel.Content = string.Format("{0}x{1} px; {2} DPI", e.Image.PixelWidth, e.Image.PixelHeight, e.Image.DpiX);
        }

        void valueFontSizeNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.ValueFontEmSize = valueFontSizeNumericUpDown.Value;
        }

        void fontFamilySelector_SelectedFamilyChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.ValueFontTypeface = new Typeface(fontFamilySelector.SelectedFamily.Source);
        }

        void barsRatioNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.BarsRatio = (double)barsRatioNumericUpDown.Value / 10.0;
        }

        void useOptionalCheckSum_Checked(object sender, RoutedEventArgs e)
        {
            barcodeWriter.Settings.OptionalCheckSum = useOptionalCheckSum.IsChecked.Value;
        }

        void valueGapNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.ValueGap = (int)valueGapNumericUpDown.Value;
        }

        void backgroundColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.BackColor = backgroundColorPicker.SelectedColor;
        }

        void foregroundColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.ForeColor = foregroundColorPicker.SelectedColor;
        }

        void valueVisibleCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool enabled = valueVisibleCheckBox.IsChecked.Value;
            valueAutoLetterSpacingCheckBox.IsEnabled = enabled;
            valueGapNumericUpDown.IsEnabled = enabled;
            valueFontSizeNumericUpDown.IsEnabled = enabled;
            fontFamilySelector.IsEnabled = enabled;
            if (barcodeGroupsTabPages.SelectedItem == linearBarcodesTabItem)
                barcodeWriter.Settings.ValueVisible = valueVisibleCheckBox.IsChecked.Value;
            else
                barcodeWriter.Settings.Value2DVisible = valueVisibleCheckBox.IsChecked.Value;
        }

        void valueAutoLetterSpacingCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            barcodeWriter.Settings.ValueAutoLetterSpacing = valueAutoLetterSpacingCheckBox.IsChecked.Value;
        }

        void linearBarcodeHeight_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.Height = (int)linearBarcodeHeight.Value;
        }

        void encodingInfoCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            encodingInfoComboBox.IsEnabled = encodingInfoCheckBox.IsChecked.Value;
            EncodeValue();
        }

        void encodingInfoComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EncodeValue();
        }

        private void EncodeValue()
        {
            if (barcodeWriter.Settings == null)
                return;
            string oldValue = barcodeWriter.Settings.Value;
            try
            {
                barcodeWriter.BarcodeSymbologySubset = SelectedBarcodeSubset;
                if (SelectedBarcodeSubset is GS1BarcodeSymbologySubset)
                {
                    //  encode GS1 barcode value
                    barcodeWriter.Settings.ValueItems = new ValueItemBase[] { new GS1ValueItem(_GS1ApplicationIdentifierValues) };
                }
                else if (SelectedBarcodeSubset is MailmarkCmdmBarcodeSymbology)
                {
                    //  encode Mailmark barcode value
                    barcodeWriter.Settings.ValueItems = new ValueItemBase[] { _mailmarkCmdmValueItem };
                }
                else if (SelectedBarcodeSubset is PpnBarcodeSymbology)
                {
                    //  encode PPN barcode value
                    barcodeWriter.Settings.ValueItems = new ValueItemBase[] { _ppnBarcodeValue };
                }
                else if (SelectedBarcodeSubset != null)
                {
                    barcodeWriter.Settings.Value = barcodeValueTextBox.Text;
                }
                else
                {
                    if (encodingInfoCheckBox.IsChecked.Value && encodingInfoCheckBox.IsEnabled)
                    {
                        EciCharacterEncoder encoder = new EciCharacterEncoder(barcodeWriter.Settings.Barcode);
                        encoder.AppendText((EciCharacterEncoding)encodingInfoComboBox.SelectedItem, barcodeValueTextBox.Text);
                        barcodeWriter.Settings.ValueItems = encoder.ToValueItems();
                        barcodeWriter.Settings.PrintableValue = barcodeValueTextBox.Text;
                    }
                    else
                    {
                        barcodeWriter.Settings.PrintableValue = "";
                        barcodeWriter.Settings.Value = barcodeValueTextBox.Text;
                    }
                }
            }
            catch (WriterSettingsException exc)
            {
                ShowErrorMessage(exc);
            }
        }

        void linearBarcodeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (barcodeWriter.Settings == null)
                return;
            BarcodeType oldBarcodeType = barcodeWriter.Settings.Barcode;
            try
            {
                if (linearBarcodeTypeComboBox.SelectedItem is BarcodeSymbologySubset)
                {
                    SelectedBarcodeSubset = (BarcodeSymbologySubset)linearBarcodeTypeComboBox.SelectedItem;
                    EncodeValue();
                }
                else
                {
                    barcodeWriter.Settings.Barcode = (BarcodeType)linearBarcodeTypeComboBox.SelectedItem;
                }
            }
            catch (WriterSettingsException exc)
            {
                ShowErrorMessage(exc);
                barcodeWriter.Settings.Barcode = oldBarcodeType;
                linearBarcodeTypeComboBox.SelectedItem = oldBarcodeType;
            }

            australianPostCustomInfoDockPanel.Visibility = Visibility.Collapsed;
            msiChecksumDockPanel.Visibility = Visibility.Collapsed;
            code128ModeDockPanel.Visibility = Visibility.Collapsed;
            postalADMiltiplierDockPanel.Visibility = Visibility.Collapsed;
            eanSubtypeDockPanel.Visibility = Visibility.Collapsed;
            barsRatioDockPanel.Visibility = Visibility.Collapsed;
            useCode39ExtendedTableDockPanel.Visibility = Visibility.Collapsed;
            useOptionalCheckSumDockPanel.Visibility = Visibility.Collapsed;
            enableTelepenNumericModeDockPanel.Visibility = Visibility.Collapsed;
            rssLinkageFlagDockPanel.Visibility = Visibility.Collapsed;
            rss14StackedOmniDockPanel.Visibility = Visibility.Collapsed;
            rssExpandedStackedSegmentPerRowDockPanel.Visibility = Visibility.Collapsed;
            code16KRowsDockPanel.Visibility = Visibility.Collapsed;
            code16KModeDockPanel.Visibility = Visibility.Collapsed;
            switch (barcodeWriter.Settings.Barcode)
            {
                case BarcodeType.Code16K:
                    code16KRowsDockPanel.Visibility = Visibility.Visible;
                    code16KModeDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.MSI:
                    barsRatioDockPanel.Visibility = Visibility.Visible;
                    msiChecksumDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Code128:
                    code128ModeDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.AustralianPost:
                    postalADMiltiplierDockPanel.Visibility = Visibility.Visible;
                    australianPostCustomInfoDockPanel.Visibility = Visibility.Visible;
                    break;
                case BarcodeType.RoyalMail:
                case BarcodeType.DutchKIX:
                case BarcodeType.IntelligentMail:
                case BarcodeType.Postnet:
                case BarcodeType.Planet:
                    postalADMiltiplierDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.EAN13:
                case BarcodeType.EAN13Plus2:
                case BarcodeType.EAN13Plus5:
                    eanSubtypeDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Code11:
                case BarcodeType.Codabar:
                    barsRatioDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Interleaved2of5:
                    useOptionalCheckSumDockPanel.Visibility = Visibility.Visible;
                    barsRatioDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Standard2of5:
                    useOptionalCheckSumDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Code39:
                    barsRatioDockPanel.Visibility = Visibility.Visible;
                    useCode39ExtendedTableDockPanel.Visibility = Visibility.Visible;
                    useOptionalCheckSumDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.Telepen:
                    enableTelepenNumericModeDockPanel.Visibility = Visibility.Visible;
                    barsRatioDockPanel.Visibility = Visibility.Visible;
                    break;

                case BarcodeType.RSS14:
                case BarcodeType.RSS14Stacked:
                case BarcodeType.RSSExpanded:
                case BarcodeType.RSSExpandedStacked:
                case BarcodeType.RSSLimited:
                    rssLinkageFlagDockPanel.Visibility = Visibility.Visible;
                    if (barcodeWriter.Settings.Barcode == BarcodeType.RSS14Stacked)
                        rss14StackedOmniDockPanel.Visibility = Visibility.Visible;
                    else if (barcodeWriter.Settings.Barcode == BarcodeType.RSSExpandedStacked)
                        rssExpandedStackedSegmentPerRowDockPanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        void twoDimensionalBarcodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pdf417SettingsGrid.Visibility = Visibility.Collapsed;
            aztecSettingsGrid.Visibility = Visibility.Collapsed;
            maxiCodeSettingsGrid.Visibility = Visibility.Collapsed;
            dataMatrixSettingsGrid.Visibility = Visibility.Collapsed;
            microQrSettingsGrid.Visibility = Visibility.Collapsed;
            hanXinCodeSettingsGrid.Visibility = Visibility.Collapsed;
            qrSettingsGrid.Visibility = Visibility.Collapsed;
            microPDF417SettingsGrid.Visibility = Visibility.Collapsed;

            BarcodeSymbologySubset barcodeSubset = twoDimensionalBarcodeComboBox.SelectedItem as BarcodeSymbologySubset;
            BarcodeType baseBarcodeType;
            if (barcodeSubset != null)
                baseBarcodeType = barcodeSubset.BaseType;
            else
                baseBarcodeType = (BarcodeType)twoDimensionalBarcodeComboBox.SelectedItem;

            SelectedBarcodeSubset = barcodeSubset;
            try
            {
                if (_isInitialized)
                {
                    if (barcodeWriter.Settings != null)
                    {
                        if (SelectedBarcodeSubset == null)
                        {
                            barcodeWriter.Settings.BeginInit();
                            barcodeWriter.Settings.Barcode = baseBarcodeType;
                        }
                    }

                    UpdateUI();
                }

                // select settings panel
                switch (baseBarcodeType)
                {
                    case BarcodeType.Aztec:
                        aztecSettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.HanXinCode:
                        hanXinCodeSettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.QR:
                        qrSettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.MicroQR:
                        microQrSettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.PDF417:
                        pdf417SettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.MicroPDF417:
                        microPDF417SettingsGrid.Visibility = Visibility.Visible;
                        break;
                    case BarcodeType.DataMatrix:
                        if (!(SelectedBarcodeSubset is MailmarkCmdmBarcodeSymbology))
                        {
                            dataMatrixSettingsGrid.Visibility = Visibility.Visible;
                            datamatrixSymbolSizeComboBox_SelectionChanged(this, null);
                        }
                        break;
                    case BarcodeType.MaxiCode:
                        maxiCodeSettingsGrid.Visibility = Visibility.Visible;
                        break;
                }

                if (_isInitialized)
                    EncodeValue();

                e.Handled = true;
            }
            finally
            {
                if (_isInitialized && barcodeWriter.Settings != null)
                    if (SelectedBarcodeSubset == null)
                        barcodeWriter.Settings.EndInit();
            }
        }

        void widthAdjustNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.BarsWidthAdjustment = widthAdjustNumericUpDown.Value * 0.1;
        }

        void paddingNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.Padding = (int)paddingNumericUpDown.Value;
        }

        void minWidthNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            barcodeWriter.Settings.MinWidth = (int)minWidthNumericUpDown.Value;
        }

        void pixelFormatComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            barcodeWriter.Settings.PixelFormat = (BarcodeImagePixelFormat)pixelFormatComboBox.SelectedItem;
        }

        void barcodeValueTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            barcodeWriter.Settings.Value = barcodeValueTextBox.Text;
        }

        void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!barcodeWriter.WrongWriterSettings)
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.DefaultExt = ".png";
                saveFileDialog.Filter = "PNG Files|*.png";
                if (saveFileDialog.ShowDialog().Value)
                {

                    BitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(barcodeWriter.GetBarcodeAsBitmapSource()));
                    using (Stream stream = new FileStream(
                        saveFileDialog.FileName,
                        FileMode.Create,
                        FileAccess.Write,
                        FileShare.Write))
                    {
                        encoder.Save(stream);
                    }
                }
            }
        }

        private void saveAsSvgMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (!barcodeWriter.WrongWriterSettings)
            {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.DefaultExt = ".svg";
                saveFileDialog.Filter = "SVG Files|*.svg";
                if (saveFileDialog.ShowDialog().Value)
                {
                    if (BarcodeGlobalSettings.IsDemoVersion)
                    {
                        MessageBox.Show("The evaluation version adds noise to the barcode image.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    string svgFile = barcodeWriter.Writer.GetBarcodeAsSvgFile();
                    File.WriteAllText(saveFileDialog.FileName, svgFile);
                    ProcessStartInfo processInfo = new ProcessStartInfo(saveFileDialog.FileName);
                    processInfo.UseShellExecute = true;
                    Process.Start(processInfo);
                }
            }
        }

        void barcodeGroupsTabPages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.OriginalSource != barcodeGroupsTabPages)
                return;

            barcodeWidthDockPanel.Visibility = Visibility.Visible;
            SelectedBarcodeSubset = null;
            BarcodeType oldValue = barcodeWriter.Settings.Barcode;
            try
            {

                barcodeWriter.Settings.BeginInit();
                if (barcodeGroupsTabPages.SelectedItem == linearBarcodesTabItem)
                {
                    if (linearBarcodeTypeComboBox.SelectedItem is BarcodeSymbologySubset)
                    {
                        SelectedBarcodeSubset = (BarcodeSymbologySubset)linearBarcodeTypeComboBox.SelectedItem;
                    }
                    else
                    {
                        barcodeWriter.Settings.Barcode = (BarcodeType)linearBarcodeTypeComboBox.SelectedItem;
                    }
                    valueVisibleCheckBox.IsChecked = barcodeWriter.Settings.ValueVisible;
                    barcodeValueTextBox.IsEnabled = true;
                }
                else
                {
                    BarcodeSymbologySubset barcodeSubset = twoDimensionalBarcodeComboBox.SelectedItem as BarcodeSymbologySubset;
                    SelectedBarcodeSubset = barcodeSubset;
                    BarcodeType baseBarcodeType;
                    if (barcodeSubset != null)
                        baseBarcodeType = barcodeSubset.BaseType;
                    else
                        baseBarcodeType = (BarcodeType)twoDimensionalBarcodeComboBox.SelectedItem;

                    if (baseBarcodeType == BarcodeType.PDF417)
                    {
                        if (pdf417CompactCheckBox.IsChecked.Value)
                            barcodeWriter.Settings.Barcode = BarcodeType.PDF417Compact;
                        else
                            barcodeWriter.Settings.Barcode = BarcodeType.PDF417;
                    }
                    else if (baseBarcodeType == BarcodeType.MicroPDF417)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.MicroPDF417;
                    }
                    else if (baseBarcodeType == BarcodeType.DataMatrix)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.DataMatrix;
                        datamatrixSymbolSizeComboBox_SelectionChanged(this, null);
                    }
                    else if (baseBarcodeType == BarcodeType.Aztec)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.Aztec;
                    }
                    else if (baseBarcodeType == BarcodeType.QR)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.QR;
                        barcodeWriter.Settings.QRSymbol = (QRSymbolVersion)qrSymbolSizeComboBox.SelectedItem;
                    }
                    else if (baseBarcodeType == BarcodeType.MicroQR)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.MicroQR;
                        barcodeWriter.Settings.QRSymbol = (QRSymbolVersion)microQrSymbolSizeComboBox.SelectedItem;
                    }
                    else if (baseBarcodeType == BarcodeType.MaxiCode)
                    {
                        barcodeWriter.Settings.Barcode = BarcodeType.MaxiCode;
                        barcodeWidthDockPanel.Visibility = Visibility.Collapsed;
                    }

                    valueVisibleCheckBox.IsChecked = barcodeWriter.Settings.Value2DVisible;
                }
                UpdateUI();
                EncodeValue();
                barcodeWriter.Settings.EndInit();
                e.Handled = true;
            }
            catch (WriterSettingsException exc)
            {
                ShowErrorMessage(exc);
                barcodeWriter.Settings.Barcode = oldValue;
                barcodeWriter.Settings.EndInit();
            }
        }

        private void UpdateUI()
        {
            if (barcodeWriter.Settings == null)
                return;
            bool canEncodeECI = false;
            if (SelectedBarcodeSubset == null)
                switch (barcodeWriter.Settings.Barcode)
                {
                    case BarcodeType.Aztec:
                    case BarcodeType.DataMatrix:
                    case BarcodeType.QR:
                    case BarcodeType.PDF417:
                    case BarcodeType.PDF417Compact:
                    case BarcodeType.MicroPDF417:
                    case BarcodeType.HanXinCode:
                        canEncodeECI = true;
                        break;
                }
            encodingInfoCheckBox.IsEnabled = canEncodeECI;
            encodingInfoComboBox.IsEnabled = canEncodeECI && encodingInfoCheckBox.IsChecked.Value;
            bool useCustomValueDialog = false;
            if (SelectedBarcodeSubset != null &&
                SelectedBarcodeSubset is GS1BarcodeSymbologySubset ||
                SelectedBarcodeSubset is MailmarkCmdmBarcodeSymbology ||
                SelectedBarcodeSubset is PpnBarcodeSymbology)
                useCustomValueDialog = true;
            if (useCustomValueDialog)
            {
                subsetBarcodeValueDockPanel.Visibility = Visibility.Visible;
                barcodeValueDockPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                subsetBarcodeValueDockPanel.Visibility = Visibility.Collapsed;
                barcodeValueDockPanel.Visibility = Visibility.Visible;
            }
        }

        private void subsetBarcodeValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBarcodeSubset is GS1BarcodeSymbologySubset)
            {
                GS1ValueEditorWindow gs1Editor = new GS1ValueEditorWindow(_GS1ApplicationIdentifierValues, false);

                if (gs1Editor.ShowDialog().Value)
                {
                    _GS1ApplicationIdentifierValues = gs1Editor.GS1ApplicationIdentifierValues;
                    EncodeValue();
                }
            }
            else if (SelectedBarcodeSubset is MailmarkCmdmBarcodeSymbology)
            {
                PropertyGridWindow form = new PropertyGridWindow(_mailmarkCmdmValueItem, "Mailmark CMDM value", false);
                form.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
                if (form.ShowDialog().Value)
                {
                    EncodeValue();
                }
            }
            else if (SelectedBarcodeSubset is PpnBarcodeSymbology)
            {
                PropertyGridWindow form = new PropertyGridWindow(_ppnBarcodeValue, "PPN value", false);
                form.PropertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
                if (form.ShowDialog().Value)
                {
                    EncodeValue();
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private void setImageSizeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GetSizeWindow form = new GetSizeWindow();
            form.WidthValue = barcodeWriter.BarcodeImageWidth;
            form.HeightValue = barcodeWriter.BarcodeImageHeight;
            form.UnitsValue = barcodeWriter.BarcodeImageSizeUnits;
            form.ResolutionValue = barcodeWriter.Settings.Resolution;

            if (form.ShowDialog().Value == true)
            {
                if (BarcodeGlobalSettings.IsDemoVersion)
                {
                    MessageBox.Show("The evaluation version adds noise to the barcode image.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                barcodeWriter.BeginInit();
                barcodeWriter.BarcodeImageWidth = form.WidthValue;
                barcodeWriter.BarcodeImageHeight = form.HeightValue;
                barcodeWriter.BarcodeImageSizeUnits = form.UnitsValue;
                barcodeWriter.Settings.Resolution = form.ResolutionValue;
                barcodeWriter.EndInit();
            }
        }

        #endregion

        #endregion



        #region Tools

        /// <summary>
        /// Add a enum values to ComboBox items.
        /// </summary>
        private static void AddEnumValues(ComboBox comboBox, Type type)
        {
            Array values = Enum.GetValues(type);
            for (int i = 0; i < values.Length; i++)
                comboBox.Items.Add(values.GetValue(i));
        }

        private void ShowErrorMessage(WriterSettingsException exception)
        {
            MessageBox.Show(exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion


    }
}
