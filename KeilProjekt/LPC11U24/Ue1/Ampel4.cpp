
#include "mbed.h"
#include "BtnEventM0S.h"

Serial pc(USBTX, USBRX);

//        LSB                                           MSB
BusOut lb(P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);

// Statusled zeigt uns in welchem Zustand die Statemachine gerade ist
BusOut stLED(P1_13,P1_12);

// BtnEventM0 erledigt für uns die Abfrage der positiven Flanke
BtnEventM0 sw4(P1_16), sw3(P0_23);
// sw4==forew  sw3==backward

const int ST_ROT = 1;
const int ST_GEL = 2;
const int ST_GRUEN = 3;

class Ampel {
  public:
    void Init();
    void Rot();
    void Gelb();
    void Gruen();
  private:
    void RotAction();		//Bit 0 mit 10Hz blinken
    void GelbAction();	//Bit 2 mit 5Hz blinken
    void GruenAction(); //Bit 4 mit 2Hz blinken
  public:
    int state;
  private:
    Timer t1; // Blinken
    Timer t2; // mit 10Hz Daten zur Anzeige schicken
    Timer t3; // Zeit bis zum Umschalten ausmessen
};

Ampel amp; 

int main(void)
{
  sw3.Init(); sw4.Init();
	pc.baud(115200);
	amp.Init();
	
	while(1)
	{
		if( amp.state==1)
			amp.Rot();
		if( amp.state==2)
			amp.Gelb();
		if( amp.state==3)
			amp.Gruen();
	}
}

void Ampel::Rot()
{
	//Einmalige Eintrittsaktion
	//Zustand am Display in der 1ten Zeile anzeigen
	pc.printf("ROT\n");
	t3.reset();
	
	while(1)
	{
		RotAction();
		//Zustand abfragen
		if(t3.read_ms()>3000)
		{ state = ST_GEL; return;}
		
	}
}

void Ampel::RotAction()
{
	//blinken
	if(t1.read_ms()>100) {
		t1.reset();
		if(lb == 0)
			lb = 1;
		else
			lb = 0;
	}
	
	//Zeit zum PC senden
	if(t2.read_ms() > 100) {
		t2.reset();	// 2.., 2te Zeile am simulierten LCD-Display
		pc.printf("2 %d\n", t3.read_ms()/100);	//Zeitanzeige auf 1/10 sec genau
	}
}

void Ampel::Gelb()
{
	//Einmalige Eintrittsaktion
	//Zustand am Display in der 1ten Zeile anzeigen
	pc.printf("Gelb\n");
	t3.reset();
	
	while(1)
	{
		GelbAction();
		//Zustand abfragen
		if(t3.read_ms()>4000)
		{ state = ST_GRUEN; return;}
		
		if(sw4.CheckFlag())
		{ state = ST_ROT; return;}
		
	}
}

void Ampel::GelbAction()
{
	//blinken
	if(t1.read_ms()>200) {
		t1.reset();
		if(lb == 0)
			lb = 2;
		else
			lb = 0;
	}
	
	//Zeit zum PC senden
	if(t2.read_ms() > 200) {
		t2.reset();	// 2.., 2te Zeile am simulierten LCD-Display
		pc.printf("2 %d\n", t3.read_ms()/100);	//Zeitanzeige auf 1/10 sec genau
	}
}

void Ampel::Gruen() 
{
	//Einmalige Eintrittsaktion
	//Zustand am Display in der 1ten Zeile anzeigen
	pc.printf("Gruen\n");
	t3.reset();
	
	while(1)
	{
		GruenAction();
		
		//Zustand abfragen
		if(t3.read_ms()>5000)
		{ state = ST_ROT; return;}
		
		if(sw4.CheckFlag())
		{ state = ST_ROT; return;}
		
	}
}

void Ampel::GruenAction() 
{
	//blinken
	if(t1.read_ms()>200) {
		t1.reset();
		if(lb == 0)
			lb = 4;
		else
			lb = 0;
	}
	
	//Zeit zum PC senden
	if(t2.read_ms() > 200) {
		t2.reset();	// 2.., 2te Zeile am simulierten LCD-Display
		pc.printf("2 %d\n", t3.read_ms()/100);	//Zeitanzeige auf 1/10 sec genau
	}
}


void Ampel::Init()
{
	state = ST_ROT; t1.start(); t2.start(); t3.start();
}

