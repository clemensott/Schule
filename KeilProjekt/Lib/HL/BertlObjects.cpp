


BusOut leds(LED1,LED2,LED3,LED4);
// DigitalOut ledBlue(P1_28); // 3 blaue LEDs

BertlDrive mL(p34, P1_1, P1_0, P1_12); 
BertlDrive mR(p36, P1_4, P1_3, P1_13); 

PortEx pex;
UsDistSens us(p21,p22);

void InitBertl()
{
	leds=0;
  mL.Init(); mR.Init(); pex.Init();
	pex.useISR=0;
}


