using System;
using System.Globalization;
using System.Windows.Data;

namespace AppleMusicPlayer
{
    [ValueConversion(typeof(TimeSpan[]), typeof(string))]
    public class MediaPositionToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan position = (TimeSpan)value[0];
            TimeSpan duration = (TimeSpan)value[1];
            string format = @"m\:ss";
            if (duration.TotalHours >= 1)
            {
                format = @"H\:mm\:ss";
            }
            return $"{position.ToString(format)}/{duration.ToString(format)}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
