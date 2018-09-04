
using System;
using System.Collections;
using System.Text;

namespace LL_CS
{

  class Student
  {
    public string m_Name;
    public int m_CatNr;

    public Student(string aName, int aCatNr)
    {
      m_Name = aName; m_CatNr = aCatNr;
    }

    public void Set(string aName, int aCatNr)
    {
      m_Name = aName; m_CatNr = aCatNr;
    }

    public override string ToString()
    {
      return String.Format("{0},{1}", m_Name, m_CatNr);
    }
  }

  class StudentNameCompare : IComparer
  {
    public int Compare(object x, object y)
    {
      // die übergebenen Objekte in Student's casten
      Student s1 = (Student)x;
      Student s2 = (Student)y;
      // für strings gibts schon eine fertige Compare-Methode
      // die wir nur noch verwenden müssen.
      return string.Compare(s1.m_Name, s2.m_Name);
    }
  }

  class StudentCatNrCompare : IComparer
  {
    public int Compare(object x, object y)
    {
      // die übergebenen Objekte in Student's casten
      Student s1 = (Student)x;
      Student s2 = (Student)y;
      if (s1.m_CatNr < s2.m_CatNr)
        return -1;
      if (s1.m_CatNr > s2.m_CatNr)
        return 1;
      return 0;
    }
  }

  class IntCompare : IComparer
  {
    public int Compare(object x, object y)
    {
      return (int)x - (int)y;
    }
  }
}
