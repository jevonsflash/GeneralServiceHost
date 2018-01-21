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
using GeneralServiceHost.ViewModel;
using MahApps.Metro.Controls;

namespace GeneralServiceHost.View
{
    /// <summary>
    /// AddJobWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddJobWindow : MetroWindow
    {
        public AddJobWindow()
        {
            InitializeComponent();
            this.DataContext=new AddJobWindowViewModel();
        }

    }
}
