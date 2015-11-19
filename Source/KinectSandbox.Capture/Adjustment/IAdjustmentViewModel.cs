using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Capture.Adjustment
{
    public interface IAdjustmentViewModel
    {
        /// <summary>
        /// Gets and sets the skew to apply to the preview
        /// </summary>
        int Skew { get; set; }
    }
}
