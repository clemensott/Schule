#include "mbed.h"
#include "Serial_HL.h"
#include "FuncGenFSST2.h"

// 2 Generatoren mit switch umschalten
 
SerialBLK pc(USBTX, USBRX);
SvProtocol ua0(&pc);

void CommandHandler();
void ExecSignalChain();

SignedRampGen fg1;
RectGen fg2;
float amp1 = 1.0;
float v1; // Ausgangswert mit amp1 multipliziert

int swt=1; // 1..v1 auf fg1 geschaltet    2..v1 auf fg2 geschaltet

int main(void)
{
	pc.format(8,SerialBLK::None,1); pc.baud(500000); // 115200
  
  ua0.SvMessage("FuncGenMain3"); // Meldung zum PC senden
  
  Timer stw; stw.start();
  while(1) //  while(1)-Loop der mit dem PC kommuniziert
  {
    CommandHandler();
    if( (stw.read_ms()>10) ) // 100Hz
    { // dieser Teil wird mit 100Hz aufgerufen
      stw.reset();
      ExecSignalChain(); // Funktionsgeneratoren rechnen
      if( ua0.acqON ) {
        ua0.WriteSV(1, v1);
      }
    }
  }
  return 1;
}

void ExecSignalChain()
{
  fg1.CalcOneStep();
	fg2.CalcOneStep();
	if( swt==1 )
		v1 = amp1*fg1.val;
	if( swt==2 )
		v1 = amp1*fg2.val;
}

void CommandHandler()
{
  uint8_t cmd;
  if( !pc.IsDataAvail() )
    return;
  cmd = ua0.GetCommand();

  // mithilfe von Kommandos vom PC Frequenz und Amplitude verstellen

  if( cmd==2 ) // Frequenz
  {
		float frequ = ua0.ReadF();
    fg1.SetFrequ(frequ); fg2.SetFrequ(frequ);
		ua0.SvMessage("Set Frequ");
  }
  
  if( cmd==3 ) // Amplitute
  {
    amp1 = ua0.ReadF();
    ua0.SvMessage("Set Frequ");
  }
	
	if( cmd==4 ) // Funktionsgeneratoren umschalten
	{
		swt = ua0.ReadI16();
		ua0.SvMessage("Switch FG");
	}
}
















