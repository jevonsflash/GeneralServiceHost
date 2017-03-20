using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;

namespace GeneralServiceHost.Model
{
    public class JobInfo
    {

        public JobInfo()
        {
        }
        //
        // 摘要:
        //     Flag indicating if this job schedule is disabled.
        public bool Disabled { get; set; }
        //
        // 摘要:
        //     Name of this job schedule.
        public string Name { get; set; }
        //
        // 摘要:
        //     Date and time of the next run of this job schedule.
        public DateTime NextRun { get; set; }

        public string Logs { get; set; }

        public ScheduleInfo ScheduleInfo { get; set; }
    }
}
