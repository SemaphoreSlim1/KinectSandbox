using KinectSandbox.Common.Colors;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinectSandbox.Common;
using System.Threading;

namespace KinectSandbox.Capture.ColorMapping
{
    public class ConfigurableColorMap : IColorMap
    {
        private UInt16 minReliable;
        private UInt16 maxReliable;


        private IEventAggregator eventAggregator;

        private Dictionary<SupportedColorLayer, LayerValueRange> LayerConfiguration;
        private Dictionary<UInt16, RGB> DepthColors;
        

        public ConfigurableColorMap(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.LayerConfiguration = new Dictionary<SupportedColorLayer, LayerValueRange>();
            this.DepthColors = new Dictionary<UInt16, RGB>();

            UInt16 current = 500;
            foreach (SupportedColorLayer colorLayer in Enum.GetValues(typeof(SupportedColorLayer)))
            {
                LayerConfiguration[colorLayer] = new LayerValueRange()
                {
                    Layer = colorLayer,
                    MinValue = current,
                    MaxValue = (UInt16)(current + 50),
                    Color = RGB.Blue
                };

                current += 50;
            }

            RecomputeDepthColors();

            this.eventAggregator.GetEvent<LayerValueChanged>().Subscribe(UpdateLayerConfiguration,ThreadOption.BackgroundThread,false);
        }

        private void UpdateLayerConfiguration(LayerValueRange newValue)
        {
            LayerConfiguration[newValue.Layer] = newValue;
            RecomputeDepthColors();
        }

        private void RecomputeDepthColors()
        {            
            for(var i = UInt16.MinValue; i < UInt16.MaxValue; i++)
            {
                DepthColors[i] = RGB.Black;
            }

            var layerKeys = Enum.GetValues(typeof(SupportedColorLayer)).Cast<SupportedColorLayer>();
                        
            Parallel.ForEach(layerKeys, key => {
                var config = LayerConfiguration[key];
                for (var i = config.MinValue; i <= config.MaxValue; i++)
                {
                    DepthColors[i] = config.Color;
                }
            });
        }
       
        public void Init(UInt16 minReliable, UInt16 maxReliable)  
        {      
            this.minReliable = minReliable;
            this.maxReliable = maxReliable;
        }

        public RGB GetColorForDepth(int x, int y, UInt16 depth)
        {
            return DepthColors[depth];
        }        
    }
}
