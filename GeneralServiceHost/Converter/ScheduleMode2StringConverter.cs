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

    public class ScheduleMode2StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strings = string.Empty;
            string[] strList;
            string result = string.Empty;
            if (value == null)
            {
                return result;
            }
            if (parameter == null)
            {
                strings = "NaN|NaN|NaN";
            }
            else
            {
                strings = parameter as string;
            }
            strList = strings.Split('|');
            var currentStatus = (ScheduleMode)value;

            switch (currentStatus)
            {
                case ScheduleMode.周期任务:
                    result = strList[0];
                    break;
                case ScheduleMode.延时任务:
                    result = strList[1];
                    break;
                case ScheduleMode.不间断任务:
                    result = strList[2];
                    break;
          
                default:
                    result = strList[0];
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
