
using KinectSandbox.ColorPicker.AllColorPicker;
using KinectSandbox.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

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
            container.RegisterType<AllColorPickerView>();
            container.RegisterType<IAllColorPickerViewModel, AllColorPickerViewModel>();
            container.RegisterType<ILayeredColor, LayeredColorViewModel>();

            regionManager.RegisterViewWithRegion(KnownRegion.Named.ColorPicker, typeof(AllColorPickerView));
        }
    }
}
