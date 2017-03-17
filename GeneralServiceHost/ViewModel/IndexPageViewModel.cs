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
using ServiceJob;

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
            this.JobInfos = new ObservableCollection<JobInfo>(DataManager.Current.JobInfos);
        }

        private ObservableCollection<JobInfo> _jobInfos;

        public ObservableCollection<JobInfo> JobInfos
        {
            get
            {
                if (_jobInfos == null || _jobInfos.Count == 0)
                {
                    this._jobInfos = new ObservableCollection<JobInfo>(DataManager.Current.JobInfos);
                }
                return _jobInfos;
            }
            set
            {
                _jobInfos = value;

                RaisePropertyChanged(nameof(JobInfos));
            }
        }

        public RelayCommand RefreshCommand { get; set; }


    }
}
