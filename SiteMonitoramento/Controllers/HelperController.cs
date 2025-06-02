using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SiteMonitoramento.Controllers
{
    public class HelperController : Controller
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Id");
            if (logado == null)
                return false;
            else
                return true;
        }
    }
}
