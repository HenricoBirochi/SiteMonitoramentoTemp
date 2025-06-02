using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;
using System.Globalization;
using System.Data.SqlTypes;

namespace SiteMonitoramento.Controllers
{
    public class MedidaController : PadraoController
    {
        public MedidaController()
        {
            ExigeAutenticacao = true;
        }

        public async Task<IActionResult> ConsultaEAdicionaMedidas(int dispositivoId)
        {
            string domain = "ec2-13-218-19-179.compute-1.amazonaws.com";
            string url = $"http://{domain}:8666/STH/v1/contextEntities/type/Temp/id/urn:ngsi-ld:Temp:{dispositivoId}/attributes/temperature?lastN=50";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
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
                        throw new Exception($"Erro ao criar dispositivo: {errorDetails}");
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    using (JsonDocument doc = JsonDocument.Parse(json))
                    {
                        var root = doc.RootElement;

                        var values = root
                            .GetProperty("contextResponses")[0]
                            .GetProperty("contextElement")
                            .GetProperty("attributes")[0]
                            .GetProperty("values");

                        // Inverte o array
                        foreach (var item in values.EnumerateArray().Reverse())
                        {
                            double valor = item.GetProperty("attrValue").GetDouble();
                            string dataString = item.GetProperty("recvTime").GetString();
                            DateTime data = DateTime.Parse(dataString, null, DateTimeStyles.RoundtripKind);
                            data = data.ToLocalTime();

                            var dao = new MedidaDAO();
                            int medidaId = dao.ProximoId();
                            var medidaBanco = dao.Consulta(medidaId);

                            if (medidaBanco != null && medidaBanco.HorarioMedicao == data)
                                continue;

                            var medida = new Medida
                            {
                                DispositivoId = dispositivoId,
                                ValorMedido = valor,
                                HorarioMedicao = data,
                                MedidaId = medidaId,
                            };
                            dao.Inserir(medida);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Dispositivo");
        }
        public IActionResult ObtemDadosConsultaAvancada(double valorMedido, DateTime dataInicial, DateTime dataFinal)
        {
            try
            {
                MedidaDAO dao = new MedidaDAO();
                if (dataInicial.Date == Convert.ToDateTime("01/01/0001"))
                    dataInicial = SqlDateTime.MinValue.Value;
                if (dataFinal.Date == Convert.ToDateTime("01/01/0001"))
                    dataFinal = SqlDateTime.MaxValue.Value;
                var lista = dao.ConsultaAvancadaMedidas(valorMedido, dataInicial, dataFinal);
                return PartialView("pvGridMedidas", lista);
            }
            catch (Exception erro)
            {
                return Json(new { erro = true, msg = erro.Message });
            }
        }
    }
}
