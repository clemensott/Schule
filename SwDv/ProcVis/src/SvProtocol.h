
#ifndef SvProtocol_h
#define SvProtocol_h

#include "stdint.h"

namespace mbed {

class IStreamHL {
	public:
		virtual void PutChar(int aCh) = 0;
		virtual int GetChar() = 0;
		virtual void Write(void* aData, uint32_t aLenBytes) = 0;
		virtual void Read(void* aData, uint32_t aLenBytes) = 0;
};

class SvProtocol {
	public:
		IStreamHL* _st;
		uint8_t acqON;
    uint8_t svMessageON;
	public:
		SvProtocol(IStreamHL* aStrm) {
			acqON=0; svMessageON=1;
			_st=aStrm; 
		}
	
		// Check's first for acqOn/Off Command
    // ret 0 if acqOn/Off was handled in GetCommand
    int GetCommand();
		
		void Puts(char* aCStr); // Terminate with 0

    // \r\n is appended automatically
    void Printf(const char* format, ...);

    void SvPrintf(const char *format, ...);
    
    void WriteSV(int aId, char* aData) {
      if( !svMessageON ) return;
      _st->PutChar(aId); Puts(aData); 
    }
    
    void SvMessage(char* aTxt) {
      if( !svMessageON ) return;
      _st->PutChar(10); Puts(aTxt);
    }
    
    void VectHeader(int aId, int aNVals)
      { _st->PutChar(aId); _st->PutChar(aNVals); }
    
    void WriteSvI16(int aId, int16_t aData)
      { _st->PutChar(aId+10); _st->Write(&aData,2); }
    
    void WriteSvI32(int aId, int32_t aData)
      { _st->PutChar(aId); _st->Write(&aData,4); }
    
    void WriteSV(int aId, float aData)
      { _st->PutChar(aId+20); _st->Write(&aData,4); }
    
    // float in 3.13 Format
    void WriteSV3p13(int aId, float aData);

    int16_t ReadI16()
      { int16_t ret; _st->Read(&ret,2); return ret; }

    int32_t ReadI32()
      { int32_t ret; _st->Read(&ret,4); return ret; }
    
    float ReadF()
      { float ret; _st->Read(&ret,4); return ret; }
};

} // namespace mbed

// SV-Id Ranges and DataTypes for SvVis3 Visualisation-Tool
//----------------------------------------------------------
// Id = 10       : string
// Id = 1 .. 9   : format 3.13  2 Bytes
// Id = 11 .. 20 : short        2 Bytes
// Id = 21 .. 30 : float        4 Bytes

#endif































