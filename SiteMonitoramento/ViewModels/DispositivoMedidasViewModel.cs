using System;

namespace SiteMonitoramento.ViewModels
{
    public class DispositivoMedidasViewModel
    {
        public string DispositivoNome { get; set; }
        public double ValorMedido { get; set; }
        public DateTime HorarioMedicao { get; set; }
    }
}
