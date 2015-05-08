using KinectSandbox.Common.Colors;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Prism.ViewModel.Subscription;

namespace KinectSandbox.ColorPicker.ViewModel
{
    public class IndividualColorPickerViewModel : ViewModelBase
    {
        public IndividualColorPickerViewModel(IVmInit init) : base(init)
        {
            var blue = RGB.Blue;
            this.SelectedColor = Color.FromRgb(blue.R, blue.G, blue.B);

            this.eventAggregator.GetEvent<PropertyChangedEvent>().Subscribe(NotifyColorChange,ThreadOption.PublisherThread,false,info => info.Sender == this && info.PropertyName == "SelectedColor" );                                 

            this.SelectedLayer = SupportedColorLayer.Layer1;
        }

        private void NotifyColorChange(PropertyChangedInfo e)
        {
            this.eventAggregator.GetEvent<LayerValueChanged>().Publish(new LayerValueRange() { 
                Layer = this.SelectedLayer,
                MinValue = (ushort)this.MinValue,
                MaxValue = (ushort)this.MaxValue,
                Color = new RGB(this.SelectedColor.R,this.SelectedColor.G,this.SelectedColor.B)
            }); 
        }


        #region SupportedColorLayer Property

        /// <summary>
        /// Gets and sets the color layer that this picker represents
        /// </summary>
        [DefaultValue(SupportedColorLayer.Layer1)]
        public SupportedColorLayer SelectedLayer
        {
            get { return Get<SupportedColorLayer>(); }
            set { 
                Set(value);
                LayerName = value.ToString();
            }
        }

        #endregion

        #region LayerName Property

        /// <summary>
        /// Gets and sets the name of the layer
        /// </summary>
        [DefaultValue("")]
        public String LayerName
        {
            get { return Get<String>(); }
            private set { Set(value); }
        }

        #endregion
        
        #region MinValue Property

        /// <summary>
        /// Gets and sets the minimum value that this layer will color
        /// </summary>
        [DefaultValue(0)]
        public int MinValue
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        #endregion
        
        #region MaxValue Property

        /// <summary>
        /// Gets and sets the maximum value this layer will color
        /// </summary>
        [DefaultValue(500)]
        public int MaxValue
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        #endregion
        
        #region SelectedColor Property

        /// <summary>
        /// Gets and sets the selected color for this layer
        /// </summary>
        public Color SelectedColor
        {
            get { return Get<Color>(); }
            set { Set(value); }
        }

        #endregion        
    }
}
