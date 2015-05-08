using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectSandbox.Common;
using System.Windows.Media;

namespace KinectSandbox.Capture.ColorMapping
{
    public class LayeredColorMap : IColorMap
    {
        private UInt16 minReliable;
        private UInt16 maxReliable;

        private Dictionary<UInt16, RGB> depthColors = new Dictionary<UInt16, RGB>();

        public void Init(UInt16 minReliable, UInt16 maxReliable, IEnumerable<RGB> colors)
        {
            this.minReliable = minReliable;
            this.maxReliable = maxReliable;
            //sandbox is between 30" and 40"
            //or 762 and 1016
            for (UInt16 i = 0; i < Int16.MaxValue; i++)
            {
                
                if (i.Between(500, 765))
                { depthColors[i] = RGB.Lime.ApplyIntensity(GetIntensityForDepth(i)); }
                else if (i.Between(765, 800))
                { depthColors[i] = RGB.White; }
                else if(i.Between(800,850))
                { depthColors[i] = RGB.Maroon; }
                else if(i.Between(850,900))
                { depthColors[i] = RGB.Olive; }
                else if(i.Between(950,1000))
                { depthColors[i] = RGB.Yellow; }
                else if(i.Between(1000,1200))
                { depthColors[i] = RGB.Blue; }
                else
                { depthColors[i] = RGB.Black;}

            }
        }

        private byte GetIntensityForDepth(UInt16 depth)
        {
            return (byte)(depth >= minReliable && depth <= maxReliable ? depth : 0);
        }

        public RGB GetColorForDepth(int x, int y, UInt16 depth)
        {
            return depthColors[depth];
        }


    }
}
