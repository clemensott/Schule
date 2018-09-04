
using System;
using System.Text;
using System.Collections;

namespace ContainerDemo
{
  public class Student
  {
    public string _name;
    public int _catNr;

    public Student(string aName, int aCatNr)
    {
      _name = aName;
      _catNr = aCatNr;
    }

    public override string ToString()
    {
      return _name + " " + _catNr.ToString();
    }
  }

  class StudentNameComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      return string.Compare(((Student)x)._name, ((Student)y)._name);
    }
  }

  class StudentCatNrComparer : IComparer
  {
    public int Compare(object x, object y)
    {
      if (((Student)x)._catNr == ((Student)y)._catNr)
        return 0;
      if (((Student)x)._catNr > ((Student)y)._catNr)
        return 1;
      if (((Student)x)._catNr < ((Student)y)._catNr)
        return -1;
      return 0;
    }
  }
}
