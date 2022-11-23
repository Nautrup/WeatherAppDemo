using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class DateTimeConverter : IValueConverter
    {
        DateTime time = new DateTime();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long dateTime = (long)value;
            return time.AddSeconds(dateTime).ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
