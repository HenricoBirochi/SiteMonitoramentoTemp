using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

namespace SiteMonitoramento.Controllers
{
    public class MedidaController : PadraoController
    {
        public MedidaController()
        {
            ExigeAutenticacao = true;
        }
    }
}
