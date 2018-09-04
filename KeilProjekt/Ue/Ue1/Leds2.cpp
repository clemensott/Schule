
#include "mbed.h"

// DigitalOut eine Variable für ein Bit
// BusOut eine Variable für eine ganze Bitgruppe

// Alle 12-Leds des M0-Boards zu einer Bitgruppe zusammenfassen

//        LSB                                                      MSB
//        2^0   2^1   2^2                                          2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1


int main()
{
 
}

