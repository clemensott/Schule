using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixRef
{
  class MatrixRef
  {
    const int MAX_XY = 4;
    
    static void Main(string[] args)
    {
      int[,] A; // Zeiger auf eine Matrix ( noch kein Speicher )
      int[,] B;
      int[,] ptr;

      // Speicherplatz anfordern und A u. B auf diesen Speicherplatz zeigen lassen.
      A = new int[MAX_XY, MAX_XY];
      B = new int[MAX_XY, MAX_XY];

      // In unseren Überlegungen für die Life-Cells bildet der erste Matrix-Index
      // die X-Richtung und der 2te die Y-Richtung ab.
      // für die X-Richtung verwenden wir i und x
      // für die Y-Richtung verwenden wir j und y

      ptr = A; // ptr zeigt jetzt auf die Matrix A
      // etwas an die Koordinaten (1,1) und (2,1) schreiben
      ptr[1,1]=1; ptr[2,1]=1;
      PrintMatrix("A", A); PrintMatrix("B", B);
      
      Console.WriteLine();
      
      ptr = B; // ptr zeigt jetzt auf die Matrix B
      // etwas an die Koordinaten (1,2) und (2,2) schreiben
      ptr[1,2]=2; ptr[2,2]=2;
      PrintMatrix("A", A); PrintMatrix("B", B);
    }
    
    static void PrintMatrix(string aName, int[,] aMat)
    {
      Console.WriteLine("{0}:", aName);
      // Schleife für die Y-Richtung
      for (int y = 0; y < MAX_XY; y++)
      {
        // Schleife für die X-Richtung
        for (int x = 0; x < MAX_XY; x++)
        {
          Console.Write("{0} ", aMat[x,y]);
        }
        Console.WriteLine();
      }
    }
  }
}
