
#include "FuncGenFSST2.h"

SignedRampGen::SignedRampGen()
{
  val = 0;
  SetPointsPerPeriod(10); // Default ist 10Pts/Periode
}

void SignedRampGen::SetPointsPerPeriod(float aPoints)
{
  _inc = 2.0/aPoints;
}

void SignedRampGen::SetFrequ(float aFrequ)
{
  SetPointsPerPeriod(1.0/aFrequ);
}

void SignedRampGen::CalcOneStep()
{
  val = val + _inc;
  if( val>1.0 )
    val = -1 + (val - 1.0); 
}



TriangleGen::TriangleGen()
{
  val=_phase=0;
  SetPointsPerPeriod(100);
}

void TriangleGen::SetPointsPerPeriod(float aPoints)
{
  _inc = 4.0/aPoints;
}

void TriangleGen::SetFrequ(float aFrequ)
{
  SetPointsPerPeriod(1.0/aFrequ);
}

void TriangleGen::CalcOneStep()
{
  _phase = _phase + _inc;
  if( _phase>1.0 ) {
    _phase = -1 + (_phase - 1.0);
    if( _state==1 )
      _state=2;
    else
      _state=1;
  }
  if( _state==1 )
    val = _phase;
  else
    val = -_phase;
}



RectGen::RectGen()
{
  val=0; _phase=0; _thrs=0;
  SetPointsPerPeriod(10); // Default ist 10Pts/Periode
}

void RectGen::SetPointsPerPeriod(float aPoints)
{
  _inc = 2.0/aPoints;
}

void RectGen::SetFrequ(float aFrequ)
{
  SetPointsPerPeriod(1.0/aFrequ);
}

void RectGen::SetPulsWidth(float aPercent)
{
  _thrs = 1.0-aPercent;
}

void RectGen::CalcOneStep()
{
  _phase = _phase + _inc;
  if( _phase>1.0 )
    _phase = -1 + (_phase - 1.0); 
	if( _phase>_thrs )
    val = 1.0;
  else
    val = -1.0;
}


















