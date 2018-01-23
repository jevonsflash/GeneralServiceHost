using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace GeneralServiceHost.Model
{
    public class ScheduleInfo : ViewModelBase
    {

        public ScheduleInfo()
        {
            this.IsGeneralJob = true;
            this.IsGuard = false;
            this.ByMinute = new ByMinuteInfo();
            this.ByHour = new ByHourInfo();
            this.ByDay = new ByDayInfo();
            this.ByWeek = new ByWeekInfo();
            this.ByMonth = new ByMonthInfo();
            this.BySpecified=new BySpecifiedInfo();
            
        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private bool _isToRunNow;
        public bool IsToRunNow
        {
            get { return _isToRunNow; }
            set
            {
                _isToRunNow = value;
                RaisePropertyChanged(nameof(IsToRunNow));
            }
        }

        private ScheduleType _type;

        public ScheduleType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }

        private ByMonthInfo _byMonth;

        public ByMonthInfo ByMonth
        {
            get { return _byMonth; }
            set
            {
                _byMonth = value;
                RaisePropertyChanged();
            }
        }

        private ByWeekInfo _byWeek;

        public ByWeekInfo ByWeek
        {
            get { return _byWeek; }
            set
            {
                _byWeek = value;
                RaisePropertyChanged();

            }
        }

        private ByDayInfo _byDay;

        public ByDayInfo ByDay
        {
            get { return _byDay; }
            set
            {
                _byDay = value;
                RaisePropertyChanged();
            }
        }

        private ByHourInfo _byHour;

        public ByHourInfo ByHour
        {
            get { return _byHour; }
            set
            {
                _byHour = value;

                RaisePropertyChanged();
            }
        }

        private ByMinuteInfo _byMinute;

        public ByMinuteInfo ByMinute
        {
            get { return _byMinute; }
            set
            {
                _byMinute = value;
                RaisePropertyChanged();

            }
        }

        private BySpecifiedInfo _bySpecified;

        public BySpecifiedInfo BySpecified
        {
            get { return _bySpecified; }
            set
            {
                _bySpecified = value;
                RaisePropertyChanged();

            }
        }



        private int _delayFor;

        public int DelayFor
        {
            get { return _delayFor; }
            set
            {
                _delayFor = value;
                RaisePropertyChanged();
            }
        }

        private bool _isGeneralJob;
        public bool IsGeneralJob
        {
            get
            {
                return _isGeneralJob;

            }
            set
            {
                _isGeneralJob = value;
                RaisePropertyChanged(nameof(IsGeneralJob));
            }
        }

        private bool _isGuard;
        public bool IsGuard
        {
            get
            {
                return _isGuard;

            }
            set
            {
                _isGuard = value;
                RaisePropertyChanged(nameof(IsGuard));
            }
        }

        private string _asmPath;

        public string AsmPath
        {
            get { return _asmPath; }
            set
            {
                _asmPath = value;
                RaisePropertyChanged(nameof(AsmPath));
            }
        }
        private string _asmName;
        public string AsmName
        {
            get { return _asmName; }
            set
            {
                _asmName = value;
                RaisePropertyChanged(nameof(AsmName));
            }
        }

    }


}
