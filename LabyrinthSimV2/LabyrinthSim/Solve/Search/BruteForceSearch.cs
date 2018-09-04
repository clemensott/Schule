using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim
{
    class BruteForceSearch : Search
    {
        public BruteForceSearch(Labyrinth labyrinth, ITarget target, IRelationInterpreter interpreter)
            : base(labyrinth, target, interpreter)
        {
        }

        protected override IEnumerable<Block> GetNeighborsOrdered(Block block)
        {
            return GetNeighbors(block);
        }

        protected override bool TryAdd(Block next)
        {
            if (CurrentLength > 0 && !IsOpen(next, current[CurrentLength - 1])) return false;
            if (GetCurrent().Contains(next)) return false;

            return true;
        }
    }
}
