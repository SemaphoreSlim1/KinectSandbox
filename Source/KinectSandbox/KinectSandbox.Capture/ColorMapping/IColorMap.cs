using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.Capture.ColorMapping
{
   

    public interface IColorMap
    {
        void Init(UInt16 minReliable, UInt16 maxReliable, IEnumerable<RGB> colors);

        RGB GetColorForDepth(int x, int y, UInt16 depth);       
    }
}
