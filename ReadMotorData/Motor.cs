using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMotorData
{
    class Motor
    {
        public Encoder A { get; set; } = new Encoder();

        public Encoder B { get; set; } = new Encoder();
    }
}
