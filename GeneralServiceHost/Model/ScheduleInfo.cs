using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneralServiceHost.Model
{
    public class ScheduleInfo : ViewModelBase
    {

        public ScheduleInfo()
        {
            this.Mode = ScheduleMode.周期任务;
            this.IsGuard = false;
            this.ByMinute = new ByMinuteInfo();
            this.ByHour = new ByHourInfo();
            this.ByDay = new ByDayInfo();
            this.ByWeek = new ByWeekInfo();
            this.ByMonth = new ByMonthInfo();
            this.BySpecified = new BySpecifiedInfo();
            this.Type = ScheduleType.Unspecified;

        }
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private bool _isToRunNow;
        public bool IsToRunNow
        {
            get { return _isToRunNow; }
            set
            {
                _isToRunNow = value;
                OnPropertyChanged(nameof(IsToRunNow));
            }
        }

        private ScheduleType _type;

        public ScheduleType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        private ByMonthInfo _byMonth;

        public ByMonthInfo ByMonth
        {
            get { return _byMonth; }
            set
            {
                _byMonth = value;
                OnPropertyChanged();
            }
        }

        private ByWeekInfo _byWeek;

        public ByWeekInfo ByWeek
        {
            get { return _byWeek; }
            set
            {
                _byWeek = value;
                OnPropertyChanged();

            }
        }

        private ByDayInfo _byDay;

        public ByDayInfo ByDay
        {
            get { return _byDay; }
            set
            {
                _byDay = value;
                OnPropertyChanged();
            }
        }

        private ByHourInfo _byHour;

        public ByHourInfo ByHour
        {
            get { return _byHour; }
            set
            {
                _byHour = value;

                OnPropertyChanged();
            }
        }

        private ByMinuteInfo _byMinute;

        public ByMinuteInfo ByMinute
        {
            get { return _byMinute; }
            set
            {
                _byMinute = value;
                OnPropertyChanged();

            }
        }

        private BySpecifiedInfo _bySpecified;

        public BySpecifiedInfo BySpecified
        {
            get { return _bySpecified; }
            set
            {
                _bySpecified = value;
                OnPropertyChanged();

            }
        }



        private int _delayFor;

        public int DelayFor
        {
            get { return _delayFor; }
            set
            {
                _delayFor = value;
                OnPropertyChanged();
            }
        }

        private ScheduleMode _mode;

        public ScheduleMode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                OnPropertyChanged();


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
                OnPropertyChanged(nameof(IsGuard));
            }
        }

        private string _asmPath;

        public string AsmPath
        {
            get { return _asmPath; }
            set
            {
                _asmPath = value;
                OnPropertyChanged(nameof(AsmPath));
            }
        }
        private string _asmName;
        public string AsmName
        {
            get { return _asmName; }
            set
            {
                _asmName = value;
                OnPropertyChanged(nameof(AsmName));
            }
        }

    }


}
