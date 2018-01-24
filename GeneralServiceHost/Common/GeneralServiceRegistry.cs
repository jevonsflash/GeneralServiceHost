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

        public void SetAndRegistryGeneralService(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {
            var isToRunNow = scheduleInfo.IsToRunNow;
            var name = scheduleInfo.Name;
            switch (scheduleInfo.Type)
            {
                case ScheduleType.Day:
                    var bydaycontext = scheduleInfo.ByDay;

                    if (bydaycontext != null && !string.IsNullOrEmpty(name))
                    {

                        if (isToRunNow)
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow()
                                .AndEvery(bydaycontext.Value).Days().At(bydaycontext.Hour, bydaycontext.Minute);

                        }
                        else
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunEvery(bydaycontext.Value).Days().At(bydaycontext.Hour, bydaycontext.Minute);

                        }

                    }
                    break;
                case ScheduleType.Hour:
                    var byhourcontext = scheduleInfo.ByDay;

                    if (byhourcontext != null && !string.IsNullOrEmpty(name))
                    {
                        if (isToRunNow)
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow()
                                .AndEvery(byhourcontext.Value).Hours().At(byhourcontext.Minute);

                        }
                        else
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunEvery(byhourcontext.Value).Hours().At(byhourcontext.Minute);

                        }
                    }

                    break;
                case ScheduleType.Week:
                    var byweekcontext = scheduleInfo.ByWeek;

                    if (byweekcontext != null && !string.IsNullOrEmpty(name))
                    {
                        if (isToRunNow)
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow()
                                .AndEvery(byweekcontext.Value).Weeks()
                                .On(byweekcontext.Dayofweek).At(byweekcontext.Hour, byweekcontext.Minute);


                        }
                        else
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunEvery(byweekcontext.Value).Weeks()
                                .On(byweekcontext.Dayofweek).At(byweekcontext.Hour, byweekcontext.Minute);

                        }
                    }

                    break;
                case ScheduleType.Month:
                    var bymonthcontext = scheduleInfo.ByMonth;
                    if (bymonthcontext != null && !string.IsNullOrEmpty(name))
                    {
                        if (bymonthcontext.IsMonthByweek)
                        {
                            switch (bymonthcontext.WeekOfMonth)
                            {
                                case WeekOfMonthType.第一个星期:
                                    if (isToRunNow)
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheFirst(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    else
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunEvery(bymonthcontext.Value).Months().OnTheFirst(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    break;
                                case WeekOfMonthType.第二个星期:
                                    if (isToRunNow)
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheSecond(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    else
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunEvery(bymonthcontext.Value).Months().OnTheSecond(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    break;
                                case WeekOfMonthType.第三个星期:
                                    if (isToRunNow)
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheThird(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    else
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunEvery(bymonthcontext.Value).Months().OnTheThird(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    break;
                                case WeekOfMonthType.第四个星期:
                                    if (isToRunNow)
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheFourth(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    else
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunEvery(bymonthcontext.Value).Months().OnTheFourth(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    break;
                                case WeekOfMonthType.最后一个星期:
                                    if (isToRunNow)
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheLast(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    else
                                    {
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunEvery(bymonthcontext.Value).Months().OnTheLast(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                    }
                                    break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }

                        }
                        else
                        {
                            if (bymonthcontext.OnDay > 0)
                            {
                                Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().On(bymonthcontext.OnDay).At(bymonthcontext.Hour, bymonthcontext.Minute);

                            }
                            else
                            {
                                Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow().AndEvery(bymonthcontext.Value).Months().OnTheLastDay().At(bymonthcontext.Hour, bymonthcontext.Minute);

                            }


                        }

                    }
                    break;

                case ScheduleType.Minute:
                    var byminutecontext = scheduleInfo.ByMinute;

                    if (byminutecontext != null && !string.IsNullOrEmpty(name))
                    {
                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunNow()
                            .AndEvery(byminutecontext.Value).Minutes();
                    }
                    break;
                default:
                    break;

            }
        }

        public void SetAndRegistryDelayService(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {
                var name = scheduleInfo.Name;
                switch (scheduleInfo.Type)
                {
                    case ScheduleType.Day:
                        var bydaycontext = scheduleInfo.ByDay;

                        if (bydaycontext != null && !string.IsNullOrEmpty(name))
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunOnceIn(bydaycontext.Value).Days().At(bydaycontext.Hour, bydaycontext.Minute);
                        }
                        break;
                    case ScheduleType.Hour:
                        var byhourcontext = scheduleInfo.ByDay;

                        if (byhourcontext != null && !string.IsNullOrEmpty(name))
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunOnceIn(byhourcontext.Value).Hours().At(byhourcontext.Minute);
                        }

                        break;
                    case ScheduleType.Week:
                        var byweekcontext = scheduleInfo.ByWeek;

                        if (byweekcontext != null && !string.IsNullOrEmpty(name))
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunOnceIn(byweekcontext.Value).Weeks()
                                .On(byweekcontext.Dayofweek).At(byweekcontext.Hour, byweekcontext.Minute);
                        }

                        break;
                    case ScheduleType.Month:
                        var bymonthcontext = scheduleInfo.ByMonth;
                        if (bymonthcontext != null && !string.IsNullOrEmpty(name))
                        {
                            if (bymonthcontext.IsMonthByweek)
                            {
                                switch (bymonthcontext.WeekOfMonth)
                                {
                                    case WeekOfMonthType.第一个星期:
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheFirst(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                        break;
                                    case WeekOfMonthType.第二个星期:
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheSecond(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                        break;
                                    case WeekOfMonthType.第三个星期:
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheThird(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                        break;
                                    case WeekOfMonthType.第四个星期:
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheFourth(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                        break;
                                    case WeekOfMonthType.最后一个星期:
                                        Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheLast(bymonthcontext.Dayofweek).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                        break;
                                    default:
                                        throw new ArgumentOutOfRangeException();
                                }

                            }
                            else
                            {
                                if (bymonthcontext.OnDay > 0)
                                {
                                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().On(bymonthcontext.OnDay).At(bymonthcontext.Hour, bymonthcontext.Minute);
                                }
                                else
                                {
                                    Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name).ToRunOnceIn(bymonthcontext.Value).Months().OnTheLastDay().At(bymonthcontext.Hour, bymonthcontext.Minute);

                                }
                            }

                        }
                        break;

                    case ScheduleType.Minute:
                        var byminutecontext = scheduleInfo.ByMinute;

                        if (byminutecontext != null && !string.IsNullOrEmpty(name))
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunOnceIn(byminutecontext.Value).Minutes();
                        }
                        break;
                    case ScheduleType.Specified:
                        var byspecifiedcontext = scheduleInfo.BySpecified;

                        if (byspecifiedcontext != null && !string.IsNullOrEmpty(name))
                        {
                            Schedule(Job(scheduleInfo, action)).NonReentrant().WithName(name)
                                .ToRunOnceAt(byspecifiedcontext.Time);
                        }

                        break;

                    default:
                        break;
                }
            

        }


        private static Action Job(ScheduleInfo scheduleInfo, Action<ScheduleInfo> action)
        {
            return () =>
            {
                action(scheduleInfo);
            };
        }




    }


}
