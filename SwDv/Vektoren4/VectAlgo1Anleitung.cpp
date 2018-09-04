

#include  "stdio.h"
#include  "iostream.h"


// Die 2 aufsteigend sortierten Arrays aA und aB 
// zu einem aufsteigend sortierten Array aC zusammenfassen
// aSzA,aSzB aSzC Größe von aA, aB und aC
void MergeSorted(int aA[], int aB[], int aC[], int aSzA, int aSzB, int* aSzC)
{
  int iA, iB, iC;

  iB=0; iC=0;

	Für iA von 0 bis aSzA-1
	{
    Solange aB[iB]<=aA[iA] und iB innerhalb von aSzB
		{
			Wert aus aB[] in aC[] übernehmen
			iB und iC erhöhen
		}
		
		Wert aus aA[] in aC[] übernehmen
		iC erhöhen
	}
	
	*aSzC zurückliefern
}



void DeleteSeveralNumbers(int aSrc[], int aDest[], int aSrcSize, int* aDestSize)
{
  int destIdx = 0;
  int i, currCopy;

  do {
    currCopy = 0;
		Für i von 0 bis aSrcSize-1
		{
      Falls wir noch kein neues currCopy haben
      Neues currCopy mit der folgenden Bedingung suchen
			Wenn currCopy==0 und aSrc[i]!=0 
      {
        Neues currCopy aus aSrc[i] übernehmen
        currCopy auf aDest ablegen
        destIdx erhöhen
      }
      
			Wenn wir schon ein currCopy haben // ( aSrc[i]==currCopy )
        aSrc[i] als bearbeitet markieren // ( =0 setzen )
    }
  }
  while( solange ein currCopy gefunden wird ); // ( currCopy!=0 )
	
	*aDestSize zurückliefern
}


void DeleteSeveralNumbersV2(int aSrc[], int aDest[], int aSrcSize, int* aDestSize)
{
	int destSize = 0;
	int i, j, foundInDest;

	Für i von 0 bis aSrcSize-1
	{
		foundInDest = 0;
		Für j von 0 bis destSize-1
		{
			Nachsehen ob aSrc[i] in aDest[] vorkommt
			Wenn ja 
				foundInDest=1; break;
		}
		
		if( foundInDest!=1 ) // Wenn nicht gefunden
		{
			aSrc[i] auf aDest[destSize]
			destSize erhöhen.
		}
	}

	*aDestSize = destSize;
}



void VectOut(int aVect[], int aSize)
{
  for(int i=0; i<aSize; i++)
    printf("%d ",aVect[i]);
  printf("\n");
}


