using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim.PresentationSetups
{
    class AllOpenBruteForceSetup : PresentationSetup
    {
        private int size;
        public AllOpenBruteForceSetup(int size)
        {
            this.size = size;
        }

        protected override string GetName()
        {
            return "AllOpenBruteForce";
        }

        protected override void GeneralSetup(LabyrinthControl lc)
        {
            Labyrinth lab = new Labyrinth(size, size, size / 2 - 1, size / 2 - 1, 2);

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
                InterpreterType = RelationInterpreterType.Actual
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
            yield return new Action<LabyrinthControl>(Start);
        }

        private void Start(LabyrinthControl lc)
        {
            lc.Searches.First().BeginSearch();
        }

        protected override bool HaveSetup(LabyrinthControl lc)
        {
            return lc.Searches.Count != 1;
        }
    }
}
