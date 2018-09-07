using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace LabyrinthSim
{
    enum LookDirection { Top, Bottom, Right, Left }

    class Robot : ISearcher, INotifyPropertyChanged
    {
        private const double lenghtFactor = 0.7, widthFactor = 0.5, robotLinesThickness = 3;

        private bool toTarget, hasChanges, isSearching;
        private ITarget startTarget;
        IRelationInterpreter actual, best, worst;
        private Color color;

        public Block Position { get; private set; }

        public LookDirection Direction { get; private set; }

        public SearchView DriveSearch { get; private set; }

        public SearchView BestSearch { get; private set; }

        public SearchView WorstSearch { get; private set; }

        public Labyrinth Labyrinth { get; private set; }

        public Color Color
        {
            get { return color; }
            set
            {
                if (value == color) return;

                color = value;
                Brush = new SolidColorBrush(color);

                OnPropertyChanged("Color");
                OnPropertyChanged("Brush");
            }
        }

        public Brush Brush { get; private set; }

        public bool IsSearching
        {
            get { return isSearching; }
            set
            {
                if (value == isSearching) return;

                isSearching = value;
                OnPropertyChanged("IsSearching");
            }
        }

        public bool Canceled { get; private set; }

        public Waiter RobotSearchWaiter { get; set; }

        public Waiter MoveWaiter { get; set; }

        public Waiter TargetWaiter { get; set; }

        public Task SearchTask { get; private set; }

        public bool HasChanges
        {
            get { return hasChanges || DriveSearch.HasChanges; }
            set
            {
                if (value == HasChanges) return;

                DriveSearch.HasChanges = hasChanges = value;
            }
        }

        public Robot(Labyrinth labyrinth)
        {
            Labyrinth = labyrinth;

            toTarget = true;
            startTarget = new SingleTarget(Block.Origin);

            actual = new ActualInterpreter();
            best = new BestCaseInterpreter();
            worst = new WorstCaseInterpreter();

            Brush = Brushes.Blue;

            DriveSearch = new SearchView()
            {
                Labyrinth = Labyrinth,
                Target = Labyrinth.Target,
                IsLabyrinthTarget = false,
                Start = Block.Origin,
                InterpreterType = RelationInterpreterType.BestCase,
                SearchType = SearchType.Direct
            };

            BestSearch = new SearchView()
            {
                Labyrinth = Labyrinth,
                Target = Labyrinth.Target,
                IsLabyrinthTarget = true,
                Start = Block.Origin,
                InterpreterType = RelationInterpreterType.BestCase,
                SearchType = SearchType.Direct,
                PossibleRouteColor = Colors.Gold
            };

            WorstSearch = new SearchView()
            {
                Labyrinth = Labyrinth,
                Target = Labyrinth.Target,
                IsLabyrinthTarget = true,
                Start = Block.Origin,
                InterpreterType = RelationInterpreterType.WorstCase,
                SearchType = SearchType.Direct,
                PossibleRouteColor = Colors.Aqua
            };
        }

        public Task BeginSearch()
        {
            if (IsSearching) return SearchTask;
            IsSearching = true;

            MakeLabyrinthUnkown();
            Position = Block.Origin;
            Direction = LookDirection.Bottom;
            toTarget = true;

            return Task.Factory.StartNew(new Action(Search));
        }

        private void MakeLabyrinthUnkown()
        {
            for (int i = 0; i < Labyrinth.H.Width; i++)
            {
                for (int j = 0; j < Labyrinth.H.Height; j++)
                {
                    if (Labyrinth.H[i, j] < 2) Labyrinth.H[i, j] += 2;
                }
            }

            for (int i = 0; i < Labyrinth.V.Width; i++)
            {
                for (int j = 0; j < Labyrinth.V.Height; j++)
                {
                    if (Labyrinth.V[i, j] < 2) Labyrinth.V[i, j] += 2;
                }
            }
        }

        public void CancelSearch()
        {
            while (IsSearching)
            {
                Canceled = true;

                DriveSearch?.CancelSearch();
                BestSearch?.CancelSearch();
                WorstSearch?.CancelSearch();

                RobotSearchWaiter?.PulseAll();
                MoveWaiter?.PulseAll();
                TargetWaiter?.PulseAll();

                Task.Delay(20).Wait(20);
            }

            Canceled = false;
        }

        private void Search()
        {
            UpdateLearnLabyrinth(Position);
            WorstSearch.BeginSearch().Wait();
            BestSearch.BeginSearch().Wait();
            DriveSearch.BeginSearch().Wait();

            int robotSearchIndex = 0;

            do
            {
                if (UpdateLearnLabyrinth(Position)) WorstSearch.BeginSearch().Wait();
                if (IsRouteBlocked(BestSearch.Route)) BestSearch.BeginSearch().Wait();

                if (IsRouteBlocked(DriveSearch.Route) || DriveSearch.Route.Length - robotSearchIndex <= 1)
                {
                    DriveSearch.Start = Position;
                    DriveSearch.Target = GetTarget();
                    DriveSearch.BeginSearch().Wait();

                    robotSearchIndex = 0;
                    Direction = GetDirection(true, robotSearchIndex);

                    RobotSearchWaiter?.Wait();

                    if (Canceled) break;
                }

                robotSearchIndex++;
                Position = DriveSearch.Route[robotSearchIndex];

                Direction = GetDirection(false, robotSearchIndex);

                HasChanges = true;
                MoveWaiter?.Wait();

                if (!Canceled && GetTarget().Is(Position))
                {
                    toTarget = !toTarget;
                    HasChanges = true;
                    TargetWaiter?.Wait();
                }

                if (Canceled) break;
            }
            while (WorstSearch.Route.PossibleLength <= 0 || WorstSearch.Route.PossibleLength > BestSearch.Route.PossibleLength);

            System.Diagnostics.Debug.WriteLine("RobotEndSearch: worstLen: {0}, bestLen: {1}",
                WorstSearch.Route.PossibleLength, BestSearch.Route.PossibleLength);

            IsSearching = false;
        }

        private bool IsRouteBlocked(IEnumerable<Block> blocks)
        {
            if (blocks == null) return true;

            Block[] array = blocks.ToArray();

            if (array.Length <= 1) return true;

            for (int i = 1; i < array.Length; i++)
            {
                if (!best.IsOpen(Labyrinth[array[i - 1], array[i]])) return true;
            }

            return false;
        }

        private ITarget GetTarget()
        {
            return toTarget ? Labyrinth.Target : startTarget;
        }

        private bool UpdateLearnLabyrinth(Block block)
        {
            bool willOpen = Labyrinth[block, block.Left] == 2 || Labyrinth[block, block.Right] == 2 ||
                Labyrinth[block, block.Top] == 2 || Labyrinth[block, block.Bottom] == 2;

            Labyrinth[block, block.Left] = actual.IsOpen(Labyrinth[block, block.Left]) ? 0 : 1;
            Labyrinth[block, block.Right] = actual.IsOpen(Labyrinth[block, block.Right]) ? 0 : 1;
            Labyrinth[block, block.Top] = actual.IsOpen(Labyrinth[block, block.Top]) ? 0 : 1;
            Labyrinth[block, block.Bottom] = actual.IsOpen(Labyrinth[block, block.Bottom]) ? 0 : 1;

            return willOpen;
        }

        private LookDirection GetDirection(bool toNext, int robotIndex)
        {
            if ((DriveSearch?.Route?.Length ?? 0) <= 1) return LookDirection.Bottom;

            Block fromBlock, toBlock;

            if (toNext || robotIndex <= 0)
            {
                fromBlock = Position;
                toBlock = DriveSearch.Route[robotIndex + 1];
            }
            else
            {
                fromBlock = DriveSearch.Route[robotIndex - 1];
                toBlock = Position;
            }

            if (fromBlock.Top == toBlock) return LookDirection.Top;
            else if (fromBlock.Bottom == toBlock) return LookDirection.Bottom;
            else if (fromBlock.Right == toBlock) return LookDirection.Right;
            else if (fromBlock.Left == toBlock) return LookDirection.Left;

            return LookDirection.Bottom;
        }

        public void Draw(DrawingContext dc, Vector blockVect)
        {
            Point head = new Point(), backLeft = new Point(), backRight = new Point();

            switch (Direction)
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

            head = new Point(blockVect.X * (Position.X + 0.5 + head.X), blockVect.Y * (Position.Y + 0.5 + head.Y));
            backLeft = new Point(blockVect.X * (Position.X + 0.5 + backLeft.X), blockVect.Y * (Position.Y + 0.5 + backLeft.Y));
            backRight = new Point(blockVect.X * (Position.X + 0.5 + backRight.X), blockVect.Y * (Position.Y + 0.5 + backRight.Y));

            Pen pen = new Pen(Brushes.Blue, robotLinesThickness);
            dc.DrawLine(pen, head, backLeft);
            dc.DrawLine(pen, backLeft, backRight);
            dc.DrawLine(pen, backRight, head);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
