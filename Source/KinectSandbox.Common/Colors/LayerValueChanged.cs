using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common.Colors
{
    public class LayerValueChanged : PubSubEvent<LayerValueRange>
    {}
}
