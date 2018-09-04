
using System;
using System.Collections;

namespace LL_CS
{
  interface IHLContainer
  {
    int Count();
    object Fist();
    object Next();
    
    void AddHead(object aObj);
    object RemoveHead();
    void AddTail(object aObj);
    object RemoveTail();

    object At(int aPos);
    object Find(object aTestObject, IComparer aCmp);
    object Remove(object aObj);
    object RemoveAt(int aIdx, object aObj);
    
    void InsertSorted(object aObj, IComparer aCmp);
    void InsertAtPos(object aObj, int aPos);
    void Print();
  }
}
