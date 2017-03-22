using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GeneralServiceHost.Model;

namespace GeneralServiceHost.Manager
{
    public class DataManager : ViewModelBase
    {
        public DataManager()
        {
            JobInfos.CollectionChanged += JobInfos_CollectionChanged;
        }

        private void JobInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var aa = e.NewItems;
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
                if (_jobInfos == null)
                {
                    _jobInfos = new ObservableCollection<JobInfo>();
                }

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
