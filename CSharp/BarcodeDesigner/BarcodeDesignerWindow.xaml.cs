using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Vintasoft.WpfBarcode;
using Vintasoft.WpfBarcode.BarcodeStructure;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// A window that allows to show the "design" barcode and set the "design" settings.
    /// </summary>
    public partial class BarcodeDesignerWindow : Window
    {

        #region Fields

        /// <summary>
        /// Dictionary: "barcode renderer name" => "barcode renderer".
        /// </summary>
        static Dictionary<string, BarcodeDrawingContextRenderer> _barcodeRenderers;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeDesignerWindow"/> class.
        /// </summary>
        public BarcodeDesignerWindow()
        {
            InitializeComponent();

            foreach (string barcodeRendererName in _barcodeRenderers.Keys)
                barcodeRendererComboBox.Items.Add(barcodeRendererName);
            barcodeRendererComboBox.SelectedIndex = 0;
            PreviewScale = 9;
        }

        /// <summary>
        /// Initializes the <see cref="BarcodeDesignerWindow"/> class.
        /// </summary>
        static BarcodeDesignerWindow()
        {
            _barcodeRenderers = new Dictionary<string, BarcodeDrawingContextRenderer>();

            ConnectedMatixBarcodeRenderer connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawMultiplyConnectedCells = false;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.6f;
            connectedMatixBarcodeRenderer.DotDiameter = 0.9f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Circuit board (square without connected dots)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawMultiplyConnectedCells = false;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.6f;
            connectedMatixBarcodeRenderer.DotDiameter = 1f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            connectedMatixBarcodeRenderer.Modulation = 1;
            _barcodeRenderers.Add("Circuit board (square without connected dots, black only)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
            connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Circuit board 1 (orthogonal beveled square)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
            connectedMatixBarcodeRenderer.DotDiameter = 0.8f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.Modulation = 1;
            _barcodeRenderers.Add("Circuit board 2 (orthogonal beveled square)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
            connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Circuit board (diagonal beveled square)", connectedMatixBarcodeRenderer);


            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Square;
            connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Circuit board (orthogonal square)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Square;
            connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.4f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Circuit board (diagonal square)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = true;
            _barcodeRenderers.Add("Circuit board (circle diagonal)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Circuit board (circle orthogonal)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Rhombus;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = true;
            _barcodeRenderers.Add("Circuit board (diagonal rhombus)", connectedMatixBarcodeRenderer);


            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 0.75f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 1;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            _barcodeRenderers.Add("Lines (orthogonal)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 0.6f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 1;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = false;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = true;
            _barcodeRenderers.Add("Lines (diagonal)", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
            connectedMatixBarcodeRenderer.DotDiameter = 0.8f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.3f;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.Modulation = 1;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Lines (orthogonal + diagonal beveled square)", connectedMatixBarcodeRenderer);


            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 0.8f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 1f;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = true;
            connectedMatixBarcodeRenderer.Modulation = 1;
            _barcodeRenderers.Add("Khaki 1", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 0.8f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 1f;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = true;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.Modulation = 1;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            connectedMatixBarcodeRenderer.AlignmentPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Khaki 2", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 1f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 1 / 3f;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Cross;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            connectedMatixBarcodeRenderer.AlignmentPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Grating 1", connectedMatixBarcodeRenderer);

            connectedMatixBarcodeRenderer = new ConnectedMatixBarcodeRenderer();
            connectedMatixBarcodeRenderer.DotDiameter = 1f;
            connectedMatixBarcodeRenderer.ConnectionLineWidthFactor = 0.5f;
            connectedMatixBarcodeRenderer.ConnectOrthogonalCells = true;
            connectedMatixBarcodeRenderer.ConnectDiagonalCells = false;
            connectedMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Rhombus;
            connectedMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            connectedMatixBarcodeRenderer.TimingPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            connectedMatixBarcodeRenderer.AlignmentPatternsColor = connectedMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Grating 2", connectedMatixBarcodeRenderer);

            DotMatixBarcodeRenderer dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Square;
            dotMatixBarcodeRenderer.DotDiameter = 0.8f;
            _barcodeRenderers.Add("Dot matrix (square)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.BeveledSquare;
            dotMatixBarcodeRenderer.DotDiameter = 0.9f;
            _barcodeRenderers.Add("Dot matrix (beveled square)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Circle;
            dotMatixBarcodeRenderer.DotDiameter = 0.8f;
            _barcodeRenderers.Add("Dot matrix (circle)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Rhombus;
            dotMatixBarcodeRenderer.DotDiameter = 1;
            _barcodeRenderers.Add("Dot matrix (rhombus)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Cross;
            dotMatixBarcodeRenderer.DotDiameter = 0.9f;
            _barcodeRenderers.Add("Dot matrix (cross 1)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.Cross;
            dotMatixBarcodeRenderer.DotDiameter = 1.25f;
            dotMatixBarcodeRenderer.Modulation = 1;
            _barcodeRenderers.Add("Dot matrix (cross 2)", dotMatixBarcodeRenderer);

            dotMatixBarcodeRenderer = new DotMatixBarcodeRenderer();
            dotMatixBarcodeRenderer.DotType = BarcodeMatixDotType.DiagonalCross;
            dotMatixBarcodeRenderer.DotDiameter = 0.4f;
            dotMatixBarcodeRenderer.Modulation = 1;
            dotMatixBarcodeRenderer.DrawAlignmentPatternAsSinglePattern = false;
            dotMatixBarcodeRenderer.TimingPatternsColor = dotMatixBarcodeRenderer.DataLayerColor;
            _barcodeRenderers.Add("Dot matrix (diagonal cross)", dotMatixBarcodeRenderer);

            _barcodeRenderers.Add("Barcode structure highlight", new BarcodeStructureHighlightRenderer());

            _barcodeRenderers.Add("Simple dots", new BarcodeSimpleRenderer());
        }

        #endregion



        #region Properties

        double _previewScale = 0;
        /// <summary>
        /// Gets or sets the preview scale of barcode.
        /// </summary>
        public double PreviewScale
        {
            get
            {
                return _previewScale;
            }
            set
            {
                scaleSlider.Value = value * 10;
                _previewScale = scaleSlider.Value / 10;

                SetPreviewParams();
            }
        }

        /// <summary>
        /// Gets or sets the barcode renderer.
        /// </summary>
        public BarcodeDrawingContextRenderer BarcodeRenderer
        {
            get
            {
                return barcodeStructureControl.BarcodeRenderer;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                foreach (string renderName in _barcodeRenderers.Keys)
                {
                    if (value == _barcodeRenderers[renderName])
                    {
                        barcodeRendererComboBox.SelectedItem = renderName;
                        break;
                    }
                }
            }
        }

        BarcodeStructureBase _barcode;
        /// <summary>
        /// Gets or sets the barcode.
        /// </summary>
        public BarcodeStructureBase Barcode
        {
            get
            {
                return _barcode;
            }
            set
            {
                _barcode = value;
                SetBarcodeStructureTree();
                if (barcodeStructureControl.BarcodeRenderer != null)
                    barcodeStructureControl.BarcodeRenderer.BarcodeElement = value;
                SetPreviewParams();
            }
        }

        #endregion



        #region Methods

        /// <summary>
        /// Sets the preview parameters.
        /// </summary>
        private void SetPreviewParams()
        {
            if (_barcode != null)
            {
                barcodeStructureControl.RenderingScale = _previewScale;
                Int32Rect barcodeBoundingBox = _barcode.GetBoundingBox();
                int width = (int)Math.Round((barcodeBoundingBox.Width + 2) * PreviewScale);
                int height = (int)Math.Round((barcodeBoundingBox.Height + 2) * PreviewScale);
                barcodeStructureControl.Width = width;
                barcodeStructureControl.Height = height;
            }
        }

        /// <summary>
        /// Sets the barcode structure tree.
        /// </summary>
        private void SetBarcodeStructureTree()
        {
            barcodeStructureTreeView.Items.Clear();
            if (_barcode != null)
            {
                AddBarcodeElementToTree(barcodeStructureTreeView.Items, _barcode);
            }
        }

        /// <summary>
        /// Adds the barcode element to the visual tree.
        /// </summary>
        /// <param name="nodes">The node collection.</param>
        /// <param name="element">The element to add.</param>
        private void AddBarcodeElementToTree(ItemCollection nodes, BarcodeElement element)
        {
            TreeViewItem node = new TreeViewItem();
            node.Header = element.ToString();
            node.Tag = element;
            nodes.Add(node);
            if (element is BarcodeMatrixElement)
            {
                BarcodeMatrixElement matrixElement = (BarcodeMatrixElement)element;
                for (int y = 0; y < matrixElement.Matrix.GetLength(0); y++)
                {
                    StringBuilder lineName = new StringBuilder();
                    for (int x = 0; x < matrixElement.Matrix.GetLength(1); x++)
                    {
                        if (matrixElement.Matrix[y, x] != null)
                        {
                            if (matrixElement.Matrix[y, x] == BarcodeElements.BlackCell)
                                lineName.Append("# ");
                            else
                                lineName.Append(". ");
                        }
                        else
                        {
                            lineName.Append("  ");
                        }
                    }
                    TreeViewItem lineNameNode = new TreeViewItem();
                    lineNameNode.Header = lineName.ToString();
                    node.Items.Add(lineNameNode);
                }
            }
            else if (element is BarcodeLayerElement)
            {
                AddBarcodeElementToTree(node.Items, ((BarcodeLayerElement)element).Element);
            }
            else if (element is IEnumerable)
            {
                foreach (BarcodeElement subElement in (IEnumerable)element)
                    AddBarcodeElementToTree(node.Items, subElement);
            }
        }

        /// <summary>
        /// Sets the preview scale.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void scaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _previewScale = scaleSlider.Value / 10;
            SetPreviewParams();
        }

        /// <summary>
        /// Updates the preview parameters when preview panel size was changed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void previewPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetPreviewParams();
        }

        /// <summary>
        /// Sets the delected barcode renderer.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void barcodeRendererComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BarcodeDrawingContextRenderer barcodeRenderer = _barcodeRenderers[(string)barcodeRendererComboBox.SelectedItem];
            barcodeRenderer.BarcodeElement = Barcode;
            barcodeStructureControl.BarcodeRenderer = barcodeRenderer;
            barcodeRendererPropertyGrid.SelectedObject = barcodeRenderer;
        }

        /// <summary>
        /// Updates the barcode image when the barcode renderer property was changed.
        /// </summary>
        /// <param name="s">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyValueChangedEventArgs"/> instance
        /// containing the event data.</param>
        private void barcodeRendererPropertyGrid_PropertyValueChanged(
            object s,
            System.Windows.Forms.PropertyValueChangedEventArgs e)
        {
            barcodeStructureControl.InvalidateVisual();
        }

        /// <summary>
        /// Closes the dialog with "OK" dialog result.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        /// <summary>
        /// Closes the dialog with "Cancel" dialog result.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        #endregion

    }
}
