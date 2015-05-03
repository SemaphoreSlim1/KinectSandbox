using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.Property
{
    public interface IPropertyStore
    {
        /// <summary>
        /// Declares the object which "owns" this property store
        /// </summary>
        /// <param name="owner">The object which owns this property store</param>
        void DeclareOwner(Object owner);

        /// <summary>
        /// Gets the value from the backing store, using default values from the owner if this is the first retrieval
        /// </summary>
        /// <typeparam name="T">The type of value to return</typeparam>
        /// <param name="propertyName">The name of the property to retrieve from the backing store</param>        
        /// <returns>The value from the backing store, or the default value, if this is the first retrieval</returns>
        T Get<T>(String propertyName);

        /// <summary>
        /// Sets the value in the backing store, and raises property changing and property changed notifications, if appropriate
        /// </summary>
        /// <param name="propertyName">The name of the property to set</param>
        /// <param name="value">The new value</param>
        /// <returns>True, if the value was updated in the backing store</returns>
        Boolean Set(String propertyName, Object value);

    }
}
