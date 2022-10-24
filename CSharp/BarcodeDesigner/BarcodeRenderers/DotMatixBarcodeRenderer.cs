using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

using Vintasoft.Barcode;
using Vintasoft.Barcode.BarcodeStructure;


namespace WpfSimpleBarcodeWriterDemo
{
    /// <summary>
    /// A matrix barcode renderer that uses custom dots for drawing a matrix barcode.
    /// </summary>
    public class DotMatixBarcodeRenderer : MatrixBarcodeRendererBase
    {

        #region Fields

        /// <summary>
        /// The brush that is used for drawing the black cells of alignment pattern.
        /// </summary>
        SolidColorBrush _alignmentPatternBlackBrush;
        
        /// <summary>
        /// The brush that is used for drawing the white cells of alignment pattern.
        /// </summary>
        SolidColorBrush _alignmentPatternWhiteBrush;

        /// <summary>
        /// The brush that is used for drawing the black cells of search pattern.
        /// </summary>
        SolidColorBrush _searchPatternBlackBrush;

        /// <summary>
        /// The brush that is used for drawing the white cells of search pattern.
        /// </summary>
        SolidColorBrush _searchPatternWhiteBrush;

        /// <summary>
        /// The brush that is used for drawing the black cells of timing pattern.
        /// </summary>
        SolidColorBrush _timingPatternBlackBrush;

        /// <summary>
        /// The brush that is used for drawing the white cells of timing pattern.
        /// </summary>
        SolidColorBrush _timingPatternWhiteBrush;

        /// <summary>
        /// The brush that is used for drawing the black cells of data layer.
        /// </summary>
        SolidColorBrush _dataLayerBlackBrush;

        /// <summary>
        /// The brush that is used for drawing the white cells of data layer.
        /// </summary>
        SolidColorBrush _dataLayerWhiteBrush;

        /// <summary>
        /// The brush that is used for drawing the black cells of format information.
        /// </summary>
        SolidColorBrush _formatInformationBlackBrush;

        /// <summary>
        /// The brush that is used for drawing the white cells of format information.
        /// </summary>
        SolidColorBrush _formatInformationWhiteBrush;

        #endregion



        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DotMatixBarcodeRenderer"/> class.
        /// </summary>
        public DotMatixBarcodeRenderer()
        {
            FormatInformationColor = DataLayerColor;
        }

        #endregion



        #region Properties

        float _dotDiameter = 0.8f;
        /// <summary> 
        /// Gets or sets a dot diameter.
        /// </summary>
        [Category("Design")]
        [Description("A dot diameter.")]
        public float DotDiameter
        {
            get
            {
                return _dotDiameter;
            }
            set
            {
                _dotDiameter = Math.Max(0, value);
            }
        }

        BarcodeMatixDotType _dotType = BarcodeMatixDotType.Circle;
        /// <summary>
        /// Gets or sets a dot type.
        /// </summary>
        [Category("Design")]
        [Description("A dot type.")]
        public BarcodeMatixDotType DotType
        {
            get
            {
                return _dotType;
            }
            set
            {
                _dotType = value;
            }
        }

        bool _drawAlignmentPatternAsSinglePattern = true;
        /// <summary>
        /// Gets or sets a value indicating whether renderer must
        /// draw an alignment pattern as a single pattern.
        /// </summary>
        /// <value>
        /// <b>True</b> if renderer must draw an alignment pattern as a single pattern;
        /// otherwise, <b>false</b>.
        /// </value>
        [Category("Design")]
        public bool DrawAlignmentPatternAsSinglePattern
        {
            get
            {
                return _drawAlignmentPatternAsSinglePattern;
            }
            set
            {
                _drawAlignmentPatternAsSinglePattern = value;
            }
        }

        #endregion



        #region Methods

        #region PUBLIC

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Dot Matrix";
        }

        #endregion


        #region PROTECTED

        /// <summary>
        /// Renders the barcode.
        /// </summary>
        /// <seealso cref="BarcodeElement" />
        public override void Render()
        {
            _alignmentPatternBlackBrush = new SolidColorBrush(AlignmentPatternsColor);
            _alignmentPatternBlackBrush.Freeze();
            _alignmentPatternWhiteBrush = new SolidColorBrush(GetLightenColor(AlignmentPatternsColor));
            _alignmentPatternWhiteBrush.Freeze();
            _searchPatternBlackBrush = new SolidColorBrush(SearchPatternsColor);
            _searchPatternBlackBrush.Freeze();
            _searchPatternWhiteBrush = new SolidColorBrush(GetLightenColor(SearchPatternsColor));
            _searchPatternWhiteBrush.Freeze();
            _timingPatternBlackBrush = new SolidColorBrush(TimingPatternsColor);
            _timingPatternBlackBrush.Freeze();
            _timingPatternWhiteBrush = new SolidColorBrush(GetLightenColor(TimingPatternsColor));
            _timingPatternWhiteBrush.Freeze();
            _dataLayerBlackBrush = new SolidColorBrush(DataLayerColor);
            _dataLayerBlackBrush.Freeze();
            _dataLayerWhiteBrush = new SolidColorBrush(GetLightenColor(DataLayerColor));
            _dataLayerWhiteBrush.Freeze();
            _formatInformationBlackBrush = new SolidColorBrush(FormatInformationColor);
            _formatInformationBlackBrush.Freeze();
            _formatInformationWhiteBrush = new SolidColorBrush(GetLightenColor(FormatInformationColor));
            _formatInformationWhiteBrush.Freeze();

            base.Render();
        }

        /// <summary>
        /// Renders the barcode matrix element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected override void RenderBarcodeMatrixElement(
            BarcodeMatrixElement element,
            int x,
            int y)
        {
            const double penWidth = 1;

            // customize drawing of search and alignment patterns

            bool isAztecSearchPattern = false;
            if (element == BarcodeElements.AztecCompactSearchPattern)
                isAztecSearchPattern = true;
            else if (element == BarcodeElements.AztecFullRangeSearchPattern)
                isAztecSearchPattern = true;

            bool isQrAlignmentPattern = false;
            if (element == BarcodeElements.QrAlignmentPattern)
                isQrAlignmentPattern = true;

            bool isHanXinRightTopOrLeftBottom = false;
            if (element == BarcodeElements.HanXinCodeRightTopSearchPattern)
                isHanXinRightTopOrLeftBottom = true;
            else if (element == BarcodeElements.HanXinCodeLeftBottomSearchPattern)
                isHanXinRightTopOrLeftBottom = true;

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
                Pen blackPen = new Pen(new SolidColorBrush(SearchPatternsColor), 1);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;
                // draw outer rectangle 7x7
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 1.5, y + 1.5, 6, 6));

                // draw inner cell 3x3
                SolidColorBrush brush = new SolidColorBrush(SearchPatternsColor);
                DrawingContext.DrawRectangle(brush, null, new Rect(x + 3.75, y + 3.75, 1.5, 1.5));
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 3.5, y + 3.5, 2, 2));
            }
            // QR alignment pattern
            else if (DrawAlignmentPatternAsSinglePattern && isQrAlignmentPattern)
            {
                // (x,y)
                //    #####
                //    #   #
                //    # # #
                //    #   #
                //    #####
                //       (x+5,y+5)
                Pen blackPen = new Pen(new SolidColorBrush(AlignmentPatternsColor), DotDiameter);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;
                // draw outer rectangle 5x5
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 0.5, y + 0.5, 4, 4));
                // draw inner cell 1x1
                SolidColorBrush brush = new SolidColorBrush(AlignmentPatternsColor);
                FillDot(brush, x + 2, y + 2);
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
                Pen blackPen = new Pen(new SolidColorBrush(SearchPatternsColor), penWidth);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;

                // draw outer rectangles: 13x13, 9x9, 5x5
                int rectCount;
                if (element == BarcodeElements.AztecFullRangeSearchPattern)
                    rectCount = 3;
                else
                    rectCount = 2;
                for (int i = 0; i < rectCount; i++)
                {
                    DrawingContext.DrawRectangle(null, blackPen,
                        new Rect(x + 0.5, y + 0.5,
                        (rectCount - i) * 4, (rectCount - i) * 4));
                    x += 2;
                    y += 2;
                }

                // draw inner cell 1x1
                blackPen = new Pen(blackPen.Brush, 0.5);
                DrawingContext.DrawRectangle(blackPen.Brush, blackPen, new Rect(x + 0.25, y + 0.25, 0.5, 0.5));
            }
            // Han Xin Code search pattern (LeftTop)
            else if (element == BarcodeElements.HanXinCodeLeftTopSearchPattern)
            {
                // (x,y)
                //    #######
                //    #
                //    # #####
                //    # # 
                //    # # ###
                //    # # ###
                //    # # ###
                //       (x+7,y+7)
                Pen blackPen = new Pen(new SolidColorBrush(SearchPatternsColor), penWidth);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;
                // draw outer line                
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 1.5f, y + 8f),
                        new Point(x + 1.5f, y + 1.5f),
                        new Point(x + 8f, y + 1.5f)));
                // draw center line
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 3.5f, y + 8f),
                        new Point(x + 3.5f, y + 3.5f),
                        new Point(x + 8f, y + 3.5f)));
                // draw cell 3x3
                SolidColorBrush brush = new SolidColorBrush(SearchPatternsColor);
                DrawingContext.DrawRectangle(brush, null, new Rect(x + 5.75f, y + 5.75f, 1.5f, 1.5f));
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 5.5f, y + 5.5f, 2f, 2f));
            }
            // Han Xin Code search pattern (RightBottom)
            else if (element == BarcodeElements.HanXinCodeRightBottomSearchPattern)
            {
                // (x,y)
                //    ### # #
                //    ### # #
                //    ### # #
                //        # #
                //    ##### #
                //          #
                //    #######
                //       (x+7,y+7)
                Pen blackPen = new Pen(new SolidColorBrush(SearchPatternsColor), penWidth);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;
                // draw outer line
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 1f, y + 7.5f),
                        new Point(x + 7.5f, y + 7.5f),
                        new Point(x + 7.5f, y + 1f)));
                // draw center line
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 1f, y + 5.5f),
                        new Point(x + 5.5f, y + 5.5f),
                        new Point(x + 5.5f, y + 1f)));
                // draw cell 3x3
                SolidColorBrush brush = new SolidColorBrush(SearchPatternsColor);
                DrawingContext.DrawRectangle(brush, null, new Rect(x + 1.75f, y + 1.75f, 1.5f, 1.5f));
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 1.5f, y + 1.5f, 2f, 2f));
            }
            // Han Xin Code search pattern (RightTop or LeftBottom)
            else if (isHanXinRightTopOrLeftBottom)
            {
                // (x,y)
                //    #######
                //          #
                //    ##### #
                //        # #
                //    ### # #
                //    ### # #
                //    ### # #
                //    ### # #
                //       (x+7,y+7)
                Pen blackPen = new Pen(new SolidColorBrush(SearchPatternsColor), penWidth);
                if (DotType != BarcodeMatixDotType.Square)
                    blackPen.LineJoin = PenLineJoin.Round;
                // draw outer line
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 1f, y + 1.5f),
                        new Point(x + 7.5f, y + 1.5f),
                        new Point(x + 7.5f, y + 8f)));
                // draw center line
                DrawingContext.DrawGeometry(null, blackPen,
                    CreateConnectedLines(
                        new Point(x + 1f, y + 3.5f),
                        new Point(x + 5.5f, y + 3.5f),
                        new Point(x + 5.5f, y + 8f)));
                // draw cell 3x3
                SolidColorBrush brush = new SolidColorBrush(SearchPatternsColor);
                DrawingContext.DrawRectangle(brush, null, new Rect(x + 1.75f, y + 5.75f, 1.5f, 1.5f));
                DrawingContext.DrawRectangle(null, blackPen, new Rect(x + 1.5f, y + 5.5f, 2f, 2f));
            }
            // DataMatrix search pattern
            else if (element.Name.Contains("Search Pattern"))
            {
                // draw as single pattern use graphics path
                GeometryGroup group = new GeometryGroup();
                SolidColorBrush blackBrush = GetBrush(x, y, true);
                for (int yc = 0; yc < element.Height; yc++)
                {
                    for (int xc = 0; xc < element.Width; xc++)
                    {
                        if (element.Matrix[yc, xc] == BarcodeElements.BlackCell)
                        {
                            Rect rect = new Rect(x + xc, y + yc, 1, 1);
                            group.Children.Add(new RectangleGeometry(rect));
                        }
                    }
                }
                DrawingContext.DrawGeometry(blackBrush, null, group);
            }
            else
            {
                base.RenderBarcodeMatrixElement(element, x, y);
            }
        }

        /// <summary>
        /// Renders the barcode element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="x">The X-coordinate of element.</param>
        /// <param name="y">The Y-coordinate of element.</param>
        protected override void RenderBarcodeElement(BarcodeElement element, int x, int y)
        {
            if (element == BarcodeElements.BlackCell)
            {
                SolidColorBrush brush = GetBrush(x, y, true);
                FillDot(brush, x, y);
            }
            else if (element == BarcodeElements.WhiteCell)
            {
                SolidColorBrush brush = GetBrush(x, y, false);
                FillDot(brush, x, y);
            }
            else
            {
                base.RenderBarcodeElement(element, x, y);
            }
        }

        /// <summary>
        /// Returns the brush for cell at specified location.
        /// </summary>
        /// <param name="x">The X-coordinate of cell.</param>
        /// <param name="y">The Y-coordinate of cell.</param>
        /// <param name="isBlack">Defines when brush creates for "black" cell.</param>
        protected virtual SolidColorBrush GetBrush(int x, int y, bool isBlack)
        {
            if (IsAlignmentPattern(x, y))
            {
                if (isBlack)
                    return _alignmentPatternBlackBrush;
                return _alignmentPatternWhiteBrush;
            }
            if (IsSearchPattern(x, y))
            {
                if (isBlack)
                    return _searchPatternBlackBrush;
                return _searchPatternWhiteBrush;
            }
            if (IsTimingPattern(x, y))
            {
                if (isBlack)
                    return _timingPatternBlackBrush;
                return _timingPatternWhiteBrush;
            }
            if (IsDataLayer(x, y))
            {
                if (isBlack)
                    return _dataLayerBlackBrush;
                return _dataLayerWhiteBrush;
            }
            if (IsFormatInformation(x, y))
            {
                if (isBlack)
                    return _formatInformationBlackBrush;
                return _formatInformationWhiteBrush;
            }
            throw new NotImplementedException();
        }

        #endregion


        #region PRIVATE

        /// <summary>
        /// Fills the dot at specified location.
        /// </summary>
        /// <param name="brush">The brush.</param>
        /// <param name="x">The X-coordinate of dot.</param>
        /// <param name="y">The Y-coordinate of dot.</param>
        private void FillDot(SolidColorBrush brush, int x, int y)
        {
            if (brush.Color.Equals(BackgroundColor))
                return;

            switch (DotType)
            {
                case BarcodeMatixDotType.Circle:
                    Rect bbox = GetDotBoundingBox(x, y);
                    double rx = bbox.Width / 2;
                    double ry = bbox.Height / 2;
                    Point center = new Point(bbox.X + rx, bbox.Y + ry);
                    DrawingContext.DrawEllipse(brush, null, center, rx, ry);
                    break;
                case BarcodeMatixDotType.Rhombus:
                    DrawingContext.DrawGeometry(brush, null, CreateConnectedLines(GetRhombusPolygon(GetDotBoundingBox(x, y))));
                    break;
                case BarcodeMatixDotType.Square:
                    DrawingContext.DrawRectangle(brush, null, GetDotBoundingBox(x, y));
                    break;
                case BarcodeMatixDotType.BeveledSquare:
                    DrawingContext.DrawGeometry(brush, null, CreateConnectedLines(GetBeveledSquarePolygon(GetDotBoundingBox(x, y))));
                    break;
                case BarcodeMatixDotType.Cross:
                    DrawingContext.DrawGeometry(brush, null, CreateConnectedLines(GetCrossPolygon(GetDotBoundingBox(x, y))));
                    break;
                case BarcodeMatixDotType.DiagonalCross:
                    Pen pen = new Pen(brush, DotDiameter);
                    pen.EndLineCap = PenLineCap.Round;
                    pen.StartLineCap = PenLineCap.Round;
                    DrawingContext.DrawLine(pen, new Point(x, y), new Point(x + 1, y + 1));
                    DrawingContext.DrawLine(pen, new Point(x + 1, y), new Point(x, y + 1));
                    break;
            }
        }

        /// <summary>
        /// Returns a polygon that contains rhombus.
        /// </summary>
        /// <param name="bbox">The bounding box of a rhombus.</param>
        /// <returns>A polygon that contains rhombus.</returns>
        private Point[] GetRhombusPolygon(Rect bbox)
        {
            return new Point[]{
                new Point(bbox.X + bbox.Width / 2, bbox.Y),
                new Point(bbox.X + bbox.Width, bbox.Y + bbox.Height / 2),
                new Point(bbox.X + bbox.Width / 2, bbox.Y + bbox.Height),
                new Point(bbox.X, bbox.Y + bbox.Height / 2)
            };
        }

        /// <summary>
        /// Returns a polygon that contains beveled square.
        /// </summary>
        /// <param name="bbox">The bounding box of a beveled square.</param>
        /// <returns>A polygon that contains beveled square.</returns>
        private Point[] GetBeveledSquarePolygon(Rect bbox)
        {
            const float t1 = 1 / 4f;
            const float t2 = 1 - t1;
            return new Point[]{
                new Point(bbox.X + bbox.Width * t1, bbox.Y),
                new Point(bbox.X + bbox.Width * t2, bbox.Y),
                new Point(bbox.X + bbox.Width, bbox.Y + bbox.Height * t1),
                new Point(bbox.X + bbox.Width, bbox.Y + bbox.Height * t2),
                new Point(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height),
                new Point(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height),
                new Point(bbox.X, bbox.Y + bbox.Height * t2),
                new Point(bbox.X, bbox.Y + bbox.Height * t1)
            };
        }

        /// <summary>
        /// Returns a polygon that contains cross.
        /// </summary>
        /// <param name="bbox">The bounding box of a cross.</param>
        /// <returns>A polygon that contains cross.</returns>
        private Point[] GetCrossPolygon(Rect bbox)
        {
            const double t1 = 1 / 3f;
            const double t2 = 1 - t1;
            return new Point[]{
                new Point(bbox.X + bbox.Width * t1, bbox.Y),
                new Point(bbox.X + bbox.Width * t2, bbox.Y),
                new Point(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height * t1),

                new Point(bbox.X + bbox.Width, bbox.Y + bbox.Height * t1),
                new Point(bbox.X + bbox.Width, bbox.Y + bbox.Height * t2),
                new Point(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height * t2),

                new Point(bbox.X + bbox.Width * t2, bbox.Y + bbox.Height),
                new Point(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height),
                new Point(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height * t2),

                new Point(bbox.X, bbox.Y + bbox.Height * t2),
                new Point(bbox.X, bbox.Y + bbox.Height * t1),
                new Point(bbox.X + bbox.Width * t1, bbox.Y + bbox.Height * t1)
            };
        }

        /// <summary>
        /// Returns a bounding box of barcode cell (dot).
        /// </summary>
        /// <param name="x">The X-coordinate of dot.</param>
        /// <param name="y">The Y-coordinate of dot.</param>
        private Rect GetDotBoundingBox(int x, int y)
        {
            return new Rect(x + (1 - DotDiameter) / 2, y + (1 - DotDiameter) / 2, DotDiameter, DotDiameter);
        }

        /// <summary> 
        /// Creates the connected lines.
        /// </summary>
        /// <param name="points">The points of lines.</param>
        private Geometry CreateConnectedLines(params Point[] points)
        {
            PathGeometry pathGeometry = new PathGeometry();

            if (points != null && points.Length != 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = points[0];
                foreach (Point point in points)
                    figure.Segments.Add(new LineSegment(point, true));
                pathGeometry.Figures.Add(figure);
            }

            return pathGeometry;
        }

        #endregion

        #endregion

    }
}
