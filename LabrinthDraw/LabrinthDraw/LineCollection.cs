using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabrinthDraw
{
    class LineCollection : IEnumerable<Line>
    {
        private Line[] array;

        public int Count { get { return array.Length; } }

        public LineCollection(IEnumerable<Line> collection)
        {
            array = collection.ToArray();
        }

        public IEnumerator<Line> GetEnumerator()
        {
            return array.OfType<Line>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return array.GetEnumerator();
        }
    }
}
