using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealState.Dal.Contexs;
using RealState.Dal.Entities;
using RealState.UI.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace RealState.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly RealStateDbContext _context;
        private readonly ILogger<AccountController> _logger; // اضافه شد

        public AccountController(RealStateDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogInformation("Login GET called");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            _logger.LogInformation("Login POST called for UserName={UserName}", model.UserName);

            if (string.IsNullOrWhiteSpace(model.UserName))
                ModelState.AddModelError("UserName", "لطفاً نام کاربری را وارد کنید");

            if (string.IsNullOrWhiteSpace(model.Password))
                ModelState.AddModelError("Password", "لطفاً رمز عبور را وارد کنید");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Login failed validation for UserName={UserName}", model.UserName);
                return PartialView("Login", model);
            }

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                bool isAdmin = false;

                if (user == null)
                {
                    var admin = await _context.Admins.FirstOrDefaultAsync(a => a.AdminUserName == model.UserName);
                    if (admin != null)
                    {
                        user = new User
                        {
                            Id = admin.Id,
                            UserName = admin.AdminUserName,
                            Password = admin.AdminPassword,
                            IsActive = true
                        };
                        isAdmin = true;
                        _logger.LogInformation("Admin user {UserName} found", user.UserName);
                    }
                }
                else
                {
                    isAdmin = await _context.Admins.AnyAsync(a => a.AdminUserName == user.UserName);
                }

                if (user == null)
                {
                    _logger.LogWarning("Login failed: User {UserName} not found", model.UserName);
                    ModelState.AddModelError("UserName", "نام کاربری اشتباه است");
                    return PartialView("Login", model);
                }

                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);
                if (result != PasswordVerificationResult.Success)
                {
                    _logger.LogWarning("Login failed: Incorrect password for User {UserName}", model.UserName);
                    ModelState.AddModelError("Password", "رمز عبور اشتباه است");
                    return PartialView("Login", model);
                }

                if (!isAdmin)
                {
                    user.IsActive = true;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties { IsPersistent = true });

                _logger.LogInformation("Login successful for User {UserName}", model.UserName);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during login for User {UserName}", model.UserName);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutAsync()
        {
            _logger.LogInformation("LogoutAsync called for User={User}", User.Identity?.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            _logger.LogInformation("Logout GET called");
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("Register GET called");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(SignUpViewModel model)
        {
            _logger.LogInformation("Register POST called for UserName={UserName}", model.UserName);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Register validation failed for UserName={UserName}", model.UserName);
                return PartialView(model);
            }

            try
            {
                bool usernameExists = await _context.Users.AnyAsync(u => u.UserName == model.UserName);
                if (usernameExists)
                {
                    _logger.LogWarning("Register failed: UserName {UserName} already exists", model.UserName);
                    ModelState.AddModelError("UserName", "نام کاربری از قبل وجود دارد");
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    bool emailExists = await _context.Users.AnyAsync(u => u.Email == model.Email);
                    if (emailExists)
                    {
                        _logger.LogWarning("Register failed: Email {Email} already exists", model.Email);
                        ModelState.AddModelError("Email", "ایمیل از قبل ثبت شده است");
                    }
                }

                if (!ModelState.IsValid)
                    return PartialView(model);

                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    JoneDate = DateTime.Now
                };

                var passwordHasher = new PasswordHasher<User>();
                user.Password = passwordHasher.HashPassword(user, model.Password);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Register successful for UserName={UserName}", model.UserName);
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception during registration for UserName={UserName}", model.UserName);
                throw;
            }
        }

        #endregion
    }
}
