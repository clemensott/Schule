using System;
using System.Collections.Generic;

namespace LabyrinthSim.PresentationSetups
{
    class ExplainBruteForceSetup : PresentationSetup
    {
        protected override string GetName()
        {
            return "ExplainBruteForce";
        }

        protected override void GeneralSetup(LabyrinthControl lc)
        {
            Labyrinth lab = GetLabyrinth(Name + extention, 2);

            Waiter waiter = new Waiter();
            waiter.Time = TimeSpan.FromMilliseconds(1);
            waiter.Breakpoints.Add(new UIntText(145));
            waiter.Breakpoints.Add(new UIntText(230));
            waiter.Breakpoints.Add(new UIntText(2463));

            SearchView search = new SearchView()
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
        }

        protected override bool HaveSetup(LabyrinthControl lc)
        {
            return lc.Searches.Count != 1;
        }
    }
}
