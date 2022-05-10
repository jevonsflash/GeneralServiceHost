
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ByMinuteInfo: ViewModelBase
    {
        public ByMinuteInfo()
        {
            this.Value = 1;
        }

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
    }
}
