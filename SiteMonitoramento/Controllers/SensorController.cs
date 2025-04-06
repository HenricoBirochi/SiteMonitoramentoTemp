using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.Collections.Generic;

namespace SiteMonitoramento.Controllers
{
    public class SensorController : Controller
    {
        public IActionResult ListaSensores()
        {
            SensorDAO dao = new SensorDAO();
            var sensores = dao.ListagemSensoresTipoSensoresJoin();
            return View(sensores);
        }
        public IActionResult InserirSensor()
        {
            try
            {
                ViewBag.Operacao = "I";
                Sensor sensor = new Sensor();
                SensorDAO dao = new SensorDAO();
                sensor.SensorId = dao.ProximoId();

                PreparaListaTipoSensoresParaCombo();
                return View("CadastroSensor", sensor);
            }
            catch (Exception erro)
            {
                return View("Erro", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult EditarSensor(int SensorId)
        {
            try
            {
                ViewBag.Operacao = "E";
                Sensor sensor = new Sensor();
                SensorDAO dao = new SensorDAO();
                sensor = dao.Consulta(SensorId);

                PreparaListaTipoSensoresParaCombo();
                return View("CadastroSensor", sensor);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Salvar(Sensor sensor, char Operacao)
        {
            try
            {
                SensorDAO dao = new SensorDAO();
                ValidaDados(sensor);
                if (ModelState.IsValid)
                {
                    if (Operacao == 'I')
                        dao.Inserir(sensor);
                    else
                        dao.Alterar(sensor);


                    //aqui eu pucho a lista dos sensore pra ele conseguir retornar a página que mostra os sensores
                    var sensores = dao.ListagemSensoresTipoSensoresJoin();
                    return View("ListaSensores", sensores);
                }
                else
                {
                    PreparaListaTipoSensoresParaCombo();
                    ViewBag.Operacao = Operacao;
                    return View("CadastroSensor", sensor);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult DeletarSensor(int SensorId)
        {
            SensorDAO dao = new SensorDAO();
            dao.Excluir(SensorId);

            var sensores = dao.ListagemSensoresTipoSensoresJoin();
            return View("ListaSensores", sensores);
        }
        private void ValidaDados(Sensor sensor)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            SensorDAO dao = new SensorDAO();
            if (string.IsNullOrEmpty(sensor.SensorNome))
                ModelState.AddModelError("SensorNome", "Preencha o nome.");
        }
        private void PreparaListaTipoSensoresParaCombo()
        {
            TipoSensorDAO dao = new TipoSensorDAO();
            var tipoSensores = dao.Listagem();
            List<SelectListItem> listaTiposDeSensores = new List<SelectListItem>();

            listaTiposDeSensores.Add(new SelectListItem("Selecione um tipo de Sensor...", "0"));
            foreach (var tipoSensor in tipoSensores)
            {
                SelectListItem item = new SelectListItem(tipoSensor.NomeTecnico, tipoSensor.TipoSensorId.ToString());
                listaTiposDeSensores.Add(item);
            }
            ViewBag.TipoSensores = listaTiposDeSensores;
        }
    }
}
