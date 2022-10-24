using System.Windows;
using System.Windows.Forms;

namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// A window that allows to show a grid with object properties.
    /// </summary>
    public partial class PropertyGridWindow : Window
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridWindow"/> class.
        /// </summary>
        /// <param name="obj">The object, which must be shown in property grid.</param>
        /// <param name="formTitle">The form title.</param>
        public PropertyGridWindow(object obj, string formTitle)
            : this(obj, formTitle, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGridWindow"/> class.
        /// </summary>
        /// <param name="obj">The object, which must be shown in property grid.</param>
        /// <param name="formTitle">The form title.</param>
        /// <param name="canCancel">Indicates that form must have "Cancel" button.</param>
        public PropertyGridWindow(object obj, string formTitle, bool canCancel)
        {
            InitializeComponent();

            Title = formTitle;
            cancelButton.IsEnabled = canCancel;
            _propertyGrid.SelectedObject = obj;
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets the property grid.
        /// </summary>
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
