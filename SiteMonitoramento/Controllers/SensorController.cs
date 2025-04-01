using Microsoft.AspNetCore.Mvc;

namespace SiteMonitoramento.Controllers
{
    public class SensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
