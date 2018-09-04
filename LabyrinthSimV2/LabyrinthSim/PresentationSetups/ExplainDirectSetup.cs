using System;
using System.Collections.Generic;

namespace LabyrinthSim.PresentationSetups
{
    class ExplainDirectSetup : PresentationSetup
    {
        protected override string GetName()
        {
            return "ExplainDirect";
        }

        protected override void GeneralSetup(LabyrinthControl lc)
        {
            Labyrinth lab = GetLabyrinth(Name + extention, 2);

            Waiter waiter = new Waiter();
            waiter.Time = TimeSpan.FromMilliseconds(1);
            waiter.Breakpoints.Add(new UIntText(97));
            waiter.Breakpoints.Add(new UIntText(406));
            waiter.Breakpoints.Add(new UIntText(412));
            waiter.Breakpoints.Add(new UIntText(432));
            waiter.Breakpoints.Add(new UIntText(553));
            waiter.Breakpoints.Add(new UIntText(1190));
            waiter.Breakpoints.Add(new UIntText(1521));

            SearchView search = new SearchView()
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
                TryAddWaiter = waiter,
                BlockAddWaiter = waiter,
                IsVisibilitiesExpanded = true,
                IsBlockAddExpanded = true
            };

            ClearSearches(lc);
            lc.Robot = null;
            lc.Labyrinth = lab;
            lc.Searches.Add(search);

            lc.IsViewActual = true;
            lc.IsViewRobot = false;
            lc.IsViewVirtual = false;
        }

        protected override IEnumerable<Action<LabyrinthControl>> GetSubSetuper()
        {
            yield return new Action<LabyrinthControl>(ToBreakpoint0);
            yield return new Action<LabyrinthControl>(ToBreakpoint1);
            yield return new Action<LabyrinthControl>(ToBreakpoint2);
            yield return new Action<LabyrinthControl>(ToBreakpoint3);
            yield return new Action<LabyrinthControl>(ToBreakpoint4);
            yield return new Action<LabyrinthControl>(ToBreakpoint5);
            yield return new Action<LabyrinthControl>(ToBreakpoint6);
        }

        private void ToBreakpoint0(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 0);
        }

        private void ToBreakpoint1(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 1);
        }

        private void ToBreakpoint2(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 2);
            lc.Searches[0].IsLengthVisable = true;
        }

        private void ToBreakpoint3(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 3);
        }

        private void ToBreakpoint4(LabyrinthControl lc)
        {
            lc.Searches[0].IsDistancesVisable = true;
            ToBreakpoint(lc, 4);
        }

        private void ToBreakpoint5(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 5);
        }

        private void ToBreakpoint6(LabyrinthControl lc)
        {
            ToBreakpoint(lc, 6);
        }

        protected override bool HaveSetup(LabyrinthControl lc)
        {
            return lc.Searches.Count != 1;
        }
    }
}
