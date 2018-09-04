
#include "mbed.h"
#include "BtnEventM0.h"

Serial pc(USBTX, USBRX);
//        LSB                                                      MSB
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

BtnEventM0 sw4(P1_16), sw3(P0_23), sw1(P0_10), sw2(P0_15);

int blinkIdx = 0;
int hh=0, mm=0;

void ShowMode();
void ShowTime();

int main()
{
  pc.baud(500000);
	sw4.Init(); sw3.Init(); sw1.Init(); sw2.Init();
  ShowMode();
  ShowTime();
  
  Timer t1; t1.start();
  while(1)
  {
    if( sw4.CheckFlag() ) {
      if( blinkIdx==-1 )
				blinkIdx=0;
			else if( blinkIdx==0 )
				blinkIdx=3;
			else if( blinkIdx==3 )
				blinkIdx=-1;
      ShowMode();
    }
    if( t1.read_ms()>150 ) {
      t1.reset();
			if( blinkIdx==-1 ) {
				mm++;
				if( mm>29 )
					{ mm=0; hh++; }
				ShowTime();
			}
    }
    if( blinkIdx==0 ) {
      if( sw3.CheckFlag() ) {
        hh++;
        if( hh>20 ) hh=0;
        ShowTime();
      }
    }
    if( blinkIdx==3 ) {
      if( sw3.CheckFlag() ) {
        mm++;
        if( mm>30 ) mm=0;
        ShowTime();
      }
    }
  }
}

// --------------------------------
// Ansteuerung der LCD-Anzeige
//--------------------------------

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
  // 2..in die 2te Zeile schreiben
  pc.printf("2 %02d:%02d\n",hh,mm);
}





