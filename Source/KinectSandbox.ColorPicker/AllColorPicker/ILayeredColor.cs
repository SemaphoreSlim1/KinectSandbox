using KinectSandbox.Common.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.AllColorPicker
{
    public interface ILayeredColor
    {
        SupportedColorLayer SelectedLayer { get; set; }
        string LayerName { get; }
        int MinValue { get; set; }
        int MaxValue { get; set; }
        Color SelectedColor { get; set; }
    }
}
