
#ifndef Serial_HL_h
#define Serial_HL_h

#include "platform.h"
// #include "Stream.h"
#include "FunctionPointer.h"
#include "serial_api.h"
#include "SvProtocol.h"

namespace mbed {

class SerialBLK : public IStreamHL {
	public:
		SerialBLK(PinName tx, PinName rx);
		void baud(int baudrate);
  
    virtual void PutChar(int aCh);
		virtual int GetChar();
		virtual void Write(void* aData, uint32_t aLenBytes);
		virtual void Read(void* aData, uint32_t aLenBytes);
		
		enum Parity {
        None = 0,
        Odd,
        Even,
        Forced1,
        Forced0
		};
		enum IrqType {
        RxIrq = 0,
        TxIrq
		};
    
    void format(int bits=8, Parity parity=SerialBLK::None, int stop_bits=1);
		
		int readable();
		int writeable();
		int IsDataAvail()
			{ return readable(); }
		
		// fptr A pointer to a void function, or 0 to set as none
    // type Which serial interrupt to attach the member function to (Seriall::RxIrq for receive, TxIrq for transmit buffer empty)
    void attach(void (*fptr)(void), IrqType type=RxIrq);
		
		// tptr pointer to the object to call the member function on
    // mptr pointer to the member function to be called
    // type Which serial interrupt to attach the member function to (Seriall::RxIrq for receive, TxIrq for transmit buffer empty)
		template<typename T>
    void attach(T* tptr, void (T::*mptr)(void), IrqType type=RxIrq) {
        if((mptr != NULL) && (tptr != NULL)) {
            _irq[type].attach(tptr, mptr);
            serial_irq_set(&_serial, (SerialIrq)type, 1);
        }
    }
		
		static void _irq_handler(uint32_t id, SerialIrq irq_type);
	protected:
		serial_t        _serial;
    FunctionPointer _irq[2];
    int             _baud;
};

} // namespace mbed

#endif
