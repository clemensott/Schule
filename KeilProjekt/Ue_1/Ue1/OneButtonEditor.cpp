
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
const int minutesPerHour = 60;
const int S_INCREASE=1, S_HOURS=2, S_MINUTES=3;
const int S_ONE=1, S_HOLDING=2;

void ChangeCnt(int change);
void HandleButton(BtnEventM0* btn, Timer* t, int pressed, int holding, int change);

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
		
		if( stm.state==S_INCREASE )
			stm.Increase();
		else if( stm.state==S_HOURS )
			stm.Hours();
		else if( stm.state==S_MINUTES )
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
		else if(pressedState){
			pc.printf("Break");
			break;
		}		
		
		if(btnUp.CheckButton()) {
			pc.printf("Press%d %d ",pressedIncrease,holdingIncrease);
			if(pressedIncrease == 0){
				pressedIncrease = 1;
			
				tUp.reset();
				tUp.start();
			}
			
			if(holdingIncrease == 1 && isHoldingWaitMillis < tUp.read_ms()){
				ChangeCnt(minutesPerHour);

				tUp.reset();
				tUp.start();
			}
			else if(holdingClickMillis < tUp.read_ms()){
				ChangeCnt(minutesPerHour);

				tUp.reset();
				tUp.start();
				
				holdingIncrease = 1;
			}
		}
		else if(pressedIncrease == 1 && holdingIncrease == 0) {
			ChangeCnt(minutesPerHour);
			pc.printf("Else");
			tUp.reset();
			tUp.start();
			
			pressedIncrease = 0;
		}
		else {
			pressedIncrease = 0;
			holdingIncrease = 0;
		}
		
		if(btnDown.CheckButton()) {
			if(pressedDecrease == 0){
				pressedDecrease = 1;
			
				tUp.reset();
				tUp.start();
			}
			
			if(holdingDecrease == 1 && isHoldingWaitMillis < tUp.read_ms()){
				ChangeCnt(-minutesPerHour);

				tUp.reset();
				tUp.start();
			}
			else if(holdingClickMillis < tUp.read_ms()){
				ChangeCnt(-minutesPerHour);

				tUp.reset();
				tUp.start();
				
				holdingDecrease = 1;
			}
		}
		else if(pressedDecrease == 1 && holdingDecrease == 0) {
			ChangeCnt(-minutesPerHour);
			
			tUp.reset();
			tUp.start();
			
			pressedDecrease = 0;
		}
		else {
			pressedDecrease = 0;
			holdingDecrease = 0;
		}
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
		else if(pressedState){
			pc.printf("Break");
			break;
		}		
		
		if(btnUp.CheckButton()) {
			if(pressedIncrease == 0){
				pressedIncrease = 1;
			
				tUp.reset();
				tUp.start();
			}
			
			if(holdingIncrease == 1 && isHoldingWaitMillis < tUp.read_ms()){
				ChangeCnt(1);

				tUp.reset();
				tUp.start();
			}
			else if(holdingClickMillis < tUp.read_ms()){
				ChangeCnt(1);

				tUp.reset();
				tUp.start();
				
				holdingIncrease = 1;
			}
		}
		else if(pressedIncrease == 1 && holdingIncrease == 0) {
			ChangeCnt(1);
			
			tUp.reset();
			tUp.start();
			
			pressedIncrease = 0;
		}
		else {
			pressedIncrease = 0;
			holdingIncrease = 0;
		}
		
		if(btnDown.CheckButton()) {
			if(pressedDecrease == 0){
				pressedDecrease = 1;
			
				tDown.reset();
				tDown.start();
			}
			
			if(holdingDecrease == 1 && isHoldingWaitMillis < tDown.read_ms()){
				ChangeCnt(-1);

				tDown.reset();
				tDown.start();
			}
			else if(holdingClickMillis < tDown.read_ms()){
				ChangeCnt(-1);
	
				tDown.reset();
				tDwon.start();
				
				holdingDecrease = 1;
			}
		}
		else if(pressedDecrease == 1 && holdingDecrease == 0) {
			ChangeCnt(-1);
			
			tDown.reset();
			tDown.start();
			
			pressedDecrease = 0;
		}
		else {
			pressedDecrease = 0;
			holdingDecrease = 0;
		}
	}
	
	stm.state = S_INCREASE;
}

void StmDBL::Init()
{
	stm.state = S_INCREASE;
}

void ChangeCnt(int change){
	cnt += change;
	lb = cnt;
}

void HandleButton(BtnEventM0* btn, Timer* t, int* pressed, int* holding, int change) {
	if(btn.CheckButton()) {
			if(pressed == 0){
				pressed = 1;
			
				t.reset();
				t.start();
			}
			
			if(holding == 1 && isHoldingWaitMillis < t.read_ms()){
				ChangeCnt(change;

				t.reset();
				t.start();
			}
			else if(holdingClickMillis < t.read_ms()){
				ChangeCnt(change);
	
				t.reset();
				t.start();
				
				holding = 1;
			}
		}
		else if(pressed == 1 && holding == 0) {
			ChangeCnt(-1);
			
			t.reset();
			t.start();
			
			pressed = 0;
		}
		else {
			pressed = 0;
			holding = 0;
		}
}
