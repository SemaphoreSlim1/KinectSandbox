using KinectSandbox.Common.Colors;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;
using Prism.Mvvm.Events;
using Prism.Mvvm.Property;
using System;
using System.ComponentModel;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.ViewModel
{
    public class IndividualColorPickerViewModel : ViewModelBase
    {

        /// <summary>
        /// Gets and sets the color layer that this picker represents
        /// </summary>
        [DefaultValue(SupportedColorLayer.Layer1)]
        public SupportedColorLayer SelectedLayer
        {
            get { return Get<SupportedColorLayer>(); }
            set
            {
                Set(value);
                LayerName = value.ToString();
            }
        }

        /// <summary>
        /// Gets (and privately sets) the name of the layer
        /// </summary>
        [DefaultValue("")]
        public String LayerName
        {
            get { return Get<String>(); }
            private set { Set(value); }
        }

        /// <summary>
        /// Gets and sets the minimum value that this layer will color
        /// </summary>
        [DefaultValue(0)]
        public int MinValue
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        /// <summary>
        /// Gets and sets the maximum value this layer will color
        /// </summary>
        [DefaultValue(500)]
        public int MaxValue
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        /// <summary>
        /// Gets and sets the selected color for this layer
        /// </summary>
        public Color SelectedColor
        {
            get { return Get<Color>(); }
            set { Set(value); }
        }

        public IndividualColorPickerViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator) 
            : base(propertyStore, eventAggregator)
        {
            var blue = RGB.Blue;
            this.SelectedColor = Color.FromRgb(blue.R, blue.G, blue.B);            
            this.SelectedLayer = SupportedColorLayer.Layer1;

            this.eventAggregator.GetEvent<PropertyChangedEvent>()
               .Subscribe(NotifyColorChange, ThreadOption.PublisherThread, false,
               info => info.Sender == this
               &&
               (
                   info.PropertyName == "SelectedColor"
                   || info.PropertyName == "MinValue"
                   || info.PropertyName == "MaxValue"));
        }

        private void NotifyColorChange(PropertyChangedInformation e)
        {
            this.eventAggregator.GetEvent<LayerValueChanged>().Publish(new LayerValueRange() { 
                Layer = this.SelectedLayer,
                MinValue = (ushort)this.MinValue,
                MaxValue = (ushort)this.MaxValue,
                Color = new RGB(this.SelectedColor.R,this.SelectedColor.G,this.SelectedColor.B)
            }); 
        }                                                                
    }
}
