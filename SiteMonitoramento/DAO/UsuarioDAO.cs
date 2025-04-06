using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.DAO
{
    public class UsuarioDAO
    {
        private SqlParameter[] CriaParametros(Usuario usuario)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("usuarioId", usuario.UsuarioId);
            parametros[1] = new SqlParameter("usuarioNome", usuario.UsuarioNome);
            parametros[2] = new SqlParameter("senha", usuario.Senha);
            parametros[3] = new SqlParameter("email", usuario.Email);
            parametros[4] = new SqlParameter("cpf", usuario.CPF);
            return parametros;
        }
        private Usuario MontaUsuario(DataRow registro)
        {
            Usuario u = new Usuario();
            u.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            u.UsuarioNome = registro["usuarioNome"].ToString();
            u.Senha = registro["senha"].ToString();
            u.Email = registro["email"].ToString();
            u.CPF = registro["cpf"].ToString();
            return u;
        }

        public void Inserir(Usuario usuario)
        {
            string sql = "spInserirUsuario";
            HelperDAO.ExecutaProc(sql, CriaParametros(usuario));
        }

        public void Alterar(Usuario usuario)
        {
            string sql = "spAlterarUsuario";
            HelperDAO.ExecutaProc(sql, CriaParametros(usuario));
        }

        public void Excluir(int id)
        {
            string sql = "spExcluirUsuario";
            SqlParameter[] parametros = { new SqlParameter("@usuarioId", id) };
            HelperDAO.ExecutaProc(sql, parametros);
        }

        public Usuario Consulta(int id)
        {
            string sql = "spConsultarUsuario";
            SqlParameter[] parametros = { new SqlParameter("@usuarioId", id) };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUsuario(tabela.Rows[0]);
        }

        public List<Usuario> Listagem()
        {
            List<Usuario> lista = new List<Usuario>();
            string sql = "spListarUsuarios";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuario(registro));
            return lista;
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
    }
}