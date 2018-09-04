
#include "stdio.h"

// Bearbeitung von Arrays mithilfe von Funktionen
// Die Funktion bekommt das Array und die Länge des
// Arrays als Parameter übergeben.

// aAry mit aLaenge Daten befüllen
// Bsp.: FillAry(A, 10, 10, 3); ergibt [10, 20, 30]
void FillAry(int aAry[], int aVon, int aSchritt, int aLaenge);

// Elemente von aAry auf der Konsole ausgeben
void PrintAry(int aAry[], int aLaenge);

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

  // A ausgeben


	// Weitere Aufgaben:
	//1. B mit 20, 23, ... befüllen
	
	//2. B ausgeben
	//3. A mit CopyAry() nach B kopieren
	
	//4. B ausgeben
}

void CopyAry(int aQuelle[], int aZiel[], int aLaenge)
{

}

void PrintAry(int aAry[], int aLaenge)
{
	
}

void FillAry(int aAry[], int aVon, int aSchritt, int aLaenge)
{
	
}

