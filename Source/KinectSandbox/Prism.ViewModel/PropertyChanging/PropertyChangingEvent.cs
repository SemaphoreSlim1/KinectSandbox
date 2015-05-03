using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.PropertyChanging
{
    /// <summary>
    /// Prism Pub-Sub event for a non-cancelable notification that a property is about to change
    /// </summary>
    public class PropertyChangingEvent : PubSubEvent<PropertyChangingInfo>
    {}
}
