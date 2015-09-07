using DependencyViewModel;
using KinectSandbox.Capture.ColorMapping;
using KinectSandbox.Common;
using KinectSandbox.Common.Events;
using Microsoft.Kinect;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectSandbox.Capture.Preview
{
    public class PreviewViewModel : ViewModelBase, IPreviewViewModel
    {
        private IColorMap colorMap;

        private KinectSensor sensor;

        /// <summary>
        /// Gets the frame reader
        /// </summary>
        private MultiSourceFrameReader reader;

        /// <summary>
        /// Returns the current depth values.
        /// </summary>
        private UInt16[] DepthData { get; set; }

        /// <summary>
        /// Returns the RGB pixel values.
        /// </summary>
        private byte[] Pixels { get; set; }

        /// <summary>
        /// Returns the width of the bitmap.
        /// </summary>
        private int Width { get; set; }

        /// <summary>
        /// Returns the height of the bitmap.
        /// </summary>
        private int Height { get; set; }        
      

        /// <summary>
        /// The bitmap to display
        /// </summary>
        private WriteableBitmap Bitmap { get; set; }

        public ImageSource ImageSource
        {
            get { return Bitmap; }
        }
               

        /// <summary>
        /// Gets and sets the skew to apply to the image
        /// </summary>
        [DefaultValue(0)]
        public int Skew
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        private readonly IEventAggregator eventAggregator;

        public PreviewViewModel(IPropertyStore propertyStore, IColorMap colorMap, IEventAggregator eventAggregator)
            : base(propertyStore)
        {
            this.colorMap = colorMap;
            this.eventAggregator = eventAggregator;

            InitKinect();

            //this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;

            this.eventAggregator.GetEvent<AdjustmentChangedEvent>()
                .Subscribe(UpdateSkew, ThreadOption.BackgroundThread);
        }

        private void UpdateSkew(AdjustmentChangedInformation info)
        {
            this.Skew = info.Skew;
        }

        private void InitKinect()
        {
            sensor = KinectSensor.GetDefault();
            sensor.IsAvailableChanged += sensor_IsAvailableChanged;
            reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth);
            reader.MultiSourceFrameArrived += reader_FrameArrived;

            sensor.Open();
        }

        private void InitBitmap()
        {
            Width = 512; //depthFrame.FrameDescription.Width;
            Height = 424; //depthFrame.FrameDescription.Height;

            DepthData = new UInt16[Width * Height];


            Pixels = new byte[DepthData.Length * Constants.BYTES_PER_PIXEL];
            Bitmap = new WriteableBitmap(Width, Height, Constants.DPI, Constants.DPI, PixelFormats.Bgr32, null);            
        }

        void reader_FrameArrived(Object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var multiSourceFrame = e.FrameReference.AcquireFrame();

            if (multiSourceFrame == null)
            { return; }

            var depthFrame = multiSourceFrame.DepthFrameReference.AcquireFrame();              

            if (depthFrame == null)
            { return; }

            if (Bitmap == null)
            {
                InitBitmap();
                colorMap.Init(depthFrame.DepthMinReliableDistance, depthFrame.DepthMaxReliableDistance);
            }

            depthFrame.CopyFrameDataToArray(DepthData);

            //This yeilds a black diagonal line where x==y - not sure why, but will look into later.
            //Parallel.For(0, Width, x =>
            //{
            //    Parallel.For(0, Height, y =>
            //    {
            //        if (y == 423)
            //        { return; }

            //        var depthIndex = (512 * y) + x + y;

            //        var depth = DepthData[depthIndex];
            //        var color = colorMap.GetColorForDepth(x, y, depth);

            //        var pixelIndexStart = (depthIndex * 4);
            //        Pixels[pixelIndexStart] = color.B;
            //        Pixels[pixelIndexStart + 1] = color.G;
            //        Pixels[pixelIndexStart + 2] = color.R;
            //        Pixels[pixelIndexStart + 3] = 255;

            //    });
            //});



            var x = 0;
            var y = 0;
            for (var i = 0; i < DepthData.Length; i++)
            {
                var color = colorMap.GetColorForDepth(x, y, DepthData[i]);
                Pixels[i * 4] = color.B;
                Pixels[(i * 4) + 1] = color.G;
                Pixels[(i * 4) + 2] = color.R;
                Pixels[(i * 4) + 3] = 255;

                if (x > Width)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x++;
                }
            }

            depthFrame.Dispose();

            Bitmap.Lock();

            Marshal.Copy(Pixels, 0, Bitmap.BackBuffer, Pixels.Length);
            Bitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));

            Bitmap.Unlock();

            OnPropertyChanged(()=>ImageSource);
        }

        void sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            //this.SensorAvailable = e.IsAvailable;
            //this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;
        }
    }
}
