using KinectSandbox.Common.Colors;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;
using Prism.Mvvm.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.ViewModel
{
    public class AllColorPickerViewModel : ViewModelBase
    {
        public AllColorPickerViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator, Func<IndividualColorPickerViewModel> colorPickerViewModelFactory) 
            : base(propertyStore, eventAggregator) 
        {
            //init the layer collection
            var layers = new List<IndividualColorPickerViewModel>();

            var supportedLayers = Enum.GetValues(typeof(SupportedColorLayer)).Cast<SupportedColorLayer>().OrderBy(layer => layer);
            int current = 500;

            foreach(var layer in supportedLayers)
            {
                var layerVM = colorPickerViewModelFactory();
                layerVM.SelectedLayer = layer;
                layerVM.SelectedColor = Color.FromRgb(RGB.Blue.R, RGB.Blue.G, RGB.Blue.B);
                layerVM.MinValue = current;
                layerVM.MaxValue = current += 50;                
                layers.Add(layerVM);
            }

            this.Layers = layers;
        }

                
        #region Layers Property

        /// <summary>
        /// Gets and sets the view models for each supported layer
        /// </summary>
        
        public IEnumerable<IndividualColorPickerViewModel> Layers
        {
            get { return Get<IEnumerable<IndividualColorPickerViewModel>>(); }
            set { Set(value); }
        }

        #endregion
        
    }
}
