using System;
using System.Collections.Generic;

namespace LabyrinthSim.PresentationSetups
{
    class CustomSetup : PresentationSetup
    {
        protected override string GetName()
        {
            return "Custom";
        }

        protected override void GeneralSetup(LabyrinthControl lc)
        {
            Labyrinth lab = GetLabyrinth(Name + extention, 2);
            Robot robot = new Robot(lab);

            robot.WorstSearch.IsVisible = true;
            robot.WorstSearch.BlockAddWaiter = new Waiter();
            robot.WorstSearch.TryAddWaiter = new Waiter();
            robot.WorstSearch.IsLengthVisable = true;

            robot.BestSearch.IsVisible = true;
            robot.BestSearch.BlockAddWaiter = new Waiter();
            robot.BestSearch.TryAddWaiter = new Waiter();
            robot.BestSearch.IsVisible = false;
            robot.BestSearch.IsLengthVisable = true;

            robot.DriveSearch.IsVisible = true;
            robot.DriveSearch.BlockAddWaiter = new Waiter();
            robot.DriveSearch.TryAddWaiter = new Waiter();

            Waiter moveWaiter = new Waiter();
            moveWaiter.Time = TimeSpan.FromMilliseconds(100);

            Waiter targetWaiter = new Waiter();
            targetWaiter.Pause = true;

            robot.MoveWaiter = moveWaiter;
            robot.TargetWaiter = targetWaiter;
            robot.RobotSearchWaiter = new Waiter();

            SearchView directSearch = new SearchView()
            {
                Labyrinth = lab,
                Start = Block.Origin,
                IsLabyrinthTarget = true,
                IsVisible = true,
                SearchType = SearchType.Direct,
                IsDistancesVisable = false,
                IsCountsVisable = false,
                IsLengthVisable = false,
                InterpreterType = RelationInterpreterType.Actual,
                TryAddWaiter = new Waiter(),
                BlockAddWaiter = new Waiter(),
                IsVisibilitiesExpanded = true,
                IsBlockAddExpanded = true
            };

            SearchView bruteForceSearch = new SearchView()
            {
                Labyrinth = lab,
                Start = Block.Origin,
                IsLabyrinthTarget = true,
                IsVisible = true,
                SearchType = SearchType.BruteForce,
                IsDistancesVisable = false,
                IsCountsVisable = false,
                IsLengthVisable = false,
                InterpreterType = RelationInterpreterType.Actual,
                TryAddWaiter = new Waiter(),
                BlockAddWaiter = new Waiter(),
                IsVisibilitiesExpanded = true,
                IsBlockAddExpanded = true
            };

            ClearSearches(lc);
            lc.Robot = robot;
            lc.Labyrinth = lab;
            lc.Searches.Add(robot.WorstSearch);
            lc.Searches.Add(robot.BestSearch);
            lc.Searches.Add(robot.DriveSearch);
            lc.Searches.Add(directSearch);
            lc.Searches.Add(bruteForceSearch);

            lc.IsViewActual = true;
            lc.IsViewRobot = true;
            lc.IsViewVirtual = true;
        }

        protected override IEnumerable<Action<LabyrinthControl>> GetSubSetuper()
        {
            yield return new Action<LabyrinthControl>(Nothing);
        }

        private void Nothing(LabyrinthControl lc)
        {

        }

        protected override bool HaveSetup(LabyrinthControl lc)
        {
            return false;
        }
    }
}
