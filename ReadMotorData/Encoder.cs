using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadMotorData
{
    class Encoder
    {
        public Power[] Values { get; set; } = Enumerable.Range(0, 1001).Select(i => new Power()).ToArray();
    }
}
