namespace SiteMonitoramento.Models
{
    public class UsuarioSensorDTO
    {
        //Esta classe apenas serve para fazer o select com join que esta dento da classe UsuarioSensorDAO
        public UsuarioSensor UsuarioSensor { get; set; }//Ela vai facilitar esse processo dessa select com join
        public string UsuarioNome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string SensorNome { get; set; }
        public int TipoSensorId { get; set; }
    }
}
