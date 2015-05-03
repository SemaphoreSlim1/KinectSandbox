using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.ViewModel
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IVmInit init)
            : base(init)
        { }
    }
}
