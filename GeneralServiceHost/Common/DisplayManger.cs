using GeneralServiceHost.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Common
{
    public class DisplayManger
    {
        public static DayOfWeek IntToDayOfWeek(int value)
        {
            var result = DayOfWeek.Monday;
            switch (value)
            {
                case 1:
                    result = DayOfWeek.Monday;
                    break;
                case 2:
                    result = DayOfWeek.Tuesday;
                    break;
                case 3:
                    result = DayOfWeek.Wednesday;
                    break;
                case 4:
                    result = DayOfWeek.Thursday;
                    break;
                case 5:
                    result = DayOfWeek.Friday;
                    break;
                case 6:
                    result = DayOfWeek.Saturday;
                    break;
                case 7:
                    result = DayOfWeek.Sunday;
                    break;
                default:
                    break;
            }
            return result;
        }

        public static string DayOfWeekToStr(DayOfWeek value)
        {

            var result = string.Empty;
            var dayOfWeek = (DayOfWeek)value;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = "星期日";
                    break;
                case DayOfWeek.Monday:
                    result = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    result = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    result = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    result = "星期四";
                    break;
                case DayOfWeek.Friday:
                    result = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    result = "星期六";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        public static string ScheduleTypeToStr(ScheduleType value)
        {

            var result = string.Empty;
            var scheduleType = (ScheduleType)value;
            switch (scheduleType)
            {
                case ScheduleType.Hour:
                    result = "按小时";
                    break;
                case ScheduleType.Day:
                    result = "按天";
                    break;
                case ScheduleType.Month:
                    result = "按月";
                    break;
                case ScheduleType.Week:
                    result = "按星期";
                    break;
                case ScheduleType.Minute:
                    result = "按分";
                    break;
                case ScheduleType.Specified:
                    result = "按计划时间";
                    break;
            }

            return result;
        }

    }
}
