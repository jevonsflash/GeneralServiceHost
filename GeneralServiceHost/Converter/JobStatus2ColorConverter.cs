using GeneralServiceHost.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace GeneralServiceHost.Converter
{
    public class JobStatus2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = string.Empty;
            if (value == null)
            {
                return color;
            }
            var currentStatus = (JobStatusType)value;

            switch (currentStatus)
            {
                case JobStatusType.Obsolete:
                    color = "Gray";
                    break;
                case JobStatusType.Pending:
                    color = "Gold";
                    break;
                case JobStatusType.Rerunning:
                    color = "Green";
                    break;
                case JobStatusType.Running:
                    color = "Green";
                    break;
                case JobStatusType.Stop:
                    color = "Red";
                    break;
                case JobStatusType.Unspecified:
                    color = "Purple";
                    break;
                default:
                    color = "Blue";
                    break;
            }

            return color;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

