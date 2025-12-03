using Microsoft.AspNetCore.Http;
using Serena.DAL.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Serena.BLL.Models.Account
{
    public class RegisterDoctorViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be longer than 50 characters")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "Gender is required")]
        [Range(0, 1, ErrorMessage = "Please select a valid gender")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [MinimumAge(25, ErrorMessage = "Doctors must be at least 25 years old")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-25);

        [Required(ErrorMessage = "Specialization is required")]
        public string Specialization { get; set; } = string.Empty;

        [Display(Name = "Sub Specialization")]
        public string? SubSpecialization { get; set; }

        [Required(ErrorMessage = "Rank is required")]
        public string Rank { get; set; } = string.Empty;

        [Required(ErrorMessage = "License number is required")]
        [Display(Name = "License Number")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "License number must be between 5 and 20 characters")]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Years of experience is required")]
        [Display(Name = "Years of Experience")]
        [Range(0, 50, ErrorMessage = "Years of experience must be between 0 and 50")]
        public int YearsOfExperience { get; set; }

        [Required(ErrorMessage = "National ID is required")]
        [Display(Name = "National ID")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be 14 digits")]
        public string NationalID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip code is required")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Marital status is required")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }
    }
}
