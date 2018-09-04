using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    class Common : IPadToIRL
    {
        public IRL Get(Gamepad pad)
        {
            return new XYRL(pad.LeftThumbX / 32768f, (pad.RightTrigger - pad.LeftTrigger) / -255f);
        }
    }
}
