using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Serena.BLL.Models.Account
{
    public class RegisterHospitalViewModel
    {
        [Required(ErrorMessage = "Hospital name is required")]
        [Display(Name = "Hospital Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be at least {2} characters long", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Hospital phone is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        [Display(Name = "Hospital Phone")]
        public string HospitalPhone { get; set; }

        [Required(ErrorMessage = "Emergency phone is required")]
        [Phone(ErrorMessage = "Invalid emergency phone number")]
        [Display(Name = "Emergency Phone")]
        public string EmergencyPhone { get; set; }

        [Required(ErrorMessage = "Rank is required")]
        [Display(Name = "Rank")]
        public string Rank { get; set; }

        [Required(ErrorMessage = "Average cost is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Average cost must be positive")]
        [Display(Name = "Average Cost")]
        public decimal AverageCost { get; set; }

        [Display(Name = "Hospital Logo")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "At least one address is required")]
        public List<HospitalAddressViewModel> Addresses { get; set; } = new List<HospitalAddressViewModel>();

        [Required(ErrorMessage = "At least one department is required")]
        public List<HospitalDepartmentViewModel> Departments { get; set; } = new List<HospitalDepartmentViewModel>();
    }

    // HospitalAddressViewModel.cs
    public class HospitalAddressViewModel
    {
        [Required(ErrorMessage = "Street is required")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "District is required")]
        public string District { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Latitude is required")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Longitude { get; set; }

        public bool IsPrimary { get; set; }
    }

    // HospitalDepartmentViewModel.cs
    public class HospitalDepartmentViewModel
    {
        [Required(ErrorMessage = "Department name is required")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
