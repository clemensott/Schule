using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LabyrinthSim
{
    enum SearchType { Direct, Min, BruteForce }

    enum RelationInterpreterType { Actual, BestCase, WorstCase }

    class SearchView : ISearcher, INotifyPropertyChanged
    {
        private static Pen nonPen = new Pen();

        private Search route;
        private Color possibleRouteColor, currentRouteColor, nextBlockColor, textColor, startColor, targetColor;
        private RelationInterpreterType interpreterType;
        private SearchType searchType;
        private bool isVisible, isDistancesVisable, isCountsVisable, isLengthVisable, isSearching,
            isLabyrinthTarget, hasChanges, isVisibilitiesExpanded, isBlockAddExpanded, isTryAddExpanded,
            isSearchTypeExpanded, isInterpreterTypeExpanded, isBrushesExpanded, isStartExpanded, isTargetExpanded;
        private Block start;
        private ITarget target;
        private Labyrinth labyrinth;
        private Waiter blockAddWaiter, tryAddWaiter;

        public Search Route
        {
            get { return route; }
            private set
            {
                if (value == route) return;

                route = value;
                OnPropertyChanged("Route");

                HasChanges = true;
            }
        }

        public Color PossibleRouteColor
        {
            get { return possibleRouteColor; }
            set
            {
                if (value == possibleRouteColor) return;

                possibleRouteColor = value;
                PossibleRouteBrush = ToBrush(possibleRouteColor);

                OnPropertyChanged("PossibleRouteColor");
                OnPropertyChanged("PossibleRouteBrush");

                HasChanges = true;
            }
        }

        public Brush PossibleRouteBrush { get; private set; }

        public Color CurrentRouteColor
        {
            get { return currentRouteColor; }
            set
            {
                if (value == currentRouteColor) return;

                currentRouteColor = value;
                CurrentRouteBrush = ToBrush(currentRouteColor);

                OnPropertyChanged("CurrentRouteColor");
                OnPropertyChanged("CurrentRouteBrush");

                HasChanges = true;
            }
        }

        public Brush CurrentRouteBrush { get; private set; }

        public Color NextBlockColor
        {
            get { return nextBlockColor; }
            set
            {
                if (value == nextBlockColor) return;

                nextBlockColor = value;
                NextBlockBrush = ToBrush(nextBlockColor);

                OnPropertyChanged("CurrentBlockColor");
                OnPropertyChanged("CurrentBlockBrush");

                HasChanges = true;
            }
        }

        public Brush NextBlockBrush { get; private set; }

        public Color TextColor
        {
            get { return textColor; }
            set
            {
                if (value == textColor) return;

                textColor = value;
                TextBrush = ToBrush(textColor);

                OnPropertyChanged("CurrentBlockColor");
                OnPropertyChanged("CurrentBlockBrush");

                HasChanges = true;
            }
        }

        public Brush TextBrush { get; private set; }

        public Color StartColor
        {
            get { return startColor; }
            set
            {
                if (value == startColor) return;

                startColor = value;
                StartBrush = ToBrush(startColor);

                OnPropertyChanged("StartColor");
                OnPropertyChanged("StartBrush");

                HasChanges = true;
            }
        }

        public Brush StartBrush { get; private set; }

        public Color TargetColor
        {
            get { return targetColor; }
            set
            {
                if (value == targetColor) return;

                targetColor = value;
                TargetBrush = ToBrush(targetColor);

                OnPropertyChanged("TargetColor");
                OnPropertyChanged("TargetBrush");

                HasChanges = true;
            }
        }

        public Brush TargetBrush { get; private set; }

        public RelationInterpreterType InterpreterType
        {
            get { return interpreterType; }
            set
            {
                if (value == interpreterType) return;

                interpreterType = value;
                OnPropertyChanged("InterpreterType");
            }
        }

        public IRelationInterpreter Interpreter
        {
            get
            {
                switch (InterpreterType)
                {
                    case RelationInterpreterType.Actual:
                        return new ActualInterpreter();

                    case RelationInterpreterType.BestCase:
                        return new BestCaseInterpreter();

                    case RelationInterpreterType.WorstCase:
                        return new WorstCaseInterpreter();
                }

                return new ActualInterpreter();
            }
        }

        public SearchType SearchType
        {
            get { return searchType; }
            set
            {
                if (value == searchType) return;

                searchType = value;
                OnPropertyChanged("SearchType");
            }
        }

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                if (value == isVisible) return;

                isVisible = value;
                OnPropertyChanged("IsVisible");

                hasChanges = true;
            }
        }

        public bool IsDistancesVisable
        {
            get { return isDistancesVisable; }
            set
            {
                if (value == isDistancesVisable) return;

                isDistancesVisable = value;
                OnPropertyChanged("IsDistancesVisable");

                HasChanges = true;
            }
        }

        public bool IsCountsVisable
        {
            get { return isCountsVisable; }
            set
            {
                if (value == isCountsVisable) return;

                isCountsVisable = value;
                OnPropertyChanged("IsCountsVisable");

                HasChanges = true;
            }
        }

        public bool IsLengthVisable
        {
            get { return isLengthVisable; }
            set
            {
                if (value == isLengthVisable) return;

                isLengthVisable = value;
                OnPropertyChanged("IsLengthVisable");

                HasChanges = true;
            }
        }

        public bool IsSearching
        {
            get { return isSearching; }
            private set
            {
                if (value == isSearching) return;

                isSearching = value;
                OnPropertyChanged("IsSearching");
            }
        }

        public Block Start
        {
            get { return start; }
            set
            {
                if (value == start) return;

                start = value;
                OnPropertyChanged("Start");
            }
        }

        public bool IsLabyrinthTarget
        {
            get { return isLabyrinthTarget; }
            set
            {
                if (value == isLabyrinthTarget) return;

                isLabyrinthTarget = value;
                OnPropertyChanged("IsLabyrinthTarget");
            }
        }

        public ITarget Target
        {
            get { return target; }
            set
            {
                if (value == target) return;

                target = value;
                OnPropertyChanged("Target");
            }
        }

        public Labyrinth Labyrinth
        {
            get { return labyrinth; }
            set
            {
                if (value == labyrinth) return;

                labyrinth = value;
                OnPropertyChanged("Labyrinth");
            }
        }

        public Waiter BlockAddWaiter
        {
            get { return blockAddWaiter; }
            set
            {
                if (value == blockAddWaiter) return;

                blockAddWaiter = value;
                OnPropertyChanged("BlockAddWaiter");
            }
        }

        public Waiter TryAddWaiter
        {
            get { return tryAddWaiter; }
            set
            {
                if (value == tryAddWaiter) return;

                tryAddWaiter = value;
                OnPropertyChanged("TryAddWaiter");
            }
        }

        public bool IsVisibilitiesExpanded
        {
            get { return isVisibilitiesExpanded; }
            set
            {
                if (value == isVisibilitiesExpanded) return;

                isVisibilitiesExpanded = value;
                OnPropertyChanged("IsVisibilitiesExpanded");
            }
        }

        public bool IsBlockAddExpanded
        {
            get { return isBlockAddExpanded; }
            set
            {
                if (value == isBlockAddExpanded) return;

                isBlockAddExpanded = value;
                OnPropertyChanged("IsBlockAddExpanded");
            }
        }

        public bool IsTryAddExpanded
        {
            get { return isTryAddExpanded; }
            set
            {
                if (value == isTryAddExpanded) return;

                isTryAddExpanded = value;
                OnPropertyChanged("IsTryAddExpanded");
            }
        }

        public bool IsSearchTypeExpanded
        {
            get { return isSearchTypeExpanded; }
            set
            {
                if (value == isSearchTypeExpanded) return;

                isSearchTypeExpanded = value;
                OnPropertyChanged("IsSearchTypeExpanded");
            }
        }

        public bool IsInterpreterTypeExpanded
        {
            get { return isInterpreterTypeExpanded; }
            set
            {
                if (value == isInterpreterTypeExpanded) return;

                isInterpreterTypeExpanded = value;
                OnPropertyChanged("IsInterpreterTypeExpanded");
            }
        }

        public bool IsBrushesExpanded
        {
            get { return isBrushesExpanded; }
            set
            {
                if (value == isBrushesExpanded) return;

                isBrushesExpanded = value;
                OnPropertyChanged("IsBrushesExpanded");
            }
        }

        public bool IsStartExpanded
        {
            get { return isStartExpanded; }
            set
            {
                if (value == isStartExpanded) return;

                isStartExpanded = value;
                OnPropertyChanged("IsStartExpanded");
            }
        }

        public bool IsTargetExpanded
        {
            get { return isTargetExpanded; }
            set
            {
                if (value == isTargetExpanded) return;

                isTargetExpanded = value;
                OnPropertyChanged("IsTargetExpanded");
            }
        }

        public Task SearchTask { get; private set; }

        public bool HasChanges
        {
            get { return hasChanges || ((Route?.HasChanges ?? false) && IsVisible); }
            set
            {
                value = value && IsVisible;

                if (value == HasChanges) return;

                hasChanges = value;

                if (!value && Route != null) Route.HasChanges = value;
            }
        }

        public SearchView()
        {
            StartColor = Colors.GreenYellow;
            TargetColor = Colors.Yellow;
            PossibleRouteColor = Colors.LightGreen;
            CurrentRouteColor = Colors.Orange;
            NextBlockColor = Colors.Pink;
            TextColor = Colors.DarkGoldenrod;

            InterpreterType = RelationInterpreterType.Actual;

            Start = Block.Origin;
        }

        public Task BeginSearch()
        {
            if (IsSearching) return SearchTask;
            IsSearching = true;

            ITarget target = (IsLabyrinthTarget ? Labyrinth.Target : Target);
            if (target == null) target = new SquareTarget(new Block(Labyrinth.Width / 2, Labyrinth.Height / 2));

            if (SearchType == SearchType.BruteForce) Route = new BruteForceSearch(Labyrinth, target, Interpreter);
            else if (SearchType == SearchType.Direct) Route = new DirectSearch(Labyrinth, target, Interpreter);
            else Route = new MinSearch(Labyrinth, target, Interpreter);

            if (BlockAddWaiter != null) BlockAddWaiter.Count = 0u;
            if (TryAddWaiter != null) TryAddWaiter.Count = 0u;

            Route.BlockAddWaiter = BlockAddWaiter;
            Route.TryAddWaiter = TryAddWaiter;

            return SearchTask = Task.Factory.StartNew(new Action(Search));
        }

        private void Search()
        {
            Route.Add(Start);

            Route.CurrentLength = 0;
            Route.Next = Block.None;
            IsSearching = false;
        }

        public void CancelSearch()
        {
            if (!IsSearching || Route == null) return;

            Route.Canceled = true;
            Route.BlockAddWaiter?.PulseAll();
            Route.TryAddWaiter?.PulseAll();

            while (IsSearching) Task.Delay(20).Wait(20);
        }

        public void Draw(DrawingContext dc, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            Block startBlock = Route?.Any() == true ? Route.First() : Block.None;
            DrawBlock(dc, blockVect, offsetFactor, thicknessFactor, startBlock, StartBrush);

            foreach (Block targetBlock in Route?.Target.GetBlocks() ?? Enumerable.Empty<Block>())
            {
                DrawBlock(dc, blockVect, offsetFactor, thicknessFactor, targetBlock, TargetBrush);
            }

            DrawRoutes(dc, blockVect, offsetFactor, thicknessFactor);
            DrawBlock(dc, blockVect, offsetFactor, thicknessFactor, Route.Next, NextBlockBrush);

            DrawLengths(dc, blockVect, offsetFactor, thicknessFactor);
            DrawDistances(dc, blockVect, offsetFactor, thicknessFactor);
            DrawCounts(dc, blockVect, offsetFactor, thicknessFactor);
        }

        private void DrawBlock(DrawingContext dc, Vector blockVect,
            double offsetFactor, double thicknessFactor, Block block, Brush brush)
        {
            if (block.X < 0) return;

            Rect rect = GetRect(block, block, blockVect, offsetFactor, thicknessFactor);

            dc.DrawRectangle(brush, nonPen, rect);
        }

        private void DrawRoutes(DrawingContext dc, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            if (Route.CurrentLength > 0 && Route.PossibleLength > 0)
            {
                Vector cornerOffset = blockVect * (thicknessFactor / 4.0);

                Block[] possible = Route.GetPossible().ToArray();
                Block[] current = Route.GetCurrent().ToArray();

                if (possible.Length > 0)
                {
                    DrawRoute(dc, PossibleRouteBrush, possible, blockVect, offsetFactor, thicknessFactor / 2.0);
                }

                if (current.Length > 0)
                {
                    double furtherOffset = offsetFactor + thicknessFactor / 2.0;

                    DrawRoute(dc, CurrentRouteBrush, current, blockVect, furtherOffset, thicknessFactor / 2.0);
                }
            }
            else if (Route.CurrentLength > 0)
            {
                Block[] current = Route.GetCurrent().ToArray();

                if (current.Length > 0)
                {
                    DrawRoute(dc, CurrentRouteBrush, current, blockVect, offsetFactor, thicknessFactor);
                }
            }
            else if (Route.PossibleLength > 0)
            {
                Block[] current = Route.GetPossible().ToArray();

                if (current.Length > 0)
                {
                    DrawRoute(dc, PossibleRouteBrush, current, blockVect, offsetFactor, thicknessFactor);
                }
            }
            else { }
        }

        private void DrawRoute(DrawingContext dc, Brush brush, Block[] blocks, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            Block preBlock = blocks[0];

            foreach (Block curBlock in blocks)
            {
                Rect rect = GetRect(preBlock, curBlock, blockVect, offsetFactor, thicknessFactor);

                dc.DrawRectangle(brush, nonPen, rect);

                preBlock = curBlock;
            }
        }

        private Rect GetRect(Block block1, Block block2, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            Point topLeft, bottomRight;

            if (block1.X < block2.X || block1.Y < block2.Y)
            {
                topLeft = GetTopLeft(block1, blockVect, offsetFactor);
                bottomRight = GetBottomRight(block2, blockVect, offsetFactor, thicknessFactor);
            }
            else
            {
                topLeft = GetTopLeft(block2, blockVect, offsetFactor);
                bottomRight = GetBottomRight(block1, blockVect, offsetFactor, thicknessFactor);
            }

            return new Rect(topLeft, bottomRight);
        }

        private Point GetTopLeft(Block block, Vector blockVect, double offsetFactor)
        {
            double x = (block.X + offsetFactor) * blockVect.X;
            double y = (block.Y + offsetFactor) * blockVect.Y;

            return new Point(x, y);
        }

        private Point GetTopRight(Block block, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            double x = (block.X + offsetFactor + thicknessFactor) * blockVect.X;
            double y = (block.Y + offsetFactor) * blockVect.Y;

            return new Point(x, y);
        }

        private Point GetBottomLeft(Block block, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            double x = (block.X + offsetFactor) * blockVect.X;
            double y = (block.Y + offsetFactor + thicknessFactor) * blockVect.Y;

            return new Point(x, y);
        }

        private Point GetBottomRight(Block block, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            double x = (block.X + offsetFactor + thicknessFactor) * blockVect.X;
            double y = (block.Y + offsetFactor + thicknessFactor) * blockVect.Y;

            return new Point(x, y);

        }

        private void DrawLengths(DrawingContext dc, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            // BottomLeft
            if (!IsLengthVisable) return;

            double textHeight = blockVect.Y * thicknessFactor / 2.0;

            if (Route.CurrentLength > 0)
            {
                int length = Route.CurrentLength;
                FormattedText lengthFText = GetFormattedText(length, textHeight, TextBrush);
                Block last = Route.GetCurrent().Last();

                Vector textOffset = new Vector(0, lengthFText.Height);
                Point point = GetBottomLeft(last, blockVect, offsetFactor, thicknessFactor) - textOffset;

                dc.DrawText(lengthFText, point);
            }

            if (Route.PossibleLength > 0)
            {
                int length = Route.PossibleLength;
                FormattedText lengthFText = GetFormattedText(length, textHeight, TextBrush);
                Block last = Route.GetPossible().Last();

                Vector textOffset = new Vector(0, lengthFText.Height);
                Point point = GetBottomLeft(last, blockVect, offsetFactor, thicknessFactor) - textOffset;

                dc.DrawText(lengthFText, point);
            }

            if (Route.Next != Block.None)
            {
                int length = Route.Target.MinDistance(Route.Next);
                FormattedText lengthFText = GetFormattedText(length, textHeight, TextBrush);

                Vector textOffset = new Vector(0, lengthFText.Height);
                Point point = GetBottomLeft(Route.Next, blockVect, offsetFactor, thicknessFactor) - textOffset;

                dc.DrawText(lengthFText, point);
            }
        }

        private void DrawDistances(DrawingContext dc, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            // TopRight
            if (!IsDistancesVisable) return;

            double textHeight = blockVect.Y * thicknessFactor / 2.0;
            int maxDistance = Route.Labyrinth.Width * Route.Labyrinth.Height;

            for (int i = 0; i < Route.Distances.GetLength(0); i++)
            {
                for (int j = 0; j < Route.Distances.GetLength(1); j++)
                {
                    int distance = Route.Distances[i, j];
                    if (distance >= maxDistance) continue;

                    string distanceText = distance.ToString();
                    FormattedText distanceFText = GetFormattedText(distance, textHeight, TextBrush);
                    Vector textOffset = new Vector(distanceFText.Width, 0);
                    Point point = GetTopRight(new Block(i, j), blockVect, offsetFactor, thicknessFactor) - textOffset;

                    dc.DrawText(distanceFText, point);
                }
            }
        }

        private void DrawCounts(DrawingContext dc, Vector blockVect, double offsetFactor, double thicknessFactor)
        {
            // BottomRight
            if (!IsCountsVisable) return;

            double textHeight = blockVect.Y * thicknessFactor / 2.0;
            Vector positionOffset = (blockVect * offsetFactor) + new Vector(blockVect.X * thicknessFactor, 0);

            for (int i = 0; i < Route.Counts.GetLength(0); i++)
            {
                for (int j = 0; j < Route.Counts.GetLength(1); j++)
                {
                    int counts = Route.Counts[i, j];
                    if (counts <= 0) continue;

                    string countText = counts.ToString();
                    FormattedText countFText = GetFormattedText(counts, textHeight, TextBrush);
                    Vector textOffset = new Vector(countFText.Width, countFText.Height);
                    Point point = GetBottomRight(new Block(i, j), blockVect, offsetFactor, thicknessFactor) - textOffset;

                    dc.DrawText(countFText, point);
                }
            }
        }

        private static readonly CultureInfo ci = CultureInfo.CurrentCulture;
        private static readonly FlowDirection fd = FlowDirection.LeftToRight;
        private static readonly Typeface tp = new Typeface("Arial");
        private static FormattedText[] fTextes = new FormattedText[0];

        private static FormattedText GetFormattedText(int no, double height, Brush brush)
        {
            if (fTextes.Length <= no) Array.Resize(ref fTextes, no + 1);

            if (fTextes[no] == null) return fTextes[no] = new FormattedText(no.ToString(), ci, fd, tp, height, brush);

            FormattedText fText = fTextes[no];
            fText.SetFontSize(height);
            fText.SetForegroundBrush(brush);

            return fText;
        }

        private Brush ToBrush(Color color)
        {
            return new SolidColorBrush(color);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged == null) return;

            var args = new PropertyChangedEventArgs(name);

            if (Thread.CurrentThread == Application.Current.Dispatcher.Thread) PropertyChanged(this, args);
            else Application.Current.Dispatcher.BeginInvoke(PropertyChanged, this, args);
        }
    }
}