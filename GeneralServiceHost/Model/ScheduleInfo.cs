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


        private TimeSpan _time;

        public TimeSpan Time
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
            get { return Time.Hours; }
        }
        public int Minute
        {
            get { return Time.Minutes; }
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
    }


}
