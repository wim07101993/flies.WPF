using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace Flies.Wpf.Converters
{
    public class StringToIpAddressConverter : IValueConverter
    {
        public static StringToIpAddressConverter Instance { get; } = new StringToIpAddressConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            return string.IsNullOrWhiteSpace(s)
                ? default
                : IPAddress.Parse(s);
        }
    }
}
