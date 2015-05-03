using KinectSandbox.Capture.ColorMapping;
using KinectSandbox.Common;
using Microsoft.Kinect;
using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectSandbox.Capture.ViewModel
{
    public class PreviewViewModel : ViewModelBase, IPreviewViewModel
    {
        private IColorMap colorMap;

        private KinectSensor sensor;
        private DepthFrameReader reader;

        /// <summary>
        /// Returns the current depth values.
        /// </summary>
        public UInt16[] DepthData { get; private set; }

        /// <summary>
        /// Returns the RGB pixel values.
        /// </summary>
        public byte[] Pixels { get; private set; }
        
        /// <summary>
        /// Returns the width of the bitmap.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Returns the height of the bitmap.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// The bitmap to display
        /// </summary>
        private WriteableBitmap Bitmap { get; set; }

        public ImageSource ImageSource
        {
            get { return Bitmap; }
        }

        /// <summary>
        /// Gets and sets the kinect connection status
        /// </summary>
        public String StatusText
        {
            get { return Get<String>(); }
            set { Set(value); }
        }

        public PreviewViewModel(IVmInit init, IColorMap colorMap)
            : base(init)
        {
            this.colorMap = colorMap;

            InitKinect();

            this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;
        }

        private void InitKinect()
        {
            sensor = KinectSensor.GetDefault();
            sensor.IsAvailableChanged += sensor_IsAvailableChanged;

            reader = sensor.DepthFrameSource.OpenReader();
            var minDepth = reader.DepthFrameSource.DepthMinReliableDistance;
            var maxDepth = reader.DepthFrameSource.DepthMaxReliableDistance;

            reader.FrameArrived += reader_FrameArrived;
            colorMap.Init(minDepth, maxDepth, null);

            sensor.Open();
        }

        void reader_FrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {                       
            using (var depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame == null)
                { return; } //nothing to see here. Move along.

                var minDepth = depthFrame.DepthMinReliableDistance;
                var maxDepth = depthFrame.DepthMaxReliableDistance;

                if (Bitmap == null)
                {
                    Width = depthFrame.FrameDescription.Width;
                    Height = depthFrame.FrameDescription.Height;
                    DepthData = new ushort[Width * Height];
                    Pixels = new byte[Width * Height * Constants.BYTES_PER_PIXEL];
                    Bitmap = new WriteableBitmap(Width, Height, Constants.DPI, Constants.DPI, Constants.FORMAT, null);
                }

                depthFrame.CopyFrameDataToArray(DepthData);

                // Convert the depth to RGB.
                int colorIndex = 0;

                for (int depthIndex = 0; depthIndex < DepthData.Length; ++depthIndex)
                {
                    // Get the depth for this pixel
                    ushort depth = DepthData[depthIndex];

                    var color = colorMap.GetColorForDepth(depth);

                    Pixels[colorIndex++] = color.B; // Blue
                    Pixels[colorIndex++] = color.G; // Green
                    Pixels[colorIndex++] = color.R; // Red

                    // We're outputting BGR, the last byte in the 32 bits is unused so skip it
                    // If we were outputting BGRA, we would write alpha here.
                    ++colorIndex;
                }

                Bitmap.Lock();

                Marshal.Copy(Pixels, 0, Bitmap.BackBuffer, Pixels.Length);
                Bitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));

                Bitmap.Unlock();
            }

            this.eventAggregator.GetEvent<PropertyChangedEvent>().Publish(new PropertyChangedInfo()
            {
                Sender = this,
                PropertyName = "ImageSource"
            });
        }

        
        void sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            //this.SensorAvailable = e.IsAvailable;
            this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;
        }
    }
}
