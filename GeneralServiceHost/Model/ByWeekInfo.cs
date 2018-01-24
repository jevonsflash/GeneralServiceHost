using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ByWeekInfo:ViewModelBase
    {
        public ByWeekInfo()
        {
            Time = DateTime.Now;
            this.Value = 1;
            this.Dayofweek = DayOfWeek.Monday;
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
    }
}
