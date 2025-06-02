using Newtonsoft.Json;
using System;

namespace SiteMonitoramento.Models
{
    public class Medida
    {
        public int MedidaId { get; set; }

        [JsonProperty("attrValue")]
        public double ValorMedido { get; set; }

        [JsonProperty("recvTime")]
        public DateTime HorarioMedicao { get; set; }

        public int DispositivoId { get; set; }
    }
}
