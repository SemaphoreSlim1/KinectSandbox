using KinectSandbox.Capture;
using KinectSandbox.View;
using KinectSandbox.ViewModel;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KinectSandbox
{
    public class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Creates the shell, or "Main Window" of the application
        /// </summary>        
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<IShellView>() as DependencyObject;
        }

        /// <summary>
        /// Initializes the shell
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.AddNewExtension<Interception>();

            Container.RegisterType<IShellView, ShellView>();
            Container.RegisterType<IShellViewModel, ShellViewModel>();

            Container.RegisterType<IPropertyStore, PropertyStore>(new PerResolveLifetimeManager());
            Container.RegisterType<IVmInit, VmInit>(new PerResolveLifetimeManager());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var mc = new ModuleCatalog();
            mc.AddModule(typeof(ViewModelModule));
            mc.AddModule(typeof(CaptureModule));
            

            return mc;
        }
    }
}
