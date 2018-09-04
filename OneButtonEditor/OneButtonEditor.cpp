
#include "mbed.h"
#include "BtnEventM0.h"

Serial pc(USBTX, USBRX);
//        LSB                                                      MSB
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

BtnEventM0 btn(P1_16);

const int S_BASE=1, S_UP=2, S_DOWN=3;

class StmDBL {
	public:
		void Init();
		void Base();
		void Up();
		void Down();
	public:
		int state;
};

StmDBL stm;

int main()
{
	pc.baud(500000);
	// Theoriefrage:
	// Erklären Sie die prinzipielle Funktionsweise einer StateMachine
	// Wie arbeiten die untenstehende while-Schleife und die Funktionen von StmDBL
	// zusammen um die StateMachine zu realisieren
	while(1) {
		if( stm.state==S_BASE )
			stm.Base();
		if( stm.state==S_UP )
			stm.Up();
		if( stm.state==S_DOWN )
			stm.Down();
	}
}

void StmDBL::Base()
{ 
}

void StmDBL::Up()
{ 
}

void StmDBL::Down()
{ 
}

void CkeckForDoubleClick()
{
	if( btn.CheckFlag() ) {
		wait_ms(300);
		if( btn.CheckFlag() )
			; // es war ein DoubleClick
		else
			; // es war ein EinfachClick
	}
}

void CkeckForContinousPress()
{
	if( btn.CheckFlag() ) {
		// Aktion für EinfachClick z.B. cnt++ ausführen
		wait_ms(300);
		if( btn.CheckFlag() )
			; // es war ein DoubleClick
		else if( btn.CheckButton() )
			; // ContinousPress erkannt
	}
}




