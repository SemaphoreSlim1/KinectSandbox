using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectSandbox.Capture.ColorMapping
{
    public struct RGB
    {
        public byte R;
        public byte G;
        public byte B;

        public RGB(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        public static RGB Black
        {
            get { return new RGB(byte.MaxValue, byte.MaxValue, byte.MaxValue); }
        }

        public static RGB White
        {
            get { return new RGB(0, 0, 0); }
        }

        public static RGB Red
        {
            get { return new RGB(byte.MaxValue, 0, 0); }
        }

        public static RGB Lime
        {
            get { return new RGB(0, byte.MaxValue, 0); }
        }

        public static RGB Blue
        {
            get { return new RGB(0, 0, byte.MaxValue); }
        }

        public static RGB Yellow
        {
            get { return new RGB(255, 255, 0); }
        }

        public static RGB Cyan
        {
            get { return new RGB(0, 255, 255); }
        }

        public static RGB Magenta
        {
            get { return new RGB(255, 0, 255); }
        }

        public static RGB Silver
        {
            get { return new RGB(192, 192, 192); }
        }

        public static RGB Gray
        {
            get { return new RGB(128, 128, 128); }
        }

        public static RGB Maroon
        {
            get { return new RGB(128, 0, 0); }
        }

        public static RGB Olive
        {
            get { return new RGB(128, 128, 0); }
        }

        public static RGB Green
        {
            get { return new RGB(0, 128, 0); }
        }

        public static RGB Purple
        {
            get { return new RGB(128, 0, 128); }
        }

        public static RGB Teal
        {
            get { return new RGB(0, 128, 128); }
        }

        public static RGB Navy
        {
            get { return new RGB(0, 0, 128); }
        }

        public RGB ApplyIntensity(byte intensity)
        {

            var r = (byte)(this.R * intensity);
            var g = (byte)(this.G * intensity);
            var b = (byte)(this.B * intensity);

            return new RGB(r, g, b);
        }

        //public static RGB operator +(RGB color1, RGB color2)
        //{
        //    return new RGB((byte)(color1.R + color2.R),
        //        (byte)(color1.G + color2.G),
        //        (byte)(color1.B + color2.B));
        //}
        //public static RGB operator -(RGB color1, RGB color2)
        //{
        //    return new RGB((byte)(color1.R - color2.R),
        //        (byte)(color1.G - color2.G),
        //        (byte)(color1.B - color2.B));
        //}

    }
}
