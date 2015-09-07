using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectSandbox.Common
{
    public static class Constants
    {
        /// <summary>
        /// Kinect DPI.
        /// </summary>
        public static readonly double DPI = 96.0;

        /// <summary>
        /// Default format.
        /// </summary>
        //public static readonly PixelFormat FORMAT = PixelFormats.Bgr32;

        private static int Bgr32BitsPerPixel = 32;

        /// <summary>
        /// Bytes per pixel.
        /// </summary>
        public static readonly int BYTES_PER_PIXEL = (Bgr32BitsPerPixel + 7) / 8;

    }
}
