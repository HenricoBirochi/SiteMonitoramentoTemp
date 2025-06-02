using System;

namespace SiteMonitoramento.Models
{
    public class UsuarioDispositivoDTO
    {
        //Esta classe apenas serve para fazer o select com join que esta dentro da classe UsuarioSensorDAO
        public UsuarioDispositivo UsuarioDispositivo { get; set; }//Ela vai facilitar esse processo dessa select com join
        public string UsuarioNome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public byte[] ImagemEmByte { get; set; } //Essa imagem vai ser armazenada no banco de dados em bytes
                                                 //isso porque os bytes ocupam menos espaço na memória do pc
        public string ImagemEmBase64 //Essa imagem vai ser exibida no front
        {
            get
            {
                if (ImagemEmByte != null)
                    return Convert.ToBase64String(ImagemEmByte);
                return string.Empty;
            }
        }
        public string DispositivoNome { get; set; }
    }
}
