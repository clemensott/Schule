
using System;
using System.Collections.Generic;
using System.Text;
// using ZedGraph;
using ZedHL;

namespace vis1
{
  class ValueHandler
  {
    const int MAX_CHAN = 10;
    public GraphRingBuffer[] Chan = new GraphRingBuffer[MAX_CHAN];

    public virtual void SetValue(int aIdx, double aVal)
    {
    }
    public virtual void Reset()
    {
    }
  }

  class DefaultValueHandler : ValueHandler
  {
    public DefaultValueHandler()
    {
    }

    public override void SetValue(int aIdx, double aVal)
    {
      if (Chan[aIdx] == null) return;
      Chan[aIdx].Add(aVal);
    }
  }

  // In:Chan[0] Out:Chan[1]
  class TP1ValueHandler : ValueHandler
  {
    const double ALPHA = 0.1;
    double y=0.0; // filter output
    
    public override void SetValue(int aIdx, double aVal)
    {
      if (Chan[aIdx] == null) return;
      Chan[aIdx].Add(aVal);
      if (aIdx == 0)
      {
        y = y * (1 - ALPHA) + aVal * ALPHA;
        Chan[1].Add(y);
      }
    }
  }

  // In:Chan[0] Out:Chan[0],Chan[1]
  class GyroIntegrator : ValueHandler
  {
    const double ALPHA = 0.01; // 0.1
    const double K2 = 0.002;
    const double BIAS = 392;
    double sum=0.0, sumTP=0.0;
    double dt = (1/500.0)*10;

    public override void SetValue(int aIdx, double aVal)
    {
      if (aIdx != 0) return;
      double x = aVal - BIAS;
      sum = x * dt + sum - K2*sum;
      // sumTP = sumTP * (1 - ALPHA) + sum * ALPHA;
      Chan[0].Add(x);
      Chan[1].Add(sum);
    }

    public override void Reset()
    {
      sum = 0.0;
    }
  }

  // In:Chan[0] Out:Chan[0],Chan[1]
  class GyroTP : ValueHandler
  {
    const double BIAS = 392;
    const double ALPHA = 0.003;
    double y = 0.0; // filter output
    
    public override void SetValue(int aIdx, double aVal)
    {
      if (aIdx != 0) return;
      double x = aVal - BIAS;
      y = y*(1-ALPHA) + x*ALPHA*10;
      // Chan[0].Add(x);
      Chan[1].Add(y);
    }
  }
}
