using System;
using System.Windows;
using System.Windows.Media;

using Vintasoft.WpfBarcode;
using Vintasoft.WpfBarcode.BarcodeStructure;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// A renderer that highlights the barcode structure.
    /// </summary>
    public class BarcodeStructureHighlightRenderer : MatrixBarcodeRendererBase
    {

        #region Fields

        /// <summary>
        /// The path that is used for drawing the black cells of data layer.
        /// </summary>
        GeometryGroup _dataLayerBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of data layer.
        /// </summary>
        GeometryGroup _dataLayerWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of format information.
        /// </summary>
        GeometryGroup _formatInformationBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of format information.
        /// </summary>
        GeometryGroup _formatInformationWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of search pattern.
        /// </summary>
        GeometryGroup _searchPatternBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of search pattern.
        /// </summary>
        GeometryGroup _searchPatternWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of timing pattern.
        /// </summary>
        GeometryGroup _timingPatternBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of timing pattern.
        /// </summary>
        GeometryGroup _timingPatternWhitePath;

        /// <summary>
        /// The path that is used for drawing the black cells of alignment pattern.
        /// </summary>
        GeometryGroup _alignmentPatternsBlackPath;

        /// <summary>
        /// The path that is used for drawing the white cells of alignment pattern.
        /// </summary>
        GeometryGroup _alignmentPatternsWhitePath;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeStructureHighlightRenderer"/> class.
        /// </summary>
        public BarcodeStructureHighlightRenderer()
        {
            Modulation = 0.5f;
        }

        #endregion



        #region Methods

        /// <summary>
        /// Renders the barcode.
        /// </summary>
        /// <seealso cref="BarcodeElement" />
        public override void Render()
        {
            // create paths
            _alignmentPatternsBlackPath = new GeometryGroup();
            _alignmentPatternsWhitePath = new GeometryGroup();
            _dataLayerBlackPath = new GeometryGroup();
            _dataLayerWhitePath = new GeometryGroup();
            _searchPatternBlackPath = new GeometryGroup();
            _searchPatternWhitePath = new GeometryGroup();
            _timingPatternBlackPath = new GeometryGroup();
            _timingPatternWhitePath = new GeometryGroup();
            _formatInformationBlackPath = new GeometryGroup();
            _formatInformationWhitePath = new GeometryGroup();
            _alignmentPatternsBlackPath.FillRule = FillRule.Nonzero;
            _alignmentPatternsWhitePath.FillRule = FillRule.Nonzero;

            // render barcode
            base.Render();

            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(GetLightenColor(SearchPatternsColor))),
                null, _searchPatternWhitePath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(SearchPatternsColor)),
                null, _searchPatternBlackPath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(GetLightenColor(TimingPatternsColor))),
                null, _timingPatternWhitePath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(TimingPatternsColor)),
                null, _timingPatternBlackPath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(GetLightenColor(AlignmentPatternsColor))),
                null, _alignmentPatternsWhitePath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(AlignmentPatternsColor)),
                null, _alignmentPatternsBlackPath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(GetLightenColor(FormatInformationColor))),
                null, _formatInformationWhitePath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(FormatInformationColor)),
                null, _formatInformationBlackPath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(GetLightenColor(DataLayerColor))),
                null, _dataLayerWhitePath);
            DrawingContext.DrawGeometry(
                Freeze(new SolidColorBrush(DataLayerColor)),
                null, _dataLayerBlackPath);
            
            // free resources
            _alignmentPatternsBlackPath = null;
            _alignmentPatternsWhitePath = null;
            _dataLayerBlackPath = null;
            _dataLayerWhitePath = null;
            _searchPatternBlackPath = null;
            _searchPatternWhitePath = null;
            _timingPatternBlackPath = null;
            _timingPatternWhitePath = null;
            _formatInformationBlackPath = null;
            _formatInformationWhitePath = null;
        }

        /// <summary>
        /// Freezes the specified brush.
        /// </summary>
        /// <param name="brush">The brush.</param>
        private Brush Freeze(Brush brush)
        {
            brush.Freeze();
            return brush;
        }

        /// <summary>
        /// Renders the barcode element.
        /// </summary>
        /// <param name="barcodeElement">The barcode element to render.</param>
        /// <param name="x">The X-coordinate, where barcode element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode element must be rendered.</param>
        protected override void RenderBarcodeElement(BarcodeElement barcodeElement, int x, int y)
        {
            bool isMatrixCell = false;
            if (barcodeElement == BarcodeElements.BlackCell)
                isMatrixCell = true;
            else if (barcodeElement == BarcodeElements.WhiteCell)
                isMatrixCell = true;


            if (isMatrixCell)
            {
                GeometryGroup path = SelectPath(x, y);

                Rect rect = new Rect(x, y, 1, 1);
                path.Children.Add(new RectangleGeometry(rect));
            }
            else
            {
                base.RenderBarcodeElement(barcodeElement, x, y);
            }
        }

        /// <summary>
        /// Selects the path for cell at specified location.
        /// </summary>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected GeometryGroup SelectPath(int x, int y)
        {
            bool isBlack = false;
            if (GetMatrixElement(x, y) == BarcodeElements.BlackCell)
                isBlack = true;

            if (IsAlignmentPattern(x, y))
            {
                if (isBlack)
                    return _alignmentPatternsBlackPath;
                return _alignmentPatternsWhitePath;
            }
            if (IsSearchPattern(x, y))
            {
                if (isBlack)
                    return _searchPatternBlackPath;
                return _searchPatternWhitePath;
            }
            if (IsTimingPattern(x, y))
            {
                if (isBlack)
                    return _timingPatternBlackPath;
                return _timingPatternWhitePath;
            }
            if (IsDataLayer(x, y))
            {
                if (isBlack)
                    return _dataLayerBlackPath;
                return _dataLayerWhitePath;
            }
            if (IsFormatInformation(x, y))
            {
                if (isBlack)
                    return _formatInformationBlackPath;
                return _formatInformationWhitePath;
            }
            throw new NotImplementedException();
        }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Structure Highlight";
        }

        #endregion

    }
}
