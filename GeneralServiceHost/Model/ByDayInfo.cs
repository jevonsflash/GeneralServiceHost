using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ByDayInfo:ViewModelBase
    {
        public ByDayInfo()
        {
            Time = DateTime.Now;
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

    }
}
