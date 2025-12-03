using System.ComponentModel.DataAnnotations;

namespace Serena.BLL.Models.Account
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.AddYears(_minimumAge) > DateTime.Today)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }
            }
            return ValidationResult.Success;
        }
    }
}