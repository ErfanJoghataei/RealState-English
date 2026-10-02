// FILE: Controllers/AboutController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // LOG ADDED
using System;

namespace RealState.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly ILogger<AboutController> _logger; // LOG ADDED

        public AboutController(ILogger<AboutController> logger) // LOG ADDED
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult About()
        {
            var userId = User.Identity?.Name ?? "Anonymous"; // LOG ADDED
            _logger.LogInformation("➡️ Enter About Action | User: {UserId}", userId); // LOG ADDED

            try
            {
                var result = PartialView();
                _logger.LogInformation("✅ About Action executed successfully | User: {UserId}", userId); // LOG ADDED
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error in About Action | User: {UserId}", userId); // LOG ADDED
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
