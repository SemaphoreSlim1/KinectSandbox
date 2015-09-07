using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml;

namespace KinectSandbox.Universal
{
    public class TitleBarBehavior : DependencyObject, IBehavior
    {
        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            var newTitleBar = associatedObject as UIElement;
            if (newTitleBar == null)
            {
                throw new ArgumentException(this.GetType().Name + " can only be attached to " + (typeof(UIElement)).Name);
            }

            Window.Current.SetTitleBar(newTitleBar);
        }

        public void Detach()
        { }
        
        /// <summary>
        /// Gets and sets whether this <see cref="UIElement"/> is chromeless. This is a dependency property.
        /// </summary>
        public bool IsChromeless
        {
            get { return (bool)GetValue(IsChromelessProperty); }
            set { SetValue(IsChromelessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsChromeless.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChromelessProperty =
            DependencyProperty.Register("IsChromeless", typeof(bool), typeof(TitleBarBehavior), new PropertyMetadata(false, OnIsChromelessChanged));

        private static void OnIsChromelessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = (bool)e.NewValue;
        }


    }
}
