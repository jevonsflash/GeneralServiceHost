using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralServiceHost.Helper;
using GeneralServiceHost.Model;


namespace GeneralServiceHost.Manager
{
    public class OutputManager
    {

        private static Object _locker = new object();

        static string outputsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");


        static OutputManager()
        {
            DirFileHelper.CreateDirectory(outputsPath);
        }

        public static async Task<string> ReadOutput(string fileName)
        {
            string outputsFile = Path.Combine(outputsPath, fileName);
            
            var result = await Task.Run(() =>
            {
                DirFileHelper.ExistsFile(outputsFile);
                var jsonInfos = DirFileHelper.ReadFile(outputsFile);
                return jsonInfos;

            });
            return result;


        }

        public static async void SaveOutputs(string fileName, string content)
        {
            string outputsFile = Path.Combine(outputsPath, fileName);
            await Task.Run(() =>
            {
                lock (_locker)
                {
                    DirFileHelper.ExistsFile(outputsFile);
                    DirFileHelper.WriteText(outputsFile, content);
                }
            });


        }
        public static async void AppendOutput(string fileName, string content)
        {
            string outputsFile = Path.Combine(outputsPath, fileName);

            await Task.Run(() =>
            {
                lock (_locker)
                {
                   
                    DirFileHelper.AppendText(outputsFile, content);
                }
            });
        }



        public static async void AppendOutput(string fileName, DateTime createTime, string content)
        {
            string outputsFile = Path.Combine(outputsPath, fileName);

            await Task.Run(() =>
            {
                lock (_locker)
                {
                    string value = string.Format("[{0}]{1}", createTime.ToString("yyyy-MM-dd hh:mm:ss"), content);
                    DirFileHelper.AppendText(outputsFile, value);
                }
            });
        }

        public static async Task<string> PopupOutput(string fileName)
        {
            string outputsFile = Path.Combine(outputsPath, fileName);
            var result = await Task.Run(() =>
            {

                DirFileHelper.ExistsFile(outputsFile);
                var jsonInfos = DirFileHelper.ReadLastLines(outputsFile);
                return jsonInfos;

            });
            return result;


        }
    }
}