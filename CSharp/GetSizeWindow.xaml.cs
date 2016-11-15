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
        public GetSizeWindow(string name, double value, int resolution, UnitOfMeasure units)
        {
            InitializeComponent();

            Title = string.Format(Title, name);
            sizeLabel.Content = string.Format((string)sizeLabel.Content, name);

            valueTextBox.Text = value.ToString();

            unitsComboBox.Items.Add(UnitOfMeasure.Inches);
            unitsComboBox.Items.Add(UnitOfMeasure.Centimeters);
            unitsComboBox.Items.Add(UnitOfMeasure.Millimeters);
            unitsComboBox.Items.Add(UnitOfMeasure.Pixels);
            unitsComboBox.SelectedItem = units;

            resolutionNumericUpDown.Value = (int)resolution;
        }

        double _value;
        public double Value
        {
            get
            {
                return _value;
            }
        }

        UnitOfMeasure _units;
        public UnitOfMeasure Units
        {

            get
            {
                return _units;
            }
        }

        int _resolution;
        public int Resolution
        {
            get
            {
                return _resolution;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            _units = (UnitOfMeasure)unitsComboBox.SelectedItem;
            _resolution = resolutionNumericUpDown.Value;
            try
            {
                _value = double.Parse(valueTextBox.Text, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
        }
    }
}
