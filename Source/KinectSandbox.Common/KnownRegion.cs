using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSandbox.Common
{
    public class KnownRegion
    {
        private static KnownRegion named = new KnownRegion();
        public static KnownRegion Named { get { return named; } }

        public String Adjustment { get { return "AdjustmentRegion"; } }

        public String KinectPreview { get { return "KinectPreviewRegion"; } }

        public String ColorPicker { get { return "ColorPickerRegion"; } }
    }
}
