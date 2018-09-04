using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    class RightThumb : IPadToIRL
    {
        public IRL Get(Gamepad pad)
        {
            return new XYRL(pad.RightThumbX / -32768f, pad.RightThumbY / 32768f);
        }
    }
}
