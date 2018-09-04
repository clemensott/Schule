#include "mbed.h"

//        LSB                                                      MSB
//        2^0   2^1   2^2                                          2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1

// DigitalIn sw4(P1_16);

//        Sw1    Sw2    Sw3    Sw4
//        Bit0   Bit1   Bit2   Bit3
BusIn btn(P0_10, P0_15, P0_23, P1_16);

// Sw1 => D18 blinken,
// Sw2 => D17 blinken ...usw...usw..
// Sw3 => D16 
// Sw4 => D15

void BtnBlinker1();

void BtnBlinker2();
//wenn Sw1 und Sw2 gedrückt sollen D20 und D19 leuchten

//wenn Sw3 oder Sw4 gedrückt sollen D18 und D17 leuchten

//ansonsten finster

void LeftRunLight();
void RightRunLight();

void LeftSnake();

int main()
{
  lb = 0;
	while(1)
	{
		LeftSnake();
	}
}

void BtnBlinker1()
{
    if(btn & 1)
		{
			lb = 4;
		}
		if(btn & 2)
		{
			lb = 8;
		}
		if(btn & 4)
		{
			lb = 16;
		}
		if(btn & 8)
		{
			lb = 32;
		}
		
		wait_ms(100);
		lb = 0;
		wait_ms(100);
  
}

void BtnBlinker2()
{
	if(btn & 1 && btn & 2)
	{
		lb = 1;
		lb = 2;
	}
	else if(btn & 4 || btn & 8)
	{
		lb = 4;
		lb = 8;
	}
	else
		lb = 0;
}

void LeftRunLight()
{
	lb = 1;
	for(int i = 0; i < 12; i++)
	{
		wait_ms(100);
		lb = (lb << 1) | 1;
		
	}
}

void RightRunLight()
{
	lb = 12;
	for(int i = 0; i < 12; i++)
	{
		wait_ms(100);
		lb = (lb >> 1) | 1;
		
	}
}

void LeftSnake()
{
	lb = 1;
	for(int i = 0; i < 12; i++)
	{
		wait_ms(100);
		lb = (lb << 1) | 1;
		
	}
	
	for(int i = 0; i < 12; i++)
	{
		wait_ms(100);
		lb = (lb << 1) | 0;
		
	}
	
}
