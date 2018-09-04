
#include "mbed.h"

// Lauflicht welches sich bei jedem Btn-Click um einen Schritt weiter bewegt
// Lösung1: mit Polling geht im Prinzip verliert aber Clicks

// Alle 12-Leds des M0-Boards zu einer Bitgruppe zusammenfassen
//        LSB                                                      MSB
//        2^0   2^1   2^2                                          2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1

DigitalIn sw4(P1_16);

// 1x Aufrufen => LL um einen Schritt weiterbewegt
void OneRunLightStep();

int prevSw4=0; // Schalterzustand so wie er bei der letzten Abfrage war

// CheckButton() liefert nur dann 1 wenn eine 
// am Button eine Flanke erkannt wurde
int CheckButton();

int main()
{
  lb = 1;
  while(1)
  {
    if( CheckButton() )
      OneRunLightStep();
  }
}

int CheckButton()
{
  int ret = 0;
  if( (sw4==1) && prevSw4==0 )
    ret = 1;
  prevSw4 = sw4; // alten Zustand merken
  return ret;
}

void OneRunLightStep()
{
  // if( lb==2048 )
  if( lb==0 ) // wenn finster LED1 wieder setzen
    lb = 1;
  else
    lb = lb << 1; // ansonsten schieben
}













