using KinectSandbox.Common.Colors;
using Microsoft.Practices.ServiceLocation;
using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KinectSandbox.ColorPicker.ViewModel
{
    public class AllColorPickerViewModel : ViewModelBase
    {
        public AllColorPickerViewModel(IVmInit init) : base(init) 
        {

            //init the layer collection
            var layers = new List<IndividualColorPickerViewModel>();

            var supportedLayers = Enum.GetValues(typeof(SupportedColorLayer)).Cast<SupportedColorLayer>().OrderBy(layer => layer);
            var current = 500;

            foreach(var layer in supportedLayers)
            {
                var initValues = ServiceLocator.Current.GetInstance<IVmInit>();
                var layerVM = new IndividualColorPickerViewModel(initValues);
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
