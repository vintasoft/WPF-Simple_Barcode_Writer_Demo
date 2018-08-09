using System;
using System.Globalization;
using System.Windows;
using Vintasoft.WpfBarcode;

namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// GetSizeWindow logic.
    /// </summary>
    public partial class GetSizeWindow : Window
    {
        public GetSizeWindow()
        {
            InitializeComponent();

            unitsValueEditor.Items.Add(UnitOfMeasure.Inches);
            unitsValueEditor.Items.Add(UnitOfMeasure.Centimeters);
            unitsValueEditor.Items.Add(UnitOfMeasure.Millimeters);
            unitsValueEditor.Items.Add(UnitOfMeasure.Pixels);
            unitsValueEditor.SelectedItem = UnitOfMeasure.Pixels;
        }

        /// <summary>
        /// Gets or sets an image width, in units.
        /// </summary>
        public double WidthValue
        {
            get
            {
                string value = widthValueEditor.Text.Replace(',', '.');
                try
                {
                    return double.Parse(value, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            set
            {
                widthValueEditor.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets or sets an image height, in units.
        /// </summary>
        public double HeightValue
        {
            get
            {
                string value = heightValueEditor.Text.Replace(',', '.');
                try
                {
                    return double.Parse(value, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
            set
            {
                heightValueEditor.Text = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets or sets the unit of measure of width and height.
        /// </summary>
        public UnitOfMeasure UnitsValue
        {
            get
            {
                return (UnitOfMeasure)unitsValueEditor.SelectedItem;
            }
            set
            {
                unitsValueEditor.SelectedItem = value;
            }
        }

        /// <summary>
        /// Gets or sets an image resolution.
        /// </summary>
        public double ResolutionValue
        {
            get
            {
                return (double)resolutionValueEditor.Value;
            }
            set
            {
                resolutionValueEditor.Value = (int)Math.Round(value);
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
