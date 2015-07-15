using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Capture.ViewModel
{
    public class AdjustmentViewModel : ViewModelBase, IAdjustmentViewModel
    {
        public AdjustmentViewModel(IVmInit init) : base(init, "Adjustment")
        { }

        #region Skew Property

        /// <summary>
        /// Gets and sets the skew to apply to the preview
        /// </summary>
        [DefaultValue(0)]
        public int Skew
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        #endregion

    }
}
