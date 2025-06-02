using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
                return View(id);
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

        #region Metodos Assincronos para trabalhar com a remoção e adição de dispositivos na API
        public async Task<IActionResult> SaveAssincrono(Dispositivo model, string Operacao)
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
                if (Operacao == "I")
                {
                    await CriaDispositivoMongo();
                    await CriaComandosDispositivo();
                    await CriaSubscribeTemperature();
                    DAO.Inserir(model);
                    return RedirectToAction("Index");
                }
                DAO.Alterar(model);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }

        }
        public async Task<IActionResult> DeleteAssincrono(int id)
        {
            try
            {
                await DeletaDispositivoIotAgent(id);
                await DeletaDispositivoOrion(id);
                DAO.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        #endregion

        #region Cria Dispositivo na API
        public async Task CriaDispositivoMongo()
        {
            var dao = new DispositivoDAO();
            int idUltimoDisp = dao.ProximoId();
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:4041/iot/devices";
            var content = new StringContent("{\n  \"devices\": [\n    {\n      \"device_id\": \"temp" + idUltimoDisp + "\",         \n      " +
                "\"entity_name\": \"urn:ngsi-ld:Temp:" + idUltimoDisp + "\",   \n      \"entity_type\": \"Temp\",          \n      \"protocol\": " +
                "\"PDI-IoTA-UltraLight\",  \n      \"transport\": \"MQTT\",            \n\n      \n      \"commands\": [\n        { \"name\": \"on\", " +
                "\"type\": \"command\" },  \n        { \"name\": \"off\", \"type\": \"command\" }  \n      ],\n\n      \n      \"attributes\": [\n        " +
                "{ \"object_id\": \"s\", \"name\": \"state\", \"type\": \"Text\" }, \n        { \"object_id\": \"t\", \"name\": \"temperature\", " +
                "\"type\": \"Number\" }  \n      ]\n    }\n  ]\n}\n", null, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");
            request.Content = content;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao criar dispositivo: {errorDetails}");
                    }
                }
            }
        }
        public async Task CriaComandosDispositivo()
        {
            var dao = new DispositivoDAO();
            int idUltimoDisp = dao.ProximoId();
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:1026/v2/registrations";
            var content = new StringContent("{\n  \"description\": \"Device Commands\", \n  \"dataProvided\": {\n    \"entities\": [\n      {\n        " +
                "\"id\": \"urn:ngsi-ld:Temp:" + idUltimoDisp + "\", \"type\": \"Temp\" \n      }\n    ],\n    \"attrs\": [\"on\", \"off\"] \n  },\n  \"provider\": " +
                "{\n    \"http\": { \"url\": \"http://" + domain + ":4041\" }, \n    \"legacyForwarding\": true \n  }\n}\n", null, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");
            request.Content = content;

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao criar dispositivo: {errorDetails}");
                    }
                }
            }
        }


        public async Task CriaSubscribeTemperature()
        {
            var dao = new DispositivoDAO();
            int idUltimoDisp = dao.ProximoId();
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:1026/v2/subscriptions";
            var content = new StringContent("{\r\n  \"description\": \"Notify STH-Comet of all Motion Sensor count changes\", \n  \"subject\": {\r\n    \"entities\": " +
                "[\r\n      {\r\n        \"id\": \"urn:ngsi-ld:Temp:" + idUltimoDisp + "\",\r\n        \"type\": \"Temp\"\r\n      }\r\n    ],\r\n    \"condition\": " +
                "{ \"attrs\": [\"temperature\"] } \n  },\r\n  \"notification\": {\r\n    \"http\": {\r\n      \"url\": \"http://" + domain + ":8666/notify\" \n    }," +
                "\r\n    \"attrs\": [\r\n      \"temperature\" \n    ],\r\n    \"attrsFormat\": \"legacy\" \n  }\r\n}", null, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");
            request.Content = content;

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao criar dispositivo: {errorDetails}");
                    }
                }
            }

        }
        #endregion

        #region Deleta Dispositivo na API
        public async Task DeletaDispositivoIotAgent(int id)
        {
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:4041/iot/devices/temp" + id;

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao deletar dispositivo: {errorDetails}");
                    }
                }
            }
        }
        public async Task DeletaDispositivoOrion(int id)
        {
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com"; //Depois mudar para o ip da VM na AWS
            string url = $"http://{domain}:1026/v2/entities/urn:ngsi-ld:Temp:" + id;

            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Add("fiware-service", "smart");
            request.Headers.Add("fiware-servicepath", "/");
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(30);
                using (var response = await httpClient.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        throw new Exception($"Erro ao deletar dispositivo: {errorDetails}");
                    }
                }
            }
        }
        #endregion
    }
}
