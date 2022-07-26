﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using CommunityToolkit.Mvvm.DependencyInjection;
using FluentScheduler;
using GeneralServiceHost.Manager;
using GeneralServiceHost.Model;
using GeneralServiceHost.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Schedule = FluentScheduler.Schedule;

namespace GeneralServiceHost.View
{
    /// <summary>
    /// IndexPage.xaml 的交互逻辑
    /// </summary>
    public partial class IndexPage : Page
    {
        public IndexPage()
        {
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            IServiceScopeFactory _serviceScopeFactory = Ioc.Default.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                var addJobWindow = scope.ServiceProvider.GetRequiredService<AddJobWindow>();
                Ioc.Default.GetRequiredService<AddJobWindowViewModel>().Scope=scope;
                addJobWindow.ShowDialog();
            }


        }

        private void FrameworkElement_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var cuurentJob = ((JobInfo)e.NewValue);
            if (cuurentJob != null)
            {
                var current = cuurentJob.ScheduleInfo.Type.ToString() + "BoardTemplate";
                try
                {
                    this.ScheduleBoard.Template = FindResource(current) as ControlTemplate;

                }
                catch (Exception exception)
                {
                    this.ScheduleBoard.Template = null;
                }

            }

        }

        private void OpenFolder_OnClick(object sender, RoutedEventArgs e)
        {
            string outputsPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
            Process.Start(outputsPath);
        }
    }
}
