
using System;
using System.Collections;
using System.Text;

namespace LL_CS
{
    class CsLink
    {
        public CsLink next;
        public object data;
        public CsLink(Object aData)
        {
            data = aData;
        }
    }

    class CsLinkedList : IHLContainer
    {
        #region Members
        CsLink _head;
        CsLink _tail;
        CsLink _iter;
        #endregion

        public CsLinkedList() { }

        public void Clear()
        {
            _iter = _head = _tail = null;
        }

        public int Count()
        {
            return 0;
        }

        public object First()
        {
            _iter = null;
            if (_head == null)  // Liste ist Leer
                return null;


            _iter = _head;
            return _iter.data;
        }

        public object Next()
        {
            if (_iter == null)
                return null;

            _iter = _iter.next;

            if (_iter == null)
                return null;

            return _iter.data;
        }

        public void AddHead(object aObj)
        {
            CsLink elem = new CsLink(aObj);
            // im allgemeinen Fall passt das mit der _tail
            if (_head == null) // spezialfall Liste ist leer
            {
                _head = _tail = elem;
            }
            else
            {
                elem.next = _head;
                _head = elem;
            }



        }

        public object RemoveHead()
        {
            if (_head == null)  // Liste ist leer
                return null;



            if (_head == _tail)  // Nur ein Elemt
            {
                _tail = null;
            }
            //head und tail müssen nach dem Remove wieder stimmen

            _head = _head.next;
            return null;
        }

        public void AddTail(object aObj)
        {
            CsLink it = new CsLink(aObj);
            CsLink elem = _tail;


            if (_tail == null)
            {
                _tail = _head = it;
            }
            else
            {
                elem.next = it;
                _tail = elem.next;
            }

        }

        public object RemoveTail()
        {
            CsLink it = _head;

            if (_head == _tail)
            {
                _head = _tail = null;
            }

            while (true)
            {
                if (it.next == _tail) // Vorgänger gefunden
                    break;
                it = it.next; // iter++
            }
            _tail = it;
            it.next = null;
            return _tail;
        }

        public object At(int aPos)
        {
            CsLink it = _head;

            for (int i = 1; i <= aPos; i++)
            {
                if (i == aPos)
                    return it.data;

                it = it.next;
            }

            return null;

        }

        public object Find(Object aTestObject, IComparer aCmp)
        {
            CsLink it = _head;
            int sol;

            while (First() != null)
            {
                sol = aCmp.Compare(it.data, aTestObject);

                if (sol == 0)
                    return it.data;

                if (it.next == null)
                    return null;

                it = it.next; // iter++
            }
            return null;



        }

        public object Remove(Object aObj)
        {
            object obj = First();
            object clear;

            while (obj != null)
            {
                if (_iter.next.data == aObj)
                {
                    clear = _iter.next.data;
                    _iter.next = _iter.next.next;
                    return clear;
                }
                obj = Next();
            }
            return null;
        }

        public object RemoveAt(int aIdx)
        {
            return null;
        }

        public void InsertSorted(object aObj, IComparer aCmp)
        {

            CsLink iter;
            CsLink prev;
            CsLink elem = new CsLink(aObj);
            iter = prev = _head;

            if (First() == null)
            {
                iter = elem;
                _head = prev = _tail = elem;
                return;
            }

            while (iter != null)
            {
                if (aCmp.Compare(iter.data, aObj) > 0)
                {
                    if (iter == _head)
                    {
                        elem.next = _head;
                        _head = elem;
                    }
                    else
                    {
                        prev.next = elem;
                        elem.next = iter;
                    }
                    return;
                }

                prev = iter;
                iter = iter.next;
            }


            _tail.next = elem;
            _tail = elem;


        }

        public void InsertAtPos(object aObj, int aPos)
        {
        }

        public void Print()
        {
            object data = First();

            while (data != null)
            {
                Console.Write("{0} ", data);
                data = Next();
            }

            Console.WriteLine();

        }
    }
}
