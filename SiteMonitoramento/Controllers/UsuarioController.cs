using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            Usuario usuario = new Usuario();
            UsuarioDAO dao = new UsuarioDAO();
            usuario.UsuarioId = dao.ProximoId();
            return View(usuario);
        }
        public IActionResult Salvar(Usuario usuario)
        {
            UsuarioDAO dao = new UsuarioDAO();
            dao.Inserir(usuario);
            return RedirectToAction("Index");
        }
    }
}
