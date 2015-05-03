using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.Subscription
{
    public class Subscriber<T>
    {
        public ViewModelBase ViewModel { get; protected set; }
        public String PropertyName { get; protected set; }

        public Subscriber(ViewModelBase vm, Expression<Func<T>> propertyNameExpr)
        {
            this.ViewModel = vm;
            this.PropertyName = (propertyNameExpr.Body as MemberExpression).Member.Name;
        }

        public SubscriptionToken SubscribeTo<TProperty>(ViewModelBase triggerVm, Expression<Func<TProperty>> triggerPropertyNameExpr)
        {            
            var triggerPropertyName = (triggerPropertyNameExpr.Body as MemberExpression).Member.Name;

            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var token = eventAggregator.GetEvent<PropertyChanged.PropertyChangedEvent>().Subscribe((info) =>
            {
                var subscriberPropertyInfo = this.ViewModel.GetType().GetProperty(this.PropertyName);

                if (subscriberPropertyInfo.PropertyType.IsAssignableFrom(typeof(DelegateCommand)))
                {
                  (subscriberPropertyInfo.GetValue(this.ViewModel) as DelegateCommand).RaiseCanExecuteChanged();
                }
                else
                {
                    eventAggregator.GetEvent<PropertyChanged.PropertyChangedEvent>().Publish(new PropertyChanged.PropertyChangedInfo()
                    {
                        Sender = this.ViewModel,
                        PropertyName = this.PropertyName,
                        NewValue = subscriberPropertyInfo.GetValue(this.ViewModel)
                    });
                }
            }, ThreadOption.UIThread, true, filter => filter.Sender == triggerVm && filter.PropertyName == triggerPropertyName);

            return token;
        }

    }
}
