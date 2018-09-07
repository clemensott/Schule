namespace X_Y_To_R_L_Lib
{
    public class RL : IRL
    {
        public float L { get; private set; }

        public float R { get; private set; }

        public RL(float r, float l)
        {
            R = r;
            L = l;
        }
    }
}
