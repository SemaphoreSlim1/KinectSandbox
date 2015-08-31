
using KinectSandbox.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

using KinectSandbox.Capture.ColorMapping;
using KinectSandbox.Capture.Preview;
using KinectSandbox.Capture.Adjustment;

namespace KinectSandbox.Capture
{
    public class CaptureModule : IModule
    {
        private IUnityContainer container;
        private IRegionManager regionManager;

        public CaptureModule(IUnityContainer container, IRegionManager regionManager)
        {
            this.container = container;
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            container.RegisterType<PreviewView>();
            container.RegisterType<IPreviewViewModel, PreviewViewModel>();

            container.RegisterType<AdjustmentView>();
            container.RegisterType<IAdjustmentViewModel, AdjustmentViewModel>();

            container.RegisterType<IColorMap, ConfigurableColorMap>();

            regionManager.RegisterViewWithRegion(KnownRegion.Named.KinectPreview, typeof(PreviewView));
            regionManager.RegisterViewWithRegion(KnownRegion.Named.Adjustment, typeof(AdjustmentView));
        }
    }
}
