using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FluentScheduler;
using GeneralServiceHost.Model;
using GeneralServiceHost.View;

namespace GeneralServiceHost.Common
{
    class GeneralServiceRegistry : Registry
    {

        public GeneralServiceRegistry()
        {


        }

        public void SetAndRegistryGeneralService<T>(ScheduleInfo scheduleInfo) where T : IJob
        {
            if (scheduleInfo.IsToRunNow)
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule<T>().WithName(scheduleInfo.Name).ToRunEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }

            }
        }

        public void TestGeneralService(ScheduleInfo scheduleInfo, Action action)
        {

            Schedule(action).WithName("Test").ToRunOnceIn(scheduleInfo.Value).Seconds();

        }

        public void SetAndRegistryGeneralService(ScheduleInfo scheduleInfo, Action action)
        {
            if (scheduleInfo.IsToRunNow)
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule(action).WithName(scheduleInfo.Name).ToRunEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }

            }
        }


        private DayOfWeek CastDayOfWeek(int value)
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


    }

}
