using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.Capture.ViewModel
{
    public interface IPreviewViewModel
    {
        /// <summary>
        /// Gets and sets the connection status of the kinect sensor
        /// </summary>
        String StatusText { get; set; }

        /// <summary>
        /// Gets the bitmap to display
        /// </summary>
        ImageSource ImageSource { get; }        
    }
}
