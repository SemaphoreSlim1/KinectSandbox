using Microsoft.Practices.Prism.PubSubEvents;
using Prism.ViewModel.PropertyChanged;
using Prism.ViewModel.PropertyChanging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.Property
{
    public class MemoryPropertyStore : IPropertyStore
    {
        protected ViewModelBase owner;
        protected readonly IDictionary<String, PropertyValue> propertyStore;
        protected readonly IEventAggregator eventAggregator;

        public MemoryPropertyStore(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.propertyStore = new Dictionary<String, PropertyValue>();
        }

        public virtual void DeclareOwner(ViewModelBase owner)
        {
            this.owner = owner;
        }

        public virtual T Get<T>(String propertyName)
        {
            if (propertyStore.ContainsKey(propertyName))
            {
                if (propertyStore[propertyName].IsInitialized)
                {
                    return (T)propertyStore[propertyName].Value; //happy path - it was present and initialized
                }

                //not so ahppy path - we have it in the store, but it's not initialized
                propertyStore[propertyName].Value = GetDefaultValue<T>(propertyName);
                propertyStore[propertyName].ValueType = typeof(T);
                propertyStore[propertyName].IsInitialized = true;
            }
            else
            {
                //not-happy path - the value store didn't contain a reference at all. Create it, and initialize it.
                propertyStore[propertyName] = new PropertyValue()
                {
                    Value = GetDefaultValue<T>(propertyName),
                    ValueType = typeof(T),
                    IsInitialized = true
                };                
            }

            return (T)propertyStore[propertyName].Value;
        }
        
        public virtual Boolean Set(String propertyName, Object newValue)
        {
            if (propertyStore.ContainsKey(propertyName))
            {
                if (propertyStore[propertyName].IsInitialized)
                {
                    if (Object.Equals(propertyStore[propertyName].Value, (Object)newValue))
                    { return false; } //the values are equal, no need to proceed further                    
                }

                var oldValue = propertyStore[propertyName].Value;
                this.eventAggregator.GetEvent<PropertyChangingEvent>().Publish(new PropertyChangingInfo() { 
                    Sender = owner,
                    PropertyName = propertyName,
                    OldValue = oldValue,
                    NewValue = newValue
                });

                propertyStore[propertyName].Value = newValue;

                this.eventAggregator.GetEvent<PropertyChangedEvent>().Publish(new PropertyChangedInfo() { 
                    Sender = owner,
                    PropertyName = propertyName,
                    NewValue = newValue
                });

                return true;
            }
            else
            {
                //this is the first time - it's not in the dictionary
                this.eventAggregator.GetEvent<PropertyChangingEvent>().Publish(new PropertyChangingInfo()
                {
                    Sender = owner,
                    PropertyName = propertyName,
                    OldValue = null,
                    NewValue = newValue
                });

                propertyStore[propertyName] = new PropertyValue()
                {
                    Value = newValue,
                    ValueType = newValue.GetType(),
                    IsInitialized = true
                };

                this.eventAggregator.GetEvent<PropertyChangedEvent>().Publish(new PropertyChangedInfo()
                {
                    Sender = owner,
                    PropertyName = propertyName,
                    NewValue = newValue
                });

                return true;
            }            
        }

        /// <summary>
        /// Gets the default value of the specified property name
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="propertyName">The name of the property</param>
        /// <returns>the default value for the specified property</returns>
        protected T GetDefaultValue<T>(String propertyName)
        {
            var propertyInfo = owner.GetType().GetProperty(propertyName);
            var defaultAttributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), false) as DefaultValueAttribute[];

            if (defaultAttributes != null && defaultAttributes[0] != null)
            {
                return (T)defaultAttributes[0].Value;
            }
            else
            {
                return default(T); //no default value, so return the default for the type
            }
        }

    }
}
