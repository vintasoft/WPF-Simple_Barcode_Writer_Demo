using System.Windows;
using System.Windows.Forms;

namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// Interaction logic for WpfPropertyGridWindow.xaml
    /// </summary>
    public partial class PropertyGridWindow : Window
    {

        #region Constructor

        public PropertyGridWindow(object obj, string formTitle)
            : this(obj, formTitle, false)
        {
        }

        public PropertyGridWindow(object obj, string formTitle, bool canCancel)
        {
            InitializeComponent();

            Title = formTitle;
            cancelButton.IsEnabled = canCancel;
            _propertyGrid.SelectedObject = obj;
        }

        #endregion



        #region Properties

        public PropertyGrid PropertyGrid
        {
            get
            {
                return _propertyGrid;
            }
        }

        #endregion



        #region Method

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

    }
}
