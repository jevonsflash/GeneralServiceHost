using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
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

        private const int COR_E_ASSEMBLYEXPECTED = -2146234344;
        public AddJobWindowViewModel()
        {
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
            }

        }

        private bool ValidateSchedule()
        {
            if (string.IsNullOrEmpty(this.ScheduleInfo.AsmPath))
            {
                MessageBox.Show("信息不完整,请选择文件");
                return false;

            }
            if (this.ScheduleInfo.Type == ScheduleType.Unspecified)
            {
                MessageBox.Show("信息不完整,请选择时间单位");
                return false;
            }
            return true;
        }

        private void SetAction()
        {
            if (ValidateSchedule())
            {
                var isSuccess = JobInfoManager.RunSchedule(this.ScheduleInfo);
                if (isSuccess)
                {
                    var isCreateJobSuccess = JobInfoManager.CreateJob(this.ScheduleInfo);
                    if (isCreateJobSuccess)
                    {
                        MessageBox.Show("任务启用成功");

                    }
                    else
                    {
                        MessageBox.Show("任务启用失败");

                    }
                    JobInfoManager.Refresh();
                }
                else
                {
                    MessageBox.Show("任务启用失败");
                }

            }

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
                try
                {
                    Asm = Assembly.LoadFrom(exe);
                }
                catch (BadImageFormatException ex)
                {
                    int errorCode = System.Runtime.InteropServices.Marshal.GetHRForException(ex);
                    if (errorCode == COR_E_ASSEMBLYEXPECTED)
                    {
                        MessageBox.Show("此exe文件无法在当前的Windows中运行");
                    }
                }


            }
        }



    }
}
