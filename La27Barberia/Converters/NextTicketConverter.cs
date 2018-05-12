using La27Barberia.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace La27Barberia.Converters
{
    public class NextTicketConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            List<TicketDTO> tickets = (List<TicketDTO>)value;
            return tickets.Count > 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
