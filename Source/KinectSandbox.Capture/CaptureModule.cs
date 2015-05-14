using KinectSandbox.Capture.View;
using KinectSandbox.Capture.ViewModel;
using KinectSandbox.Common;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectSandbox.Capture.ColorMapping;

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
            container.RegisterType<IPreview, Preview>();
            container.RegisterType<IPreviewViewModel, PreviewViewModel>();

            container.RegisterType<IAdjustmentView, AdjustmentView>();
            container.RegisterType<IAdjustmentViewModel, AdjustmentViewModel>();

            container.RegisterType<IColorMap, ConfigurableColorMap>();

            regionManager.RegisterViewWithRegion(KnownRegion.Named.KinectPreview, typeof(IPreview));
            regionManager.RegisterViewWithRegion(KnownRegion.Named.Adjustment, typeof(IAdjustmentView));
        }
    }
}
