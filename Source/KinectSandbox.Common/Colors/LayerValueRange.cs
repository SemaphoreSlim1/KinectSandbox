using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common.Colors
{
    /// <summary>
    /// event information for the <see cref="LayerValueChanged" /> pubsub event
    /// </summary>
    public class LayerValueRange
    {
        public SupportedColorLayer Layer { get; set; }

        public RGB Color { get; set; }

        public UInt16 MinValue { get; set; }

        public UInt16 MaxValue { get; set; }
    }
}
