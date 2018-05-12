using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace La27Barberia.Converters
{
    public class WaitTimeConverter : IValueConverter
    {
        private const string Format = @"hh\h\:mm\m\i\n";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var timeSpan = new TimeSpan(0, (int)value, 0);
            return timeSpan.ToString(Format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
