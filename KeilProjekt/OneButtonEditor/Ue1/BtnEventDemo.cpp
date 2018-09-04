
#include "mbed.h"
#include "BtnEventM0S.h"

//        LSB                                                      MSB
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

BtnEventM0 sw4(P1_16), sw2(P0_15), sw1(P0_10), sw3(P0_23);

void OneLeftStep();
void OneRightStep();
void ExecuteAutoButtons();

int main(void)
{
	lb = 1;
	sw4.Init(); sw2.Init(); sw1.Init(); sw3.Init();
	
	while(1)
	{
		if( sw4.CheckFlag() )
			OneRightStep();
		if( sw3.CheckFlag() )
			OneLeftStep();
		if( sw1.CheckFlag() )
			OneRightStep();
		if( sw2.CheckFlag() )
			OneLeftStep();
  }
}


void OneLeftStep()
{
	if( lb==2048 ) {
		lb = 1;
		return;
	}
	lb = lb << 1;
}

void OneRightStep()
{
	if( lb==1 ) {
		lb = 2048;
		return;
	}
	lb = lb >> 1;
}







