using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            JobManager.JobEnd += JobManager_JobEnd;
            this.RefreshCommand = new RelayCommand(RefreshAction);
            this.StartCommand = new RelayCommand<string>(StartAction);
            this.AbortCommand = new RelayCommand<string>(AbortAction);

        }

        private void JobManager_JobEnd(JobEndInfo obj)
        {

            var job = DataManager.Current.JobInfos.First(c => c.Name == obj.Name);
            if (job != null)
            {
                job.Obsolete = true;
            }

        }

        private void AbortAction(string name)
        {
            if (this.SelectedJobInfo != null)
            {
                if (!DataManager.Current.JobInfos.First(c => c.Name == SelectedJobInfo.Name).Obsolete)
                {
                    JobManager.GetSchedule(SelectedJobInfo.Name).Disable();

                }
                RefreshAction();
            }
        }

        private void StartAction(string name)
        {
            if (this.SelectedJobInfo != null)
            {
                if (!DataManager.Current.JobInfos.First(c => c.Name == SelectedJobInfo.Name).Obsolete)
                {
                    JobManager.GetSchedule(SelectedJobInfo.Name).Enable();

                }

                RefreshAction();
            }
        }

        private void RefreshAction()
        {
            foreach (var schedule in DataManager.Current.JobInfos)
            {

                var c = JobManager.AllSchedules.FirstOrDefault(d => d.Name == schedule.Name);
                if (c != null)
                {
                    schedule.Name = c.Name;
                    schedule.NextRun = c.NextRun;
                    schedule.Disabled = c.Disabled;
                }
                else
                {
                    schedule.Obsolete = true;
                }
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
