using System;
using System.Text;
using System.IO;

// Noch eine Funktion mit Zuweisungen dazutun
// damit man sieht was bei Zuweisungen (val vs. Ref) passiert

// Syntax: Wie man etwas in einer Prog. Sprache ausdrückt
//         z.B. { }  vs.  begin end;
// Semantik:
//         Die Bedeutung ( Wirkung ) dessen was man mit der Syntax
//         beschrieben hat.
//         Meist wird dieselbe Semantik mit verschiedener Syntax
//         erreicht z.B. { }  vs.  begin end; weil die verschiedenen
//         Prog. Sprachen ( C#, VB, VHDL ) eben verschiedene Syntax haben.

// In C# haben Strukturen eine implizite Wert-Semantik
// und Klassen eine implizite Referenz(Pointer)-Semantik

// Ev. noch ein minimales C# LinkedList Bsp
// um zu demonstrieren wozu Referenzen noch gut sein können.

namespace StructVsClass
{
  class ClsA
  {
    public int m_A, m_B;
    public ClsA(int aA, int aB)
    {
      m_A = aA; m_B = aB;
    }
  }

  struct StB
  {
    public int m_A, m_B;
    public StB(int aA, int aB)
    {
      m_A = aA; m_B = aB;
    }
  }

  class StructVsClass
  {
    static void Main(string[] args)
    {
      Demo1();
    }

    static void Demo1()
    {
      // s1 und s2 sind jeweils eigene Objekte
      StB s1 = new StB(1, 2);
      StB s2 = s1;

      // daten werden nur in s1 verändert
      // daten in s2 bleiben unverändert
      s1.m_A = 7; s1.m_B = 8;

      // c1 und c2 zeigen auf daselbe objekt
      ClsA c1 = new ClsA(1, 2);
      ClsA c2 = c1;

      // => daten werden scheinbar auch in c2 verändert
      c1.m_A = 7; c1.m_B = 8;
    }

    static void Demo2()
    {
    }
  }
}
