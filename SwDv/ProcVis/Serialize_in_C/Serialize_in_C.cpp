
#include  "stdio.h"
#include  "iostream"
#include  "string.h"
#include  "math.h"
using namespace std;

// Als Serialisieren bezeichnet man das Verpacken der
// Datentypen string, int, float in einen ByteStrom

// Als Desierialisien bezeichnet man das Auspacken ( Lesen )
// dieser Daten aus dem ByteStrom

// Das Serialisieren und Deserialisien ist der wichtigste Vorgang
// beim Entwurf von Kommunikationsprotokollen über serielle
// ByteStrom-orientierte Verbindungen wie z.B. 
// serielle-Verbindung uC<->PC; TCP/IP Verbindung; serielle über Bluetooth

typedef short int16_t;
typedef long  int32_t;
typedef unsigned char byte;

// StreamRW demonstriert das codieren und dekodieren
// verschiedener Datentypen auf einen Byte-Buffer
// Was noch nicht behandelt wird sind Längenproblematiken:
// wiviel darf man lesen ?   wieviel darf man schreiben ?

class StreamRW {
  public:
    StreamRW(byte aBuffer[]);

    void Reset() // Reset _idx to Start-Position
    {
      _idx = 0;
    }
    
    void WriteI16(int16_t aVal);
    void WriteI32(int32_t aVal);
    void WriteF(float aVal);
    void WriteS(char* aTxt);

    int16_t ReadI16();
    int32_t ReadI32();
    float   ReadF();
    void    ReadS(char* aDest); 
  private:
    byte* _buffer; // ptr to ByteStream with Data
    int _idx; // current Read/Write Index in the stream
};


byte gBuffer[20];

void main()
{
  // StreamRW verwaltet den gBuffer
  // => gBuffer muss im Konstruktor übergeben werden
  StreamRW strm(gBuffer);
  
  strm.WriteI16(0xAB12);
  strm.WriteI32(0x34CDEF78);
  // strm.WriteF(3.1415);
  strm.WriteS("Hallo");

  // idx auf den Anfang des Streams setzen
  strm.Reset();
  int16_t v1 = strm.ReadI16();
  int32_t v2 = strm.ReadI32();

  char txt[20];
  strm.ReadS(txt);
}


StreamRW::StreamRW(byte aBuffer[])
{
  _buffer = aBuffer;
  _idx = 0;
}

void StreamRW::WriteI16(int16_t aVal)
{
  // ptr zeigt auf das LB von aVal
  byte* ptr = (byte*)&aVal;

  // *ptr; Wert des LowBytes

  // das LB von aVal auf den Stream schreiben
  _buffer[_idx] = *ptr;
  _idx++; // Schreibindex um eine Byte weiterbewegen

  ptr++; // ptr auf das HB weiterschalten

  _buffer[_idx] = *ptr;
  _idx++;
}

void StreamRW::WriteI32(int32_t aVal)
{
  byte* ptr = (byte*)&aVal;

  _buffer[_idx] = *ptr; // Byte0
  _idx++; ptr++; // Pointer incrementieren

  _buffer[_idx] = *ptr; // Byte1
  _idx++; ptr++;
  
  _buffer[_idx] = *ptr; // Byte2
  _idx++; ptr++;
  
  _buffer[_idx] = *ptr; // Byte3
  _idx++; ptr++; 
}

void StreamRW::WriteF(float aVal)
{
  byte* ptr = (byte*)&aVal;

  _buffer[_idx] = *ptr; // Byte0
  _idx++; ptr++; // Pointer incrementieren

  _buffer[_idx] = *ptr; // Byte1
  _idx++; ptr++;
  
  _buffer[_idx] = *ptr; // Byte2
  _idx++; ptr++;
  
  _buffer[_idx] = *ptr; // Byte3
  _idx++; ptr++; 
}


void StreamRW::WriteS(char* aTxt)
{
  int i; // idx welcher dem aTxt entlang fährt
  i = 0;
  while(1)
  {
    _buffer[_idx] = aTxt[i];
    _idx++; i++;
    if( aTxt[i]=='\0' )
      break;
  }
}

// Für aDest muss ein char-Array übergeben werden
// das groß genug ist um den Text aufzunehmen
void StreamRW::ReadS(char* aDest)
{
  int i; // idx welcher dem aTxt entlang fährt
  i = 0;
  while(1)
  {
    aDest[i] = _buffer[_idx];
    _idx++; i++;
    if( aDest[i]=='\0' )
      break;
  }
}


int16_t StreamRW::ReadI16()
{
  int16_t val;

  // auf val einen ptr setzen damit wir val
  // Byte-weise beschreiben können
  byte* ptr = (byte*)&val;

  // LB aus dem Buffer auf das LB von val schreiben
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  // HB aus dem Buffer auf das HB von val schreiben
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  return val;
}

int32_t StreamRW::ReadI32()
{
  int32_t val;

  // auf val einen ptr setzen damit wir val
  // Byte-weise beschreiben können
  byte* ptr = (byte*)&val;

  // LB aus dem Buffer auf das LB von val schreiben
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  // HB aus dem Buffer auf das HB von val schreiben
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  *ptr = _buffer[_idx];
  _idx++; ptr++;
  
  return val;
}







