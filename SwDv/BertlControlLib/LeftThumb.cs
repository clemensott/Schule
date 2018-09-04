using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    class LeftThumb : IPadToIRL
    {
        public IRL Get(Gamepad pad)
        {
            return new XYRL(pad.LeftThumbX / -32768f, pad.LeftThumbY / 32768f);
        }
    }
}
