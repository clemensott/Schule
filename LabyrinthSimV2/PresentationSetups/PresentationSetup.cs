using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LabyrinthSim.PresentationSetups
{
    abstract class PresentationSetup
    {
        public string Name { get; private set; }

        public int SubIndex { get; private set; }

        public Action<LabyrinthControl>[] SubSetuper { get; private set; }

        public PresentationSetup()
        {
            Name = GetName();
            SubSetuper = GetSubSetuper().ToArray();

            SubIndex = 0;
        }

        public void Setup(LabyrinthControl lc)
        {
            GeneralSetup(lc);

            SubIndex = 0;
            ExecuteAsync(lc);
        }

        public void Previous(LabyrinthControl lc)
        {
            SubIndex = (SubIndex - 1 + SubSetuper.Length) % SubSetuper.Length;
            ExecuteAsync(lc);
        }

        public void Next(LabyrinthControl lc)
        {
            SubIndex = (SubIndex + 1) % SubSetuper.Length;

            if (HaveSetup(lc)) GeneralSetup(lc);

            ExecuteAsync(lc);
        }

        private void ExecuteAsync(LabyrinthControl lc)
        {
            Task.Factory.StartNew(new Action<object>(Execute), lc);
        }

        private void Execute(object obj)
        {
            LabyrinthControl lc = obj as LabyrinthControl;

            SubSetuper[SubIndex](lc);
        }

        protected void ClearSearches(LabyrinthControl lc)
        {
            foreach (SearchView search in lc.Searches)
            {
                search.CancelSearch();
            }

            lc.Searches.Clear();
        }

        protected abstract string GetName();

        protected abstract void GeneralSetup(LabyrinthControl lc);

        protected abstract IEnumerable<Action<LabyrinthControl>> GetSubSetuper();

        protected abstract bool HaveSetup(LabyrinthControl lc);

        protected void ToBreakpoint(LabyrinthControl lc, int id)
        {
            SearchView search = lc.Searches.First();
            Waiter waiter = search.BlockAddWaiter;

            ToBreakpoint(search, waiter, id);
        }

        protected void ToBreakpoint(ISearcher search, Waiter waiter, int id)
        {
            uint breakpoint = waiter.Breakpoints[id].Value;

            if (waiter.Count > breakpoint) search.CancelSearch();
            if (waiter.Count != breakpoint)
            {
                search.BeginSearch();
                while (true)
                {
                    while (!waiter.IsPaused) Task.Delay(20).Wait(20);

                    if (waiter.Count >= breakpoint) break;

                    waiter.Pause = false;
                    waiter.PulseAll();
                }
            }

            waiter.Pause = true;
        }

        public override string ToString()
        {
            return Name;
        }


        protected const string extention = ".txt";

        public static IEnumerable<PresentationSetup> GetSetups()
        {
            yield return new ExplainBruteForceSetup();
            yield return new ExplainDirectSetup();
            yield return new RobotSetup();
            yield return new AllOpenBruteForceSetup(16);
            yield return new CustomSetup();
        }

        protected static Labyrinth GetLabyrinth(string path, int startValue)
        {
            try
            {
                string[] generalParts = File.ReadAllText(path).Split('|');

                string[] sizeParts = generalParts[0].Split('x');
                int width = int.Parse(sizeParts[0]);
                int height = int.Parse(sizeParts[1]);

                string[] targetParts = generalParts[1].Split('x');
                int targetX = int.Parse(targetParts[0]);
                int targetY = int.Parse(targetParts[1]);

                Labyrinth lab = new Labyrinth(width, height, targetX, targetY, startValue);

                foreach (string hClosedText in generalParts[2].Split(';'))
                {
                    string[] hClosedParts = hClosedText.Split('x');
                    int x = int.Parse(hClosedParts[0]);
                    int y = int.Parse(hClosedParts[1]);

                    lab.H[x, y] = 3;
                }

                foreach (string vClosedText in generalParts[3].Split(';'))
                {
                    string[] vClosedParts = vClosedText.Split('x');
                    int x = int.Parse(vClosedParts[0]);
                    int y = int.Parse(vClosedParts[1]);

                    lab.V[x, y] = 3;
                }

                return lab;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "PresentationSetup.GetLabyrinth");
                throw;
            }
        }
    }
}
