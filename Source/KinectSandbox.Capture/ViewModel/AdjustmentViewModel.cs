using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;
using Prism.Mvvm.Property;
using System.ComponentModel;

namespace KinectSandbox.Capture.ViewModel
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

        public AdjustmentViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator)
                : base(propertyStore, eventAggregator)
        { }


    }
}
