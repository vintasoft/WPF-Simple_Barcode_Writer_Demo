using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Vintasoft.WpfBarcode.GS1;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// Interaction logic for GS1ValueEditorWindow.xaml
    /// </summary>
    public partial class GS1ValueEditorWindow : Window
    {
        
        #region Nested class

        private class ListViewData
        {

            #region Constructor

            public ListViewData(GS1ApplicationIdentifierValue value)
            {
                _ai = value.ApplicationIdentifier.ApplicationIdentifier;
                _aiTitle = value.ApplicationIdentifier.DataTitle;
                _aiValue = value.Value;
            }

            #endregion



            #region Properties

            private string _ai;
            public string Ai
            {
                get
                {
                    return _ai;
                }
                set
                {
                    _ai = value;
                }
            }

            private string _aiTitle;
            public string AiTitle
            {
                get
                {
                    return _aiTitle;
                }
                set
                {
                    _aiTitle = value;
                }
            }

            private string _aiValue;
            public string AiValue
            {
                get
                {
                    return _aiValue;
                }
                set
                {
                    _aiValue = value;
                }
            }

            #endregion

        }

        #endregion



        #region Fields

        bool _readOnly = false;
        bool _existsAISelected = false;
        List<GS1ApplicationIdentifierValue> _identifierValuesList = new List<GS1ApplicationIdentifierValue>();

        #endregion



        #region Constructor

        public GS1ValueEditorWindow(GS1ApplicationIdentifierValue[] gs1ApplicationIdentifierValues, bool readOnly)
        {
            InitializeComponent();

            aiNumberComboBox.SelectionChanged += new SelectionChangedEventHandler(aiNumberComboBox_SelectionChanged);
            aiListView.SelectionChanged += new SelectionChangedEventHandler(aiListView_SelectionChanged);

            if (readOnly)
            {
                addButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
            aiNumberComboBox.IsEnabled = !readOnly;

            gs1BarcodePrintableValueTextBox.IsReadOnly = readOnly;
            setPrintableValueButton.IsEnabled = !readOnly;
            setDataValueButton.IsEnabled = !readOnly;

            GS1ApplicationIdentifier[] applicationIdentifiers = GS1ApplicationIdentifiers.ApplicationIdentifiers;
            for (int i = 0; i < applicationIdentifiers.Length; i++)
                aiNumberComboBox.Items.Add(string.Format("{0}: {1}", applicationIdentifiers[i].ApplicationIdentifier, applicationIdentifiers[i].DataTitle));
            _GS1ApplicationIdentifierValues = gs1ApplicationIdentifierValues;
            _identifierValuesList.AddRange(gs1ApplicationIdentifierValues);
            _readOnly = readOnly;
            aiValueTextBox.IsReadOnly = readOnly;

            if (readOnly)
                Title = "GS1 Value Decoder";
            else
                Title = "GS1 Value Editor";

            ShowPrintableValue();
            ShowAI();
        }

        #endregion



        #region Properties

        GS1ApplicationIdentifierValue[] _GS1ApplicationIdentifierValues;
        public GS1ApplicationIdentifierValue[] GS1ApplicationIdentifierValues
        {
            get
            {
                return _GS1ApplicationIdentifierValues;
            }
        }


        #endregion



        #region Methods

        void aiNumberComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_existsAISelected)
            {
                GS1ApplicationIdentifier ai = GS1ApplicationIdentifiers.ApplicationIdentifiers[aiNumberComboBox.SelectedIndex];
                aiDataContentLabel.Content = ai.DataContent;
                string format = ai.Format;
                if (ai.IsContainsDecimalPoint)
                    format += " (with decimal point)";
                aiDataFormatLabel.Content = format;
                aiValueTextBox.Text = "";
            }
        }

        void aiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (aiListView.SelectedIndex >= 0)
            {
                ListViewData listViewData = aiListView.SelectedItem as ListViewData;
                aiNumberComboBox.SelectedIndex = GS1ApplicationIdentifiers.IndexOfApplicationIdentifier(listViewData.Ai);
                aiValueTextBox.Text = listViewData.AiValue;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddAI(GS1ApplicationIdentifiers.ApplicationIdentifiers[aiNumberComboBox.SelectedIndex].ApplicationIdentifier, aiValueTextBox.Text);
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (aiListView.Items.Count > 0 && aiListView.SelectedIndex >= 0)
            {
                int index = aiListView.SelectedIndex;
                _identifierValuesList.RemoveAt(index);
                aiListView.Items.RemoveAt(index);
                ShowPrintableValue();
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (SetPrintableValue())
            {
                _GS1ApplicationIdentifierValues = _identifierValuesList.ToArray();
                DialogResult = true;
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void ShowAI()
        {
            if (_GS1ApplicationIdentifierValues.Length == 0)
            {
                aiNumberComboBox.SelectedIndex = 0;
            }
            else
            {
                for (int i = 0; i < _identifierValuesList.Count; i++)
                    AddAIToTable(_identifierValuesList[i]);
                aiListView.SelectedIndex = 0;
            }
        }

        private void AddAIToTable(GS1ApplicationIdentifierValue value)
        {
            aiListView.Items.Add(new ListViewData(value));
        }

        private void AddAI(string number, string value)
        {
            GS1ApplicationIdentifierValue ai = null;
            try
            {
                ai = new GS1ApplicationIdentifierValue(GS1ApplicationIdentifiers.FindApplicationIdentifier(number), value);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            _identifierValuesList.Add(ai);
            ShowPrintableValue();
            AddAIToTable(ai);
        }

        private void ShowPrintableValue()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _identifierValuesList.Count; i++)
                sb.Append(_identifierValuesList[i].ToString());
            gs1BarcodePrintableValueTextBox.Text = sb.ToString();
        }


        private void setDataValueButton_Click(object sender, RoutedEventArgs e)
        {
            if (aiListView.Items.Count > 0 && aiListView.SelectedIndex >= 0)
            {
                int index = aiListView.SelectedIndex;
                GS1ApplicationIdentifierValue ai = null;
                try
                {
                    string number = GS1ApplicationIdentifiers.ApplicationIdentifiers[aiNumberComboBox.SelectedIndex].ApplicationIdentifier;
                    ai = new GS1ApplicationIdentifierValue(GS1ApplicationIdentifiers.FindApplicationIdentifier(number), aiValueTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _identifierValuesList[index] = ai;
                aiListView.Items[index] = new ListViewData(ai);
            }
        }

        private bool SetPrintableValue()
        {
            try
            {
                GS1ApplicationIdentifierValue[] values = GS1Codec.ParsePrintableValue(gs1BarcodePrintableValueTextBox.Text);
                _identifierValuesList.Clear();
                _identifierValuesList.AddRange(values);
                aiListView.Items.Clear();
                ShowAI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void setPrintableValueButton_Click(object sender, RoutedEventArgs e)
        {
            SetPrintableValue();
        }

        #endregion

    }
}
