using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Common
{
    public class KnownRegion
    {
        private static KnownRegion named = new KnownRegion();
        public static KnownRegion Named { get { return named; } }

        public String KinectPreview { get { return "KinectPreviewRegion"; } }
    }
}
