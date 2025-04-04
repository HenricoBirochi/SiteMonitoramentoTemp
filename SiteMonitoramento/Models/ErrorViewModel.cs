using System;

namespace SiteMonitoramento.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string erro)
        {
            Erro = erro;
        }
        public ErrorViewModel()
        {
            
        }
        public string Erro { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
