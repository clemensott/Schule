
// Bitoperationen in C
//   &  |  ^  <<  >>


// Der Bitschiebe-Befehl in C/C++

int  a;

a = B#0001;  // in a steht Binär 0001

a = a << 1;  // in a steht jetzt B#0010
a = a << 1;  // in a steht jetzt B#0100

a = a >> 2;  // in a steht jetzt B#0001
a = a >> 1;  // in a steht jetzt B#0000


// Bitwise OR

B#1001 |  B#0010 = B#1011


// Bits in einem Byte mit Bitwise-And abfragen

B#1010 & B#0101 = B#0000

B#1010 & B#1000 = B#1000

// !!gezieltes!! abfragen ob ein Bit in einem Byte gesetzt ist

  2^3    2^2  2^1  2^0
B#  0    0    0    0;

int btn;

if( btn & 1 )  B#xxxx & B#0001
  printf("Bit0 ist gesetzt");

if( btn & 2 ) B#xxxx & B#0010
  printf("Bit1 ist gesetzt");

if( btn & 4 )
  printf("Bit2 ist gesetzt");

if( btn & 8 )
  printf("Bit3 ist gesetzt");


// Mit dem Bitwise-OR Bits in einem Byte setzen 
// ohne die schon gesetzen Bits zu zerstören

int leds;

leds = B#0001; // Bit0 ist gesetzt

leds = B#0010; // Bit1 ist gesetzt aber Bit0 ist leider wieder gelöscht

// Die Lösung ist, daß man Bits dazuverodert
leds = leds | B#0010; // leds ist jetzt B#0011;


