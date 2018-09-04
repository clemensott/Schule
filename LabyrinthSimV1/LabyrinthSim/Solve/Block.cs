using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    class Block
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Blockrelation Top { get; private set; }

        public Blockrelation Bottom { get; private set; }

        public Blockrelation Right { get; private set; }

        public Blockrelation Left { get; private set; }

        private Block(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Block GetTopBlock()
        {
            return Top.GetOther(this);
        }

        public Block GetBottomBlock()
        {
            return Bottom.GetOther(this);
        }

        public Block GetRightBlock()
        {
            return Right.GetOther(this);
        }

        public Block GetLeftBlock()
        {
            return Left.GetOther(this);
        }

        public Blockrelation GetRelation(Block block)
        {
            return GetRelations().First(r => r.Block1 == block || r.Block2 == block);
        }

        public IEnumerable<Blockrelation> GetRelations()
        {
            yield return Top;
            yield return Bottom;
            yield return Right;
            yield return Left;
        }

        public void GetPossibleRoute(Search route, Block least, ITarget target)
        {
            GetRoute(route, least, target, getOpenBlocks);
        }

        public void GetMaybeRoute(Search route, Block least, ITarget target)
        {
            GetRoute(route, least, target, GetNotCloseBlocks);
        }

        private void GetRoute(Search route, Block least, ITarget target, Func<Block, IEnumerable<Block>> getNeighbours)
        {
            if (!route.Add(this)) return;

            if (target.Is(this)) route.Update();
            else if (route.Canceled) return;
            else
            {
                int currentLength = route.CurrentLength;

                foreach (Block block in GetOrdered(least, target, getNeighbours))
                {
                    if (route.Any && route.Length <= currentLength + 1 + target.MinDistance(block)) continue;

                    route.CurrentLength = currentLength;
                    block.GetRoute(route, this, target, getNeighbours);
                }
            }
        }

        private IEnumerable<Block> GetOrdered(Block least, ITarget target, Func<Block, IEnumerable<Block>> getNeighbours)
        {
            bool included = false;
            foreach (Block block in getNeighbours(this).OrderBy(b => target.DirectDistance(b)))
            {
                if (block != least) yield return block;
                else included = true;
            }

            if (included) yield return least;
        }

        private static readonly Func<Block, IEnumerable<Block>> getOpenBlocks =
           new Func<Block, IEnumerable<Block>>(GetOpenBlocks);
        private static IEnumerable<Block> GetOpenBlocks(Block block)
        {
            return block.GetRelations().Where(r => r.Relation == RelationType.Open).Select(r => r.GetOther(block));
        }

        private static readonly Func<Block, IEnumerable<Block>> getNotCloseBlocks =
            new Func<Block, IEnumerable<Block>>(GetNotCloseBlocks);
        private static IEnumerable<Block> GetNotCloseBlocks(Block block)
        {
            return block.GetRelations().Where(r => r.Relation != RelationType.Close).Select(r => r.GetOther(block));
        }

        public static Block[,] GetBlocks(int width, int height)
        {
            Block[,] blocks = GetEmptyBlocks(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Block block = blocks[x, y];

                    block.Top = y > 0 ? blocks[x, y - 1].Bottom : new Blockrelation(blocks[x, y]);
                    block.Bottom = y + 1 < height ? new Blockrelation(blocks[x, y], blocks[x, y + 1]) : new Blockrelation(blocks[x, y]);
                    block.Right = x + 1 < width ? new Blockrelation(blocks[x, y], blocks[x + 1, y]) : new Blockrelation(blocks[x, y]);
                    block.Left = x > 0 ? blocks[x - 1, y].Right : new Blockrelation(blocks[x, y]);
                }
            }

            return blocks;
        }

        public override string ToString()
        {
            return X.ToString() + " x " + Y.ToString();
        }

        private static Block[,] GetEmptyBlocks(int width, int height)
        {
            Block[,] blocks = new Block[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    blocks[i, j] = new Block(i, j);
                }
            }

            return blocks;
        }
    }
}
