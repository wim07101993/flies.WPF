using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace Flies.Wpf.Converters
{
    public class MultiCommandConverter : IMultiValueConverter
    {
        public static MultiCommandConverter Instance { get; } = new MultiCommandConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var command = new MultiCommand() { Commands = new List<ICommand>() };

            foreach (var obj in values)
            {
                if (obj is ICommand c)
                    command.Commands.Add(c);
            }

            return command;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return value is MultiCommand m ? m.Commands.ToArray() : null;
        }

        class MultiCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            public List<ICommand> Commands { get; set; }

            public bool CanExecute(object parameter) => Commands?.Any(x => x.CanExecute(parameter)) == true;

            public void Execute(object parameter)
            {
                if (Commands == null || !Commands.Any())
                    return;

                foreach (var command in Commands)
                    command.Execute(parameter);
            }
        }
    }
}
