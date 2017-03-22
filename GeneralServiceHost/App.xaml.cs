using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FluentScheduler;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Model;

namespace GeneralServiceHost
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs");
            var dir2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            DirFileHelper.CreateDir(dir);
            DirFileHelper.CreateDir(dir2);
            
            
        }
    }
}
