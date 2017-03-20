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
using FluentScheduler;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GeneralServiceHost.Common;
using GeneralServiceHost.Helper;
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
            this.ScheduleInfo = new ScheduleInfo() { Name = "程序集选择完成后显示" };
            this.PropertyChanged += AddJobWindowViewModel_PropertyChanged;
        }

        private void AddJobWindowViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ScheduleInfo))
            {

            }
        }

        private void UploadFileAction()
        {
            ChoiceDLL();
            if (_asm != null)
            {
                this.ScheduleInfo.Name = _asm.GetName().Name;
                MessageBox.Show("加载程序集完成");

            }

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

                _asm = Assembly.LoadFrom(exe);



            }
        }

        public void run()
        {




            _generalServiceRegistry.TestGeneralService(ScheduleInfo, () =>
            {
                CmdProcessor(_asm.Location, OutputAction, ErrorAction);


            });

            _generalServiceRegistry.SetAndRegistryGeneralService(ScheduleInfo, () =>
           {
               CmdProcessor(_asm.Location, OutputAction, ErrorAction);


           });
            JobManager.Initialize(_generalServiceRegistry);

            var schedule = JobManager.GetSchedule(ScheduleInfo.Name);
            if (schedule != null)
            {
                DataManager.Current.JobInfos.Add(new JobInfo()
                {
                    Name = schedule.Name,
                    Disabled = schedule.Disabled,
                    NextRun = schedule.NextRun,
                    ScheduleInfo = ScheduleInfo
                });
                var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output", _asm.GetName().Name + ".txt");

                DirFileHelper.CreateFile(dir);
                MessageBox.Show("成功添加");
            }

        }

        private void ErrorAction(object arg1, DataReceivedEventArgs arg2)
        {
            //throw new NotImplementedException();
        }

        private void OutputAction(object sender, DataReceivedEventArgs arg2)
        {
            var process = sender as Process;
            if (process != null)
            {
                var name = process.ProcessName;
                var dir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output", name + ".txt");
                DirFileHelper.AppendText(dir, "\n" + arg2.Data);
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
