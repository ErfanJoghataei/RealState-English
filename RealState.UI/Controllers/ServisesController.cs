using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RealState.UI.Controllers
{
    public class ServisesController : Controller
    {
        private readonly ILogger<ServisesController> _logger;

        public ServisesController(ILogger<ServisesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Servises()
        {
            _logger.LogInformation("➡️ Servises GET called");
            return View("Servises");
        }
    }
}
