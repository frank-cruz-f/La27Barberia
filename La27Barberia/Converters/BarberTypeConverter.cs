using La27Barberia.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace La27Barberia.Converters
{
    public class BarberTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if((int)value == (int)BarberType.Barber)
            {
                return "Barbero";
            }
            else if((int)value == (int)BarberType.Stylist)
            {
                return "Estilista";
            }
            else
            {
                return "Manicura/Pedicura";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
