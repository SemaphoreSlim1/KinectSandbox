using KinectSandbox.Common.Colors;
using System;

namespace KinectSandbox.ColorMapping
{
    public interface IColorMap
    {
        void Init(UInt16 minReliable, UInt16 maxReliable);

        RGB GetColorForDepth(int x, int y, UInt16 depth);
    }
}
