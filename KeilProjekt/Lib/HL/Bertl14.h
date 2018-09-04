
#ifndef Bertl14_h
#define Bertl14_h

class Motor {
	public:
		Motor(PinName pwm, PinName fwd, PinName rev);
		void SetPow(float aPow);
	protected:
    PwmOut _pwm;
    DigitalOut _fwd;
    DigitalOut _rev;
};

class BertlDrive : public Motor {
	public:
		int16_t encCnt;
	public:
		BertlDrive(PinName pwm, PinName fwd, PinName rev, PinName encoder);
		void Init();
	private:
		void EncoderISR();
		InterruptIn _enc;
};


const int BTN_FLL = 0x80;
const int BTN_FL  = 0x04;
const int BTN_FM  = 0x01;
const int BTN_FR  = 0x08;
const int BTN_FRR = 0x40;
const int BTN_BL = 0x10;
const int BTN_BM = 0x02;
const int BTN_BR = 0x20;

const int LED_FL1 = 0x01; // white die vordere
const int LED_FL2 = 0x02; // red die hintere
const int LED_FR1 = 0x04; // white
const int LED_FR2 = 0x08; // red
const int LED_ALL_FRONT = 0x0F;

const int LED_BL1 = 0x20; // red back left outher
const int LED_BL2 = 0x10; // red back left inner
const int LED_BR1 = 0x80; // red back right outher
const int LED_BR2 = 0x40; // red back right inner
const int LED_ALL_BACK = 0xF0;


class PortEx {
	public:
		// Current State of Buttons is refreshed with ReadButtons()
		int16_t btns;
		uint8_t btnEvent;
		uint8_t useISR;
	public:
		PortEx();
		void Init();
		
		void SetLedPort(uint8_t aBitPattern); // NO local Bit-OR
		void SetLeds(uint8_t aBitPattern);
		void ToggleLeds(uint8_t aBitPattern);
		void ClearLeds();
		
		void ReadButtons();
		void WaitUntilButtonPressed();

		bool IsButton(int aBitPattern)
			{ return btns & aBitPattern; }
		
		bool IsAnyFrontButton()
			{ return btns & (BTN_FL|BTN_FM|BTN_FR); }
		
		bool IsAnyBackButton()
			{ return btns & (BTN_BL|BTN_BM|BTN_BR); }
	private:
		uint8_t _currLeds;
		void p6ISR();
		I2C _i2c;
		const int DEV = 0x40;
		InterruptIn _p6Event;
};


class UsDistSens {
	public:
		UsDistSens(PinName pinTrigger, PinName pinEcho);
		void StartMeas();
	private:
		void RisingISR();
		void FallingISR();
	private:
		DigitalOut trigger;
		InterruptIn echo;
		Timer stw;
	public:
		int dist;
		float distCM;
};

#endif
