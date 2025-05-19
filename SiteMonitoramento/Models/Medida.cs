using System;

namespace SiteMonitoramento.Models
{
    public class Medida
    {
        public int MedidaId { get; set; }
        public double ValorMedido { get; set; }
        public DateTime HorarioMedicao { get; set; }
        public string Parametro { get; set; }

        public int SensorId { get; set; }
    }
}
