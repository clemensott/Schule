using System.Collections.Generic;

namespace LabyrinthSim
{
    interface ITarget
    {
        Block Main { get; }
        IEnumerable<Block> GetBlocks();
        bool Is(Block block);
        double DirectDistance(Block block);
        int MinDistance(Block block);
    }
}
