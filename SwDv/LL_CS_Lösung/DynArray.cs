
using System;
using System.Collections;
using System.Text;

namespace LL_CS
{
    class DynArray
    {
        #region Members
        protected int _capacity;
        protected int _count;   // wieviel gültige Daten sind im Array
        protected object[] _ary;
        #endregion

        public DynArray()
        {
            _count = 0;
            _capacity = 10;
            _ary = new object[_capacity];
        }

        protected void CheckSpace()
        {
            if (_count < _capacity - 2)
                return;
            _capacity += 10;
            object[] newAry = new object[_capacity];
            for (int i = 0; i < _count; i++)
                newAry[i] = _ary[i];
            // delete _ary; würde man in C++ noch brauchen
            _ary = newAry;
        }

        protected void CreateSlot(int aIdx)
        {
            if(_count == 0)
            {
                _count++;
                return;
            }

            _count++;
            CheckSpace();


            for (int i = _count - 1; i >= aIdx; i--)
            {
                _ary[i +1] = _ary[i];
            }
        }

        protected void RemoveSlot(int aIdx)
        {

            for (int i = aIdx; i <= _count + 1; i++)
            {
                _ary[i] = _ary[i + 1];
            }

            _count--;
        }
    }


    class CsArrayList : DynArray, IHLContainer
    {
        public CsArrayList()
        {
        }

        public int Count()
        {
            return _count;
        }

        public void Clear()
        {
            for (int i = 0; i < _count; i++)
                _ary[i] = 0;

            _count = 0;
        }

        public object First()
        {
            return _ary[0];
        }

        public object Next()
        {
            return null;
        }

        public void AddHead(object aObj)
        {
            CreateSlot(0);
            _ary[0] = aObj;
        }

        public object RemoveHead()
        {
            object ret = _ary[0];
            RemoveSlot(0);
            return ret;
        }

        public void AddTail(object aObj)
        {
            CheckSpace();
            _ary[_count] = aObj; _count++;
        }

        public object RemoveTail()
        {
            if (_count == 0)
                return null;
            _count--;
            object ret = _ary[_count];
            _ary[_count] = null;
            return ret;
        }

        public object At(int aPos)
        {
            return _ary[aPos];
        }

        public object Find(object aTestObject, IComparer aCmp)
        {
            for (int i = 0; i < _count; i++)
                if (aCmp.Compare(_ary[i], aTestObject) == 0)
                    return _ary[i];

            return null;
        }

        public object Remove(object aObj)
        {
            for(int i = 0; i < _count; i++)
                if(aObj == _ary[i])
                {
                    RemoveSlot(i);
                    return aObj;
                }
            return null;
        }

        public object RemoveAt(int aIdx)
        {
            object student = _ary[aIdx];
            RemoveSlot(aIdx);
            return student;
        }

        public void InsertSorted(object aObj, IComparer aCmp)
        {
            if (_count == 0)
            {
                CreateSlot(0);
                _ary[0] = aObj;
                return;
            }

            for (int i = 0; i < _count; i++)
            {
                if (aCmp.Compare(_ary[i], aObj) > 0)
                {
                    CreateSlot(i);
                    _ary[i] = aObj;
                    return;
                }  
            }

            CreateSlot(_count);
            _ary[_count - 1] = aObj;
        }

        public void InsertAtPos(object aObj, int aPos)
        {
            CreateSlot(aPos);
            _ary[aPos] = aObj;
        }

        public void Print()
        {
            if (_count == 0)
            {
                Console.WriteLine("Empty!!");
                return;
            }
            for (int i = 0; i < _count; i++)
                Console.Write(" {0}", _ary[i]);
            Console.WriteLine();
        }
    }
}
