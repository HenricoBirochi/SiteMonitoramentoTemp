using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

namespace SiteMonitoramento.Controllers
{
    public class DispositivoController : PadraoCrudeController<Dispositivo>
    {
        public DispositivoController()
        {
            DAO = new DispositivoDAO();
            GeraProximoId = true;
            NomeViewIndex = "Dispositivos";
            NomeViewForm = "CadastroDispositivo";
        }
        public IActionResult VisualizarDispositivo(int id)
        {
            try
            {
                DispositivoDAO dao = new DispositivoDAO();
                var listaDispositivoMedidas = dao.ListagemDispositivoMedidasJoin(id);
                return View(listaDispositivoMedidas);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        protected override void PreencheDadosParaView(string Operacao, Dispositivo model)
        {
            if (GeraProximoId && Operacao == "I")
                model.DispositivoId = DAO.ProximoId();
        }
        protected override void ValidaDados(Dispositivo model, string operacao)
        {
            ModelState.Clear();
            if (string.IsNullOrEmpty(model.DispositivoNome))
                ModelState.AddModelError("DispositivoNome", "O nome do dispositivo precisa ser preenchido");
        }

    }
}
