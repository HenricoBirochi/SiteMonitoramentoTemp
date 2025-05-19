using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.Controllers
{
    public class HomeController : PadraoController
    {
        public HomeController()
        {
            ExigeAutenticacao = false;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Sobre()
        {
            return View();
        }
        public IActionResult PrototipoDashboards()
        {
            return View();
        }
    }
}
