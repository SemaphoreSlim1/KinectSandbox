using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common
{
    public static class Extensions
    {
        public static Boolean Between(this UInt16 num, UInt16 min, UInt16 max)
        {
            return num >= min && num < max;
        }
    }
}
