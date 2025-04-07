using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System.Collections.Generic;
using System;

namespace SiteMonitoramento.Controllers
{
    public class TipoSensorController : Controller
    {
        public IActionResult ListaTipoSensores()
        {
            TipoSensorDAO dao = new TipoSensorDAO();
            var tipoSensores = dao.Listagem();
            return View(tipoSensores);
        }
        public IActionResult InserirTipoSensor()
        {
            try
            {
                ViewBag.Operacao = "I";
                TipoSensor tipoSensor = new TipoSensor();
                TipoSensorDAO dao = new TipoSensorDAO();
                tipoSensor.TipoSensorId = dao.ProximoId();

                return View("CadastroTipoSensor", tipoSensor);
            }
            catch (Exception erro)
            {
                return View("Erro", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult EditarTipoSensor(int TipoSensorId)
        {
            try
            {
                ViewBag.Operacao = "E";
                TipoSensor tipoSensor = new TipoSensor();
                TipoSensorDAO dao = new TipoSensorDAO();
                tipoSensor = dao.Consulta(TipoSensorId);

                return View("CadastroTipoSensor", tipoSensor);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Salvar(TipoSensor tipoSensor, char Operacao)
        {
            try
            {
                TipoSensorDAO dao = new TipoSensorDAO();
                ValidaDados(tipoSensor);
                if (ModelState.IsValid)
                {
                    if (Operacao == 'I')
                        dao.Inserir(tipoSensor);
                    else
                        dao.Alterar(tipoSensor);

                    //aqui eu pucho a lista dos sensore pra ele conseguir retornar a página que mostra os sensores
                    var tipoSensores = dao.Listagem();
                    return View("ListaTipoSensores", tipoSensores);
                }
                else
                {
                    ViewBag.Operacao = Operacao;
                    return View("CadastroTipoSensor", tipoSensor);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult DeletarTipoSensor(int TipoSensorId)
        {
            TipoSensorDAO dao = new TipoSensorDAO();
            dao.Excluir(TipoSensorId);

            var tipoSensores = dao.Listagem();
            return View("ListaTipoSensores", tipoSensores);
        }
        private void ValidaDados(TipoSensor tipoSensor)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            TipoSensorDAO dao = new TipoSensorDAO();
            if (string.IsNullOrEmpty(tipoSensor.NomeTecnico))
                ModelState.AddModelError("NomeTecnico", "Preencha o nome tecnico.");
            if (string.IsNullOrEmpty(tipoSensor.ParametroMedido))
                ModelState.AddModelError("ParametroMedido", "Preencha o nome do parâmetro.");
        }
    }
}
