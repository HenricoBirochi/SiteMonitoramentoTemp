using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

namespace SiteMonitoramento.Controllers
{
    public class AmbienteController : PadraoCrudeController<Ambiente>
    {
        public AmbienteController()
        {
            DAO = new AmbienteDAO();
            GeraProximoId = true;
            NomeViewIndex = "Ambientes";
            NomeViewForm = "CadastroAmbiente";
        }
        public IActionResult VisualizarAmbiente(int id)
        {
            try
            {
                AmbienteDAO dao = new AmbienteDAO();
                var listaSensoresAmbiente = dao.ListagemAmbienteSensoresTipoSensoresJoin(id);
                return View(listaSensoresAmbiente);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected override void PreencheDadosParaView(string Operacao, Ambiente model)
        {
            if (GeraProximoId && Operacao == "I")
                model.AmbienteId = DAO.ProximoId();
        }
        protected override void ValidaDados(Ambiente model, string operacao)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.AmbienteNome))
                ModelState.AddModelError("AmbienteNome", "O nome do ambiente precisa ser preenchido");
        }

    }
}
