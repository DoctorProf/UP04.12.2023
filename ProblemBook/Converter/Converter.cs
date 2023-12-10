using System.Globalization;
using System.Windows.Data;

namespace ProblemBook.Converter
{
    public class TruncateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue && stringValue.Length > 10)
            {
                return stringValue.Substring(0, 10) + "...";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}