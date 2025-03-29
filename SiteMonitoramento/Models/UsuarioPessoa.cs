using SiteMonitoramento.Models.Interfaces;

namespace SiteMonitoramento.Models
{
    public class UsuarioPessoa : IUsuario
    {
        public int UsuarioPessoaId { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
    }
}
