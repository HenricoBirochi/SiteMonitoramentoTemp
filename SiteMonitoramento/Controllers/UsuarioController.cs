using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SiteMonitoramento.Controllers
{
    public class UsuarioController : PadraoController
    {
        public UsuarioController()
        {
            ExigeAutenticacao = false;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Cadastro()
        {
            try
            {
                ViewBag.Operacao = "I";
                Usuario usuario = new Usuario();
                UsuarioDAO dao = new UsuarioDAO();
                usuario.UsuarioId = dao.ProximoId();
                return View(usuario);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Editar(int id)
        {
            try
            {
                ViewBag.Operacao = "A";
                UsuarioDAO dao = new UsuarioDAO();
                var usuario = dao.Consulta(id);
                if (usuario != null)
                    return View("Cadastro", usuario);
                return View("Index", "Home");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Salvar(Usuario usuario, string operacao)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                ValidaDados(usuario, operacao);
                if (ModelState.IsValid)
                {
                    if (operacao == "I")
                    {
                        dao.Inserir(usuario);
                        return RedirectToAction("Index", "Home");
                    }
                    dao.Alterar(usuario);
                    return RedirectToAction("Index", "Home");
                }
                if (operacao == "I")
                {
                    ViewBag.Operacao = "I";
                    return View("Cadastro", usuario);
                }
                ViewBag.Operacao = "A";
                return View("Cadastro", usuario);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                UsuarioDAO dao = new UsuarioDAO();
                dao.Delete(id);
                HttpContext.Session.Clear();
                return RedirectToAction("Cadastro");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        /// <summary>
        /// Converte a imagem recebida no form em um vetor de bytes
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByte(IFormFile file)
        {
            if (file != null)
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray();
                }
            else
                return null;
        }
        private void ValidaDados(Usuario usuario, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            UsuarioDAO dao = new UsuarioDAO();

            //Verificando se já existe algum usuário com esse email, Nome e CPF
            var usuarios = dao.Listagem();
            Usuario usuarioExistente = usuarios.Find(u => u.Email == usuario.Email);
            if (usuarioExistente != null && usuarioExistente.UsuarioId != usuario.UsuarioId)
                ModelState.AddModelError("Email", "Esse email já está cadastrado");
            usuarioExistente = usuarios.Find(u => u.UsuarioNome == usuario.UsuarioNome);
            if (usuarioExistente != null && usuarioExistente.UsuarioId != usuario.UsuarioId)
                ModelState.AddModelError("UsuarioNome", "Esse nome de usuário já existe");
            usuarioExistente = usuarios.Find(u => u.CPF == usuario.CPF);
            if (usuarioExistente != null && usuarioExistente.UsuarioId != usuario.UsuarioId)
                ModelState.AddModelError("CPF", "Esse CPF já existe");

            //Verificando se a senha tem pelo menos 8 caracteres, se ela não é nula, se ela tem um caracter especial e se ela tem uma letara maiúscula
            if (string.IsNullOrEmpty(usuario.Senha) || usuario.Senha.Length < 8)
                ModelState.AddModelError("Senha", "Coloque pelo menos 8 caracteres na senha.");
            if (!Regex.IsMatch(usuario.Senha, "[A-Z]"))
                ModelState.AddModelError("Senha", "A senha deve conter pelo menos uma letra maiúscula.");
            if (!Regex.IsMatch(usuario.Senha, "[^a-zA-Z0-9]"))
                ModelState.AddModelError("Senha", "A senha deve conter pelo menos um caractere especial.");

            //Verificando se os campos estão vazios
            if (string.IsNullOrEmpty(usuario.UsuarioNome))
                ModelState.AddModelError("UsuarioNome", "Preencha o nome do usuário.");
            if (string.IsNullOrEmpty(usuario.Email))
                ModelState.AddModelError("Email", "Preencha o email corretamente.");
            if (string.IsNullOrEmpty(usuario.CPF))
                ModelState.AddModelError("CPF", "Preencha o CPF corretamente.");

            //Imagem será obrigatio apenas na inclusão.
            //Na alteração iremos considerar a que já estava salva.
            if (usuario.Imagem == null && operacao == "I")
                ModelState.AddModelError("Imagem", "Escolha uma imagem.");
            if (usuario.Imagem != null && usuario.Imagem.Length / 1024 / 1024 >= 2)
                ModelState.AddModelError("Imagem", "Imagem limitada a 2 mb.");
            if (ModelState.IsValid)
            {
                //na alteração, se não foi informada a imagem, iremos manter a que já estava salva.
                if (operacao == "A" && usuario.Imagem == null)
                {
                    Usuario user = dao.Consulta(usuario.UsuarioId);
                    usuario.ImagemEmByte = user.ImagemEmByte;
                }
                else
                {
                    usuario.ImagemEmByte = ConvertImageToByte(usuario.Imagem);
                }
            }
        }

        //Login e LogOff
        public IActionResult FazLogin(string email, string senha)
        {
            UsuarioDAO dao = new UsuarioDAO();
            var listaUsuario = dao.Listagem();
            foreach (var usuario in listaUsuario)
            {
                if (email == usuario.Email && senha == usuario.Senha)
                {
                    HttpContext.Session.SetString("Id", usuario.UsuarioId.ToString());
                    return RedirectToAction("index", "Home");
                }
            }
            ViewBag.Erro = "Usuário ou senha inválidos!";
            return View("Login");
        }
        public IActionResult LogOff()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
