using Microsoft.AspNetCore.Mvc;

namespace SiteMonitoramento.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
