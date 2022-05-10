﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentScheduler;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Manager;
using GeneralServiceHost.Model;
using Microsoft.Win32;

namespace GeneralServiceHost.ViewModel
{
    public class IndexPageViewModel : ViewModelBase
    {
        public IndexPageViewModel()
        {
            this.RefreshCommand = new RelayCommand(RefreshAction);
            this.StartCommand = new RelayCommand<string>(StartAction);
            this.AbortCommand = new RelayCommand<string>(AbortAction);
            this.RemoveCommand = new RelayCommand<string>(RemoveAction);
            this.PropertyChanged += IndexPageViewModel_PropertyChanged;
            DataManager.Current.ReadFinishedEvent += Current_ReadFinishedEvent;
            DataManager.Current.Read();

        }


        private async void IndexPageViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(this.SelectedJobInfo) && this.SelectedJobInfo != null)
            {
                if (this.SelectedJobInfo.SbLog.Count == 0)
                {
                    var fileName = SelectedJobInfo.Name + ".log";

                    var str = await OutputManager.ReadOutput(fileName);

                    if (!string.IsNullOrEmpty(str))
                    {
                        var strList = str.Split('\n');

                        this.SelectedJobInfo.SbLog = new ObservableCollection<string>(strList);
                    }


                }
            }
        }

        private void Current_ReadFinishedEvent(object sender, EventArgs e)
        {
            JobInfoManager.Refresh();
        }

        private void RemoveAction(string obj)
        {
            if (this.SelectedJobInfo != null)
            {
                
                JobInfoManager.Dorp(SelectedJobInfo.Name);

            }
        }

        private void RefreshAction()
        {
            JobInfoManager.Refresh();
        }

        private void AbortAction(string name)
        {
            if (this.SelectedJobInfo != null)
            {
                JobInfoManager.Abort(SelectedJobInfo.Name);

            }
        }

        private void StartAction(string name)
        {
            if (this.SelectedJobInfo != null)
            {
                JobInfoManager.Start(SelectedJobInfo.Name);

            }
        }

        private JobInfo _selectedJobInfo;

        public JobInfo SelectedJobInfo
        {
            get
            {
                return _selectedJobInfo;
            }
            set
            {
                _selectedJobInfo = value;
                OnPropertyChanged(nameof(SelectedJobInfo));
            }
        }

        private Dictionary<string,string> _statusTypes;

        public Dictionary<string, string> StatusTypes
        {
            get
            {
                var result = new Dictionary<string, string>
                {
                    { "过期", "Gray" },
                    { "挂起", "Gold" },
                    { "正在执行", "Green" },
                    { "停止", "Red" },
                    { "未指定", "Purple" }
                };

                return result;
            }
           
        }


        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand<string> StartCommand { get; set; }
        public RelayCommand<string> AbortCommand { get; set; }
        public RelayCommand<string> RemoveCommand { get; set; }


    }
}
