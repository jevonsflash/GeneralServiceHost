using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ByHourInfo:ViewModelBase
    {
        public ByHourInfo()
        {
            this.Value = 1;
            this.OnMinute = 59;
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

        private int _onMinute;

        public int OnMinute
        {
            get { return _onMinute; }
            set
            {
                _onMinute = value;
                RaisePropertyChanged();
            }
        }

        public Dictionary<int, string> MinuteList
        {
            get
            {
                var result = new Dictionary<int, string>();
                for (int i = 0; i <= 59; i++)
                {
                    var current = i.ToString();
                    result.Add(i, current);
                }
                return result;
            }
        }

    }
}
