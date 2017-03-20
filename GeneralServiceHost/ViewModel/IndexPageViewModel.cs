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

        }

        private void RefreshAction()
        {
            DataManager.Current.JobInfos = new ObservableCollection<JobInfo>(JobManager.AllSchedules.Select(c => new JobInfo()
            {
                Name = c.Name,
                NextRun = c.NextRun,
                Disabled = c.Disabled
            }));
        }



        public RelayCommand RefreshCommand { get; set; }


    }
}
