
#include "mbed.h"
#include "BtnEventM0S.h"

BusOut lb1(P1_13,P1_12,P1_7,P1_6);
BusOut lb2(P1_4,P1_3,P1_1,P1_0);
BusOut lb3(LED4,LED3,LED2,LED1);


void Led1_Blinker_Task(); // 10Hz
void Led2_Blinker_Task();	// 5Hz
void Led3_Blinker_Task();	// 2Hz

Timer T1, T2, T3;

int main(void)
{
	lb1 = lb2 = lb3 = 0;
	T1.start(); T2.start(); T3.start();
	
	while(1)
	{
		if(T1.read_ms() > 100)	// 100ms = 10Hz
		{
			T1.reset();
			Led1_Blinker_Task();
		}
		
		if(T2.read_ms() > 200)	 
		{
			T2.reset();
			Led2_Blinker_Task();
		}
		
		if(T3.read_ms() > 500)	
		{
			T3.reset();
			Led3_Blinker_Task();
		}
	}
}

void Led1_Blinker_Task()
{
	if(lb1 == 0)
		lb1=0xF;
	else
		lb1=0;
}

void Led2_Blinker_Task()
{
	if(lb2 == 0)
		lb2=0xF;
	else
		lb2=0;
}

void Led3_Blinker_Task()
{
	if(lb3 == 0)
		lb3=0xF;
	else
		lb3=0;
}






