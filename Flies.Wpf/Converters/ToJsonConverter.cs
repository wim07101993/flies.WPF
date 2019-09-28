using System;
using System.Globalization;
using System.Windows.Data;
using Newtonsoft.Json;

namespace Flies.Wpf.Converters
{
    public class ToJsonConverter : IValueConverter
    {
        public static ToJsonConverter Instance { get; } = new ToJsonConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) 
            => value == null ? null : JsonConvert.SerializeObject(value, Formatting.Indented);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) 
            => value == null ? null : JsonConvert.DeserializeObject(value.ToString(), targetType);
    }
}
