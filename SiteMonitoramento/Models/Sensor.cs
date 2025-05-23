namespace SiteMonitoramento.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string SensorNome { get; set; }
        public int TipoSensorId { get; set; }
        public int AmbienteId { get; set; }
    }
}