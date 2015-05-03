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
        private UInt16 min;
        private UInt16 max;
        private List<RGB> Colors;
        private Int16 stepsPerColor;
                
        public void Init(UInt16 minDepth, UInt16 maxDepth, IEnumerable<RGB> colors)
        {
            this.min = minDepth;
            this.max = maxDepth;
            this.Colors = new List<RGB>(colors.Count());

            foreach(var color in colors)
            {
                Colors.Add(new RGB(color.R, color.G, color.B));
            }

            stepsPerColor = (Int16)((max - min) / Colors.Count());
            
        }

        public RGB GetColorForDepth(UInt16 depth)
        {
            var intensity = (byte)(depth >= min && depth <= max ? depth : 0);

            RGB desiredColor = new RGB();

            for (var i = 0; i < Colors.Count(); i++ )
            {
                if(min + (i*stepsPerColor) > depth)
                {
                    desiredColor = Colors[i];
                    break;
                }
            }

            return desiredColor;
            
        }

       
    }
}
