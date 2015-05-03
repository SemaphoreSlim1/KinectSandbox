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
        void Init(UInt16 minDepth, UInt16 maxDepth, IEnumerable<RGB> colors);

        RGB GetColorForDepth(UInt16 depth);       
    }
}
