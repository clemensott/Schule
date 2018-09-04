
using System;
using System.Collections;

namespace LL_CS
{
    interface IHLContainer
    {
        void Clear();
        int Count();
        object First();
        object Next();

        void AddHead(object aObj);
        object RemoveHead();
        void AddTail(object aObj);
        object RemoveTail();

        object At(int aPos);
        object Find(object aTestObject, IComparer aCmp);
        object Remove(object aObj);
        object RemoveAt(int aIdx);

        void InsertSorted(object aObj, IComparer aCmp);
        void InsertAtPos(object aObj, int aPos);
        void Print();
    }
}
