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
                if (Operacao == 'I')
                    dao.Inserir(tipoSensor);
                else
                    dao.Alterar(tipoSensor);

                //aqui eu pucho a lista dos sensore pra ele conseguir retornar a página que mostra os sensores
                var tipoSensores = dao.Listagem();
                return View("ListaTipoSensores", tipoSensores);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult DeletarSensor(int TipoSensorId)
        {
            TipoSensorDAO dao = new TipoSensorDAO();
            dao.Excluir(TipoSensorId);

            var tipoSensores = dao.Listagem();
            return View("ListaSensores", tipoSensores);
        }
    }
}
