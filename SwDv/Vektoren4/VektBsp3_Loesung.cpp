
#include "stdafx.h"

// Bearbeitung von Arrays mithilfe von Funktionen
// Die Funktion bekommt das Array und die Länge des
// Arrays als Parameter übergeben.

// aAry mit aLaenge Daten befüllen
// Bsp.: FillAry(A, 10, 10, 3); ergibt [10, 20, 30]
void FillAry(int aAry[], int aVon, int aSchritt, int aLaenge);

// Elemente von aAry auf der Konsole ausgeben
void PrintAry(char aName[], int aAry[], int aLaenge);

// Elemente von aQuelle auf aZiel kopieren
// Die Arrays sind aLaenge lang
void CopyAry(int aQuelle[], int aZiel[], int aLaenge);

// Elemente von aQuelle in gespiegelter Form auf aZiel schreiben
// Bsp.: aQuelle[4, 7, 9, 5]  =>  aZiel[5, 9, 7, 4]
void Spiegeln(int aQuelle[], int aZiel[], int aLaenge);

// Elemente von aQuelle an aZiel anhaengen
// Bsp.: aQuelle[10,11,12]; aZiel[20,21,22]; => aZiel[20,21,22, 10,11,12]
// return = aQLaenge + aZLaenge also Anhaengen() soll die neue Laenge von
// Ziel zurückgeben
int Anhaengen(int aQuelle[], int aZiel[], int aQLaenge, int aZLaenge);

// Bsp.: aA[7, 8, 9]  aB[14, 15, 16]
// =>    aC[7, 14, 8, 15, 9, 16]
// returns Lange von C
int Mischen(int aA[], int aB[], int aC[], int aLaenge);




void main()
{
	int A[5], B[5];
  
  // A mit 13, 15, 17 ... befüllen
	FillAry(A, 13, 2, 5);

  // A ausgeben
	PrintAry("A", A, 5);

	// Weitere Aufgaben:
	//1. B mit 20, 23, ... befüllen
	FillAry(B, 20, 3, 5);
	
	//2. B ausgeben
	PrintAry("B", B, 5);

	//3. A mit CopyAry() nach B kopieren
	CopyAry(A, B, 5);

	//4. B ausgeben
	PrintAry("B", B, 5);
}

void FillAry(int aAry[], int aVon, int aSchritt, int aLaenge)
{
	int val = aVon;

	for (int i = 0; i < aLaenge;i++)
	{
		aAry[i] = val;
		val += aSchritt;
	}
}

void CopyAry(int aQuelle[], int aZiel[], int aLaenge)
{
	for (int i = 0; i < aLaenge; i++)
	{
		aQuelle[i] = aZiel[i];
	}
}

void PrintAry(char aName[], int aAry[], int aLaenge)
{
	printf("%s = \n", aName);

	for (int i = 0; i < aLaenge; i++)
	{
		printf("%d = %d\n", i, aAry[i]);
	}
}

void Spiegeln(int aQuelle[], int aZiel[], int aLaenge)
{
	for (int i = 0; i < aLaenge; i++)
	{
		aZiel[i] = aQuelle[aLaenge - i - 1];
	}
}

int Anhaengen(int aQuelle[], int aZiel[], int aQLaenge, int aZLaenge)
{
	for (int i = 0; i < aQLaenge; i++)
	{
		aZiel[aZLaenge + i] = aQuelle[i];
	}

	return aQLaenge + aZLaenge;
}

int Mischen(int aA[], int aB[], int aC[], int aLaenge)
{
	for (int i = 0; i < aLaenge; i++)
	{
		aC[i * 2] = aA[i];
		aC[i * 2 + 1] = aB[i];
	}

	return aLaenge * 2;
}