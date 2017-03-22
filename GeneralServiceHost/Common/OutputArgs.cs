using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Common
{
    public class OutputArgs : EventArgs
    {
        public OutputArgs(string outputContent, string jobName)
        {
            this.OutputContent = outputContent;
            this.JobName = jobName;
        }
        public string OutputContent { get; set; }
        public string JobName { get; set; }
    }
}
