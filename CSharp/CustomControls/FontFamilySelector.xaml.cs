using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControls
{
    /// <summary>
    /// Interaction logic for FontFamilySelector.xaml
    /// </summary>
    public partial class FontFamilySelector : UserControl
    {
        
        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FontFamilySelector"/> class.
        /// </summary>
        public FontFamilySelector()
        {
            InitializeComponent();
            foreach (FontFamily family in Fonts.SystemFontFamilies)
                fontFamilyComboBox.Items.Add(family);
            fontFamilyComboBox.SelectionChanged += new SelectionChangedEventHandler(fontFamilyComboBox_SelectionChanged);
        }

        #endregion



        #region Properties

        /// <summary>
        /// Gets or sets the selected <see cref="FontFamily"/>.
        /// </summary>
        public FontFamily SelectedFamily
        {
            get
            {
                return (FontFamily)fontFamilyComboBox.SelectedItem;
            }
            set
            {
                if (fontFamilyComboBox.SelectedItem != value)
                {
                    fontFamilyComboBox.SelectedItem = value;
                    OnSelectedFamilyChanged();
                }
            }
        }

        #endregion



        #region Methods

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectedFamilyChanged();
        }

        /// <summary>
        /// Raises the <see cref="FontFamilySelector.OnSelectedFamilyChanged"/> event.
        /// </summary>
        protected virtual void OnSelectedFamilyChanged()
        {
            if (SelectedFamilyChanged != null)
                SelectedFamilyChanged(this, EventArgs.Empty);
        }

        #endregion



        #region Events

        /// <summary>
        /// Occurs when the SelectedFamily property changes.
        /// </summary>
        public event EventHandler SelectedFamilyChanged;

        #endregion

    }
}
