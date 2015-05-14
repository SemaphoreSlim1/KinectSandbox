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
        /// Gets the bitmap to display
        /// </summary>
        ImageSource ImageSource { get; }        
    }
}
