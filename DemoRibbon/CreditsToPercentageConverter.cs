using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DemoRibbon
{
    public class CreditsToPercentageConverter: IValueConverter
    {
        public int Max { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int credits = (int)value;
            //Max = 150;
            int percentage = (int) (credits * 1.0 / Max * 100);

            string result = $"{percentage} %";
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
