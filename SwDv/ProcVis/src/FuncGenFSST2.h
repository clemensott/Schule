
#ifndef FuncGenFSST_h
#define FuncGenFSST_h

// Amplituden fix auf +/-1

class SignedRampGen {
	public:
		float val; // momentaner Ausgangswert
  private:
		float _inc; 
	public:
		SignedRampGen(); // Konstruktor
    
		void SetPointsPerPeriod(float aPoints);
	
	  // bezogen auf Fsample 0..0.5
	  void SetFrequ(float aFrequ);

    // Einen Abtastwert berechnen
	  // wird bei z.B. Fsample=100Hz  100x pro sec afgerufen
		void CalcOneStep();
};


class TriangleGen {
	public:
		float val; // momentaner Ausgangswert
  private:
    float _inc;
    int   _state;
    float _phase;
	public:
		TriangleGen();
    
		void SetPointsPerPeriod(float aPoints);
	
		// bezogen auf Fsample 0..0.5
	  void SetFrequ(float aFrequ);
	
		// Einen Abtastwert berechnen
		void CalcOneStep();
};


class RectGen {
	public:
		float val; // momentaner Ausgangswert
	private:
		float _inc;
		float _phase;
    float _thrs; // Threshold für die PulsWidth
	public:
		RectGen();
    
		void SetPointsPerPeriod(float aPoints);
	
		void SetFrequ(float aFrequ);

    // Dauer des ON-Pulses in Prozent ( 0..1 )
		void SetPulsWidth(float aPercent);

    // Einen Abtastwert berechnen
		void CalcOneStep();
};

#endif



















