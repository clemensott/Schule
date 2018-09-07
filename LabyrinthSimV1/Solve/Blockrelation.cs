using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthSim
{
    enum RelationType { Unkown, Open, Close }

    class Blockrelation
    {
        public Block Block1 { get; private set; }

        public Block Block2 { get; private set; }

        public RelationType Relation { get; private set; }

        public Blockrelation(Block block)
        {
            Block1 = block;
            Block2 = null;

            Relation = RelationType.Close;
        }

        public Blockrelation(Block block1, Block block2)
        {
            Block1 = block1;
            Block2 = block2;

            Relation = RelationType.Unkown;
        }

        public void Open()
        {
            Relation = RelationType.Open;
        }

        public void Close()
        {
            Relation = RelationType.Close;
        }

        public Block GetOther(Block block)
        {
            return block == Block1 ? Block2 : Block1;
        }

        public override string ToString()
        {
            double x = Math.Abs(Block1.X + Block2?.X ?? Block1.X) / 2.0;
            double y = Math.Abs(Block1.Y + Block2?.Y ?? Block1.Y) / 2.0;

            return string.Format("{0} x {1}", x, y);
        }
    }
}
