#include "mbed.h"

//Lauflicht welches bei jedem ButtonClick um einen Schritt weiter geht

//        LSB                                                      MSB
//        2^0   2^1   2^2                                          2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1
BusIn btn(P0_10, P0_15, P0_23, P1_16);


void OneRunLightStep();

int CheckButton();
int prevBtn = 0;

int main()
{
	
	while(1)
	{
		
		if(CheckButton())
		{
			
			OneRunLightStep();
			wait_ms(100);
		}
	}
}

//Einen Schritt des RunLigh's ausführen
void OneRunLightStep()
{
	if(lb == 0)
		lb = 1;
	else
		lb = lb << 1;
}

int CheckButton()
{
	int ret;
	
	if(btn & 1)
	{
		if(prevBtn == 0)
		{
			ret = 1;
		}
		else
			ret = 0;
		
		prevBtn = (btn & 1);
		return ret;
	
	}
	
}
