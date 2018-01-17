using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ProcessResult
    {
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
    }
}
