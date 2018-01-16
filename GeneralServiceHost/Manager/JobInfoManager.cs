using FluentScheduler;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneralServiceHost.Manager
{
    public class JobInfoManager
    {
        static JobInfoManager()
        {
            JobManager.JobEnd += JobManager_JobEnd;
            JobManager.JobStart += JobManager_JobStart;
        }

        private static void JobManager_JobStart(JobStartInfo obj)
        {
            Refresh();
        }

        private static void JobManager_JobEnd(JobEndInfo obj)
        {
            Refresh();
        }

        /// <summary>
        /// 刷新状态
        /// </summary>
        public static void Refresh()
        {
            foreach (var schedule in DataManager.Current.JobInfos)
            {

                var c = JobManager.AllSchedules.FirstOrDefault(d => d.Name == schedule.Name);
                if (c != null)
                {
                    schedule.Name = c.Name;
                    schedule.NextRun = c.NextRun;
                    schedule.Disabled = c.Disabled;
                }
                else
                {
                    schedule.Obsolete = true;
                }
            }

            DataManager.Current.Save();
        }
        public static void Start(string name)
        {
            if (!DataManager.Current.JobInfos.First(c => c.Name == name).Obsolete)
            {
                JobManager.GetSchedule(name).Enable();

            }

            Refresh();
        }

        public static void Abort(string name)
        {
            if (!DataManager.Current.JobInfos.First(c => c.Name == name).Obsolete)
            {
                JobManager.GetSchedule(name).Disable();

            }
            Refresh();
        }

        public static void Run(ScheduleInfo ScheduleInfo)
        {
            GeneralServiceRegistry _generalServiceRegistry = new GeneralServiceRegistry();
            if (DataManager.Current.JobInfos.Where(c => c.Name == ScheduleInfo.Name).Any())
            {
                MessageBox.Show(string.Format("已经包含名称为{0}的Job", ScheduleInfo.Name));

                return;
            }

            if (ScheduleInfo.IsGeneralJob)
            {
                _generalServiceRegistry.SetAndRegistryGeneralService(ScheduleInfo, (sch) =>
              {
                  if (ScheduleInfo.IsGuard)
                  {
                      CmdProcessor(sch, OutputAction, ErrorAction);
                  }
                  else
                  {
                      CmdProcessor(sch, OutputAction);
                  }


              });

            }
            else
            {
                _generalServiceRegistry.SetAndRegistryDelayService(ScheduleInfo, (sch) =>
               {

                   if (ScheduleInfo.IsGuard)
                   {
                       CmdProcessor(sch, OutputAction, ErrorAction);
                   }
                   else
                   {
                       CmdProcessor(sch, OutputAction);
                   }


               });

            }

            JobManager.Initialize(_generalServiceRegistry);

            var schedule = JobManager.GetSchedule(ScheduleInfo.Name);
            if (schedule != null)
            {

                var jobInfo = new JobInfo()
                {
                    Name = schedule.Name,
                    Disabled = schedule.Disabled,
                    NextRun = schedule.NextRun,
                    ScheduleInfo = ScheduleInfo

                };
                DataManager.Current.JobInfos.Add(jobInfo);
                MessageBox.Show("成功添加");
            }

            Refresh();

        }

        private static void ErrorAction(object sender)
        {
            var process = sender as Process;
            process.Start();
        }

        private static void OutputAction(object sender, OutputArgs arg2)
        {

            var process = sender as Process;
            if (process != null)
            {
                var name = process.StartInfo.Arguments;
                var content = arg2.OutputContent;
                var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output", name + ".txt");
                var log = DataManager.Current.JobInfos.FirstOrDefault(c => c.Name == arg2.JobName).SbLog;
                log.AppendLine(content);
                //Messenger.Default.Send(new MessageBase(this));
                //DirFileHelper.AppendText(dir, "\r\n" + content);

            }
        }

        private static void CmdProcessor(ScheduleInfo scheduleInfo, Action<object, OutputArgs> outputDataReceivedAction, Action<object> errorDataReceivedAction = null, Action<object> exitDataReceivedAction = null)
        {

            var p = new Process()
            {
                StartInfo =
                {

                    FileName = scheduleInfo.AsmPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError=true,
                    CreateNoWindow = true,
                    Arguments = scheduleInfo.Name

                }
            };

            p.Start();
            p.OutputDataReceived += (e, o) =>
            {
                var arg = new OutputArgs(o.Data, scheduleInfo.Name);
                outputDataReceivedAction(e, arg);
            };
            p.ErrorDataReceived += (e, o) =>
            {
                //var msg = ("\n程序异常，错误代码:" + p.ExitCode);
                //var arg = new OutputArgs(o.Data + msg, scheduleInfo.Name);
                //outputDataReceivedAction(e, arg);
                errorDataReceivedAction?.Invoke(o);
            };

            //当EnableRaisingEvents为true，进程退出时Process会调用下面的委托函数
            p.Exited += (o, e) =>
            {
                var msg = ("\n程序执行完毕，代码:" + p.ExitCode);
                var arg = new OutputArgs(msg, scheduleInfo.Name);
                outputDataReceivedAction(e, arg);

                exitDataReceivedAction?.Invoke(o);


            };
            p.EnableRaisingEvents = true;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();


        }

    }
}
