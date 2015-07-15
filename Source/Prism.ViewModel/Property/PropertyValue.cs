using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism.ViewModel.Property
{
    [Serializable]
    public class PropertyValue
    {
        public Object Value{ get; set;}

        public Boolean IsInitialized { get; set; }

        public Type ValueType { get; set; }
    }
}
