using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace LabyrinthSim
{
    class LabyrinthControl : UserControl
    {
        private const int defaultBlocksWidth = 10, defaultBlocksHeight = 10;
        private const double cornerThickness = 5, wallThickness = 3, searchDistanceToBorder = 0.1;

        private static readonly Pen wallPenOpen = new Pen(Brushes.Transparent, wallThickness),
            wallPenClosed = new Pen(Brushes.Black, wallThickness),
            wallPenVirtual = new Pen(Brushes.LightGray, wallThickness);

        public static readonly DependencyProperty LabyrinthProperty =
            DependencyProperty.Register("Labyrinth", typeof(Labyrinth), typeof(LabyrinthControl),
                new PropertyMetadata(null, new PropertyChangedCallback(OnLabyrinthPropertyChanged)));

        private static void OnLabyrinthPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LabyrinthControl s = sender as LabyrinthControl;
            Labyrinth value = (Labyrinth)e.NewValue;

            if (value != null)
            {
                foreach (SearchView search in s.Searches) search.Labyrinth = value;
            }

            s.HasChanges = true;

        }

        public static readonly DependencyProperty RobotProperty =
            DependencyProperty.Register("Robot", typeof(Robot), typeof(LabyrinthControl),
                new PropertyMetadata(null, new PropertyChangedCallback(OnRobotPropertyChanged)));

        private static void OnRobotPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LabyrinthControl s = sender as LabyrinthControl;

            s.robot = (Robot)e.NewValue;

            if (s.IsViewRobot) s.HasChanges = true;
        }

        public static readonly DependencyProperty IsViewActualProperty =
            DependencyProperty.Register("IsViewActual", typeof(bool), typeof(LabyrinthControl),
                new PropertyMetadata(true, new PropertyChangedCallback(OnIsViewActualPropertyChanged)));

        private static void OnIsViewActualPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as LabyrinthControl).HasChanges = true;
        }

        public static readonly DependencyProperty IsViewVirtualProperty =
            DependencyProperty.Register("IsViewVirtual", typeof(bool), typeof(LabyrinthControl),
                new PropertyMetadata(true, new PropertyChangedCallback(OnIsViewVirtualPropertyChanged)));

        private static void OnIsViewVirtualPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            (sender as LabyrinthControl).HasChanges = true;
        }

        public static readonly DependencyProperty IsViewRobotProperty =
            DependencyProperty.Register("IsViewRobot", typeof(bool), typeof(LabyrinthControl),
                new PropertyMetadata(true, new PropertyChangedCallback(OnIsViewRobotPropertyChanged)));

        private static void OnIsViewRobotPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            LabyrinthControl s = sender as LabyrinthControl;

            if (s.Robot != null) s.HasChanges = true;
        }

        private bool hasChanges;
        private DispatcherTimer timer;
        private Robot robot;

        public Labyrinth Labyrinth
        {
            get { return (Labyrinth)GetValue(LabyrinthProperty); }
            set { SetValue(LabyrinthProperty, value); }
        }

        public int BlocksWidth { get { return Labyrinth?.Width ?? 0; } }

        public int BlocksHeight { get { return Labyrinth?.Height ?? 0; } }

        public Robot Robot
        {
            get { return robot; }
            set { SetValue(RobotProperty, value); }
        }

        public bool IsViewActual
        {
            get { return (bool)GetValue(IsViewActualProperty); }
            set { SetValue(IsViewActualProperty, value); }
        }

        public bool IsViewVirtual
        {
            get { return (bool)GetValue(IsViewVirtualProperty); }
            set { SetValue(IsViewVirtualProperty, value); }
        }

        public bool IsViewRobot
        {
            get { return (bool)GetValue(IsViewRobotProperty); }
            set { SetValue(IsViewRobotProperty, value); }
        }

        public ObservableCollection<SearchView> Searches { get; private set; }

        public bool HasChanges
        {
            get
            {
                return hasChanges || (Labyrinth?.HasChanges ?? false) ||
                    (Robot?.HasChanges ?? false) || Searches.Any(s => s.HasChanges);
            }

            set
            {
                if (value == HasChanges) return;

                hasChanges = value;

                if (!value)
                {
                    if (Labyrinth != null) Labyrinth.HasChanges = value;
                    if (Robot != null) Robot.HasChanges = value;

                    foreach (SearchView search in Searches) search.HasChanges = value;
                }
            }
        }

        public LabyrinthControl()
        {
            Searches = new ObservableCollection<SearchView>();
            Searches.CollectionChanged += Searches_CollectionChanged;

            timer = new DispatcherTimer(TimeSpan.FromMilliseconds(20),
                DispatcherPriority.Normal, RaiseOnRender, Dispatcher);
            timer.Start();
        }

        private void Searches_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Labyrinth == null) return;

            foreach (SearchView search in e.NewItems?.OfType<SearchView>() ?? Enumerable.Empty<SearchView>())
            {
                search.Labyrinth = Labyrinth;
            }
        }

        private void RaiseOnRender(object sender, EventArgs e)
        {
            if (HasChanges) InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            foreach (SearchView search in Searches)
            {
                search?.Route?.BlockAddWaiter?.UpdateCount();
                search?.Route?.TryAddWaiter?.UpdateCount();
            }

            Robot?.MoveWaiter?.UpdateCount();
            Robot?.RobotSearchWaiter?.UpdateCount();
            Robot?.TargetWaiter?.UpdateCount();

            HasChanges = false;

            //System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();

            Rect controlRect = new Rect(new Size(ActualWidth, ActualHeight));
            drawingContext.DrawRectangle(Brushes.Transparent, new Pen(), controlRect);

            if (BlocksWidth == 0 || BlocksHeight == 0) return;

            RenderTarget(drawingContext);

            SearchView[] visibleSearches = GetVisibleSearches().ToArray();
            Vector vectBlock = new Vector(ActualWidth / BlocksWidth, ActualHeight / BlocksHeight);
            double thicknissFactor = (1.0 - 2 * searchDistanceToBorder) / visibleSearches.Length;

            for (int i = 0; i < visibleSearches.Length; i++)
            {
                double offsetFactor = i * thicknissFactor + searchDistanceToBorder;

                visibleSearches[i].Draw(drawingContext, vectBlock, offsetFactor, thicknissFactor);
            }

            if (IsViewRobot) Robot?.Draw(drawingContext, new Vector(ActualWidth / BlocksWidth, ActualHeight / BlocksHeight));

            RenderWalls(drawingContext);
            RenderBorders(drawingContext);
            RenderCorners(drawingContext);

            base.OnRender(drawingContext);

            //System.Diagnostics.Debug.WriteLine("RenderEnded: " + sw.ElapsedMilliseconds);
        }

        private IEnumerable<SearchView> GetVisibleSearches()
        {
            foreach (SearchView search in Searches)
            {
                if (!search.IsVisible || search.Route == null) continue;
                if (search.Route.PossibleLength > 0 || search.Route.CurrentLength > 0) yield return search;
            }
        }

        private void RenderTarget(DrawingContext drawingContext)
        {
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            int x = Labyrinth.Target.TopLeft.X, y = Labyrinth.Target.TopLeft.Y;

            Rect rect = new Rect(widthFactor * x, heightFactor * y, widthFactor * 2, heightFactor * 2);
            drawingContext.DrawRectangle(Brushes.Yellow, new Pen(), rect);
        }

        private void RenderWalls(DrawingContext drawingContext)
        {
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            for (int i = 0; i < Labyrinth.H.Width; i++)
            {
                for (int j = 0; j < Labyrinth.H.Height; j++)
                {
                    int relationValue = Labyrinth.H[i, j];

                    if (IsViewActual && IsViewVirtual)
                    {
                        Point actual1 = new Point(widthFactor * (i + 1), heightFactor * j);
                        Point actual2 = new Point(widthFactor * (i + 1), heightFactor * (j + 0.5));
                        Point unkown1 = new Point(widthFactor * (i + 1), heightFactor * (j + 0.5));
                        Point unkown2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue % 2), actual1, actual2);
                        drawingContext.DrawLine(GetPen(relationValue), unkown1, unkown2);
                    }
                    else if (IsViewActual)
                    {
                        Point actual1 = new Point(widthFactor * (i + 1), heightFactor * j);
                        Point actual2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue % 2), actual1, actual2);
                    }
                    else if (IsViewVirtual)
                    {
                        Point unkown1 = new Point(widthFactor * (i + 1), heightFactor * j);
                        Point unkown2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue), unkown1, unkown2);
                    }
                }
            }

            for (int i = 0; i < Labyrinth.V.Width; i++)
            {
                for (int j = 0; j < Labyrinth.V.Height; j++)
                {
                    int relationValue = Labyrinth.V[i, j];

                    if (IsViewActual && IsViewVirtual)
                    {
                        Point actual1 = new Point(widthFactor * i, heightFactor * (j + 1));
                        Point actual2 = new Point(widthFactor * (i + 0.5), heightFactor * (j + 1));
                        Point unkown1 = new Point(widthFactor * (i + 0.5), heightFactor * (j + 1));
                        Point unkown2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue % 2), actual1, actual2);
                        drawingContext.DrawLine(GetPen(relationValue), unkown1, unkown2);
                    }
                    else if (IsViewActual)
                    {
                        Point actual1 = new Point(widthFactor * i, heightFactor * (j + 1));
                        Point actual2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue % 2), actual1, actual2);
                    }
                    else if (IsViewVirtual)
                    {
                        Point unkown1 = new Point(widthFactor * i, heightFactor * (j + 1));
                        Point unkown2 = new Point(widthFactor * (i + 1), heightFactor * (j + 1));

                        drawingContext.DrawLine(GetPen(relationValue), unkown1, unkown2);
                    }
                }
            }
        }

        private void RenderBorders(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(wallPenClosed, new Point(0, 0), new Point(0, ActualHeight));
            drawingContext.DrawLine(wallPenClosed, new Point(0, ActualHeight), new Point(ActualWidth, ActualHeight));
            drawingContext.DrawLine(wallPenClosed, new Point(ActualWidth, ActualHeight), new Point(ActualWidth, 0));
            drawingContext.DrawLine(wallPenClosed, new Point(ActualWidth, 0), new Point(0, 0));
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

        private Pen GetPen(int realtionValue)
        {
            switch (realtionValue)
            {
                case 0:
                    return wallPenOpen;

                case 1:
                    return wallPenClosed;

                default:
                    return wallPenVirtual;
            }
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
            double widthFactor = ActualWidth / BlocksWidth;
            double heightFactor = ActualHeight / BlocksHeight;

            return new Point(widthFactor * (block.X + 0.5), heightFactor * (block.Y + 0.5));
        }
    }
}
