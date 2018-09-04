
#include "mbed.h"
#include "BtnEventM0S.h"

Serial pc(USBTX, USBRX);
//        LSB                                                      MSB
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

BtnEventM0 btnState(P1_16);
BtnEventM0 btnUp(P0_23);
BtnEventM0 btnDown(P0_10);

Timer tIncrease;
Timer tUp;
Timer tDown;


const int increaseMillis = 100, holdingClickMillis = 500, isHoldingWaitMillis = 100;
const int minutesPerHour = 30, maxCnt = 24 * minutesPerHour;
const int S_INCREASE = 1, S_HOURS = 2, S_MINUTES = 3;
const int S_ONE = 1, S_HOLDING = 2;

void ChangeCnt(int change);
void HandleButton(BtnEventM0* btn, Timer* t, int* pressed, int* holding, int change);

void ShowMode();
void ShowTime();

class StmDBL {
	public:
		void Init();
		void Increase();
		void Hours();
		void Minutes();
	public:
		int state;
};

StmDBL stm;
int blinkIdx = 0;
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
	
	while(1) {
		lb= cnt;
		
		if( stm.state == S_INCREASE ) blinkIdx = -1;
		else if( stm.state == S_HOURS ) blinkIdx = 0;
		else if( stm.state == S_MINUTES ) blinkIdx = 3;
		
		ShowMode();
		
		if( stm.state == S_INCREASE )
			stm.Increase();
		else if( stm.state == S_HOURS )
			stm.Hours();
		else if( stm.state == S_MINUTES )
			stm.Minutes();
	}
}

void StmDBL::Increase()
{	
	tIncrease.reset();
	tIncrease.start();
	
	while(!btnState.CheckButton()){
		if(tIncrease.read_ms()<increaseMillis) continue;

		ChangeCnt(1);
		
		tIncrease.reset();
		tIncrease.start();
	}
	
	while(btnState.CheckButton()){
		if(tIncrease.read_ms()<increaseMillis) continue;

		ChangeCnt(1);
		
		tIncrease.reset();
		tIncrease.start();
	}
	
	stm.state = S_HOURS;
}

void StmDBL::Hours()
{ 
	tUp.reset();
	tUp.start();
	tDown.reset();
	tDown.start();
	
	int pressedState = 0;
	int pressedIncrease = btnUp.CheckButton();
	int pressedDecrease = btnDown.CheckButton();
	
	int holdingIncrease = 0;
	int holdingDecrease = 0;
	
	while(1){
		if(btnState.CheckButton()) pressedState = 1;
		else if(pressedState) break;	
		
		HandleButton(&btnUp, &tUp, &pressedIncrease,&holdingIncrease, minutesPerHour);
		HandleButton(&btnDown, &tDown, &pressedDecrease,&holdingDecrease, -minutesPerHour);
	}
	
	stm.state = S_MINUTES;
}

void StmDBL::Minutes()
{ 
	tUp.reset();
	tUp.start();
	tDown.reset();
	tDown.start();
	
	int pressedState = 0;
	int pressedIncrease = btnUp.CheckButton();
	int pressedDecrease = btnDown.CheckButton();
	
	int holdingIncrease = 0;
	int holdingDecrease = 0;
	
	while(1){
		if(btnState.CheckButton()) pressedState = 1;
		else if(pressedState) break;		
		
		HandleButton(&btnUp, &tUp, &pressedIncrease,&holdingIncrease, 1);
		HandleButton(&btnDown, &tDown, &pressedDecrease,&holdingDecrease, -1);
	}
	
	stm.state = S_INCREASE;
}

void StmDBL::Init()
{
	stm.state = S_INCREASE;
}

void ChangeCnt(int change){
	cnt = ((cnt + change) % maxCnt  + maxCnt) % maxCnt;
	ShowTime();
	lb = cnt;
}

void HandleButton(BtnEventM0* btn, Timer* t, int* pressed, int* holding, int change) {
	if((*btn).CheckButton()) {
			if(*pressed == 0){
				*pressed = 1;
			
				(*t).reset();
//				(*t).start();
			}
			
			if(*holding == 1 && isHoldingWaitMillis < (*t).read_ms()){
				ChangeCnt(change);

				(*t).reset();
//				(*t).start();
			}
			else if(holdingClickMillis < (*t).read_ms()){
				ChangeCnt(change);
	
				(*t).reset();
//				(*t).start();
				
				*holding = 1;
			}
		}
		else if(*pressed == 1 && *holding == 0) {
			ChangeCnt(change);
			
			(*t).reset();
//			(*t).start();
			
			*pressed = 0;
		}
		else {
			*pressed = 0;
			*holding = 0;
		}
}

void ShowMode()
{
  if( blinkIdx==-1 )
    // 1..in die erste Zeile schreiben
    pc.printf("1 Clock running\n");
  if( blinkIdx==0 )
    pc.printf("1 Edit hh\n");
  if( blinkIdx==3 )
    pc.printf("1 Edit mm\n");
  // 3..BlinkIndex setzen 
  // es blinken immer 2 Zeichen ( Spalten ) beginnend mit blinkIdx
  // Kein Blinken == blinkIdx=-1;
  // 3 2  .. Anzeige blinkt ab dem 2ten Zeichen
  // 3 -1 .. Anzeige blinkt nicht
  pc.printf("3 %d\n", blinkIdx);
}

void ShowTime()
{
	int hh = cnt / minutesPerHour;
	int mm = cnt % minutesPerHour;
  // 2..in die 2te Zeile schreiben
  pc.printf("2 %02d:%02d\n",hh,mm);
}
