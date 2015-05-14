using KinectSandbox.ColorPicker.View;
using KinectSandbox.ColorPicker.ViewModel;
using KinectSandbox.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.ColorPicker
{
    public class ColorPickerModule : IModule
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        public ColorPickerModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<AllColorPicker>();
            container.RegisterType<AllColorPickerViewModel>();
            container.RegisterType<IndividualColorPickerViewModel>();

            regionManager.RegisterViewWithRegion(KnownRegion.Named.ColorPicker, typeof(AllColorPicker));
        }
    }
}
