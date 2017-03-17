using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralServiceHost.Model;

namespace GeneralServiceHost.Manager
{
    public class DataManager
    {
        private static DataManager _current;

        public static DataManager Current
        {
            get
            {
                if (_current == null)

                {
                    _current = new DataManager();
                }
                return _current;
            }
        }

        private List<JobInfo> _jobInfos;
        public List<JobInfo> JobInfos
        {
            get
            {
                if (_jobInfos == null)
                {
                    _jobInfos = new List<JobInfo>();
                }

                return _jobInfos;
            }

            set { _jobInfos = value; }
        }

    }
}
