using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace LabyrinthSim
{
    class LabyrinthControl : UserControl
    {
        private const int defaultBlocksWidth = 10, defaultBlocksHeight = 10;
        private const double cornerThickness = 5, wallThickness = 3;

        public static readonly DependencyProperty BlocksWidthProperty =
            DependencyProperty.Register("BlocksWidth", typeof(int), typeof(LabyrinthControl),
                new PropertyMetadata(defaultBlocksWidth, new PropertyChangedCallback(OnBlocksWidthPropertyChanged)));

        private static void OnBlocksWidthPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthControl)sender;
            var value = (int)e.NewValue;

            if (s.Robot.ActualLabyrinth.Width == value) return;
            s.Robot = new Robot(Labyrinth.GetActual(s.BlocksWidth, s.BlocksHeight));
        }

        public static readonly DependencyProperty BlocksHeightProperty =
            DependencyProperty.Register("BlocksHeight", typeof(int), typeof(LabyrinthControl),
                new PropertyMetadata(defaultBlocksHeight, new PropertyChangedCallback(OnBlocksHeightPropertyChanged)));

        private static void OnBlocksHeightPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthControl)sender;
            var value = (int)e.NewValue;

            if (s.Robot.ActualLabyrinth.Height == value) return;
            s.Robot = new Robot(Labyrinth.GetActual(s.BlocksWidth, s.BlocksHeight));
        }

        public static readonly DependencyProperty RobotProperty =
            DependencyProperty.Register("Robot", typeof(Robot), typeof(LabyrinthControl),
                new PropertyMetadata(new Robot(Labyrinth.GetActual(defaultBlocksWidth, defaultBlocksHeight)),
                    new PropertyChangedCallback(OnRobotPropertyChanged)));

        private static void OnRobotPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthControl)sender;
            var value = (Robot)e.NewValue;

            s.robot = value;

            value.ActualPossibleRoute.Wait = s.Wait;
            value.LearnMaybeRoute.Wait = s.Wait;
            value.LearnPossibleRoute.Wait = s.Wait;
            value.LearnRobotRoute.Wait = s.Wait;

            //value.SearchActualRoute();

            s.BlocksWidth = value.ActualLabyrinth.Width;
            s.BlocksHeight = value.ActualLabyrinth.Height;

            s.InvalidateVisual();
        }

        public static readonly DependencyProperty WaitProperty =
            DependencyProperty.Register("Wait", typeof(int), typeof(LabyrinthControl),
                new PropertyMetadata(10, new PropertyChangedCallback(OnWaitPropertyChanged)));

        private static void OnWaitPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var s = (LabyrinthControl)sender;
            var value = (int)e.NewValue;

            s.Robot.ActualPossibleRoute.Wait = value;
            s.Robot.LearnMaybeRoute.Wait = value;
            s.Robot.LearnPossibleRoute.Wait = value;
            s.Robot.LearnRobotRoute.Wait = value;
        }

        private Robot robot;
        private DispatcherTimer timer;

        public int BlocksWidth
        {
            get { return (int)GetValue(BlocksWidthProperty); }
            set { SetValue(BlocksWidthProperty, value); }
        }

        public int BlocksHeight
        {
            get { return (int)GetValue(BlocksHeightProperty); }
            set { SetValue(BlocksHeightProperty, value); }
        }

        public Robot Robot
        {
            get { return robot; }
            set { SetValue(RobotProperty, value); }
        }

        public int Wait
        {
            get { return (int)GetValue(WaitProperty); }
            set { SetValue(WaitProperty, value); }
        }

        public LabyrinthControl()
        {
            robot = (Robot)GetValue(RobotProperty);

            timer = new DispatcherTimer(TimeSpan.FromMilliseconds(20), DispatcherPriority.Normal, RaiseOnRender, Dispatcher);
            timer.Start();

        }

        private void RaiseOnRender(object sender, EventArgs e)
        {
            InvalidateVisual();
        }

        public void LabyrinthControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);

            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;
            position = new Point(position.X / widthFactor - 0.5, position.Y / heightFactor - 0.5);

            Blockrelation relation = Robot.ActualLabyrinth[position.X, position.Y];

            if (relation.Relation == RelationType.Close) relation.Open();
            else if (relation.Relation == RelationType.Open) relation.Close();

            InvalidateVisual();
            Robot = new Robot(Robot.ActualLabyrinth);
        }

        public void LabyrinthControl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(this);

            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            int x = Convert.ToInt32(Math.Round(position.X / widthFactor - 0.5));
            int y = Convert.ToInt32(Math.Round(position.Y / heightFactor - 0.5));

            Robot.ActualLabyrinth.Target = new SquareTarget(Robot.ActualLabyrinth[x, y]);
            InvalidateVisual();

            Robot = new Robot(Robot.ActualLabyrinth);
        }

        private double Distance(Point point, Blockrelation relation)
        {
            if (relation.Block2 == null) return double.MaxValue;

            double relationX = (relation.Block1.X + relation.Block2.X + 1) / 2.0;
            double relationY = (relation.Block1.Y + relation.Block2.Y + 1) / 2.0;

            return Math.Sqrt(Math.Pow(relationX - point.X, 2) + Math.Pow(relationY - point.Y, 2));
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (BlocksWidth == 0 || BlocksHeight == 0) return;

            RenderTarget(drawingContext);
            RenderWalls(drawingContext);
            RenderBorders(drawingContext);
            RenderCorners(drawingContext);
            RenderActualPossibleRoute(drawingContext);
            RenderMaybeRoute(drawingContext);
            RenderPossibleRoute(drawingContext);
            RenderRobotRoute(drawingContext);
            RenderRobot(drawingContext);

            base.OnRender(drawingContext);
        }

        private void RenderTarget(DrawingContext drawingContext)
        {
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            int x, y;
            Robot.LearnLabyrinth.GetPosition(Robot.LearnLabyrinth.Target.TopLeft, out x, out y);

            Rect rect = new Rect(widthFactor * x, heightFactor * y, widthFactor * 2, heightFactor * 2);
            drawingContext.DrawRectangle(Brushes.Yellow, new Pen(), rect);
        }

        private void RenderWalls(DrawingContext drawingContext)
        {
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            for (int i = 0; i < BlocksWidth; i++)
            {
                for (int j = 0; j < BlocksHeight; j++)
                {
                    Pen actualBottomPen = GetPen(Robot.ActualLabyrinth[i, j].Bottom.Relation);
                    Pen actualRightPen = GetPen(Robot.ActualLabyrinth[i, j].Right.Relation);
                    Pen robotBottomPen = GetPen(Robot.LearnLabyrinth[i, j].Bottom.Relation);
                    Pen robotRightPen = GetPen(Robot.LearnLabyrinth[i, j].Right.Relation);

                    Point bottomLeft = new Point(widthFactor * i, heightFactor * (j + 1));
                    Point bottomMiddle = new Point(widthFactor * (i + 0.5), heightFactor * (j + 1));
                    Point bottomRight = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                    Point rightTop = new Point(widthFactor * (i + 1), heightFactor * j);
                    Point rightMiddle = new Point(widthFactor * (i + 1), heightFactor * (j + 0.5));
                    Point rightBottom = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                    drawingContext.DrawLine(actualBottomPen, bottomLeft, bottomMiddle);
                    drawingContext.DrawLine(actualRightPen, rightBottom, rightMiddle);

                    drawingContext.DrawLine(robotBottomPen, bottomRight, bottomMiddle);
                    drawingContext.DrawLine(robotRightPen, rightTop, rightMiddle);
                }
            }
        }

        private void RenderBorders(DrawingContext drawingContext)
        {
            Pen closePen = GetPen(RelationType.Close);

            drawingContext.DrawLine(closePen, new Point(0, 0), new Point(0, ActualHeight));
            drawingContext.DrawLine(closePen, new Point(0, ActualHeight), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(closePen, new Point(ActualWidth, ActualHeight), new Point(ActualWidth, 0));
            drawingContext.DrawLine(closePen, new Point(ActualWidth, 0), new Point(0, 0));
        }

        private void RenderCorners(DrawingContext drawingContext)
        {
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            for (int i = 0; i <= BlocksWidth; i++)
            {
                for (int j = 0; j <= BlocksHeight; j++)
                {
                    Rect rect = new Rect(widthFactor * i - cornerThickness / 2,
                        heightFactor * j - cornerThickness / 2, cornerThickness, cornerThickness);
                    drawingContext.DrawRectangle(Brushes.Black, new Pen(), rect);
                }
            }
        }

        private Pen GetPen(RelationType type)
        {
            switch (type)
            {
                case RelationType.Unkown:
                    return new Pen(Brushes.Gray, wallThickness);

                case RelationType.Open:
                    return new Pen(Brushes.Transparent, wallThickness);

                default:
                    return new Pen(Brushes.Black, wallThickness);
            }
        }

        private void RenderPossibleRoute(DrawingContext drawingContext)
        {
            if (Robot.LearnPossibleRoute == null) return;
            RenderRoute(drawingContext, Robot.LearnPossibleRoute, Robot.LearnLabyrinth, new Pen(Brushes.Green, 7));
            RenderRoute(drawingContext, Robot.LearnPossibleRoute.GetCurrent(), Robot.LearnLabyrinth, new Pen(Brushes.LightGreen, 5));
        }

        private void RenderMaybeRoute(DrawingContext drawingContext)
        {
            if (Robot.LearnMaybeRoute == null) return;
            RenderRoute(drawingContext, Robot.LearnMaybeRoute, Robot.LearnLabyrinth, new Pen(Brushes.Orange, 10));
            RenderRoute(drawingContext, Robot.LearnMaybeRoute.GetCurrent(), Robot.LearnLabyrinth, new Pen(Brushes.LightPink, 8));
        }

        private void RenderRobotRoute(DrawingContext drawingContext)
        {
            if (Robot.LearnRobotRoute == null) return;
            RenderRoute(drawingContext, Robot.LearnRobotRoute, Robot.LearnLabyrinth, new Pen(Brushes.Black, 4));
            RenderRoute(drawingContext, Robot.LearnRobotRoute.GetCurrent(), Robot.LearnLabyrinth, new Pen(Brushes.Gray, 3));
        }

        private void RenderActualPossibleRoute(DrawingContext drawingContext)
        {
            if (Robot.ActualPossibleRoute == null) return;
            RenderRoute(drawingContext, Robot.ActualPossibleRoute, Robot.ActualLabyrinth, new Pen(Brushes.DarkBlue, 15));
            RenderRoute(drawingContext, Robot.ActualPossibleRoute.GetCurrent(), Robot.ActualLabyrinth, new Pen(Brushes.Blue, 12));
        }

        private void RenderRoute(DrawingContext drawingContext, IEnumerable<Block> route, Labyrinth labyrinth, Pen pen)
        {
            var array = route.ToArray();
            if (array.Length <= 1) return;

            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            Point previousPoint = GetMiddle(labyrinth, array.First());

            foreach (Block block in array.Skip(1))
            {
                Point currentPoint = GetMiddle(labyrinth, block);
                drawingContext.DrawLine(pen, previousPoint, currentPoint);
                previousPoint = currentPoint;
            }
        }

        private Point GetMiddle(Labyrinth labyrinth, Block block)
        {
            int x, y;
            labyrinth.GetPosition(block, out x, out y);

            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            return new Point(widthFactor * (x + 0.5), heightFactor * (y + 0.5));
        }

        private void RenderRobot(DrawingContext drawingContext)
        {
            const double lenghtFactor = 0.7, widthFactor = 0.5, robotLinesThickness = 3;

            Point head = new Point(), backLeft = new Point(), backRight = new Point();

            switch (Robot.Direction)
            {
                case LookDirection.Top:
                    head = new Point(0, -lenghtFactor / 2);
                    backLeft = new Point(-widthFactor / 2, lenghtFactor / 2);
                    backRight = new Point(widthFactor / 2, lenghtFactor / 2);
                    break;

                case LookDirection.Bottom:
                    head = new Point(0, lenghtFactor / 2);
                    backLeft = new Point(widthFactor / 2, -lenghtFactor / 2);
                    backRight = new Point(-widthFactor / 2, -lenghtFactor / 2);
                    break;

                case LookDirection.Right:
                    head = new Point(lenghtFactor / 2, 0);
                    backLeft = new Point(-lenghtFactor / 2, -widthFactor / 2);
                    backRight = new Point(-lenghtFactor / 2, widthFactor / 2);
                    break;

                case LookDirection.Left:
                    head = new Point(-lenghtFactor / 2, 0);
                    backLeft = new Point(lenghtFactor / 2, widthFactor / 2);
                    backRight = new Point(lenghtFactor / 2, -widthFactor / 2);
                    break;
            }

            head = GetOffsetRobot(head);
            backLeft = GetOffsetRobot(backLeft);
            backRight = GetOffsetRobot(backRight);

            Pen pen = new Pen(Brushes.Blue, robotLinesThickness);
            drawingContext.DrawLine(pen, head, backLeft);
            drawingContext.DrawLine(pen, backLeft, backRight);
            drawingContext.DrawLine(pen, backRight, head);
        }

        private Point GetOffsetRobot(Point point)
        {
            int x, y;
            Robot.LearnLabyrinth.GetPosition(Robot.Position, out x, out y);

            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            return new Point(widthFactor * (x + 0.5 + point.X), heightFactor * (y + 0.5 + point.Y));
        }
    }
}
