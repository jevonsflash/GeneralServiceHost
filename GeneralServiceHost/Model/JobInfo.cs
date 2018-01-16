using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using FluentScheduler;
using GalaSoft.MvvmLight;

namespace GeneralServiceHost.Model
{
    public class JobInfo : ViewModelBase
    {

        public JobInfo()
        {
            this.Obsolete = false;
            this.SbLog = new StringBuilder();
        }
        //
        // 摘要:
        //     Flag indicating if this job schedule is disabled.
        private bool _disabled;
        public bool Disabled
        {
            get { return _disabled; }
            set
            {
                _disabled = value;
                RaisePropertyChanged(nameof(Disabled));
            }
        }
        //
        // 摘要:
        //     Name of this job schedule.

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

      
        //
        // 摘要:
        //     Date and time of the next run of this job schedule.
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

        private StringBuilder _sbLog;
        public StringBuilder SbLog
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

        private bool _obsolete;
        public bool Obsolete
        {
            get { return _obsolete; }
            set
            {
                _obsolete = value;
                RaisePropertyChanged(nameof(Obsolete));
            }
        }

    }
}
