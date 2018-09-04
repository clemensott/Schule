using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    class Thumbs : IPadToIRL
    {
        public IRL Get(Gamepad pad)
        {
            return new RL(pad.RightThumbY / -32768f, pad.LeftThumbY / -32768f);
        }
    }
}
