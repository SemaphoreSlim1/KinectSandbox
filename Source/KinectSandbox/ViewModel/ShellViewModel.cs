using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;
using Prism.Mvvm.Property;
using System;
using System.ComponentModel;

namespace KinectSandbox.ViewModel
{
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        /// <summary>
        /// Gets and sets whether or not the app is in full screen mode
        /// </summary>
        [DefaultValue(false)]
        public Boolean IsFullScreen
        {
            get { return Get<Boolean>(); }
            set { Set(value); }
        }

        public ShellViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator)
            : base(propertyStore, eventAggregator)
        { }

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
