using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.DependencyInjection;
using FluentScheduler;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Model;
using GeneralServiceHost.View;
using GeneralServiceHost.ViewModel;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralServiceHost
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private bool _initialized;
        public App()
        {
            if (!_initialized)
            {
                _initialized = true;
                Ioc.Default.ConfigureServices(
                    new ServiceCollection()
                    //ViewModels
                    .AddSingleton<IndexPageViewModel>()
                    .AddSingleton<AddJobWindowViewModel>()
                    .AddScoped<AddJobWindow>()
                    .BuildServiceProvider());
          


            var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs");
            var dir2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            var dirLog = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            App.Current.Startup += Current_Startup;
            App.Current.Exit += Current_Exit;
            DirFileHelper.CreateDir(dir);
            DirFileHelper.CreateDir(dir2);
            DirFileHelper.CreateDir(dirLog);
            }

        }


        private void Current_Exit(object sender, ExitEventArgs e)
        {
            LogHelper.ExitThread();

        }

        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                LogHelper.LogError("UI线程全局异常" + e.Exception);
                MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogError("不可恢复的UI线程全局异常" + ex);
                MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "不可恢复的UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var exception = e.ExceptionObject as Exception;
                if (exception != null)
                {
                    LogHelper.LogError("非UI线程全局异常" + exception);
                    MessageBox.Show("An unhandled exception just occurred: " + exception.Message, "非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError("不可恢复的非UI线程全局异常" + ex);
                MessageBox.Show("An unhandled exception just occurred: " + ex.Message, "不可恢复的非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void Current_Startup(object sender, StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            LogHelper.LogFlag = true;
        }

    }
}
