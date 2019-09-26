using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace Flies.Wpf.Validation
{
    public class IpAddressValidationRule : ValidationRule
    {
        private const string Regex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var s = value as string;

            if (string.IsNullOrWhiteSpace(s))
                return new ValidationResult(false, "Please enter an IP address");

            return new Regex(Regex).IsMatch(s) 
                ? new ValidationResult(true, null) 
                : new ValidationResult(false, "Please enter a valid IP address");
        }
    }
}
