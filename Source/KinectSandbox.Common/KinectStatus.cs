using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSandbox.Common
{
    public class KinectStatus
    {
        private static KinectStatus statusText = new KinectStatus();
        public static KinectStatus StatusText { get { return statusText; } }

        public String Available { get { return "Available"; } }

        public String NotAvailable { get { return "Not available"; } }
    }
}
