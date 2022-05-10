using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeneralServiceHost.Model
{
    public class BySpecifiedInfo:ViewModelBase
    {
        public BySpecifiedInfo()
        {
            Time = DateTime.Now;

        }

        private DateTime _time;

        public DateTime Time
        {
            get { return _time; }
            set
            {
                _time = value;
                OnPropertyChanged(nameof(Time));
            }
        }
    }
}
