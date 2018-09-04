
// #include <iostream.h>
#include "stdafx.h"
#include <string.h>
#include <conio.h>
#include <ctype.h>

class Person {
  public:
    int _age;
    char _name[20];
  public:
    Person(char aName[], int aAge)
    {
      _age=aAge;
      strcpy(_name,aName);
    }

    void SetName(char aName[])
    {
      strcpy(_name,aName);
    }
};

void PrintPerson(Person* aP)
{
  printf("%s  %d\n", aP->_name, aP->_age);
}

void ZuweisungPerReference();
void ZuweisungPerValue();

void main()
{
  ZuweisungPerValue();
}

void ZuweisungPerValue()
{
  Person* p1 = new Person("",0); 
  Person* p2 = new Person("Otto",17);

  *p1 = *p2;

  p1->SetName("Hugo");
}


void ZuweisungPerReference()
{
  // Zuweisung per Reference
  Person* p1=0;  Person* p2 = new Person("Otto",17);

  PrintPerson(p2);

  p1 = p2; // p1 und p2 zeigen auf dasselbe Objekt
  (*p1).SetName("Hugo");

  PrintPerson(p2);
}


Person gVar("Sepp", 20); // globale Variable

void GlobalLokalHeap()
{
  Person locVar("Hugo", 17); // lokale Variable am Stack

  Person* p2 = new Person("Franz", 7); // Variabale mit Daten im dynamischen Speicher

  locVar = gVar; // Zuweisung per Value

  p2 = &gVar; // p2 zeigt auf gVar Zuweisung per Reference

  locVar = *p2; // Zuweisung per Value
}

