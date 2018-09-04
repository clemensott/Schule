using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class Fahrzeuge : IEnumerable<Fahrzeug>
    {
        private FahrzeugIter iter;

        public Fahrzeuge()
        {
            iter = FahrzeugGenerator.GetIter();
        }

        public IEnumerator<Fahrzeug> GetEnumerator()
        {
            return new FahrzeugEnumerator(iter);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FahrzeugEnumerator(iter);
        }
    }
}
