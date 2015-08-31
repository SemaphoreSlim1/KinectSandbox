using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.ColorPicker.AllColorPicker
{
    public interface IAllColorPickerViewModel
    {
        IEnumerable<ILayeredColor> Layers { get; }
    }
}
