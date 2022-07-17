using System;
using System.Globalization;
using System.IO;
using System.Windows.Controls;

namespace RandstalkerGui.ValidationRules
{
    public class DirectoryExistenceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = Convert.ToString(value);

            if (string.IsNullOrEmpty(strValue))
            {
                return new ValidationResult(false, "Value cannot be converted to string.");
            }

            if (!Directory.Exists(strValue))
            {
                return new ValidationResult(false, $"Directory does not exists !");
            }

            return new ValidationResult(true, null);
        }
    }
}
