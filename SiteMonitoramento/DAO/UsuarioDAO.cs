using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.Models;
using System.Reflection;

namespace SiteMonitoramento.DAO
{
    public class UsuarioDAO : PadraoDAO<Usuario>
    {
        protected override SqlParameter[] CriaParametros(Usuario usuario)
        {
            object imgByte = usuario.ImagemEmByte;
            if (imgByte == null)
                imgByte = DBNull.Value;

            SqlParameter[] parametros = new SqlParameter[6];
            parametros[0] = new SqlParameter("usuarioId", usuario.UsuarioId);
            parametros[1] = new SqlParameter("usuarioNome", usuario.UsuarioNome);
            parametros[2] = new SqlParameter("senha", usuario.Senha);
            parametros[3] = new SqlParameter("email", usuario.Email);
            parametros[4] = new SqlParameter("cpf", usuario.CPF);
            parametros[5] = new SqlParameter("imagem", imgByte);
            return parametros;
        }
        protected override Usuario MontaModel(DataRow registro)
        {
            Usuario u = new Usuario();
            u.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            u.UsuarioNome = registro["usuarioNome"].ToString();
            u.Senha = registro["senha"].ToString();
            u.Email = registro["email"].ToString();
            u.CPF = registro["cpf"].ToString();
            if (registro["imagem"] != DBNull.Value)
                u.ImagemEmByte = registro["imagem"] as byte[];
            return u;
        }

        protected override void SetTabela()
        {
            Tabela = "Usuarios";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "usuarioId";
        }
    }
}