using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace GeneralServiceHost.Model
{
    public class JobInfo
    {
        private List<string> _logs;

        public List<string> Logs
        {
            get
            {
                if (_logs == null)
                {
                    _logs = new List<string>();
                }
                return _logs;
            }
            set { _logs = value; }
        }


        public Schedule Schedule { get; set; }
    }
}
