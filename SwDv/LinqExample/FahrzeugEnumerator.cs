using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample
{
    class FahrzeugEnumerator : IEnumerator<Fahrzeug>
    {
        public Fahrzeug Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        private FahrzeugIter iter;

        public FahrzeugEnumerator(FahrzeugIter iter)
        {
            this.iter = iter;
        }

        public void Dispose()
        {
            iter = null;
            Current = null;
        }

        public bool MoveNext()
        {
            if (iter == null)
            {
                Current = null;
                return false;
            }

            iter = iter.Next;

            if (iter == null)
            {
                Current = null;
                return false;
            }

            Current = iter.Fahrzeug;
            return true;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
