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

            NonReentrantAsDefault();
        }

        public void SetAndRegistryGeneralService<T>(ScheduleInfo scheduleInfo) where T : IJob
        {

            if (scheduleInfo.IsToRunNow)
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
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
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule<T>().NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }

            }
        }

        public void SetAndRegistryDelayService(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {

            switch (scheduleInfo.Type)
            {

                case ScheduleType.Minute:
                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunOnceIn(scheduleInfo.Value).Minutes();
                    break;
                case ScheduleType.Day:
                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunOnceIn(scheduleInfo.Value).Days();

                    break;
                case ScheduleType.Hour:
                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunOnceIn(scheduleInfo.Value).Hours();

                    break;
                case ScheduleType.Week:
                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunOnceIn(scheduleInfo.Value).Weeks();

                    break;
                case ScheduleType.Month:
                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunOnceIn(scheduleInfo.Value).Months();

                    break;
                default:
                    break;
            }

        }

        public void SetAndRegistryGeneralService(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {

            if (scheduleInfo.IsToRunNow)
            {
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Minute:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Minutes();
                        break;
                    case ScheduleType.Day:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunNow().AndEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
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
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Days().At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Hour:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Hours().At(scheduleInfo.Minute);
                        break;
                    case ScheduleType.Week:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Weeks().On(CastDayOfWeek(scheduleInfo.Value)).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    case ScheduleType.Month:
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(scheduleInfo.Name).ToRunEvery(1).Months().On(scheduleInfo.Value).At(scheduleInfo.Hour, scheduleInfo.Minute);
                        break;
                    default:
                        break;
                }

            }
        }

        private static Action Job(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {
            return () =>
            {
                action(scheduleInfo);
            };
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
