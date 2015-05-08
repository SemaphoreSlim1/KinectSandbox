using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.PropertyChanged
{
    /// <summary>
    /// Prism Pub-Sub event for INotifyPropertyChanged
    /// </summary>
    public class PropertyChangedEvent : PubSubEvent<PropertyChangedInfo>
    { }
}
