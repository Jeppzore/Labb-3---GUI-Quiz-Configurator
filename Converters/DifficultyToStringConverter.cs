using Labb_3___GUI_Quiz.Model;
using System.Globalization;
using System.Windows.Data;

namespace Labb_3___GUI_Quiz.Converters
{
    public class DifficultyToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var difficulty = (Difficulty)value;
            return ((int)difficulty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var difficulty = (Difficulty)value;
            return difficulty.ToString();
        }
    }
}
