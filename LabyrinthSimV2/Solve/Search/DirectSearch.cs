using System.Collections.Generic;
using System.Linq;

namespace LabyrinthSim
{
    class DirectSearch : Search
    {
        public DirectSearch(Labyrinth labyrinth, ITarget target, IRelationInterpreter interpreter) 
            : base(labyrinth, target, interpreter)
        {
        }

        protected override IEnumerable<Block> GetNeighborsOrdered(Block block)
        {
            return GetNeighbors(block).OrderBy(n => Target.DirectDistance(n));
        }
    }
}
