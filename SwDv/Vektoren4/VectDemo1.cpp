#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	// Deklaration von Arrays
	int A[10]; // 10 Zahlen großes int Array ohne Initialisierung
	int C, i, val;

	// 9 Zahlen großes Array mit Initialisierung
	int D[] = { 10, 11, 12, 13, 14, 15, 16, 17, 18 };

	// indizierter Zugriff
	A[2] = 17;
	val = A[2];

	// Alle Werte von D auf 0 setzen
	  // Ohne for-Schleife ziemlich mühsam
	D[0] = 0; D[1] = 0; D[2] = 0;
	for (i = 0; i < 9; i++)
		D[i] = 0;

	//Aufgabe1: A mit dem Muster 10, 20, 30 ... 100 befüllen
	// so nicht A[0]=10; A[1]=20; A[2]=30; ...
	// Loesung1

	for (int i = 0; i < 10; i++)
		A[i] = (i + 1) * 10;


	// Aufgabe2:
	// Alle werte die in A[] drinnen stehen auf der Konsole ausgeben:
	// Mit der for-schleife alle elemente in A besuchen und ausgeben

	for (int i = 0; i < 10; i++)
		printf("%d, ", A[i]);

	// Aufgabe3:
	  // Werte und Index senkrecht ausgeben:
	  // 0 10
	  // 1 20
	  // 2 30


	for (int i = 0; i < 10; i++)
		printf("%d %d", i, A[i]);

	return 0;
}

