using DependencyViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Shell
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
       public ShellViewModel(IPropertyStore propertyStore)
            :base(propertyStore)
        { }
    }
}
