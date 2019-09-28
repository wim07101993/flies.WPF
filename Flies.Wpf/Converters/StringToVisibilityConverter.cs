﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Flies.Wpf.Converters
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public static StringToVisibilityConverter Instance { get; } = new StringToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is string s && !string.IsNullOrWhiteSpace(s)
                ? Visibility.Visible
                : Visibility.Collapsed;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => DependencyProperty.UnsetValue;
    }
}
