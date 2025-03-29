namespace SiteMonitoramento.Models.Interfaces
{
    public interface IUsuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
    }
}
