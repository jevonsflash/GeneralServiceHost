using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Xsl;
using FluentScheduler;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Manager;
using GeneralServiceHost.Model;
using Microsoft.Win32;

namespace GeneralServiceHost.ViewModel
{
    public class AddJobWindowViewModel : ViewModelBase
    {


        private GeneralServiceRegistry _generalServiceRegistry;
        public AddJobWindowViewModel()
        {
            this._generalServiceRegistry = new GeneralServiceRegistry();
            this.SetCommand = new RelayCommand(SetAction);
            UploadFileCommand = new RelayCommand(UploadFileAction);
            this.ScheduleInfo = new ScheduleInfo() { Name = "程序集选择完成后显示" };
            this.PropertyChanged += AddJobWindowViewModel_PropertyChanged;
        }


        private void AddJobWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Asm))
            {
                if (Asm != null)
                {
                    this.ScheduleInfo.AsmPath = Asm.Location;
                    this.ScheduleInfo.AsmName = Asm.GetName().Name;


                }
            }
        }

        private void UploadFileAction()
        {
            ChoiceDLL();
            if (Asm != null)
            {
                this.ScheduleInfo.Name = Asm.GetName().Name;
                MessageBox.Show("加载程序集完成");

            }

        }

        private void SetAction()
        {
            run();
        }

        public RelayCommand SetCommand { get; set; }
        public RelayCommand UploadFileCommand { get; set; }

        private Assembly _asm;

        public Assembly Asm
        {
            get { return _asm; }
            set
            {
                _asm = value;
                RaisePropertyChanged(nameof(Asm));
            }
        }



        private ScheduleInfo _scheduleInfo;

        public ScheduleInfo ScheduleInfo
        {
            get
            {
                if (_scheduleInfo == null)
                {
                    _scheduleInfo = new ScheduleInfo();
                }
                return _scheduleInfo;
            }
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
                InitialDirectory = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jobs")
            };
            if (dialog.ShowDialog() == true)
            {
                var exe = dialog.FileName;

                Asm = Assembly.LoadFrom(exe);

            }
        }

        public void run()
        {

            if (DataManager.Current.JobInfos.Where(c => c.Name == ScheduleInfo.Name).Any())
            {
                MessageBox.Show(string.Format("已经包含名称为{0}的Job", ScheduleInfo.Name));

                return;
            }

            if (ScheduleInfo.IsGeneralJob)
            {
                _generalServiceRegistry.SetAndRegistryGeneralService(ScheduleInfo, (sch) =>
              {
                  CmdProcessor(sch, OutputAction, ErrorAction);


              });

            }
            else
            {
                _generalServiceRegistry.SetAndRegistryDelayService(ScheduleInfo, (sch) =>
               {
                   CmdProcessor(sch, OutputAction, ErrorAction);


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
                var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output", Asm.GetName().Name + ".txt");
                DirFileHelper.CreateFile(dir);
                MessageBox.Show("成功添加");
            }

        }

        private void ErrorAction(object arg1, OutputArgs arg2)
        {

        }

        private void OutputAction(object sender, OutputArgs arg2)
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

        public void CmdProcessor(ScheduleInfo scheduleInfo, Action<object, OutputArgs> outputDataReceivedAction, Action<object, OutputArgs> errorDataReceivedAction)
        {
            var p = new Process()
            {
                StartInfo =
                {

                    FileName = scheduleInfo.AsmPath,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
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
                var arg = new OutputArgs(o.Data, scheduleInfo.Name);
                errorDataReceivedAction(e, arg);
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
