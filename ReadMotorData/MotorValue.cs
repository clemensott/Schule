using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMotorData
{
    enum MotorType { Left, Right }

    enum EncoderType { A, B }

    struct MotorValue
    {
        public int Pow { get; private set; }

        public MotorType Motor { get; private set; }

        public EncoderType Encoder { get; private set; }

        public int Value { get; private set; }

        public MotorValue(int pow, MotorType motor, EncoderType encoder, int value)
        {
            Pow = pow;
            Motor = motor;
            Encoder = encoder;
            Value = value;
        }
    }
}
