using KinectSandbox.Common.Colors;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common.Events
{
    public class LayerValueChangedEvent : PubSubEvent<LayerValueInformation>
    { }

    /// <summary>
    /// Event information for the <see cref="LayerValueChangedEvent" /> pubsub event
    /// </summary>
    public class LayerValueInformation
    {
        public SupportedColorLayer Layer { get; private set; }

        public RGB Color { get; private set; }

        public UInt16 MinValue { get; private set; }

        public UInt16 MaxValue { get; private set; }

        public LayerValueInformation(SupportedColorLayer layer, RGB color, UInt16 minValue, UInt16 maxValue)
        {
            this.Layer = layer;
            this.Color = color;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }
    }
}
