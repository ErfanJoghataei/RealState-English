using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealState.BusinessLogik.Servises.AdminServises;
using RealState.Dal.Contexs;
using RealState.Dal.Entities;
using RealState.UI.Models;
using System.Security.Claims;

namespace RealState.UI.Controllers
{
    public class PanelController : Controller
    {
        private readonly RealStateDbContext _context;
        private readonly IAdminService _adminService;
        private readonly ILogger<PanelController> _logger; // LOG ADDED

        public PanelController(RealStateDbContext context, IAdminService adminService, ILogger<PanelController> logger)
        {
            _context = context;
            _adminService = adminService;
            _logger = logger;
        }

        #region Admin Panel

        [HttpGet]
        public async Task<IActionResult> AdminPanel()
        {
            _logger.LogInformation("➡️ AdminPanel GET called");

            try
            {
                var admins = _adminService.GetAllAdmins()
                    .Select(a => new AdminViewModel { Id = a.Id, UserName = a.AdminUserName, Password = a.AdminPassword })
                    .ToList();

                var properties = await _context.properties.
                    Select(c => new PropertyViewModel
                    {
                        Code = c.Code,
                        city = c.City,
                        Price = c.Price,
                        Title = c.Title,
                        FirstMoney = c.FirstMoney,
                        MoneyRent = c.MoneyRent
                    }).ToListAsync();


                var model = new AdminPanelViewModel
                {
                    Admins = admins,
                    Properties = properties
                };


                //var model = new AdminPanelViewModel { Admins = admins };
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Exception in AdminPanel GET");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(AdminViewModel model)
        {
            _logger.LogInformation("➡️ AddAdmin POST called | UserName={UserName}", model.UserName);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("⚠️ AddAdmin failed - invalid model");
                return BadRequest("اطلاعات وارد شده صحیح نیست.");
            }

            try
            {
                await _adminService.AddAdminAsync(model.UserName, model.Password);
                _logger.LogInformation("✅ Admin added successfully | UserName={UserName}", model.UserName);
                return Ok($"{model.UserName} با موفقیت اضافه شد.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Exception in AddAdmin | UserName={UserName}", model.UserName);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAdmin(int id)
        {
            _logger.LogInformation("➡️ RemoveAdmin POST called | Id={Id}", id);

            try
            {
                await _adminService.RemoveAdminAsync(id);
                _logger.LogInformation("✅ Admin removed successfully | Id={Id}", id);
                return RedirectToAction(nameof(AdminPanel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Exception in RemoveAdmin | Id={Id}", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(AdminViewModel model)
        {
            _logger.LogInformation("➡️ EditAdmin POST called | Id={Id} | UserName={UserName}", model.Id, model.UserName);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("⚠️ EditAdmin failed - invalid model | Id={Id}", model.Id);
                return BadRequest("اطلاعات وارد شده صحیح نیست.");
            }

            try
            {
                await _adminService.EditAdminAsync(model.Id, model.UserName, model.Password);
                _logger.LogInformation("✅ Admin edited successfully | Id={Id} | UserName={UserName}", model.Id, model.UserName);
                return Ok($"ادمین با Id={model.Id} ویرایش شد.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Exception in EditAdmin | Id={Id}", model.Id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveProperty(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return BadRequest();
            }
            var property = await _context.properties.Where(c => c.Code == code).FirstOrDefaultAsync();
            if (property == null)
            {
                return NotFound();
            }
            _context.properties.Remove(property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminPanel));
        }

        #endregion

        #region User Panel

        private async Task<User?> GetCurrentUserAsync()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdStr, out var userId)) return null;

            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId);
        }

        [HttpGet]
        public async Task<IActionResult> UserPanel()
        {
            _logger.LogInformation("➡️ UserPanel GET called");

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                _logger.LogWarning("⚠️ UserPanel GET - user not logged in");
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(User model)
        {
            _logger.LogInformation("➡️ UserEdit POST called | UserId={UserId}", model.Id);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            if (user == null)
            {
                _logger.LogWarning("⚠️ UserEdit failed - user not found | UserId={UserId}", model.Id);
                return NotFound("کاربر یافت نشد.");
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            await _context.SaveChangesAsync();

            _logger.LogInformation("✅ User updated successfully | UserId={UserId}", model.Id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

            TempData["Success"] = "اطلاعات با موفقیت ویرایش شد!";
            return RedirectToAction("UserPanel");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(string currentPassword, string newPassword, string confirmPassword)
        {
            _logger.LogInformation("➡️ EditPassword POST called");

            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                _logger.LogWarning("⚠️ EditPassword failed - user not logged in");
                return RedirectToAction("Login", "Account");
            }

            if (string.IsNullOrWhiteSpace(currentPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                _logger.LogWarning("⚠️ EditPassword failed - empty fields | UserId={UserId}", user.Id);
                TempData["Error"] = "تمام فیلدها باید پر شوند.";
                return RedirectToAction("UserPanel");
            }

            if (newPassword != confirmPassword)
            {
                _logger.LogWarning("⚠️ EditPassword failed - passwords do not match | UserId={UserId}", user.Id);
                TempData["Error"] = "رمز عبور جدید با تکرار آن مطابقت ندارد.";
                return RedirectToAction("UserPanel");
            }

            var hasher = new PasswordHasher<User>();
            if (hasher.VerifyHashedPassword(user, user.Password, currentPassword) != PasswordVerificationResult.Success)
            {
                _logger.LogWarning("⚠️ EditPassword failed - current password incorrect | UserId={UserId}", user.Id);
                TempData["Error"] = "رمز عبور فعلی اشتباه است.";
                return RedirectToAction("UserPanel");
            }

            user.Password = hasher.HashPassword(user, newPassword);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("✅ Password changed successfully | UserId={UserId}", user.Id);
            TempData["Success"] = "رمز عبور با موفقیت تغییر یافت.";
            return RedirectToAction("UserPanel");
        }

        #endregion

        #region Logout

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("➡️ Logout POST called");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();

            _logger.LogInformation("✅ User logged out successfully");
            TempData["LogoutMessage"] = "با موفقیت خارج شدید.";
            return RedirectToAction("Index", "Home");
        }

        #endregion
    }
}
