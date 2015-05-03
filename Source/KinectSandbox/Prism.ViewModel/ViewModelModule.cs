using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.PropertyChanging;
using Prism.ViewModel.PropertyChanged;
using Prism.ViewModel.Property;


namespace Prism.ViewModel
{
    public class ViewModelModule : IModule
    {
        private readonly IUnityContainer container;

        public ViewModelModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            
        }
    }
}
