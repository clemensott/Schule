using SharpDX.XInput;
using X_Y_To_R_L_Lib;

namespace BertlControlLib
{
    public class BertlXbox
    {
        private IPadToIRL controlType;
        private Controller controller;

        public IRL IRL { get; private set; }

        public BertlXbox() : this(Control.Common)
        {
        }

        public BertlXbox(Control type)
        {
            controller = new Controller(UserIndex.One);

            SetControl(type);
            Upadate();
        }

        public void SetControl(Control type)
        {
            switch (type)
            {
                case Control.Common:
                    controlType = new Common();
                    break;

                case Control.LeftThumb:
                    controlType = new LeftThumb();
                    break;

                case Control.RightThumb:
                    controlType = new RightThumb();
                    break;

                case Control.Trigger:
                    controlType = new Trigger();
                    break;

                case Control.Thumbs:
                    controlType = new Thumbs();
                    break;
            }
        }

        public void Upadate()
        {
            Gamepad pad = controller.GetState().Gamepad;

            IRL = controlType.Get(pad);
        }
    }
}
