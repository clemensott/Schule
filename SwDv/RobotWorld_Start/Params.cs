
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;

namespace RobotWorld
{
  // Alle Simulations und Physik-Parameter werden zentral
  // an einer Stelle gesetzt
	public class Par
	{
    public const int TIMER_INTERVAL = 50; // in ms  FPS
		
    public const int ITER_PER_TICK = 10; // Anz. Sim-Schritte pro Frame-Update
    
    public const double DT = 1.0/(double)ITER_PER_TICK;

    public const int TOP_BORDER = 26; // Size of Menue-Strip

    public const bool showPath = true; // Show Robot Path ?
  }
}
