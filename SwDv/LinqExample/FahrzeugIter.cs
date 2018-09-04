using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class FahrzeugIter
    {
        public Fahrzeug Fahrzeug { get; set; }

        public FahrzeugIter Next { get; set; }

        public FahrzeugIter(IEnumerator<Fahrzeug> enumerator)
        {
            Fahrzeug = enumerator.Current;

            if (enumerator.MoveNext()) Next = new FahrzeugIter(enumerator);
        }
    }
}
