using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LabrinthDraw
{
    class LabyrinthDrawer : UserControl
    {
        private const double thickness = 0.05, offsetPerLine = 0.1, indexTextHeight = 0.3,
            indexTextMarginVertical = 0.01, indexTextMarginHorizontal = 0.1;

        private static readonly CultureInfo ci = CultureInfo.CurrentCulture;
        private static readonly FlowDirection fd = FlowDirection.LeftToRight;
        private static readonly Typeface tf = new Typeface("Arial");

        public static readonly DependencyProperty BlockWidthCountProperty =
            DependencyProperty.Register("BlockWidthCount", typeof(int), typeof(LabyrinthDrawer),
                new PropertyMetadata(10, new PropertyChangedCallback(OnBlockWidthCountPropertyChanged)));

        private static void OnBlockWidthCountPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthDrawer)sender;
            var value = (int)e.NewValue;

            s.InvalidateVisual();
        }

        public static readonly DependencyProperty BlockHeightCountProperty =
            DependencyProperty.Register("BlockHeightCount", typeof(int), typeof(LabyrinthDrawer),
                new PropertyMetadata(10, new PropertyChangedCallback(OnBlockHeightCountPropertyChanged)));

        private static void OnBlockHeightCountPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthDrawer)sender;
            var value = (int)e.NewValue;

            s.InvalidateVisual();
        }

        public static readonly DependencyProperty BrushesSourceProperty =
            DependencyProperty.Register("BrushesSource", typeof(Brush[]), typeof(LabyrinthDrawer),
                new PropertyMetadata(null, new PropertyChangedCallback(OnBrushesSourcePropertyChanged)));

        private static void OnBrushesSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthDrawer)sender;
            var value = (Brush[])e.NewValue;

            List<Line> newLines = new List<Line>();

            for (int i = 0; i < value.Length; i++)
            {
                Line line = s.Lines?.FirstOrDefault(l => l.Brush == value[i]) ?? new Line(value[i]);

                newLines.Add(line);
            }

            s.Lines = new LineCollection(newLines);

            s.InvalidateVisual();
        }

        public static readonly DependencyProperty LinesProperty =
            DependencyProperty.Register("Lines", typeof(LineCollection), typeof(LabyrinthDrawer),
                new PropertyMetadata(null, new PropertyChangedCallback(OnLinesPropertyChanged)));

        private static void OnLinesPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthDrawer)sender;
            var value = (LineCollection)e.NewValue;
        }

        public static readonly DependencyProperty CurrentLineProperty =
            DependencyProperty.Register("CurrentLine", typeof(Line), typeof(LabyrinthDrawer),
                new PropertyMetadata(null, new PropertyChangedCallback(OnCurrentLinePropertyChanged)));

        private static void OnCurrentLinePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthDrawer)sender;
            var value = (Line)e.NewValue;
        }

        public int BlockWidthCount
        {
            get { return (int)GetValue(BlockWidthCountProperty); }
            set { SetValue(BlockWidthCountProperty, value); }
        }

        public int BlockHeightCount
        {
            get { return (int)GetValue(BlockHeightCountProperty); }
            set { SetValue(BlockHeightCountProperty, value); }
        }

        public double BlockWidth { get { return ActualWidth / BlockWidthCount; } }

        public double BlockHeight { get { return ActualHeight / BlockHeightCount; } }

        public Brush[] BrushesSource
        {
            get { return (Brush[])GetValue(BrushesSourceProperty); }
            set { SetValue(BrushesSourceProperty, value); }
        }

        public LineCollection Lines
        {
            get { return (LineCollection)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }

        public Line CurrentLine
        {
            get { return (Line)GetValue(CurrentLineProperty); }
            set { SetValue(CurrentLineProperty, value); }
        }

        public LabyrinthDrawer()
        {
            Background = Brushes.Transparent;

            MouseLeftButtonUp += LabyrinthDrawer_MouseLeftButtonUp;
            MouseRightButtonUp += LabyrinthDrawer_MouseRightButtonUp;
        }

        private void LabyrinthDrawer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CurrentLine == null) return;

            Point mousePos = e.GetPosition(this);
            int labPosX = (int)(mousePos.X / ActualWidth * BlockWidthCount);
            int labPosY = (int)(mousePos.Y / ActualHeight * BlockHeightCount);
            Position labPos = new Position(labPosX, labPosY);

            if (CurrentLine.RemoveAll(p => p.X == labPosX && p.Y == labPosY) == 0) CurrentLine.Add(labPos);

            InvalidateVisual();
        }

        private void LabyrinthDrawer_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (CurrentLine == null) return;

            Position pos;
            Point mousePos = e.GetPosition(this);
            int labPosX = (int)(mousePos.X / BlockWidth);
            int labPosY = (int)(mousePos.Y / BlockHeight);

            int posIndex = CurrentLine.FindIndex(p => p.X == labPosX && p.Y == labPosY);

            if (posIndex == -1)
            {
                pos = new Position(labPosX, labPosY);
                CurrentLine.Add(pos);
                posIndex = CurrentLine.Count - 1;
            }
            else pos = CurrentLine.First(p => p.X == labPosX && p.Y == labPosY);

            if (pos.Index != null)
            {
                if (pos.Index + 1 == CurrentLine.Count) pos.Index = null;
                else pos.Index = (pos.Index + 1) % CurrentLine.Count;
            }
            else pos.Index = 0;

            CurrentLine[posIndex] = pos;

            InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen gridPen = new Pen(Brushes.LightGray, 1);
            Pen borderPen = new Pen(Brushes.Black, 2);

            for (int j = 1; j < BlockWidthCount; j++)
            {
                double x = j * BlockWidth;
                drawingContext.DrawLine(gridPen, new Point(x, 0), new Point(x, ActualHeight));
            }

            for (int j = 1; j < BlockHeightCount; j++)
            {
                double y = j * BlockHeight;
                drawingContext.DrawLine(gridPen, new Point(0, y), new Point(ActualWidth, y));
            }

            drawingContext.DrawLine(borderPen, new Point(0, 0), new Point(ActualWidth, 0));
            drawingContext.DrawLine(borderPen, new Point(ActualWidth, 0), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(borderPen, new Point(ActualWidth, ActualHeight), new Point(0, ActualHeight));
            drawingContext.DrawLine(borderPen, new Point(0, ActualHeight), new Point(0, 0));

            if (Lines == null) return;


            double es = ActualHeight / BlockHeightCount * indexTextHeight;

            int i = 0;
            foreach (Line line in Lines)
            {
                double offsetX = (i - (Lines.Count / 2.0) + 0.5) * offsetPerLine * BlockWidth;
                double offsetY = (i - (Lines.Count / 2.0) + 0.5) * offsetPerLine * BlockHeight;
                Pen horizontalPen = new Pen(line.Brush, BlockHeight * thickness);
                Pen verticalPen = new Pen(line.Brush, BlockWidth * thickness);

                int posIndex = i % 4;
                int j = 0;

                foreach (Position pos1 in line)
                {
                    foreach (Position pos2 in line.Skip(j + 1))
                    {
                        Position delta = Position.SubAbs(pos1, pos2);

                        bool isHorizontal = delta.X == 1 && delta.Y == 0;
                        bool isVertical = delta.X == 0 && delta.Y == 1;

                        if (!isHorizontal && !isVertical) continue;

                        double point1X = (pos1.X + 0.5) * BlockWidth + offsetX;
                        double point1Y = (pos1.Y + 0.5) * BlockHeight + offsetY;
                        double point2X = (pos2.X + 0.5) * BlockWidth + offsetX;
                        double point2Y = (pos2.Y + 0.5) * BlockHeight + offsetY;
                        Pen pen = isHorizontal ? horizontalPen : verticalPen;

                        drawingContext.DrawLine(pen, new Point(point1X, point1Y), new Point(point2X, point2Y));
                    }

                    if (pos1.Index == null) continue;

                    FormattedText ftIndex = new FormattedText(pos1.Index.ToString(), ci, fd, tf, es, line.Brush);
                    Point point;

                    double blockX = (pos1.X + indexTextMarginHorizontal) * BlockWidth;
                    double blockY = (pos1.Y + indexTextMarginVertical) * BlockHeight;
                    double blockW = (1 - 2 * indexTextMarginHorizontal) * BlockWidth;
                    double blockH = (1 - 2 * indexTextMarginVertical) * BlockHeight;
                    Rect block = new Rect(blockX, blockY, blockW, blockH);

                    if (posIndex == 0) point = block.TopLeft;
                    else if (posIndex == 1) point = new Point(block.Right - ftIndex.Width, block.Top);
                    else if (posIndex == 2) point = new Point(block.Left, block.Bottom - ftIndex.Height);
                    else point = new Point(block.Right - ftIndex.Width, block.Bottom - ftIndex.Height);

                    drawingContext.DrawText(ftIndex, point);
                }

                i++;
            }
        }
    }
}
