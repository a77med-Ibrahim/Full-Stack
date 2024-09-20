using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HospitalSystem.Entities;
using HospitalSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HospitalSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AccountController> _logger; // Changed to correct controller

        // Injecting ILogger<AccountController> into the constructor
        public AccountController(AppDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [Authorize(Roles = "Manager")]
        public IActionResult RoleManagement()
        {
            var Users = _context.UserAccounts.ToList();
            return View(Users);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult AssingRole(int userId, string role)
        {
            var user = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                user.Role = role;
                _context.SaveChanges();
                TempData["Message"] = $"Assigned role {role} to {user.FirstName} {user.LastName}.";
            }
            else
            {
                TempData["Error"] = "Error in assigning role.";
            }
            return RedirectToAction("RoleManagement");
        }

        public IActionResult Index()
        {
            var users = _context.UserAccounts.ToList();
            return View(users);
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    UserName = model.UserName
                };
                try
                {
                    _context.UserAccounts.Add(account);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{account.FirstName} {account.LastName} registered successfully. Please login.";
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Please enter a unique Email or Password");
                    return View(model);
                }
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            _logger.LogInformation("Login attempt by: {UsernameOrEmail}", model.UserNameOrEmail);

            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts
                    .FirstOrDefault(x => (x.UserName == model.UserNameOrEmail || x.Email == model.UserNameOrEmail) 
                                         && x.Password == model.Password);

                if (user != null)
                {
                    _logger.LogInformation("Login successful for user: {UsernameOrEmail}, Role: {Role}", user.Email, user.Role);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), 
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim("Name", user.FirstName),
                        new Claim(ClaimTypes.Role, user.Role)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    // Logging the redirection based on role
                    if (user.Role == "Manager")
                    {
                        _logger.LogInformation("Redirecting Manager to RoleManagement");
                        return RedirectToAction("RoleManagement");
                    }
                    else if (user.Role == "Doctor")
                    {
                        _logger.LogInformation("Redirecting Doctor to Create");
                        return RedirectToAction("Create");
                    }
                    else if (user.Role == "Nurse")
                    {
                        _logger.LogInformation("Redirecting Nurse to ViewMessages");
                        return RedirectToAction("ViewMessages");
                    }

                    return RedirectToAction("SecurePage");
                }
                else
                {
                    _logger.LogWarning("Login failed for: {UsernameOrEmail}", model.UserNameOrEmail);
                    ModelState.AddModelError("", "Username/Email or Password is not correct");
                }
            }
            else
            {
                _logger.LogWarning("Invalid model state for login attempt by: {UsernameOrEmail}", model.UserNameOrEmail);
            }

            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            _logger.LogInformation("User logged out.");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult SecurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ViewMessages()
        {
            var messages = _context.Messages.Include(m => m.Doctor).ToList(); 
            return View(messages ?? new List<Message>());
        }
    }
}
