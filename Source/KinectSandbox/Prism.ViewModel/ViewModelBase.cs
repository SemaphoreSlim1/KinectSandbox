using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.Property;
using Prism.ViewModel.PropertyChanged;
using Prism.ViewModel.PropertyChanging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanging, INotifyPropertyChanged
    {
        /// <summary>
        /// Non-cancelable notification that occurs when a property value is about to change
        /// </summary>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Notification that occurs after a property value has changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The backing store for property values
        /// </summary>
        protected readonly IPropertyStore propertyStore;     

        /// <summary>
        /// the event aggregator which handles pubsub events
        /// </summary>
        protected readonly IEventAggregator eventAggregator;

        protected ViewModelBase(IVmInit init)
        {
            this.propertyStore = init.PropertyStore;
            this.propertyStore.DeclareOwner(this);

            this.eventAggregator = init.EventAggregator;           

            this.eventAggregator.GetEvent<PropertyChangingEvent>().Subscribe(OnPropertyChanging, ThreadOption.UIThread, false, info => info.Sender == this);
            this.eventAggregator.GetEvent<PropertyChangedEvent>().Subscribe(OnPropertyChanged, ThreadOption.UIThread, false, info => info.Sender == this);
        }

        private void OnPropertyChanging(PropertyChangingInfo info)
        {
            if (PropertyChanging != null)
            { PropertyChanging(info.Sender, new PropertyChangingEventArgs(info.PropertyName)); }
        }

        private void OnPropertyChanged(PropertyChangedInfo info)
        {
            if (PropertyChanged != null)
            { PropertyChanged(info.Sender, new PropertyChangedEventArgs(info.PropertyName)); }
        }
       

        /// <summary>
        /// Gets the value for <paramref name="propertyName"/>, or the default, if this is the first retrieval.
        /// </summary>
        /// <typeparam name="T">The type of <paramref name="propertyName"/></typeparam>
        /// <param name="propertyName">The name of the property to retrieve the backging value for</param>
        /// <returns>The value for <paramref name="propertyName"/></returns>
        protected T Get<T>([CallerMemberName] String propertyName = null)
        {
            if (String.IsNullOrWhiteSpace(propertyName))
            { throw new ArgumentException("propertyName"); }

            return propertyStore.Get<T>(propertyName);
        }


        /// <summary>
        /// Sets the named property to the desired value only if <paramref name="value"/> differs from the value stored in the backing store.
        /// Publishes the change, and raises <see cref="INotifyPropertyChanging"/> and <see cref="INotifiyPropertyChanged"/> on the UI thread.
        /// </summary>
        /// <typeparam name="T">The type of the property</typeparam>
        /// <param name="newValue">The new value</param>
        /// <param name="propertyName">The name of the property to change</param>
        /// <returns>True, if the value was actually changed</returns>
        protected Boolean Set<T>(T newValue, [CallerMemberName] String propertyName = null)
        {
            if(String.IsNullOrWhiteSpace(propertyName))
            { throw new ArgumentException("propertyName"); }

            return propertyStore.Set(propertyName, newValue);
        }

    }
}
