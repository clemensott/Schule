using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim
{
    abstract class Search : IEnumerable<Block>
    {
        protected Block[] possible, current;

        public Block this[int index] { get { return GetBlocks().ElementAtOrDefault(index); } }

        public int this[Block block]
        {
            get { return Distances[block.X, block.Y]; }
            set { Distances[block.X, block.Y] = value; }
        }

        public int Length { get { return Any ? PossibleLength : CurrentLength; } }

        public int CurrentLength { get; set; }

        public int PossibleLength { get; private set; }

        public bool Any { get { return possible != null; } }

        public bool Canceled { get; set; }

        public int[,] Distances { get; private set; }

        public int[,] Counts { get; set; }

        public Block Next { get; set; }

        public Labyrinth Labyrinth { get; private set; }

        public ITarget Target { get; private set; }

        public IRelationInterpreter Interpreter { get; private set; }

        public bool HasChanges { get; set; }

        public Waiter BlockAddWaiter { get; set; }

        public Waiter TryAddWaiter { get; set; }

        public Search(Labyrinth labyrinth, ITarget target, IRelationInterpreter interpreter)
        {
            Next = Block.None;
            Labyrinth = labyrinth;
            Target = target;
            Interpreter = interpreter;

            Distances = GetDistancesArray(Labyrinth.Width, Labyrinth.Height);
            Counts = new int[Labyrinth.Width, Labyrinth.Height];

            current = new Block[Labyrinth.Width * Labyrinth.Height];
            CurrentLength = 0;
            PossibleLength = 0;
        }

        private static int[,] GetDistancesArray(int width, int height)
        {
            int[,] distances = new int[width, height];

            for (int i = 0; i < distances.GetLength(0); i++)
            {
                for (int j = 0; j < distances.GetLength(1); j++)
                {
                    distances[i, j] = width * height;
                }
            }

            return distances;
        }

        private Block[] GetBlocks()
        {
            return possible ?? current;
        }

        public void Add(Block next)
        {
            Next = next;
            Counts[next.X, next.Y]++;

            HasChanges = true;
            TryAddWaiter?.Wait();

            if (!TryAdd(next)) return;

            current[CurrentLength++] = next;

            Next = Block.None;
            HasChanges = true;
            BlockAddWaiter?.Wait();

            if (Target.Is(next)) Update();
            else if (Canceled) return;
            else
            {
                int currentLength = CurrentLength;

                foreach (Block block in GetNeighborsOrdered(next))
                {
                    if (Canceled) return;
                    CurrentLength = currentLength;
                    Add(block);
                }
            }
        }

        protected virtual bool TryAdd(Block next)
        {
            if (CurrentLength > 0 && !IsOpen(next, current[CurrentLength - 1])) return false;
            if (Any && CurrentLength + 1 + Target.MinDistance(next) >= Length) return false;
            if (!CheckDistances(next)) return false;

            return true;
        }

        protected bool IsOpen(Block block1, Block block2)
        {
            return Interpreter.IsOpen(Labyrinth[block1, block2]);
        }

        private bool CheckDistances(Block block)
        {
            int distance = this[block];

            if (distance <= CurrentLength + 1) return false;

            if (Any && this.Contains(block))
            {
                int j;
                for (j = 0; j < Length; j++) if (possible[j] == block) break;

                Block[] newAble = (Block[])current.Clone();
                int offset = j - CurrentLength;

                for (int k = j; k < Length; k++)
                {
                    newAble[k - offset] = possible[k];

                    this[possible[k]] = k - offset + 1;
                }

                possible = newAble;
                PossibleLength -= offset;
                return false;
            }

            this[block] = CurrentLength + 1;
            return true;
        }

        public void Update()
        {
            if (possible != null && CurrentLength >= PossibleLength) return;

            PossibleLength = CurrentLength;
            possible = (Block[])current.Clone();
        }

        protected abstract IEnumerable<Block> GetNeighborsOrdered(Block block);

        protected IEnumerable<Block> GetNeighbors(Block block)
        {
            Block left = block.Left;
            Block right = block.Right;
            Block top = block.Top;
            Block bottom = block.Bottom;

            if (left.X >= 0) yield return left;
            if (right.X < Labyrinth.Width) yield return right;
            if (top.Y >= 0) yield return top;
            if (bottom.Y < Labyrinth.Height) yield return bottom;
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

        public IEnumerable<Block> GetPossible()
        {
            return possible?.Take(PossibleLength) ?? Enumerable.Empty<Block>();
        }

        public override string ToString()
        {
            return "Length: " + Length;
        }
    }
}
