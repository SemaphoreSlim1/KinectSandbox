using KinectSandbox.Capture.ColorMapping;
using KinectSandbox.Common;
using KinectSandbox.Common.Colors;
using Microsoft.Kinect;
using Prism.ViewModel;
using Prism.ViewModel.Initialization;
using Prism.ViewModel.PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Gets the frame reader
        /// </summary>
        private MultiSourceFrameReader reader;

        /// <summary>
        /// Returns the current depth values.
        /// </summary>
        private UInt16[] DepthData { get; set; }

        private Body[] Bodies { get; set; }

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
        /// Gets and sets the kinect connection status
        /// </summary>
        public String StatusText
        {
            get { return Get<String>(); }
            set { Set(value); }
        }

        private ObservableCollection<CanvasPoint> bodyPoints;

        public ObservableCollection<CanvasPoint> BodyPoints
        {
            get
            {
                if(bodyPoints == null)
                { bodyPoints = new ObservableCollection<CanvasPoint>(); }

                return bodyPoints;
            }
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
            reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Depth | FrameSourceTypes.Body);
            reader.MultiSourceFrameArrived += reader_FrameArrived;
            
            //colorMap.Init(r new RGB[] { RGB.Red, RGB.White });

            sensor.Open();
        }

        private void InitBitmap()
        {
            Width = 512; //depthFrame.FrameDescription.Width;
            Height = 424; //depthFrame.FrameDescription.Height;

            DepthData = new UInt16[Width * Height];
            

            Pixels = new byte[DepthData.Length * Constants.BYTES_PER_PIXEL];
            Bitmap = new WriteableBitmap(Width, Height, Constants.DPI, Constants.DPI, Constants.FORMAT, null);

            Bodies = new Body[6];
        }

        void reader_FrameArrived(Object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var multiSourceFrame = e.FrameReference.AcquireFrame();
            
            if (multiSourceFrame == null)
            { return; }

            var depthFrame = multiSourceFrame.DepthFrameReference.AcquireFrame();
            var bodyFrame = multiSourceFrame.BodyFrameReference.AcquireFrame();

            if (depthFrame == null || bodyFrame == null)
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
            bodyFrame.Dispose();

            Bitmap.Lock();

            Marshal.Copy(Pixels, 0, Bitmap.BackBuffer, Pixels.Length);
            Bitmap.AddDirtyRect(new Int32Rect(0, 0, Width, Height));

            Bitmap.Unlock();


            this.eventAggregator.GetEvent<PropertyChangedEvent>().Publish(new PropertyChangedInfo()
            {
                Sender = this,
                PropertyName = "ImageSource"
            });
        }


        void reader_DepthFrameArrived(object sender, DepthFrameArrivedEventArgs e)
        {
            using (var depthFrame = e.FrameReference.AcquireFrame())
            {
                if (depthFrame == null)
                { return; } //nothing to see here. Move along.

                var minDepth = depthFrame.DepthMinReliableDistance;
                var maxDepth = depthFrame.DepthMaxReliableDistance;

                if (Bitmap == null)
                { InitBitmap(); }

                depthFrame.CopyFrameDataToArray(DepthData);

                // Convert the depth to RGB.
                int colorIndex = 0;


                //depthdata.length = 512*424 = 217088
             

                //var x = 0;
                //var y = 0;

                //for (int depthIndex = 0; depthIndex < DepthData.Length; depthIndex++)
                //{
                //    // Get the depth for this pixel
                //    ushort depth = DepthData[depthIndex];

                //    RGB color;

                //    //if (y > HalfHeight - 5 && y < HalfHeight + 5)
                //    //{
                //    //    color = RGB.Lime;
                //    //}else
                //    //{

                //    //}

                //    //var hypDepthIndex = (512 * y) + x + y;

                //    color = colorMap.GetColorForDepth(x, y, depth);

                //    Pixels[colorIndex++] = color.B; // Blue
                //    Pixels[colorIndex++] = color.G; // Lime
                //    Pixels[colorIndex++] = color.R; // Red

                //    colorIndex++;

                //    if (x >= Width)
                //    {
                //        x = 0;
                //        y++;
                //        if(y > maxY)
                //        { 
                //            maxY = y;
                //            Console.WriteLine("Max Y = " + maxY);
                //        }
                //    }
                //    else
                //    {
                //        x++;
                //    }
                //}

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
