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
        private UInt16 min;
        private UInt16 max;

        public void Init(UInt16 minDepth, UInt16 maxDepth, IEnumerable<RGB> colors)
        {
            this.min = minDepth;
            this.max = maxDepth;
        }

        public RGB GetColorForDepth(UInt16 depth)
        {
            // To convert to a byte, we're discarding the most-significant
            // rather than least-significant bits.
            // We're preserving detail, although the intensity will "wrap."
            // Values outside the reliable depth range are mapped to 0 (black).
            if (depth >= min && depth <= max)
            { return new RGB((byte)depth, (byte)depth, (byte)depth); }
            else
            { return new RGB() { R = 0, B = 0, G = 0 }; }
        }

       
    }
}
