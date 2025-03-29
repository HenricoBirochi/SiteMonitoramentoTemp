using SiteMonitoramento.Models.Interfaces;

namespace SiteMonitoramento.Models
{
    public class UsuarioEmpresa : IUsuario
    {
        public int UsuarioEmpresaId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CNPJ { get; set; }
    }
}
