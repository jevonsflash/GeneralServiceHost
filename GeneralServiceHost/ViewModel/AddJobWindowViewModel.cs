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
                MessageBox.Show("加载程序集完成");

            }

        }

        private void SetAction()
        {
            JobInfoManager.RunSchedule(this.ScheduleInfo);
            JobInfoManager.CreateJob(this.ScheduleInfo);
            JobInfoManager.Refresh();
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



    }
}
