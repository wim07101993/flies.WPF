using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Flies.Wpf.Converters
{
    public class ObjectToTypeNameConveter : IValueConverter
    {
        public static ObjectToTypeNameConveter Instance { get; } = new ObjectToTypeNameConveter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value?.GetType().Name;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => DependencyProperty.UnsetValue;
    }
}
