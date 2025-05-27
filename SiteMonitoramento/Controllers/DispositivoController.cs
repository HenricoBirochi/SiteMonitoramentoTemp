using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.Net;
using System.Net.Http;

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

        public override IActionResult Save(Dispositivo model, string Operacao)
        {
            try
            {

                ValidaDados(model, Operacao);
                if (ModelState.IsValid == false)
                {
                    ViewBag.Operacao = Operacao;
                    PreencheDadosParaView(Operacao, model);
                    return View(NomeViewForm, model);
                }
                else
                {
                    if (Operacao == "I")
                    {
                        DAO.Inserir(model);
                        CriaDispositivoMongo();
                    }
                    else
                        DAO.Alterar(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }
        public IActionResult CriaDispositivoMongo()
        {
            var dao = new DispositivoDAO();
            int idUltimoDisp = dao.ProximoId();
            string domain = "localhost:8080"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:4041/iot/devices";
            var content = new StringContent("{\n  \"devices\": [\n    {\n      \"device_id\": \"temp" + idUltimoDisp + "\",         \n      \"entity_name\": \"urn:ngsi-ld:Temp:" + idUltimoDisp + "\",   \n      \"entity_type\": \"Temp\",          \n      \"protocol\": \"PDI-IoTA-UltraLight\",  \n      \"transport\": \"MQTT\",            \n\n      \n      \"commands\": [\n        { \"name\": \"on\", \"type\": \"command\" },  \n        { \"name\": \"off\", \"type\": \"command\" }  \n      ],\n\n      \n      \"attributes\": [\n        { \"object_id\": \"s\", \"name\": \"state\", \"type\": \"Text\" }, \n        { \"object_id\": \"t\", \"name\": \"temperature\", \"type\": \"Number\" }  \n      ]\n    }\n  ]\n}\n", null, "application/json");
            
            //var handler = new HttpClientHandler();
            using (var httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("fiware-service", "smart");
                request.Headers.Add("fiware-servicepath", "/");
                request.Content = content;
                using (var response = httpClient.SendAsync(request).Result)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string resposta = response.Content.ReadAsStringAsync().Result;
                        return Content(resposta);
                    }
                    else
                    {
                        throw new Exception("Erro ao consultar. Code: " + response.StatusCode);
                    }
                }
            }
        }
    }
}
