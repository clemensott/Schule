#include "mbed.h"

//Lauflicht welches bei jedem ButtonClick um einen Schritt weiter geht
//Lösung 2: mit Interrupt 

//        LSB                                                      MSB
//        2^0   2^1   2^2                                          2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1
//BusIn btn(P0_10, P0_15, P0_23, P1_16);

//sw4 als Interrupt konfigurieren
InterruptIn sw4(P1_16);

// 1x Aufrufen => LL um einen Schritt weiterbewegt
void OneRunLightStep();


// Diese Funktion wird aufgerufen wenn die µc-HW eine steigende Flanke an sw4 erkennt
void Sw4ISR()
{
	// wenn sw4 jetzt immer noch 1 ist...
	if(sw4.read() == 1)
		OneRunLightStep();
}


int main()
{
	lb = 1;
	//Der µc-HW sagen, dass sie Sw4ISR() aufrufen soll wenn die steigende Flanke erkannt wurde
	sw4.rise(Sw4ISR);
	
	while(1)
	{
			
	}
}

//Einen Schritt des RunLight's ausführen
void OneRunLightStep()
{
	if(lb == 0)
		lb = 1;
	else
		lb = lb << 1;
}




