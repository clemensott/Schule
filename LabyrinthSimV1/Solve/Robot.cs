using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    enum LookDirection { Top, Bottom, Right, Left }

    class Robot
    {
        private bool toTarget;

        public Search LearnRobotRoute { get; private set; }

        public Search LearnPossibleRoute { get; private set; }

        public Search LearnMaybeRoute { get; private set; }

        public Search ActualPossibleRoute { get; private set; }

        public Block Position { get; private set; }

        public LookDirection Direction { get { return GetDirection(); } }

        public Labyrinth ActualLabyrinth { get; private set; }

        public Labyrinth LearnLabyrinth { get; private set; }

        public Robot(Labyrinth actualLabyrinth)
        {
            ActualLabyrinth = actualLabyrinth;
            LearnLabyrinth = Labyrinth.GetLern(actualLabyrinth);

            ActualPossibleRoute = new Search(ActualLabyrinth.Width * ActualLabyrinth.Height);
            LearnRobotRoute = new Search(LearnLabyrinth.Width * LearnLabyrinth.Height);
            LearnMaybeRoute = new Search(LearnLabyrinth.Width * LearnLabyrinth.Height);
            LearnPossibleRoute = new Search(LearnLabyrinth.Width * LearnLabyrinth.Height);

            toTarget = false;
            NextStep();
        }

        private Block GetStart()
        {
            return LearnLabyrinth[0, 0];
        }

        public void SearchActualRoute()
        {
            ActualPossibleRoute.SearchPossible(ActualLabyrinth[0, 0], null, ActualLabyrinth.Target);
        }

        public void NextStep()
        {
            if (LearnRobotRoute.Length <= 1)
            {
                LearnRobotRoute.Reset();
                toTarget = !toTarget;
            }

            Position = LearnRobotRoute.ElementAtOrDefault(1) ?? (toTarget ? GetStart() : Position);
            UpdateLearnLabyrinth(Position);

            SetRoutes();
        }

        private void UpdateLearnLabyrinth(Block learnBlock)
        {
            if (learnBlock.ToString() == "10 x 10") { }
            Block actualBlock = GetActualBlock(learnBlock);
            if (actualBlock.Top.Relation == RelationType.Open) learnBlock.Top.Open();
            else if (actualBlock.Top.Relation == RelationType.Close) learnBlock.Top.Close();

            if (actualBlock.Bottom.Relation == RelationType.Open) learnBlock.Bottom.Open();
            else if (actualBlock.Bottom.Relation == RelationType.Close) learnBlock.Bottom.Close();

            if (actualBlock.Right.Relation == RelationType.Open) learnBlock.Right.Open();
            else if (actualBlock.Right.Relation == RelationType.Close) learnBlock.Right.Close();

            if (actualBlock.Left.Relation == RelationType.Open) learnBlock.Left.Open();
            else if (actualBlock.Left.Relation == RelationType.Close) learnBlock.Left.Close();
        }

        private Block GetActualBlock(Block learnBlock)
        {
            int x, y;
            LearnLabyrinth.GetPosition(learnBlock, out x, out y);
            return ActualLabyrinth[x, y];
        }

        private LookDirection GetDirection()
        {
            if (LearnRobotRoute.Length <= 1) return LookDirection.Bottom;

            Block nextBlock = LearnRobotRoute[1];

            if (nextBlock == Position.GetTopBlock()) return LookDirection.Top;
            else if (nextBlock == Position.GetBottomBlock()) return LookDirection.Bottom;
            else if (nextBlock == Position.GetRightBlock()) return LookDirection.Right;
            else if (nextBlock == Position.GetLeftBlock()) return LookDirection.Left;

            return LookDirection.Bottom;
        }

        private void SetRoutes()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

            //if (LearnRobotRoute?.GetRelations().All(r => r.Relation != RelationType.Close) ?? true)
            //{
                sw.Start();

                ITarget target = toTarget ? (ITarget)LearnLabyrinth.Target : new SingleTarget(GetStart());
                Block start = LearnRobotRoute?.ElementAtOrDefault(1) ?? (toTarget ? GetStart() : Position);
                LearnRobotRoute.SearchMaybe(start, LearnRobotRoute?.FirstOrDefault(), target);

                System.Diagnostics.Debug.WriteLine("Robot: " + sw.ElapsedMilliseconds);
            //}

            //if (LearnPossibleRoute?.GetRelations().Any(r => r.Relation == RelationType.Close) ?? true)
            //{
            sw.Reset();
            sw.Start();

            LearnPossibleRoute.SearchPossible(GetStart(), null, LearnLabyrinth.Target);

            System.Diagnostics.Debug.WriteLine("Possible: " + sw.ElapsedMilliseconds);
            //}

            //if (LearnMaybeRoute?.GetRelations().Any(r => r.Relation == RelationType.Close) ?? true)
            //{
            sw.Reset();
            sw.Start();

            LearnMaybeRoute.SearchMaybe(GetStart(), null, LearnLabyrinth.Target);

            System.Diagnostics.Debug.WriteLine("Maybe: " + sw.ElapsedMilliseconds);
            //}
        }
    }
}
