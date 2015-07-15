using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace Prism.ViewModel.Property
{
    public class FilePropertyStore : MemoryPropertyStore
    {
        public FilePropertyStore(IEventAggregator eventAggregator)
            : base(eventAggregator)
        { }

        public override void DeclareOwner(ViewModelBase owner)
        {
            base.DeclareOwner(owner);


            if (File.Exists(owner.ID + ".txt"))
            {
                var binary = File.ReadAllBytes(owner.ID + ".txt");
                using (var ms = new MemoryStream(binary))
                {
                    var formatter = new BinaryFormatter();

                    var fileContents = formatter.Deserialize(ms) as Dictionary<String, PropertyValue>;

                    foreach (var kvp in fileContents)
                    {
                        var value = kvp.Value;
                        this.propertyStore[kvp.Key] = value;
                    }

                }
            }
        }


        public override bool Set(string propertyName, object newValue)
        {
            var result = base.Set(propertyName, newValue);

            if (result)
            {
                using (var ms = new MemoryStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(ms, this.propertyStore);
                    File.WriteAllBytes(owner.ID + ".txt", ms.GetBuffer());
                }
            }

            return result;
        }
    }
}
