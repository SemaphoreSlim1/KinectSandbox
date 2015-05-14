using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.PropertyChanged
{
    /// <summary>
    /// Prism Pub-Sub event information for notifying that a property changed
    /// </summary>
    public class PropertyChangedInfo
    {
        /// <summary>
        /// Gets and sets the object that contained the property that changed
        /// </summary>
        public Object Sender { get; set; }

        /// <summary>
        /// Gets and sets the name of the property that changed
        /// </summary>
        public String PropertyName { get; set; }

        /// <summary>
        /// Gets and sets the new value of the property
        /// </summary>
        public Object NewValue { get; set; }
    }
}
