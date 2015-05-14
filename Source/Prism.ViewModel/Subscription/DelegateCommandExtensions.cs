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
    public static class DelegateCommandExtensions
    {
        /// <summary>
        /// Subscribes this delegate command to property change events on the trigger vm
        /// </summary>
        /// <typeparam name="TProperty">The type of the property to subscribe to</typeparam>
        /// <param name="cmd">The command that is subscribing to property change notifications</param>
        /// <param name="triggerVm">The view model containing the trigger property</param>
        /// <param name="triggerPropertyNameExpr">The expression which represents the trigger property</param>
        /// <returns>A Prism subscription token</returns>
        public static SubscriptionToken SubscribeTo<TProperty>(this DelegateCommand cmd, ViewModelBase triggerVm, Expression<Func<TProperty>> triggerPropertyNameExpr)
        {
            var triggerPropertyName = (triggerPropertyNameExpr.Body as MemberExpression).Member.Name;

            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            var token = eventAggregator.GetEvent<PropertyChanged.PropertyChangedEvent>().Subscribe((info) =>
            {
                cmd.RaiseCanExecuteChanged();
            }, ThreadOption.UIThread, true, filter => filter.Sender == triggerVm && filter.PropertyName == triggerPropertyName);

            return token;
        }

    }
}
