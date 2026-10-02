using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RealState.BusinessLogik.Servises;
using RealState.BusinessLogik.Servises.AdminServises;
using RealState.Dal.Contexs;
using RealState.Dal.Entities;
using RealState.UI.Models;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

namespace RealState.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly RealStateDbContext _context;
        private readonly IRealStateServise _realStateServise;
        private readonly ILogger<HomeController> _logger; // اضافه شد
        private readonly IAdminService _adminService;

        public HomeController(IConfiguration configuration, RealStateDbContext context, IRealStateServise realStateServise, ILogger<HomeController> logger, IAdminService adminService)
        {
            _configuration = configuration;
            _context = context;
            _realStateServise = realStateServise;
            _logger = logger;
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string query, string province, string city, string district, int? maxPrice)
        {
            _logger.LogInformation("Home/Index called with query={Query}, province={Province}, city={City}, district={District}, maxPrice={MaxPrice}", query, province, city, district, maxPrice);

  
            try
            {
                List<Properties> properties;
                try
                {
                    properties = _realStateServise.GetAllProperties();
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Database unavailable; showing curated portfolio listings.");
                    properties = new List<Properties>();
                }
                if (properties.Count == 0)
                {
                    properties.AddRange(ShowcaseProperties.All);
                }

                if (!string.IsNullOrWhiteSpace(query))
                {
                    var normalizedQuery = query.Trim();
                    properties = properties
                        .Where(p =>
                            (!string.IsNullOrWhiteSpace(p.Title) && p.Title.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrWhiteSpace(p.Code) && p.Code.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrWhiteSpace(p.City) && p.City.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrWhiteSpace(p.neighborhood) && p.neighborhood.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase)) ||
                            (!string.IsNullOrWhiteSpace(p.province) && p.province.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(province))
                {
                    properties = properties
                        .Where(p => !string.IsNullOrWhiteSpace(p.province) && p.province.Contains(province, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(city))
                {
                    properties = properties
                        .Where(p => !string.IsNullOrWhiteSpace(p.City) && p.City.Contains(city, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(district))
                {
                    properties = properties
                        .Where(p => !string.IsNullOrWhiteSpace(p.neighborhood) && p.neighborhood.Contains(district, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (maxPrice.HasValue)
                {
                    properties = properties
                        .Where(p => p.Price.HasValue && p.Price.Value <= maxPrice.Value)
                        .ToList();
                }

                var model = new LayoutModel
                {
                    properties = properties
                };
                _logger.LogInformation("Retrieved {Count} properties", model.properties.Count);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Home/Index");
                throw; // یا نمایش صفحه خطا

            }
     
        }

        [HttpGet]
        public IActionResult SearchProperties(string query, string province, string city, string district, int? maxPrice)
        {
            _logger.LogInformation("Home/SearchProperties called with query={Query}, province={Province}, city={City}, district={District}, maxPrice={MaxPrice}", query, province, city, district, maxPrice);
            return RedirectToAction("Index", new { query, province, city, district, maxPrice });
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Home/Privacy called");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogWarning("Home/Error called with RequestId={RequestId}", Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Home/Contact called");
            return View();
        }

        [HttpGet]
        public IActionResult PropertyDetails(int id)
        {
            _logger.LogInformation("Home/PropertyDetails called with id={Id}", id);
           

            try
            {
                var property = id < 0
                    ? ShowcaseProperties.All.FirstOrDefault(p => p.Id == id)
                    : _realStateServise.GetPropertyById(id);

                if (property == null)
                {
                    _logger.LogWarning("Property with id={Id} not found", id);
                    return NotFound();
                }

                return View(property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Home/PropertyDetails for id={Id}", id);
                throw;
            }
        }

        [HttpPost]
        public IActionResult PropertyDetailsPost(int id)
        {
            _logger.LogInformation("Home/PropertyDetailsPost called with id={Id}", id);

            try
            {
                var property = _realStateServise.GetPropertyById(id);
                if (property == null)
                {
                    _logger.LogWarning("Property with id={Id} not found", id);
                    return NotFound();
                }

                return View("PropertyDetails", property);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Home/PropertyDetailsPost for id={Id}", id);
                throw;
            }
        }

        private smtpModel LoadSmtpConfig()
        {
            var cfg = _configuration.GetSection("SmtpSettings").Get<smtpModel>();
            if (cfg == null)
            {
                _logger.LogError("SmtpSettings not found in appsettings.json");
                throw new Exception("SmtpSettings یافت نشد در appsettings.json");
            }
            return cfg;
        }

        [HttpPost]
        public IActionResult SendEmail(LayoutModel layoutModel)
        {
            _logger.LogInformation("Home/SendEmail called with Email={Email}", layoutModel.EmailModel?.Massege);

            if (string.IsNullOrWhiteSpace(layoutModel.EmailModel?.Massege))
            {
                _logger.LogWarning("Empty message in SendEmail from Email={Email}", layoutModel.EmailModel?.Massege);
                ModelState.AddModelError("EmailModel.Message", "لطفا پیام خود را وارد کنید!");
                return View("Index", layoutModel);
            }

            try
            {
                var cfg = LoadSmtpConfig();

                var mail = new MailMessage
                {
                    From = new MailAddress(cfg.EmailUser),
                    Subject = "پیام از وبسایت",
                    Body = layoutModel.EmailModel.Massege
                };

                mail.To.Add("erfan.joghataei2020@gmail.com");

                using var smtp = new SmtpClient(cfg.EmailHost, cfg.EmailPort)
                {
                    Credentials = new NetworkCredential(cfg.EmailUser, cfg.EmailPass),
                    EnableSsl = true
                };
                smtp.Send(mail);

                _logger.LogInformation("Email sent successfully from Email={Email}", layoutModel.EmailModel?.Massege);
                ViewBag.Result = "ایمیل ارسال شد";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending email from Email={Email}", layoutModel.EmailModel?.Massege);
                ViewBag.Result = "خطا در ارسال ایمیل: " + ex.Message;
            }

            return View("Index", layoutModel);
        }
    }
}
