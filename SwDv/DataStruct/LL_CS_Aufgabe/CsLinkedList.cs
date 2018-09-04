
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

    public int Count()
    {
      return 0;
    }

    public object Fist()
    {
      return null;
    }

    public object Next()
    {
      return null;
    }

    public void AddHead(object aObj)
    {
    }

    public object RemoveHead()
    {
      return null;
    }

    public void AddTail(object aObj)
    {
    }

    public object RemoveTail()
    {
      return null;
    }

    public object At(int aPos)
    {
      return null;
    }

    public object Find(Object aTestObject, IComparer aCmp)
    {
      return null;
    }

    public object Remove(Object aObj)
    {
      return null;
    }

    public object RemoveAt(int aIdx, object aObj)
    {
      return null;
    }

    public void InsertSorted(object aObj, IComparer aCmp)
    {
    }

    public void InsertAtPos(object aObj, int aPos)
    {
    }

    public void Print()
    {
    }
  }
}
