using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeneralServiceHost.View;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace GeneralServiceHost
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private NotifyIcon notifier = new NotifyIcon();
        private bool _forceClose;

        public MainWindow()
        {
            InitializeComponent();
            this.notifier.MouseDown +=Notifier_MouseDown; ;
            using (var fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("GeneralServiceHost.Assets.schedule.ico"))
            {
                var icon = new System.Drawing.Icon(fs);
                this.notifier.Icon =  icon;
                this.Icon=Icon;

            }
            this.notifier.Visible = true;
        }

        private void Notifier_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                ContextMenu menu = (ContextMenu)this.FindResource("TaskBarTrayContextMenu");

                menu.IsOpen = true;

            }
            else if (e.Button == MouseButtons.Left)
            {
                this.ShowInTaskbar=true;
                this.Show();
                this.Activate();

            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_forceClose)
            {
                return;
            }
            MessageBoxResult result = System.Windows.MessageBox.Show("是否退出GSH？否则最小化到任务栏，取消则返回。", "提示", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
                this.ShowInTaskbar=false;
                this.Hide();
            }
        }

        private void MetroWindow_Closed(object sender, EventArgs e)
        {
            this.notifier.Dispose();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this._forceClose=true;
            App.Current.Shutdown();

        }
    }
}
