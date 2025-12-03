using System.ComponentModel.DataAnnotations;

namespace Serena.BLL.Models.Account
{
    public class MaximumAgeAttribute : ValidationAttribute
    {
        private readonly int _maximumAge;

        public MaximumAgeAttribute(int maximumAge)
        {
            _maximumAge = maximumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.AddYears(_maximumAge) < DateTime.Today)
                {
                    return new ValidationResult($"Please enter a valid date of birth.");
                }
            }
            return ValidationResult.Success;
        }


    }
}
