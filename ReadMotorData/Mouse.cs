using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMotorData
{
    class Mouse
    {
        public Motor Left { get; set; } = new Motor();

        public Motor Right { get; set; } = new Motor();
    }
}
