using System;
using System.Globalization;
using System.Windows.Data;
using GeneralServiceHost.Model;

namespace GeneralServiceHost.Converter
{
    public class ScheduleMode2BoolConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ScheduleMode type;
            bool result = false;
            if (value == null)
            {
                return result;
            }

            if (parameter == null)
            {
                type = ScheduleMode.周期任务;
            }
            else
            {
                type = (ScheduleMode)Enum.Parse(typeof(ScheduleMode), parameter as string, false);
            }


            var currentStatus = (ScheduleMode)value;

            result = currentStatus == type;
            return result;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ScheduleMode type;

            if (parameter == null)
            {
                type = ScheduleMode.周期任务;
            }
            else
            {
                type = (ScheduleMode)Enum.Parse(typeof(ScheduleMode), parameter as string, false);
            }
            if ((bool)value)
            {
                return type;
            }
            else
            {
                return null;
            }

        }
    }
}

