using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common.Events
{
    public class AdjustmentChangedEvent : PubSubEvent<AdjustmentChangedInformation>
    { }

    public class AdjustmentChangedInformation
    {
        public int Skew { get; private set; }

        public AdjustmentChangedInformation(int skew)
        {
            this.Skew = skew;
        }
    }
}
