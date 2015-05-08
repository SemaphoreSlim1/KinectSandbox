using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.Capture.ColorMapping
{
    public class BlackAndWhiteColorMap : IColorMap
    {
        private UInt16 minReliable;
        private UInt16 maxReliable;

        public void Init(UInt16 minReliable, UInt16 maxReliable, IEnumerable<RGB> colors)
        {
            this.minReliable = minReliable;
            this.maxReliable = maxReliable;
        }

        public RGB GetColorForDepth(int x, int y, UInt16 depth)
        {
            // To convert to a byte, we're discarding the most-significant
            // rather than least-significant bits.
            // We're preserving detail, although the intensity will "wrap."
            // Values outside the reliable depth range are mapped to 0 (black).


            if (depth >= minReliable && depth <= maxReliable)
            { return new RGB((byte)depth, (byte)depth, (byte)depth); }
            else
            { return new RGB() { R = 0, B = 0, G = 0 }; }
        }

       
    }
}
