using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace SiteMonitoramento.Controllers
{
    public class TipoSensorController : PadraoCrudeController<TipoSensor>
    {
        public TipoSensorController()
        {
            DAO = new TipoSensorDAO();
            GeraProximoId = true;
            NomeViewIndex = "ListaTipoSensores";
            NomeViewForm = "CadastroTipoSensor";
        }

        protected override void PreencheDadosParaView(string Operacao, TipoSensor model)
        {
            if (GeraProximoId && Operacao == "I")
                model.TipoSensorId = DAO.ProximoId();
        }

        protected override void ValidaDados(TipoSensor model, string operacao)
        {
            ModelState.Clear(); // limpa os erros criados automaticamente pelo Asp.net (que podem estar com msg em inglês)
            TipoSensorDAO dao = new TipoSensorDAO();
            if (string.IsNullOrEmpty(model.NomeTecnico))
                ModelState.AddModelError("NomeTecnico", "Preencha o nome tecnico.");
            if (string.IsNullOrEmpty(model.ParametroMedido))
                ModelState.AddModelError("ParametroMedido", "Preencha o nome do parâmetro.");
            if (operacao == "I" && DAO.Consulta(model.TipoSensorId) != null)
                ModelState.AddModelError("Id", "Código já está em uso!");
            if (operacao == "A" && DAO.Consulta(model.TipoSensorId) == null)
                ModelState.AddModelError("Id", "Este registro não existe!");
            if (model.TipoSensorId <= 0)
                ModelState.AddModelError("Id", "Id inválido!");
        }
    }
}
