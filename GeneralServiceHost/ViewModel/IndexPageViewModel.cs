using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FluentScheduler;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
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
            DataManager.Current.ReadFinishedEvent += Current_ReadFinishedEvent;
            DataManager.Current.Read();
            
        }

        private void Current_ReadFinishedEvent(object sender, EventArgs e)
        {
            JobInfoManager.Refresh();
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
                RaisePropertyChanged(nameof(SelectedJobInfo));
            }
        }


        public RelayCommand RefreshCommand { get; set; }
        public RelayCommand<string> StartCommand { get; set; }
        public RelayCommand<string> AbortCommand { get; set; }


    }
}
