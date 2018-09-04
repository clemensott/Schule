using System;
using System.Collections.Generic;

namespace LabyrinthSim
{
    class SingleTarget : ITarget
    {
        private Block block;

        public Block Main { get { return block; } }

        public SingleTarget(Block block)
        {
            this.block = block;
        }

        public double DirectDistance(Block block)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(this.block.X - block.X), 2) + Math.Pow(Math.Abs(this.block.Y - block.Y), 2));
        }

        public int MinDistance(Block block)
        {
            return Math.Abs(this.block.X - block.X) + Math.Abs(this.block.Y - block.Y);
        }

        public IEnumerable<Block> GetBlocks()
        {
            yield return block;
        }

        public bool Is(Block block)
        {
            return this.block == block;
        }
    }
}
