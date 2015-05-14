using KinectSandbox.ViewModel;
using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace KinectSandbox.View
{
    /// <summary>
    /// Interaction logic for ShellView.xaml
    /// </summary>
    public partial class ShellView : MetroWindow, IShellView
    {
        protected IShellViewModel ViewModel
        {
            get { return this.DataContext as IShellViewModel; }
        }

        public ShellView(IShellViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
            this.StateChanged += ShellView_StateChanged;
        }

        void ShellView_StateChanged(object sender, EventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                ViewModel.EnterFullScreen();
            }
            else
            {
                ViewModel.ExitFullScreen();
            }
        }
    }
}
