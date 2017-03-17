using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentScheduler;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GeneralServiceHost.Common;
using GeneralServiceHost.Manager;
using GeneralServiceHost.Model;
using Microsoft.Win32;

namespace GeneralServiceHost.ViewModel
{
    public class AddJobWindowViewModel : ViewModelBase
    {

        private Assembly _asm;

        private GeneralServiceRegistry _generalServiceRegistry;
        public AddJobWindowViewModel()
        {
            this._generalServiceRegistry = new GeneralServiceRegistry();
            this.SetCommand = new RelayCommand(SetAction);
            UploadFileCommand = new RelayCommand(UploadFileAction);
        }

        private void UploadFileAction()
        {
            ChoiceDLL();
            Console.WriteLine("加载外部资源完成,指定定时器");

        }

        private void SetAction()
        {
            run();
        }

        public RelayCommand SetCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }



        private ScheduleInfo _scheduleInfo;

        public ScheduleInfo ScheduleInfo
        {
            get { return _scheduleInfo; }
            set
            {
                _scheduleInfo = value;
                RaisePropertyChanged(nameof(ScheduleInfo));

            }
        }


        public void ChoiceDLL()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "EXE File|*.exe;",
                InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Service", "Jobs")
            };
            if (dialog.ShowDialog() == true)
            {
                var exe = dialog.FileName;

                _asm = Assembly.LoadFrom(exe);



            }
        }

        public void run()
        {
            var sch = new ScheduleInfo()
            {
                Name = _asm.ToString(),
                Hour = 0,
                Minute = 1,
                Value = 10,
                IsToRunNow = false,
                Type = ScheduleType.Hour,
            };



            _generalServiceRegistry.TestGeneralService(sch, () =>
            {
                CmdProcessor(_asm.Location, OutputAction, ErrorAction);


            });

            _generalServiceRegistry.SetAndRegistryGeneralService(sch, () =>
           {
               CmdProcessor(_asm.Location, OutputAction, ErrorAction);


           });
            JobManager.Initialize(_generalServiceRegistry);
            var schedule = JobManager.GetSchedule(sch.Name);
            DataManager.Current.JobInfos.Add(new JobInfo()
            {
                Logs = new List<string>(),
                Schedule = schedule

            });

            Console.WriteLine("指定定时器完成");


        }

        private void ErrorAction(object arg1, DataReceivedEventArgs arg2)
        {
            //throw new NotImplementedException();
        }

        private void OutputAction(object sender, DataReceivedEventArgs arg2)
        {
            // this.CmdOutput += arg2.Data;

            var process = sender as Process;
            if (process != null)
            {
                var name = process.ProcessName;
                var job = DataManager.Current.JobInfos.Where(c => c.Schedule.Name == name).FirstOrDefault();
                job.Logs.Add(arg2.Data);
            }
        }

        public void CmdProcessor(string file, Action<object, DataReceivedEventArgs> outputDataReceivedAction, Action<object, DataReceivedEventArgs> errorDataReceivedAction)
        {
            var p = new Process()
            {
                StartInfo =
                {

                    FileName = file,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,


                }
            };
            p.Start();
            p.OutputDataReceived += (e, o) =>
            {
                outputDataReceivedAction(e, o);
            };
            p.ErrorDataReceived += (e, o) =>
            {
                errorDataReceivedAction(e, o);
            };

            //当EnableRaisingEvents为true，进程退出时Process会调用下面的委托函数
            p.Exited += (s, _e) => Console.WriteLine("Exited with " + p.ExitCode);
            p.EnableRaisingEvents = true;
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();
            p.Close();


        }

    }
}
