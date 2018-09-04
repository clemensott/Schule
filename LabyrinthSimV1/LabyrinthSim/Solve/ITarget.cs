using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    interface ITarget
    {
        IEnumerable<Block> GetBlocks();
        bool Is(Block block);
        double DirectDistance(Block block);
        int MinDistance(Block block);
    }
}
