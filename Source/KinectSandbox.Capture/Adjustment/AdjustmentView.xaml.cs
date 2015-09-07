using System.Windows.Controls;

namespace KinectSandbox.Capture.Adjustment
{
    /// <summary>
    /// Interaction logic for AdjustmentView.xaml
    /// </summary>
    public partial class AdjustmentView : UserControl
    {
        public AdjustmentView(IAdjustmentViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
