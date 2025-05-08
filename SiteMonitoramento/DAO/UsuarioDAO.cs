using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.DAO
{
    public class UsuarioDAO : PadraoDAO<Usuario>
    {
        protected override SqlParameter[] CriaParametros(Usuario usuario)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("usuarioId", usuario.UsuarioId);
            parametros[1] = new SqlParameter("usuarioNome", usuario.UsuarioNome);
            parametros[2] = new SqlParameter("senha", usuario.Senha);
            parametros[3] = new SqlParameter("email", usuario.Email);
            parametros[4] = new SqlParameter("cpf", usuario.CPF);
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
            return u;
        }
        public int ProximoId()
        {
            string sql = "spProximoId";
            SqlParameter[] parametros =
            {
                new SqlParameter("@tabela", "Usuarios"),
                new SqlParameter("@nomeDoCampoId", "usuarioId")
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
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