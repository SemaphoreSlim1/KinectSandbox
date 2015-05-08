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
    public interface IVmInit
    {        
        IEventAggregator EventAggregator { get; set; }
        IPropertyStore PropertyStore { get; set; }        
    }
}
