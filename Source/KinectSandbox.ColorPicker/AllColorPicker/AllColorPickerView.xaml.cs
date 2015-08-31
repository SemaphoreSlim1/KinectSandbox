using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectSandbox.ColorPicker.AllColorPicker
{
    /// <summary>
    /// Interaction logic for AllColorPickerView.xaml
    /// </summary>
    public partial class AllColorPickerView : UserControl
    {
        public AllColorPickerView(IAllColorPickerViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
