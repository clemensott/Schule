using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim
{
    class MinSearch : Search
    {
        public MinSearch(Labyrinth labyrinth, ITarget target, IRelationInterpreter interpreter)
            : base(labyrinth, target, interpreter)
        {
        }

        protected override IEnumerable<Block> GetNeighborsOrdered(Block block)
        {
            return GetNeighbors(block).OrderBy(n => Target.MinDistance(block));
        }
    }
}
