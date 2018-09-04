using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    class Trigger : IPadToIRL
    {
        public IRL Get(Gamepad pad)
        {
            return new RL(pad.RightTrigger / 255f, pad.LeftTrigger / 255f);
        }
    }
}
