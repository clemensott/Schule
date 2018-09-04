
#include "mbed.h"
#include "BtnEventM0S.h"

Serial pc(USBTX, USBRX);
//        LSB                                                      MSB
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

BtnEventM0 btn(P1_16);
Timer t1;


const int doubleClickWaitMills = 300, holdingClickMills = 500, isHoldingWaitMills = 400;
const int S_BASE=1, S_UP=2, S_DOWN=3;
const int S_ONE=1, S_DOUBLE=2,S_HOLDING=3;

int CheckClick();

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
int cnt;

int main()
{
	pc.baud(115200);
	stm.Init();
	cnt = 2;
	// Theoriefrage:
	// Erklären Sie die prinzipielle Funktionsweise einer StateMachine
	// Wie arbeiten die untenstehende while-Schleife und die Funktionen von StmDBL
	// zusammen um die StateMachine zu realisieren
	lb=1;
	while(!btn.CheckButton()){}
		lb=2;
	
	while(1) {
		pc.printf("State: %d", stm.state);
		pc.printf("Cnt: %d", cnt);
		lb=cnt;
		
		if( stm.state==S_BASE )
			stm.Base();
		else if( stm.state==S_UP )
			stm.Up();
		else if( stm.state==S_DOWN )
			stm.Down();
		
//		int clickState = CheckClick();
//	  pc.printf("%d ", clickState);
	}
}

void StmDBL::Base()
{ 
	int clickState = CheckClick();
	pc.printf("Base1: %d", clickState);
	
	if(clickState==S_ONE) stm.state = S_UP;
	else if(clickState==S_DOUBLE) stm.state = S_DOWN;
	else if(clickState==S_HOLDING) {
		while(btn.CheckButton()) {}
		stm.state = S_UP;
	}
	
	pc.printf("Base2: %d", stm.state);
}

void StmDBL::Up()
{ 
	int clickState = CheckClick();
	
	if(clickState==S_ONE) cnt++;
	else if(clickState==S_DOUBLE) stm.state = S_BASE;
	else if(clickState==S_HOLDING) {
		while(btn.CheckButton()) {
			
			if(t1.read_ms() < isHoldingWaitMills) continue;
			
			t1.reset();
			cnt++;
			lb=cnt;
		}
		stm.state = S_UP;
	}
}

void StmDBL::Down()
{ 
	int clickState = CheckClick();
	
	if(clickState==S_ONE) cnt--;
	else if(clickState==S_DOUBLE) stm.state = S_BASE;
	else if(clickState==S_HOLDING) {
		while(btn.CheckButton()) {
			
			if(t1.read_ms() < isHoldingWaitMills) continue;
			
			t1.reset();
			cnt--;
			lb=cnt;
		}
		stm.state = S_UP;
	}
}

void StmDBL::Init()
{
	stm.state = S_BASE;
}

int CheckClick()
{
	pc.printf("Check1 ");
	while( !btn.CheckButton() ) {}
		pc.printf("Check2 ");
		
	t1.reset();
	t1.start();

	while( btn.CheckButton() ) {
		if(t1.read_ms() > holdingClickMills) return S_HOLDING;
		
	}
	pc.printf("Check3 ");
	t1.reset();
	t1.start();
	
	while(true) {
		if(t1.read_ms() > doubleClickWaitMills)return S_ONE;
		if( btn.CheckButton() ){
pc.printf("Check4 ");
			while(btn.CheckButton()){}
				pc.printf("Double");
			return S_DOUBLE;
		}
	}
}



