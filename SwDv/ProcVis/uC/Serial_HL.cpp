
#include "Serial_HL.h"
#include <stdarg.h>
#include <stdio.h>

namespace mbed {

SerialBLK::SerialBLK(PinName tx, PinName rx) {
  serial_init(&_serial, tx, rx);
  _baud = 9600;
  serial_irq_handler(&_serial, SerialBLK::_irq_handler, (uint32_t)this);
}

void SerialBLK::baud(int baudrate) {
  serial_baud(&_serial, baudrate);
  _baud = baudrate;
}

void SerialBLK::format(int bits, Parity parity, int stop_bits) {
  serial_format(&_serial, bits, (SerialParity)parity, stop_bits);
}

int SerialBLK::readable() {
  return serial_readable(&_serial);
}

int SerialBLK::writeable() {
  return serial_writable(&_serial);
}

void SerialBLK::attach(void (*fptr)(void), IrqType type) {
  if (fptr) {
      _irq[type].attach(fptr);
      serial_irq_set(&_serial, (SerialIrq)type, 1);
  } else {
      serial_irq_set(&_serial, (SerialIrq)type, 0);
	}
}

void SerialBLK::_irq_handler(uint32_t id, SerialIrq irq_type) {
  SerialBLK *obj = (SerialBLK*)id;
  obj->_irq[irq_type].call();
}

int SerialBLK::GetChar() {
  return serial_getc(&_serial);
}

void SerialBLK::PutChar(int aCh) {
  serial_putc(&_serial, aCh);
}

void SerialBLK::Write(void* aData, uint32_t aLenBytes)
{
	uint8_t* ptr = (uint8_t*)aData;
	for(int i=0; i<aLenBytes; i++) {
		this->PutChar(*ptr); ptr++;
	}
}

void SerialBLK::Read(void* aData, uint32_t aLenBytes)
{
	uint8_t* ptr = (uint8_t*)aData;
  for(int i=0; i<aLenBytes; i++) {
    *ptr=this->GetChar(); ptr++;
  }
}




void SvProtocol::Puts(char* aCStr)
{
  while( *aCStr != '\0' )
  {
    if( *aCStr=='\n' )
      _st->PutChar('\r');
    _st->PutChar(*aCStr);
    aCStr++;
  }
	_st->PutChar(0); // terminate with 0
}

static char sBuffer[50];

void SvProtocol::Printf(const char *format, ...)
{
  va_list vArgs;
  va_start(vArgs, format);
  vsprintf(sBuffer, (char const *)format, vArgs);
  va_end(vArgs);
  Puts(sBuffer);
}

void SvProtocol::SvPrintf(const char *format, ...)
{
  va_list vArgs;
  va_start(vArgs, format);
  vsprintf(sBuffer, (char const *)format, vArgs);
  va_end(vArgs);
  if( !svMessageON ) return;
  _st->PutChar(10);
  Puts(sBuffer);
}

void SvProtocol::WriteSV3p13(int aId, float aData) {
  // int16_t val = To3p13(aData);
  // PutChar(aId); Write(&val,2);
}

int SvProtocol::GetCommand()
{
  uint8_t cmd = _st->GetChar();
  if( cmd==1 )
  {
    this->acqON = _st->GetChar();
    if( this->acqON )
      this->SvMessage("AcqON");
    else
      this->SvMessage("AcqOFF");
    return 0;
  }
  return cmd;
}

} // namespace mbed




