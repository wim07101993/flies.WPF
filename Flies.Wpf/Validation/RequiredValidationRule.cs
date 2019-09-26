using System.Globalization;
using System.Windows.Controls;

namespace Flies.Wpf.Validation
{
    public class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;

            return string.IsNullOrWhiteSpace(s)
                ? new ValidationResult(false, "Please enter a value") 
                : new ValidationResult(true, null);
        }
    }
}
