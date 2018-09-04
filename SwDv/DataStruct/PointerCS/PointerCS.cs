using System;
using System.Text;

namespace PointerCS
{
  class Person
  {
    public int _age;
    public string _name;

    public Person(string aName, int aAge)
    {
      _age = aAge;
      _name = aName;
    }

    public void SetName(string aName)
    {
      _name = aName;
    }
  }

  struct PersonS
  {
    public int _age;
    public string _name;
    public static PersonS ini;

    public PersonS(string aName, int aAge)
    {
      _age = aAge;
      _name = aName;
    }

    public void SetName(string aName)
    {
      _name = aName;
    }
  }
  
  class PointerCS
  {
    public static PersonS gVar; // gloable Variable

    static void Main(string[] args)
    {
      // ZuweisungPerReference();
      ZuweisungPerValue();
    }

    static void ZuweisungPerReference()
    {
      // Zuweisung per Reference
      Person p1 = null; Person p2 = new Person("Otto", 17);
      
      p1 = p2; // p1 und p2 zeigen auf dasselbe Objekt
      
      p1.SetName("Hugo");
    }

    static void ZuweisungPerValue()
    {
      // hier wird sofort speicher angelegt man braucht kein new
      PersonS p1;
      PersonS p2;

      p1._age = 7; p1._name = "Otto";

      // per Value Daten werden kopiert
      p2 = p1;

      p2._name = "Sepp";
    }
  }
}
