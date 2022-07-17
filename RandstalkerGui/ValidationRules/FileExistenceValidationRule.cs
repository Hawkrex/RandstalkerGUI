using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace RandstalkerGui.ValidationRules
{
    public class FileExistenceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, "Value cannot be converted to string.");
            }

            if (!File.Exists(strValue))
            {
                return new ValidationResult(false, $"File does not exists !");
            }

            return new ValidationResult(true, null);
        }
    }
}
