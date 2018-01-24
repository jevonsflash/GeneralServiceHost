using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ByMonthInfo : ViewModelBase
    {
        public ByMonthInfo()
        {
            Time = DateTime.Now;
            this.Value = 1;
            this.OnDay = 1;
            this.Dayofweek = DayOfWeek.Monday;
            this.WeekOfMonth = WeekOfMonthType.第一个星期;

        }
        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(nameof(Value));
            }
        }


        private bool _isMonthByweek;

        public bool IsMonthByweek
        {
            get { return _isMonthByweek; }
            set
            {
                _isMonthByweek = value;
                RaisePropertyChanged();

            }
        }
        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }
        public int Hour
        {
            get { return Time.Hour; }
        }
        public int Minute
        {
            get { return Time.Minute; }
        }

        private DayOfWeek _dayofweek;

        public DayOfWeek Dayofweek
        {
            get { return _dayofweek; }
            set
            {
                _dayofweek = value;
                RaisePropertyChanged();
            }
        }

        private WeekOfMonthType _weekOfMonth;

        public WeekOfMonthType WeekOfMonth
        {
            get { return _weekOfMonth; }
            set
            {
                _weekOfMonth = value;

                RaisePropertyChanged();

            }
        }


        private int _onDay;

        public int OnDay
        {
            get { return _onDay; }
            set
            {
                _onDay = value;
                RaisePropertyChanged();
            }
        }

        public Dictionary<int, string> DayList
        {
            get
            {
                var result = new Dictionary<int, string>();
                for (int i = 1; i <= 31; i++)
                {
                    var current = i + "日";
                    result.Add(i, current);
                }
                result.Add(-1, "最后一天");
                return result;
            }
        }

    }
}
