
#include "mbed.h"
#include "Bertl14.h"

Motor::Motor(PinName pwm, PinName fwd, PinName rev) :
_pwm(pwm), _fwd(fwd), _rev(rev) 
{
	_pwm.period(0.001); _pwm=0;
	_fwd=0; _rev=0;
}

void Motor::SetPow(float aPow)
{
	if( aPow>=0.0 ) {
		_fwd=1; _rev=0;
		_pwm = aPow;
	}
	else {
		_fwd=0; _rev=1;
		_pwm = -aPow;
	}
}

BertlDrive::BertlDrive(PinName pwm, PinName fwd, PinName rev, PinName encoder) :
Motor(pwm,fwd,rev) , _enc(encoder)
{
	encCnt = 0;
}

void BertlDrive::Init()
{
	_enc.rise(this, &BertlDrive::EncoderISR);
	_enc.fall(this, &BertlDrive::EncoderISR);
}

void BertlDrive::EncoderISR()
{
	encCnt++;
}


PortEx::PortEx() : 
_i2c(p28,p27), _p6Event(p6)
{
	btns=btnEvent=0;
	useISR = 1;
}

void PortEx::Init()
{
	char cmd[4];
	_i2c.frequency(100000);
	wait(0.01);
  // Port0 Config  Port0 Out    Port1 In
	cmd[0]=0x06;     cmd[1]=0x00; cmd[2]=0xFF;
	_i2c.write(DEV, cmd, 3, false);
	SetLedPort(0);
	_p6Event.fall(this, &PortEx::p6ISR);
}

void PortEx::p6ISR()
{
	if( !useISR )
		return;
	int16_t prev = btns;
  ReadButtons();
	if( !btns )
		btns = prev;
	else
		btnEvent = 1;
}

void PortEx::SetLedPort(uint8_t aBitPattern)
{
	char cmd[4];
	cmd[0]=2; cmd[1]=~aBitPattern;
	_i2c.write(DEV, cmd, 2, false);
}

void PortEx::SetLeds(uint8_t aBitPattern)
{
	_currLeds |= aBitPattern;
	SetLedPort(_currLeds);
}

void PortEx::ToggleLeds(uint8_t aBitPattern)
{
	_currLeds ^= aBitPattern;
	SetLedPort(_currLeds);
}

void PortEx::ClearLeds()
{
	_currLeds=0; SetLedPort(0);
}


void PortEx::ReadButtons()
{
	char cmd[4];
	cmd[0]=1;
	_i2c.write(DEV, cmd, 1, true);
	_i2c.read(DEV|1, cmd, 1, false);
	btns = cmd[0];
}

void PortEx::WaitUntilButtonPressed()
{
	int prev = useISR;
	useISR = 0;
	btns = 0;
	while(1) {
		ReadButtons();
		if( btns )
			break;
		wait(0.01);
	}
	btns=btnEvent=0;
	useISR = prev;
}



UsDistSens::UsDistSens(PinName pinTrigger, PinName pinEcho) :
trigger(pinTrigger),echo(pinEcho)
{
	echo.rise(this, &UsDistSens::RisingISR);
	echo.fall(this, &UsDistSens::FallingISR);
}

void UsDistSens::StartMeas()
{
	trigger=1; wait_us(12); trigger=0;
	stw.start();
}

void UsDistSens::RisingISR()
{ stw.reset(); }

void UsDistSens::FallingISR()
{
	dist=stw.read_us();
	distCM = (float)dist*(343.2E-4/2.0);
}



