
// #include <iostream.h>
#include "stdafx.h"
#include <conio.h>
#include <ctype.h>

// ToDo ToDo ToDo ToDo ToDo ToDo ToDo ToDo
//
// Das AddToVar und das Swap() Bsp noch dazutun
//
//

// Globale Variablen und Pointer
int   iVar = 0;
float fVar = 0.0;

int*   iPtr = 0;
float* fPtr = 0;

int gCntr = 0;

// Funktionsprototypen
void PtrDemo1();
void PtrDemo2(int* aiPar1, float* afPar2);

// aVal wird um aInc erhöht
void AddTo(int* aVal, int aInc);

void main()
{
  PtrDemo1();

  // Übergabe von Variablenadressen
  PtrDemo2(&iVar, &fVar);
  
  iPtr=&iVar; fPtr=&fVar;
  // Übergabe von Pointern
  PtrDemo2(iPtr, fPtr);

  int val = 3;
  AddTo(&val, 2);
}

void AddTo(int* aVal, int aInc)
{
  *aVal = *aVal + aInc;
}

void PtrDemo1()
{
  iVar = 6;
  fVar = 7.8;
  
  // Referenzierung
  // Zuweisung von Adressen an Variablen
  iPtr = &iVar;
  fPtr = &fVar;
  
  // Pointer müssen dereferenziert * werden um auf die Werte der Variablen 
  // zugreifen zu können, auf welche die Pointer zeigen
  *iPtr = 11;
  *fPtr = 12.3;
}


void PtrDemo2(int* aiPar1, float* afPar2)
{
  // Zuweisung an die In/Out Parameter
  *aiPar1 = 3 + gCntr;
  *afPar2 = 3.2 + 4.7 + gCntr;

  // Cntr erhöhen
  gCntr++;
}


