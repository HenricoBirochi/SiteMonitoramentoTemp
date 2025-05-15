using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SiteMonitoramento.Controllers
{
    public class SensorController : PadraoCrudeController<Sensor>
    {
        public SensorController()
        {
            DAO = new SensorDAO();
            GeraProximoId = true;
            NomeViewIndex = "ListaSensores";
            NomeViewForm = "CadastroSensor";
        }

        public override IActionResult Index()
        {
            SensorDAO dao = new SensorDAO();
            var sensores = dao.ListagemSensoresTipoSensoresJoin();
            return View("ListaSensores", sensores);
        }
        protected override void PreencheDadosParaView(string Operacao, Sensor model)
        {
            if (GeraProximoId && Operacao == "I")
                model.SensorId = DAO.ProximoId();
            PreparaListaTipoSensoresParaCombo();
        }
        public override IActionResult Save(Sensor sensor, string Operacao)
        {
            try
            {
                SensorDAO dao = new SensorDAO();
                ValidaDados(sensor, Operacao);
                if (ModelState.IsValid)
                {
                    if (Operacao == "I")
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
        protected override void ValidaDados(Sensor model, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            SensorDAO dao = new SensorDAO();
            if (operacao == "I" && DAO.Consulta(model.SensorId) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.SensorId) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.SensorId <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
            if (string.IsNullOrEmpty(model.SensorNome))
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
