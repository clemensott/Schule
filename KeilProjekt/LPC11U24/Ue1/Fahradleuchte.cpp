
#include "mbed.h"
#include "BtnEventM0S.h"

BtnEventM0 sw4(P1_16), sw3(P0_23);
BusOut lb(P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
BusOut stLed(P1_13,P1_12);


class Fahrradleuchte {
	public:
		void Init();
		void State1();
		void State2();	
		void State3(); 
	private:	
		void State1Action();	// Bit0 ( Led ) mit 2Hz blinken
		void State2Action();	// Bit2 ( Led ) mit 5Hz blinken
		void State3Action();	// Bit4 ( Led ) mit 10Hz blinken
	public:
		int state;
	private:
		Timer t1;
	
		
};

Fahrradleuchte fl;

int main(void)
{
	sw3.Init(); sw4.Init();
	fl.Init();
	while(1)
	{
		if( fl.state==1)
			fl.State1();
		if( fl.state==2)
			fl.State2();
		if( fl.state==3)
			fl.State3();
	}
}


void Fahrradleuchte::Init()
{
	state=1; t1.start();
}

void Fahrradleuchte::State1()
{
	//Einmalige Aktion beim Eintritt in die Zustandsfunktion
	stLed = 1; // Anzeigen, dass wir im Zustand 1 sind
	t1.reset();
	
	while(1)
	{
		State1Action();
		State2Action();
		State3Action();
		//Btn's abfragen und möglicherweise Zustand ändern
		if( sw4.CheckFlag() )
		{ state = state + 1; return; }
		if( sw3.CheckFlag() )
		{ state = state - 1; return; }
	}
}

void Fahrradleuchte::State1Action()
{
	if( t1.read_ms()>500)
	{
		t1.reset();
		if( lb == 0)
			lb = 1;
		else
			lb = 0;
	}
}

void Fahrradleuchte::State2Action()
{
	if( t1.read_ms()>500)
	{
		t1.reset();
		if( lb == 0)
			lb = 1;
		else
			lb = 0;
	}
}

void Fahrradleuchte::State3Action()
{
	if( t1.read_ms()>200)
	{
		t1.reset();
		if(lb == 0)
			lb = 1;
		else
			lb = lb << 1;
	}
}






