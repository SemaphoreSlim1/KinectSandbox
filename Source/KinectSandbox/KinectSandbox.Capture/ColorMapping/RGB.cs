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

        public static RGB Green
        {
            get { return new RGB(0, 0, byte.MaxValue); }
        }

        public static RGB Blue
        {
            get { return new RGB(0, 0, byte.MaxValue); }
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
