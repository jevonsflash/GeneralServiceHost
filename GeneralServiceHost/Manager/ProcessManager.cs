using GeneralServiceHost.Common;
using GeneralServiceHost.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralServiceHost.Manager
{
    public class ProcessManager
    {
        public ProcessManager(ScheduleInfo scheduleInfo)
        {
            this.scheduleInfo = scheduleInfo;
        }

        public ScheduleInfo scheduleInfo { get; private set; }


        public ProcessResult RunProcess(Action<object, OutputArgs> outputDataReceivedAction, Action<object> errorDataReceivedAction = null, Action<object> exitDataReceivedAction = null)
        {
            DateTime Starttime;
            DateTime Endtime;
            var status = 0;
            var p = new Process()
            {
                StartInfo =
                    {

                        FileName = scheduleInfo.AsmPath,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError=true,
                        CreateNoWindow = true,
                        Arguments = scheduleInfo.Name,


                    }
            };
            p.Start();
            Starttime = p.StartTime;

            p.OutputDataReceived += (o, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data) && status == 0)
                {
                    var arg = new OutputArgs(e.Data, scheduleInfo.Name);
                    outputDataReceivedAction(e, arg);
                }
            };

            p.ErrorDataReceived += (o, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data) && status == 0)
                {
                    var msg = ("--------------------程序异常!--------------------\n");
                    var arg = new OutputArgs(msg + e.Data, scheduleInfo.Name);
                    outputDataReceivedAction(e, arg);
                    p.EnableRaisingEvents = false;

                    status = -1;
                }
            };

            //当EnableRaisingEvents为true，进程退出时Process会调用下面的委托函数
            p.Exited += (o, e) =>
            {
                if (status == 0)
                {


                    var msg = ("--------------------程序执行完毕!--------------------\n");
                    var arg = new OutputArgs(msg, scheduleInfo.Name);
                    outputDataReceivedAction(e, arg);

                    status = 1;
                }
            };
            p.EnableRaisingEvents = true;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            Endtime = p.ExitTime;
            p.Close();

            switch (status)
            {
                case -1:
                    errorDataReceivedAction?.Invoke(scheduleInfo);
                    break;
                case 1:
                    exitDataReceivedAction?.Invoke(scheduleInfo);
                    break;
                case 0: break;
                default:
                    break;
            }

            return new ProcessResult()
            {
                Status = status,
                Endtime = Endtime,
                Starttime = Starttime,
                Name = scheduleInfo.Name
            };
        }
    }
}
