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
using CommunityToolkit.Mvvm.Messaging;
using GeneralServiceHost.Common;
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
            WeakReferenceMessenger.Default.Register<string>(MessengerToken.CLOSEWINDOW, HandleMessage);


            Closed += AddJobWindow_Closed;
        }

        private void HandleMessage(object recipient, string obj)
        {
            this.Close();
        }

        private void AddJobWindow_Closed(object sender, EventArgs e)
        {
            ViewModelLocator.Cleanup<AddJobWindowViewModel>();
        }

        private void ComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            var current = (sender as ComboBox).SelectedItem.ToString() + "Template";
            if (this.CC != null)
            {
                try
                {
                    this.CC.Template = FindResource(current) as ControlTemplate;

                }
                catch (Exception)
                {

                    this.CC.Template = null;
                }

            }

        }

    }
}
