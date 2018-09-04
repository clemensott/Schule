using System;
using System.Collections.Generic;

namespace LabyrinthSim.PresentationSetups
{
    class RobotSetup : PresentationSetup
    {
        protected override void GeneralSetup(LabyrinthControl lc)
        {
            Labyrinth lab = GetLabyrinth(Name + extention, 2);
            Robot robot = new Robot(lab);

            Waiter robotWaiter = new Waiter();
            robotWaiter.Time = TimeSpan.FromMilliseconds(500);
            robotWaiter.Pause = true;
            robotWaiter.Breakpoints.Add(new UIntText(236));
            robotWaiter.Breakpoints.Add(new UIntText(237));
            robotWaiter.Breakpoints.Add(new UIntText(350));
            robotWaiter.Breakpoints.Add(new UIntText(383));

            robot.WorstSearch.BlockAddWaiter = new Waiter();
            robot.WorstSearch.TryAddWaiter = new Waiter();
            robot.WorstSearch.IsVisible = false;
            robot.WorstSearch.IsLengthVisable = true;

            robot.BestSearch.BlockAddWaiter = new Waiter();
            robot.BestSearch.TryAddWaiter = new Waiter();
            robot.BestSearch.IsVisible = false;
            robot.BestSearch.IsLengthVisable = true;

            robot.DriveSearch.BlockAddWaiter = new Waiter();
            robot.DriveSearch.TryAddWaiter = new Waiter();
            robot.DriveSearch.IsVisible = true;

            robot.MoveWaiter = robotWaiter;
            robot.RobotSearchWaiter = robotWaiter;
            robot.TargetWaiter = robotWaiter;

            ClearSearches(lc);
            lc.Robot = robot;
            lc.Labyrinth = lab;
            lc.Searches.Add(robot.WorstSearch);
            lc.Searches.Add(robot.BestSearch);
            lc.Searches.Add(robot.DriveSearch);

            lc.IsViewActual = false;
            lc.IsViewRobot = true;
            lc.IsViewVirtual = true;
        }

        protected override string GetName()
        {
            return "Robot";
        }

        protected override IEnumerable<Action<LabyrinthControl>> GetSubSetuper()
        {
            yield return new Action<LabyrinthControl>(Nothing);
            yield return new Action<LabyrinthControl>(BeginSearch);
            yield return new Action<LabyrinthControl>(ToBreakpoint0);
            yield return new Action<LabyrinthControl>(ToBreakpoint1);
            yield return new Action<LabyrinthControl>(ToBreakpoint2);
            yield return new Action<LabyrinthControl>(ToBreakpoint3);
            yield return new Action<LabyrinthControl>(ShowJustVirtualSafe);
        }

        private void Nothing(LabyrinthControl lc)
        {
        }

        private void BeginSearch(LabyrinthControl lc)
        {
            lc.Robot.CancelSearch();
            lc.Robot.BeginSearch();
        }

        private void ToBreakpoint0(LabyrinthControl lc)
        {
            ToBreakpoint(lc.Robot, lc.Robot.MoveWaiter, 0);
        }

        private void ToBreakpoint1(LabyrinthControl lc)
        {
            ToBreakpoint(lc.Robot, lc.Robot.MoveWaiter, 1);

            lc.Robot.BestSearch.IsVisible = true;
            lc.Robot.WorstSearch.IsVisible = true;
        }

        private void ToBreakpoint2(LabyrinthControl lc)
        {
            ToBreakpoint(lc.Robot, lc.Robot.MoveWaiter, 2);
        }

        private void ToBreakpoint3(LabyrinthControl lc)
        {
            ToBreakpoint(lc.Robot, lc.Robot.MoveWaiter, 3);
        }

        private void ShowJustVirtualSafe(LabyrinthControl lc)
        {
            lc.Dispatcher.BeginInvoke(new Action<LabyrinthControl>(ShowJustVirtual), lc);
        }

        private void ShowJustVirtual(LabyrinthControl lc)
        {
            lc.IsViewRobot = false;
            lc.Robot.DriveSearch.IsVisible = false;
            lc.Robot.BestSearch.IsVisible = false;
            lc.Robot.WorstSearch.IsVisible = false;
        }

        protected override bool HaveSetup(LabyrinthControl lc)
        {
            return lc.Robot == null || !lc.Searches.Contains(lc.Robot.DriveSearch) ||
                !lc.Searches.Contains(lc.Robot.BestSearch) || !lc.Searches.Contains(lc.Robot.WorstSearch);
        }
    }
}
