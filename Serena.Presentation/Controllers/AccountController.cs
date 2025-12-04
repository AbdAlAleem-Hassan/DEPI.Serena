using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serena.BLL.Models.Account;
using Serena.BLL.Models.Doctors;
using Serena.BLL.Models.Patients;
using Serena.BLL.Services.Doctors;
using Serena.BLL.Services.Patients;
using Serena.DAL.Common.Enums;
using Serena.DAL.Entities;

namespace Serena.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            ILogger<AccountController> logger,
            IPatientService patientService,
            IDoctorService doctorService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
            _patientService = patientService;
            _doctorService = doctorService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterPatient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterPatient(RegisterPatientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["Error"] = "Please correct the following errors:<br>" +
                                   string.Join("<br>• ", errors);
                return View(model);
            }

            try
            {
                // Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    TempData["Warning"] = "📧 This email is already registered.<br>Please use a different email or try logging in.";
                    return View(model);
                }

                // Create Application User
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Ensure Patient role exists
                    if (!await _roleManager.RoleExistsAsync("Patient"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = "Patient",
                            Description = "Patient Role"
                        });
                    }

                    // Assign Patient role
                    await _userManager.AddToRoleAsync(user, "Patient");

                    // Create Patient record
                    var patientDto = new CreateAndUpdatePatientDTO
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Gender = (Gender)model.Gender,
                        DateOfBirth = model.DateOfBirth,
                        MaritalStatus = model.MaritalStatus,
                        JobStatus = model.JobStatus,
                        InsuranceCompany = model.InsuranceCompany,
                        NationalID = model.NationalID,
                        Country = model.Country,
                        City = model.City,
                        Street = model.Street,
                        ZipCode = model.ZipCode
                    };

                    await _patientService.CreatePatientAsync(patientDto);

                    // Sign in the user
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["Success"] = $"🎉 Welcome {model.FirstName}!<br>Your patient account has been created successfully.";
                    return RedirectToAction("Index", "Home");
                }

                // Handle Identity errors
                var identityErrors = result.Errors.Select(e => e.Description).ToList();
                TempData["Error"] = "Failed to create account:<br>" +
                                   string.Join("<br>• ", identityErrors);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during patient registration");
                TempData["Error"] = "❌ An unexpected error occurred.<br>Please try again later.";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult RegisterDoctor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterDoctor(RegisterDoctorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                TempData["Error"] = "Please correct the following errors:<br>• " +
                                   string.Join("<br>• ", errors);
                return View(model);
            }

            try
            {
                // Check if user already exists
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    TempData["Warning"] = "📧 This email is already registered.<br>Please use a different email.";
                    return View(model);
                }

                // Create Application User
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Ensure Doctor role exists
                    if (!await _roleManager.RoleExistsAsync("Doctor"))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole
                        {
                            Name = "Doctor",
                            Description = "Doctor Role"
                        });
                    }

                    // Assign Doctor role
                    await _userManager.AddToRoleAsync(user, "Doctor");

                    // Create Doctor record
                    var doctorDto = new CreateAndUpdateDoctorDTO
                    {
                        UserId = user.Id,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Gender = (Gender)model.Gender,
                        DateOfBirth = model.DateOfBirth,
                        MaritalStatus = model.MaritalStatus,
                        Specialization = model.Specialization,
                        SubSpecialization = model.SubSpecialization,
                        Rank = model.Rank,
                        LicenseNumber = model.LicenseNumber,
                        YearsOfExperience = model.YearsOfExperience,
                        NationalID = model.NationalID,
                        Street = model.Street,
                        City = model.City,
                        Country = model.Country,
                        ZipCode = model.ZipCode,
                        Image = model.Image,
                    };

                    await _doctorService.CreateDoctorAsync(doctorDto);

                    // Sign in the user
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["Success"] = $"🎉 Welcome Dr. {model.LastName}!<br>Your professional account has been created successfully.";
                    return RedirectToAction("Index", "Home");
                }

                var identityErrors = result.Errors.Select(e => e.Description).ToList();
                TempData["Error"] = "Failed to create doctor account:<br>• " +
                                   string.Join("<br>• ", identityErrors);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during doctor registration");
                TempData["Error"] = "❌ An unexpected error occurred.<br>Please try again later.";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please enter valid email and password.";
                return View(model);
            }

            try
            {
                // Check if user exists
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    TempData["Error"] = "❌ Invalid email or password.";
                    return View(model);
                }

                // Check if account is active
                if (!user.IsActive)
                {
                    TempData["Warning"] = "⏸️ Your account has been deactivated.<br>Please contact support.";
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    // Get user roles for greeting
                    var roles = await _userManager.GetRolesAsync(user);
                    var roleGreeting = roles.Contains("Doctor") ? "Dr." : "";

                    TempData["Success"] = $"👋 Welcome back {roleGreeting} {user.FirstName}!";

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else if (result.IsLockedOut)
                {
                    TempData["Warning"] = "🔒 Account locked due to multiple failed attempts.<br>Try again in 15 minutes.";
                    return View(model);
                }
                else if (result.IsNotAllowed)
                {
                    TempData["Warning"] = "📧 Please confirm your email before logging in.";
                    return View(model);
                }
                else
                {
                    TempData["Error"] = "❌ Invalid email or password.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login");
                TempData["Error"] = "❌ An error occurred during login.<br>Please try again.";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Info"] = "👋 You have been logged out successfully.";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            TempData["Warning"] = "🚫 You don't have permission to access this page.";
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
        
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult RegisterHospital()
        {
            return View();
        }
        
    }
}