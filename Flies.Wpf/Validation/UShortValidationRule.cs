using System.Globalization;
using System.Windows.Controls;

namespace Flies.Wpf.Validation
{
    public class UShortValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;

            return string.IsNullOrWhiteSpace(s) || !ushort.TryParse(s, out var _)
                ? new ValidationResult(false, "Please enter a number between 0 and 65535")
                : new ValidationResult(true, null);
        }
    }
}
