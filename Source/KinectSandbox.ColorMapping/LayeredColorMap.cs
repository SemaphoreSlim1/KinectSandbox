using KinectSandbox.Common;
using KinectSandbox.Common.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.ColorMapping
{
    public class LayeredColorMap : IColorMap
    {
        private UInt16 minReliable;
        private UInt16 maxReliable;

        private Dictionary<UInt16, RGB> depthColors = new Dictionary<UInt16, RGB>();

        public void Init(UInt16 minReliable, UInt16 maxReliable)
        {}

        private byte GetIntensityForDepth(UInt16 depth)
        {
            return (byte)(depth >= minReliable && depth <= maxReliable ? depth : 0);
        }

        public RGB GetColorForDepth(int x, int y, UInt16 depth)
        {
            if (depth.Between(500, 765))
            { return RGB.Lime.ApplyIntensity(GetIntensityForDepth(depth)); }
            else if (depth.Between(765, 800))
            { return RGB.White; }
            else if (depth.Between(800, 850))
            { return RGB.Maroon; }
            else if (depth.Between(850, 900))
            { return RGB.Olive; }
            else if (depth.Between(950, 1000))
            { return RGB.Yellow; }
            else if (depth.Between(1000, 1200))
            { return RGB.Blue; }
            else
            { return RGB.Black; }
        }


    }
}
