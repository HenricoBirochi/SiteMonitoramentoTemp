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
            string sql =
            "insert into Usuarios (usuarioId, usuarioNome, senha, email, cpf)" +
            "values ( @usuarioId, @usuarioNome, @senha, @email, @cpf)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }
        public void Alterar(Usuario usuario)
        {
            string sql =
            "update Usuarios set usuarioNome = @usuarioNome, " +
            "senha = @senha, " +
            "email = @email," +
            "cpf = @cpf where usuarioId = @usuarioId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuario));
        }
        public void Excluir(int id)
        {
            string sql = "delete Usuarios where usuarioId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public Usuario Consulta(int id)
        {
            string sql = "select * from Usuarios where usuarioId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUsuario(tabela.Rows[0]);
        }
        public List<Usuario> Listagem()
        {
            List<Usuario> lista = new List<Usuario>();
            string sql = "select * from Usuarios order by usuarioNome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuario(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(usuarioId) +1, 1) as 'MAIOR' from Usuarios";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
