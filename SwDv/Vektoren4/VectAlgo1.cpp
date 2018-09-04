

#include  "stdio.h"
//#include  "iostream.h"



void VectOut(int aVect[], int aSize);

void Merge12(int aA[], int aB[], int aC[], int aSize);

// gibt die Länge von aDest zurück
int DeleteSeveralNumbers(int aSrc[], int aDest[], int aSrcSize);

void DeleteSeveralNumbersV2(int aSrc[], int aDest[], int aSrcSize, int* aDestSize);

// gibt die Länge von C zurück
int MergeSorted(int aA[], int aB[], int aC[], int aSzA, int aSzB);


// Testdaten

int A[] = { 1,2,3,4,5, 1,2,3,4,5, 1,2,3,4,6, 1,2,3,4,7, 1,2,3,4,8 }; // Size=25

int B[50];

int C[] = { 1, 5, 6, 9, 13, 14, 17 }; // Size=7

int D[] = { 2, 3, 4, 7, 8, 10, 11, 12, 15, 16 }; // Size=10

int E[] = { 10, 50, 60, 90, 130, 140, 170 }; // Size=7



void main()
{
	int szB;

	Merge12(C, E, B, 7);
	VectOut(B, 14); printf("\n");

	MergeSorted(C, D, B, 7, 10, &szB);
	VectOut(B, szB); printf("\n");

	VectOut(A, 25); printf("\n"); // Vektor A ausgeben

	DeleteSeveralNumbersV2(A, B, 25, &szB);

	VectOut(B, szB); printf("\n"); // Ergebnissvektor nach DeleteSeveralNumbers
}

void VectOut(int aVect[], int aSize)
{
	for (int i = 0; i < aSize; i++)
	{
		printf("%d; ", aVect[i]);
	}
}

void Merge12(int aA[], int aB[], int aC[], int aSize)
{
	for (int i = 0; i < aSize; i++)
	{
		aC[i * 2] = aA[i];
		aC[i * 2 + 1] = aB[i];
	}
}

// gibt die Länge von aDest zurück
int DeleteSeveralNumbers(int aSrc[], int aDest[], int aSrcSize)
{
	int aDestSize = 0;

	for (int i = 0; i < aSrcSize; i++)
	{
		bool contains = false;

		for (int j = i + 1; j < aSrcSize; j++)
		{
			if (aSrc[i] == aSrc[j])
			{
				contains = true;
				break;
			}
		}

		if (!contains)
		{
			aDest[aDestSize++] = aSrc[i];
		}
	}
}

void DeleteSeveralNumbersV2(int aSrc[], int aDest[], int aSrcSize, int* aDestSize)
{
	int destSize = DeleteSeveralNumbers(aSrc, aDest, aSrcSize);

	aDestSize = &destSize;
}

// gibt die Länge von C zurück
int MergeSorted(int aA[], int aB[], int aC[], int aSzA, int aSzB)
{
	int aI = 0, bI = 0;

	while (aI < aSzA || bI < aSzB)
	{
		if (aI >= aSzA)
		{
			aC[aI + bI] = aB[bI];
			bI++;
		}
		else if(bI>=aSzB)
		{
			aC[aI + bI] = aA[aI];
			aI++;
		}
		else if(aA[aI]<aB[aI])
		{
			aC[aI + bI] = aA[aI];
			aI++;
		}
		else
		{
			aC[aI + bI] = aB[bI];
			bI++;
		}
	}
}