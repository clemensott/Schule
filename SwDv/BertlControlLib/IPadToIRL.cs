using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    public enum Control { Common, LeftThumb, RightThumb, Trigger, Thumbs }

    interface IPadToIRL
    {
        IRL Get(Gamepad pad);
    }
}
