using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Pentagramm.Infrastructure.Attributes
{
    public class PentaPhoneAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }
            return Regex.IsMatch(value.ToString(), @"^\+[1-9]\d{1,14}$");
        }
    }
}
