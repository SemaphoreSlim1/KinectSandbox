using KinectSandbox.Capture.ColorMapping;
using KinectSandbox.Common;
using Microsoft.Kinect;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;
using Prism.Mvvm.Events;
using Prism.Mvvm.Property;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace KinectSandbox.Capture.ViewModel
{
    public class PreviewViewModel : ViewModelBase, IPreviewViewModel
    {
        #region Member variables

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

        #endregion

        #region Image Source property

        /// <summary>
        /// The bitmap to display
        /// </summary>
        private WriteableBitmap Bitmap { get; set; }

        public ImageSource ImageSource
        {
            get { return Bitmap; }
        }

        #endregion

        #region Body points

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

        #endregion
        
        #region Skew Property

        /// <summary>
        /// Gets and sets the skew to apply to the image
        /// </summary>
        [DefaultValue(0)]
        public int Skew
        {
            get { return Get<int>(); }
            set { Set(value); }
        }

        #endregion

        public PreviewViewModel(IPropertyStore propertyStore, IEventAggregator eventAggregator, IColorMap colorMap)
            : base(propertyStore, eventAggregator)
        {
            this.colorMap = colorMap;

            InitKinect();

            //this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;

            this.eventAggregator.GetEvent<PropertyChangedEvent>()
                .Subscribe(UpdateSkew, ThreadOption.PublisherThread, false, 
                info => info.Sender != this && info.PropertyName == "Skew");
        }

        private void UpdateSkew(PropertyChangedInformation info)
        {
            this.Skew = (int)info.NewValue;
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

            this.eventAggregator.GetEvent<PropertyChangedEvent>()
                .Publish(new PropertyChangedInformation(this, "ImageSource", Bitmap));
            
        }
       
        void sensor_IsAvailableChanged(object sender, IsAvailableChangedEventArgs e)
        {
            //this.SensorAvailable = e.IsAvailable;
            //this.StatusText = this.sensor.IsAvailable ? KinectStatus.StatusText.Available : KinectStatus.StatusText.NotAvailable;
        }
    }
}
