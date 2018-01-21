using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using GeneralServiceHost.Model;

namespace GeneralServiceHost.Converter
{


    public class JobStatus2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string result = string.Empty;
            if (value == null)
            {
                return result;
            }
            var currentStatus = (JobStatusType)value;

            switch (currentStatus)
            {
                case JobStatusType.Obsolete:
                    result = "过期";
                    break;
                case JobStatusType.Pending:
                    result = "挂起";
                    break;
                case JobStatusType.Rerunning:
                    result = "正在执行";
                    break;
                case JobStatusType.Running:
                    result = "正在执行";
                    break;
                case JobStatusType.Stop:
                    result = "暂停";
                    break;
                case JobStatusType.Unspecified:
                    result = "未指定";
                    break;
                default:
                    result = "未指定";
                    break;
            }

            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
