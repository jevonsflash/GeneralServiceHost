using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Model;
using Newtonsoft.Json;

namespace GeneralServiceHost.Manager
{
    public class DataManager : ViewModelBase
    {
        public event EventHandler ReadFinishedEvent;

        static string jobsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs.txt");

        public DataManager()
        {
            JobInfos = new ObservableCollection<JobInfo>();
            JobInfos.CollectionChanged += JobInfos_CollectionChanged;  
            
            
        }

        private async void ReadJobs()
        {
            var result = await Task.Run(() =>
            {
                DirFileHelper.ExistsFile(jobsFile);
                var jsonInfos = DirFileHelper.ReadFile(jobsFile);
                if (jsonInfos!=null)
                {
                    var jobInfoList = JsonConvert.DeserializeObject<List<JobInfo>>(jsonInfos);
                    return jobInfoList;
                }
                else
                {
                    return new List<JobInfo>();
                }
                
            });

            JobInfos = new ObservableCollection<JobInfo>(result);
            JobInfos.CollectionChanged += JobInfos_CollectionChanged;
            ReadFinishedEvent.Invoke(this,EventArgs.Empty);
        }

        private async void SaveJobs()
        {
            var jobInfoList = this.JobInfos.ToList();
            await Task.Run(() =>
            {
                var jsonJobs = JsonConvert.SerializeObject(jobInfoList);
                DirFileHelper.WriteText(jobsFile, jsonJobs);
            });


        }

        public void Save()
        {
            SaveJobs();
        }

        public void Read()
        {
            ReadJobs();
        }

        private void JobInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            //if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
            //{
            //    SaveJobs();
            //}


        }

        private static DataManager _current;

        public static DataManager Current
        {
            get
            {
                if (_current == null)

                {
                    _current = new DataManager();
                }
                return _current;
            }
        }

        private ObservableCollection<JobInfo> _jobInfos;
        public ObservableCollection<JobInfo> JobInfos
        {
            get
            {

                return _jobInfos;
            }

            set
            {
                _jobInfos = value;

                base.RaisePropertyChanged(nameof(JobInfos));
            }
        }


    }
}
