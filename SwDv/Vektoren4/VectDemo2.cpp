
#include "stdafx.h"

// Werte von einem Array auf das andere umkopieren

int _tmain(int argc, _TCHAR* argv[])
{
  int A[] = { 1, 10, 2, 9, 3, 8, 4, 7, 5, 6 };
	int B[10];
	int i;

  // Aufgabe1:
	// Mithilfe einer for-Schleife die Werte 
	// von A nach B kopieren
 
	for (i = 0; i < 10; i++)
	{
		B[i] = A[i];
	}

  // Aufgabe2: B ausgeben

	for (int i = 0; i < 10; i++)
		printf("%d %d", i, B[i]);
  
  return 0;
}

