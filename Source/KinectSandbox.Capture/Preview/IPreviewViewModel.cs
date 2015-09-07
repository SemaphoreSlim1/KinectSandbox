using System.Windows.Media;

namespace KinectSandbox.Capture.Preview
{
    public interface IPreviewViewModel
    {
        ImageSource ImageSource { get; }
        int Skew { get; }
    }
}
