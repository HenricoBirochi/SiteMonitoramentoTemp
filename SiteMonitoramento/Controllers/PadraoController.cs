using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

namespace SiteMonitoramento.Controllers
{
    public class PadraoController : Controller
    {
        protected bool ExigeAutenticacao { get; set; } = true;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Essa parte serve para mostrar a foto do usuário em todas as views
            int id = Convert.ToInt32(HttpContext.Session.GetString("Id"));
            UsuarioDAO dao = new UsuarioDAO();
            Usuario user = dao.Consulta(id);
            if (user != null)
                ViewBag.Usuario = user;

            //Essa parte serve para obrigar
            if (ExigeAutenticacao && !HelperController.VerificaUserLogado(HttpContext.Session))
                context.Result = RedirectToAction("Login", "Usuario");
            else
            {
                ViewBag.Logado = true;
                base.OnActionExecuting(context);
            }            
        }
    }
}
