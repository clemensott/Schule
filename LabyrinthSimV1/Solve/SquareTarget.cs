using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    class SquareTarget : ITarget
    {
        public Block TopRight { get; private set; }

        public Block TopLeft { get; private set; }

        public Block BottomRight { get; private set; }

        public Block BottomLeft { get; private set; }

        public SquareTarget(Block topLeft)
        {
            TopLeft = topLeft;
            TopRight = TopLeft.GetRightBlock();
            BottomRight = TopRight.GetBottomBlock();
            BottomLeft = BottomRight.GetLeftBlock();
        }

        public bool Is(Block block)
        {
            return GetBlocks().Any(b => b == block);
        }

        public IEnumerable<Block> GetBlocks()
        {
            yield return TopRight;
            yield return TopLeft;
            yield return BottomRight;
            yield return BottomLeft;
        }

        public double DirectDistance(Block block)
        {
            if (block.Y <= TopRight.Y) return DirectDistance(block, block.X >= TopRight.X ? TopRight : TopLeft);
            else return DirectDistance(block, block.X >= BottomRight.X ? BottomRight : BottomLeft);
        }

        private double DirectDistance(Block block1, Block block2)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(block1.X - block2.X), 2) + Math.Pow(Math.Abs(block1.Y - block2.Y), 2));
        }

        public int MinDistance(Block block)
        {
            if (block.Y <= TopRight.Y) return MinDistance(block, block.X >= TopRight.X ? TopRight : TopLeft);
            else return MinDistance(block, block.X >= BottomRight.X ? BottomRight : BottomLeft);
        }

        public int MinDistance(Block block1, Block block2)
        {
            return Math.Abs(block1.X - block2.X) + Math.Abs(block1.Y - block2.Y);
        }
    }
}
