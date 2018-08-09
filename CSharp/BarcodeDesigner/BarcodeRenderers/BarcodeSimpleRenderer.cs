using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

using Vintasoft.WpfBarcode;
using Vintasoft.WpfBarcode.BarcodeStructure;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// A simple renderer of matrix barcode.
    /// </summary>
    /// <remarks>
    /// This renderer draws the barcode search patterns and other barcode elements separately.
    /// </remarks>
    public class BarcodeSimpleRenderer : BarcodeDrawingContextRenderer
    {

        #region Fields

        /// <summary>
        /// The brush that is used for filling the barcode elements.
        /// </summary>
        SolidColorBrush _barcodeBrush;

        /// <summary>
        /// The pen that is used for drawing the barcode elements.
        /// </summary>
        Pen _barcodePen;

        /// <summary> 
        /// The geometry that is used for drawing the barcode elements.
        /// </summary>
        GeometryGroup _drawGeometry;

        /// <summary> 
        /// The geometry that is used for filling the barcode elements.
        /// </summary>
        GeometryGroup _fillGeometry;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeSimpleRenderer"/> class.
        /// </summary>
        public BarcodeSimpleRenderer()
        {
        }

        #endregion



        #region Properties

        Color _barcodeColor = Colors.Black;
        /// <summary>
        /// Gets or sets the color that is used for drawing the barcode.
        /// </summary>
        [Category("Colors")]
        [Description("Color that is used for drawing the barcode.")]
        [TypeConverter(typeof(SimpleColorConverter))]
        public Color BarcodeColor
        {
            get
            {
                return _barcodeColor;
            }
            set
            {
                _barcodeColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        [Category("Colors")]
        [TypeConverter(typeof(SimpleColorConverter))]
        public new Color BackgroundColor
        {
            get
            {
                return base.BackgroundColor;
            }
            set
            {
                base.BackgroundColor = value;
            }
        }

        #endregion



        #region Methods

        /// <summary> 
        /// Renders the barcode.
        /// </summary>
        /// <seealso cref="BarcodeElement" />
        public override void Render()
        {
            // clear groups
            _fillGeometry = new GeometryGroup();
            _drawGeometry = new GeometryGroup();

            base.Render();

            // create brush
            _barcodeBrush = new SolidColorBrush(BarcodeColor);
            _barcodeBrush.Freeze();

            // create pen
            _barcodePen = new Pen(_barcodeBrush, 1);
            _barcodePen.LineJoin = PenLineJoin.Round;
            _barcodePen.Freeze();

            // draw groups
            _fillGeometry.Freeze();
            _drawGeometry.Freeze();
            DrawingContext.DrawGeometry(_barcodeBrush, null, _fillGeometry);
            DrawingContext.DrawGeometry(null, _barcodePen, _drawGeometry);
        }

        /// <summary>
        /// Renders the barcode matrix element.
        /// </summary>
        /// <param name="element">The barcode matrix element to render.</param>
        /// <param name="x">The X-coordinate, where barcode matrix element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode matrix element must be rendered.</param>
        protected override void RenderBarcodeMatrixElement(
            BarcodeMatrixElement element,
            int x,
            int y)
        {
            // customize drawing of search patterns

            bool isAztecSearchPattern = false;
            if (element == BarcodeElements.AztecCompactSearchPattern)
                isAztecSearchPattern = true;
            else if (element == BarcodeElements.AztecFullRangeSearchPattern)
                isAztecSearchPattern = true;

            // QR search pattern
            if (element == BarcodeElements.QrSearchPattern)
            {
                // (x,y)
                //    #######
                //    #     #
                //    # ### #
                //    # ### #
                //    # ### #
                //    #     #
                //    #######
                //       (x+7,y+7)                
                // draw outer rectangle 7x7
                _drawGeometry.Children.Add(new RectangleGeometry(new Rect(x + 1.5, y + 1.5, 6, 6)));
                // draw inner cell 3x3
                _fillGeometry.Children.Add(new RectangleGeometry(new Rect(x + 3.75, y + 3.75, 1.5, 1.5)));
                _drawGeometry.Children.Add(new RectangleGeometry(new Rect(x + 3.5, y + 3.5, 2, 2)));
            }
            // Aztec search pattern
            else if (isAztecSearchPattern)
            {
                //  Compact/Rune          Full Range
                //                    (x,y)
                //                       #############
                // (x,y)                 #           #
                //    #########          # ######### #
                //    #       #          # #       # #
                //    # ##### #          # # ##### # #
                //    # #   # #          # # #   # # #
                //    # # # # #          # # # # # # #
                //    # #   # #          # # #   # # #
                //    # ##### #          # # ##### # #
                //    #       #          # #       # #
                //    #########          # ######### #
                //         (x+9,y+9)     #           #
                //                       #############
                //                               (x+13,y+13)
                // draw outer rectangles: 13x13, 9x9, 5x5
                int rectCount;
                if (element == BarcodeElements.AztecFullRangeSearchPattern)
                    rectCount = 3;
                else
                    rectCount = 2;
                for (int i = 0; i < rectCount; i++)
                {
                    _drawGeometry.Children.Add(new RectangleGeometry(
                        new Rect(x + 0.5, y + 0.5,
                                (rectCount - i) * 4, (rectCount - i) * 4)));
                    x += 2;
                    y += 2;
                }

                // draw inner cell 1x1
                Point center = new Point(x + 0.5, y + 0.5);
                _fillGeometry.Children.Add(new EllipseGeometry(center, 0.5, 0.5));
            }
            // DataMatrix and Han Xin Code search patterns
            else if (element.Name.Contains("Search Pattern"))
            {
                for (int yc = 0; yc < element.Height; yc++)
                {
                    for (int xc = 0; xc < element.Width; xc++)
                    {
                        if (element.Matrix[yc, xc] == BarcodeElements.BlackCell)
                        {
                            Rect rect = new Rect(x + xc, y + yc, 1, 1);
                            RectangleGeometry rectangle = new RectangleGeometry(rect);
                            _fillGeometry.Children.Add(rectangle);
                        }
                    }
                }
            }
            else
            {
                base.RenderBarcodeMatrixElement(element, x, y);
            }
        }

        /// <summary>
        /// Renders the barcode element.
        /// </summary>
        /// <param name="barcodeElement">The barcode element to render.</param>
        /// <param name="x">The X-coordinate, where barcode element must be rendered.</param>
        /// <param name="y">The Y-coordinate, where barcode element must be rendered.</param>
        protected override void RenderBarcodeElement(BarcodeElement barcodeElement, int x, int y)
        {
            if (barcodeElement == BarcodeElements.BlackCell)
            {
                Point center = new Point(x + 0.5, y + 0.5);
                _fillGeometry.Children.Add(new EllipseGeometry(center, 0.5, 0.5));
            }
            else
            {
                base.RenderBarcodeElement(barcodeElement, x, y);
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Simple dots";
        }

        #endregion

    }
}
