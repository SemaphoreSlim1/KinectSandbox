using DependencyViewModel;
using KinectSandbox.Common.Colors;
using KinectSandbox.Common.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.AllColorPicker
{
    public class LayeredColorViewModel : ViewModelBase, ILayeredColor
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
        public string LayerName
        {
            get { return Get<string>(); }
            private set { Set(value); }
        }

        /// <summary>
        /// Gets and sets the minimum value that this layer will color
        /// </summary>
        [DefaultValue(0)]
        public int MinValue
        {
            get { return Get<int>(); }
            set
            {
                Set(value);
                NotifyLayerChange();
            }
        }

        /// <summary>
        /// Gets and sets the maximum value this layer will color
        /// </summary>
        [DefaultValue(500)]
        public int MaxValue
        {
            get { return Get<int>(); }
            set
            {
                Set(value);
                NotifyLayerChange();
            }
        }

        /// <summary>
        /// Gets and sets the selected color for this layer
        /// </summary>
        public Color SelectedColor
        {
            get { return Get<Color>(); }
            set
            {
                Set(value);
                NotifyLayerChange();
            }
        }

        private readonly IEventAggregator eventAggregator;

        public LayeredColorViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator)
            : base(propertyStore)
        {
            this.eventAggregator = eventAggregator;

            var blue = RGB.Blue;
            this.SelectedColor = Color.FromRgb(blue.R, blue.G, blue.B);
            this.SelectedLayer = SupportedColorLayer.Layer1;
        }

        private void NotifyLayerChange()
        {
            var color = new RGB(this.SelectedColor.R, this.SelectedColor.G, this.SelectedColor.B);
            this.eventAggregator.GetEvent<LayerValueChangedEvent>().Publish(new LayerValueInformation(this.SelectedLayer, color, (ushort)this.MinValue, (ushort)this.MaxValue));
        }
    }
}
