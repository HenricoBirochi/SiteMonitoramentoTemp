
using Microsoft.AspNetCore.Http;
using System;

namespace SiteMonitoramento.Models
{
    public class Usuario 
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }

        public IFormFile Imagem { get; set; } //Essa imagem é a que você vai receber do front

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
    }
}
