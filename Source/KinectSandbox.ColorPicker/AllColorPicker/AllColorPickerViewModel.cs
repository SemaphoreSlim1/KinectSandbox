using DependencyViewModel;
using KinectSandbox.Common.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.AllColorPicker
{
    public class AllColorPickerViewModel : ViewModelBase, IAllColorPickerViewModel
    {
        /// <summary>
        /// Gets and sets the view models for each supported layer
        /// </summary>

        public IEnumerable<ILayeredColor> Layers
        {
            get { return Get<IEnumerable<ILayeredColor>>(); }
            private set { Set(value); }
        }

        public AllColorPickerViewModel(IPropertyStore propertyStore, Func<ILayeredColor> colorPickerViewModelFactory)
           : base(propertyStore)
        {
            //init the layer collection
            var layers = new List<ILayeredColor>();

            var supportedLayers = Enum.GetValues(typeof(SupportedColorLayer)).Cast<SupportedColorLayer>().OrderBy(layer => layer);
            int current = 500;

            foreach (var layer in supportedLayers)
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




    }
}
