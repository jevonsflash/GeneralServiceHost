using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FluentScheduler;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;

namespace GeneralServiceHost.Model
{
    public class JobInfo : ViewModelBase
    {

        public JobInfo()
        {
            this.Status = JobStatusType.Unspecified;
            this.SbLog = new ObservableCollection<string>();
        }


        private JobStatusType _status;

        public JobStatusType Status
        {
            get { return _status; }
            set {
                _status = value;
                RaisePropertyChanged(nameof(Status));
            }
        }


        private string _name;
        public string Name
        {
            get
            {
                return _name;


            }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private DateTime _lastRun;
        public DateTime LastRun
        {
            get
            {
                return _lastRun;

            }
            set
            {
                _nextRun = value;
                RaisePropertyChanged(nameof(LastRun));
            }
        }

        private DateTime _nextRun;
        public DateTime NextRun
        {
            get
            {
                return _nextRun;

            }
            set
            {
                _nextRun = value;
                RaisePropertyChanged(nameof(NextRun));
            }
        }

        private int _runCount;

        public int RunCount
        {
            get { return _runCount; }
            set {
                _runCount = value;
                RaisePropertyChanged(nameof(RunCount));
            }
        }

        private int _successCount;

        public int SucessCount
        {
            get { return _successCount; }
            set
            {
                _successCount = value;
                RaisePropertyChanged(nameof(SucessCount));
            }
        }

        private int _errorCount;

        public int ErrorCount
        {
            get { return _errorCount; }
            set
            {
                _errorCount = value;
                RaisePropertyChanged(nameof(ErrorCount));
            }
        }


        private ObservableCollection<string> _sbLog;

        [JsonIgnore]
        public ObservableCollection<string> SbLog
        {
            get
            {
                return _sbLog;
            }
            set
            {
                _sbLog = value;
                RaisePropertyChanged(nameof(SbLog));
            }
        }

        private ScheduleInfo _scheduleInfo;
        public ScheduleInfo ScheduleInfo
        {
            get { return _scheduleInfo; }
            set
            {
                _scheduleInfo = value;
                RaisePropertyChanged(nameof(ScheduleInfo));
            }
        }      

    }
}
