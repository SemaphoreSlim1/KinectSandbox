using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectSandbox.ViewModel
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        public ShellViewModel(IVmInit init)
            : base(init)
        { }


        #region IsFullScreen Property

        /// <summary>
        /// Gets and sets whether or not the app is in full screen mode
        /// </summary>
        [DefaultValue(false)]
        public Boolean IsFullScreen
        {
            get { return Get<Boolean>(); }
            set { Set(value); }
        }

        #endregion

        public void EnterFullScreen()
        {
            IsFullScreen = true;
        }

        public void ExitFullScreen()
        {
            IsFullScreen = false;
        }
        
    }
}
