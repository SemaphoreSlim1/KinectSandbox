using DependencyViewModel;
using System.ComponentModel;

namespace KinectSandbox.Capture.Adjustment
{
    public class AdjustmentViewModel : ViewModelBase, IAdjustmentViewModel
    {
        /// <summary>
        /// Gets and sets the skew to apply to the preview
        /// </summary>
        [DefaultValue(0)]
        public int Skew
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        public AdjustmentViewModel(IPropertyStore propertyStore)
            :base(propertyStore)
        {}
    }
}
