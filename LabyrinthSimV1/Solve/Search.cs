using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabyrinthSim
{
    class Search : IEnumerable<Block>
    {
        private bool finished;
        private int possibleLength, distancesCount;
        private Block[] possible, current;
        private BlockDistance[] distances;
        private Task currentTask;

        public Block this[int index] { get { return GetBlocks()[index]; } }

        public int Length { get { return GetLength(); } }

        public int CurrentLength { get; set; }

        public Block GoBackUntil { get; set; }

        public Task Task { get; private set; }

        public bool IsPossible { get; private set; }

        public bool IsMaybe { get; private set; }

        public bool Any { get { return possible != null; } }

        public bool Finished { get { return Task?.IsCompleted ?? finished; } }

        public bool Canceled { get; private set; }

        public int Wait { get; set; }

        public Search(int maxLength)
        {
            distances = new BlockDistance[maxLength];
            current = new Block[maxLength];
            CurrentLength = 0;
            possibleLength = maxLength;
        }

        private Block[] GetBlocks()
        {
            return possible ?? current;
        }

        private int GetLength()
        {
            return Any ? possibleLength : CurrentLength;
        }

        public bool Add(Block block)
        {
            if (!UpdateDistances(block)) return false;
            if (GetCurrent().Contains(block)) return false;

            current[CurrentLength++] = block;

            if (Wait > 0) Task.Delay(Wait).Wait(Wait + 10);
            while (Wait < 0) Task.Delay(100).Wait(100);

            return Any ? CurrentLength < Length : true;
        }

        private bool UpdateDistances(Block block)
        {
            BlockDistance addBlockDistance = GetDistance(block);

            if (addBlockDistance == null)
            {
                distances[distancesCount++] = new BlockDistance(block, CurrentLength);
                return true;
            }

            if (addBlockDistance.Distance <= CurrentLength) return false;

            if (possible != null && this.Contains(block))
            {
                int j;
                for (j = 0; j < Length; j++) if (possible[j] == block) break;

                Block[] newAble = (Block[])current.Clone();
                int offset = j - CurrentLength;

                for (int k = j; k < Length; k++)
                {
                    newAble[k - offset] = possible[k];

                    GetDistance(possible[k]).Distance = k - offset;
                }

                possible = newAble;
                possibleLength -= offset;
                return false;
            }

            GetDistance(block).Distance = CurrentLength;
            return true;
        }

        private BlockDistance GetDistance(Block block)
        {
            foreach (BlockDistance bd in distances.Take(distancesCount)) if (bd.Block == block) return bd;

            return null;
        }

        public void Update()
        {
            if (CurrentLength >= possibleLength) return;

            possibleLength = CurrentLength;
            possible = (Block[])current.Clone();
        }

        public IEnumerable<Blockrelation> GetRelations()
        {
            for (int i = 1; i < CurrentLength; i++)
            {
                yield return this[i].GetRelation(this[i - 1]);
            }
        }

        public void SearchPossible(Block start, Block least, ITarget target)
        {
            Reset();

            IsPossible = true;
            IsMaybe = false;

            start.GetPossibleRoute(this, least, target);

            finished = true;
        }

        public Task SearchPossibleAsync(Block start, Block least, ITarget target)
        {
            currentTask = new Task(new Action(() => { SearchPossible(start, least, target); }));
            currentTask.Start();

            return currentTask;
        }

        public void SearchMaybe(Block start, Block least, ITarget target)
        {
            Reset();

            IsPossible = false;
            IsMaybe = true;

            start.GetMaybeRoute(this, least, target);

            finished = true;
        }

        public Task SearchMaybeAsync(Block start, Block least, ITarget target)
        {
            currentTask = new Task(new Action(() => { SearchMaybe(start, least, target); }));
            currentTask.Start();

            return currentTask;
        }

        public void Cancel()
        {
            Canceled = true;
        }

        public void Reset()
        {
            if (Task?.IsCompleted ?? false)
            {
                Cancel();
                Task.Wait(100);
            }

            if (currentTask != null)
            {
                Task = currentTask;
                currentTask = null;
            }

            finished = false;
            CurrentLength = 0;
            possible = null;
            possibleLength = current.Length;

            distances = new BlockDistance[distances.Length];
            distancesCount = 0;
        }

        public IEnumerator<Block> GetEnumerator()
        {
            return GetBlocks().Take(Length).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetBlocks().Take(Length).GetEnumerator();
        }

        public IEnumerable<Block> GetCurrent()
        {
            return current.Take(CurrentLength);
        }

        private int CurrentIndexOf(Block block)
        {
            for (int i = 0; i < CurrentLength; i++)
            {
                if (current[i] == block) return i;
            }

            return -1;
        }

        public override string ToString()
        {
            return "Length: " + Length;
        }
    }
}
