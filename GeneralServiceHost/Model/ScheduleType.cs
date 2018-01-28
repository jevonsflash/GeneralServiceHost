using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Model
{
    public enum ScheduleType
    {
        Unspecified, //未指定
        Minute,//按分钟
        Hour,//按小时
        Day,//按天
        Week,//按星期
        Month,//按月
        Specified//指定时间
    }
}
