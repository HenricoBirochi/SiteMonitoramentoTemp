using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

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
            try
            {
                Usuario usuario = new Usuario();
                UsuarioDAO dao = new UsuarioDAO();
                usuario.UsuarioId = dao.ProximoId();
                return View(usuario);
            }
            catch (Exception erro)
            {
                return View("erro", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Salvar(Usuario usuario)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                ValidaDados(usuario);
                if (ModelState.IsValid)
                {
                    dao.Inserir(usuario);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception erro)
            {
                return View("erro", new ErrorViewModel(erro.ToString()));
            }
        }
        private void ValidaDados(Usuario usuario)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            UsuarioDAO dao = new UsuarioDAO();
            if (string.IsNullOrEmpty(usuario.UsuarioNome))
                ModelState.AddModelError("UsuarioNome", "Preencha o nome do usuário.");
            if (string.IsNullOrEmpty(usuario.Senha) && usuario.Senha.Length < 8)
                ModelState.AddModelError("Senha", "Preencha a senha corretamente.");
            if (string.IsNullOrEmpty(usuario.Email))
                ModelState.AddModelError("Email", "Preencha o email corretamente.");
            if (string.IsNullOrEmpty(usuario.CPF))
                ModelState.AddModelError("CPF", "Preencha o CPF corretamente.");
        }

        //Login e LogOff
        public IActionResult FazLogin(string email, string senha)
        {
            //Este é apenas um exemplo, aqui você deve consultar na sua tabela de usuários
            //se existe esse usuário e senha
            if (email == "admin" && senha == "1234")
            {
                HttpContext.Session.SetString("Logado", "true");
                return RedirectToAction("index", "Home");
            }
            else
            {
                ViewBag.Erro = "Usuário ou senha inválidos!";
                return View("Index");
            }
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
