using Microsoft.AspNetCore.Http;
using Serena.DAL.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serena.BLL.Models.Account
{
    public class RegisterPatientViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\u0600-\u06FF\s]+$", ErrorMessage = "First name can only contain letters and spaces")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Middle Name")]
        [StringLength(50, ErrorMessage = "Middle name cannot be longer than 50 characters")]
        [RegularExpression(@"^[a-zA-Z\u0600-\u06FF\s]*$", ErrorMessage = "Middle name can only contain letters and spaces")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\u0600-\u06FF\s]+$", ErrorMessage = "Last name can only contain letters and spaces")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^[0-9\-\+\s\(\)]{10,15}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
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
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old")]
        [MaximumAge(120, ErrorMessage = "Please enter a valid date of birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now.AddYears(-18);

        [Required(ErrorMessage = "Marital status is required")]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; } = string.Empty;

        [Display(Name = "Job Status")]
        public string? JobStatus { get; set; }

        [Display(Name = "Insurance Company")]
        public string? InsuranceCompany { get; set; }

        [Required(ErrorMessage = "National ID is required")]
        [Display(Name = "National ID")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be 14 digits")]
        [RegularExpression(@"^[0-9]{14}$", ErrorMessage = "National ID must contain only numbers")]
        public string NationalID { get; set; } = string.Empty;

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip code is required")]
        [Display(Name = "Zip Code")]
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$", ErrorMessage = "Please enter a valid zip code")]
        public string ZipCode { get; set; } = string.Empty;
    }
}