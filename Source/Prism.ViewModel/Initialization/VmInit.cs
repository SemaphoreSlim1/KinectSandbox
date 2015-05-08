using Microsoft.Practices.Prism.PubSubEvents;
using Prism.ViewModel.Property;
using Prism.ViewModel.PropertyChanged;
using Prism.ViewModel.PropertyChanging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.Initialization
{
    /// <summary>
    /// Initialization dependencies for view models
    /// </summary>
    public class VmInit : IVmInit
    {
        public IEventAggregator EventAggregator { get; set; }
        public IPropertyStore PropertyStore { get; set; }

        public VmInit(
            IEventAggregator eventAggregator, 
            IPropertyStore propertyStore)
        {
            this.EventAggregator = eventAggregator;
            this.PropertyStore = propertyStore;            
        }
    }
}
