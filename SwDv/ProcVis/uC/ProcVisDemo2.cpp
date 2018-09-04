
#include "mbed.h"
#include "Serial_HL.h"
 
SerialBLK pc(USBTX, USBRX);
SvProtocol ua0(&pc);

// BusOut leds(LED1,LED2,LED3,LED4); Bertl14
// M0-Board
//        LSB                                                      MSB
//        2^0   2^1   2^2  2^3                                     2^11
BusOut lb(P1_13,P1_12,P1_7,P1_6,P1_4,P1_3,P1_1,P1_0,LED4,LED3,LED2,LED1);
//        D20   D19   D18  D17  D16  D15  D14  D13  D4   D3   D2   D1

void CommandHandler();

int main(void)
{
	pc.format(8,SerialBLK::None,1); pc.baud(500000); // 115200
	// leds = 9;
	
	ua0.SvMessage("ProcVisDemo2"); // Meldung zum PC senden
	
	int16_t sv1=0, sv2=100;
	Timer stw; stw.start();
  while(1)
  {
    CommandHandler();
    if( (stw.read_ms()>100) ) // 10Hz
    { // dieser Teil wird mit 10Hz aufgerufen
      stw.reset();
      sv1++; sv2++;
      if( ua0.acqON ) {
        // nur wenn vom PC aus das Senden eingeschaltet wurde
        // wird auch etwas gesendet
        ua0.WriteSvI16(1, sv1);
        ua0.WriteSvI16(2, sv2);
      }
    }
  }
  return 1;
}

void CommandHandler()
{
  uint8_t cmd;
  int16_t idata1, idata2;
  
  // Fragen ob überhaupt etwas im RX-Reg steht
  if( !pc.IsDataAvail() )
    return;
  
  // wenn etwas im RX-Reg steht
	// Kommando lesen
	cmd = ua0.GetCommand();

  if( cmd==2 )
	{
		// cmd2 hat 2 int16 Parameter
    idata1 = ua0.ReadI16(); idata2 = ua0.ReadI16();
    // für die Analyse den Wert einfach nur zum PC zurücksenden
    ua0.SvPrintf("Command2 %d %d", idata1, idata2);
  }
	
	if( cmd==3 ) // LEDS schalten
	{
		int16_t val = ua0.ReadI16();
		lb = val;
		ua0.SvPrintf("switch LEDS %d", val);
	}
}
















