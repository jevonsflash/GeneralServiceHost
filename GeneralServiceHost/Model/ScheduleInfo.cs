using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public class ScheduleInfo
    {
        public string Name { get; set; }

        public bool IsToRunNow { get; set; }

        public ScheduleType Type { get; set; }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public int Value { get; set; }
    }

    public enum ScheduleType
    {
        Hour, Day, Month, Week
    }
}
