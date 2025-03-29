using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.DAO
{
    public class UsuarioPessoaDAO
    {
        private SqlParameter[] CriaParametros(UsuarioPessoa usuarioPessoa)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("usuarioPessoaId", usuarioPessoa.UsuarioPessoaId);
            parametros[1] = new SqlParameter("nome", usuarioPessoa.Nome);
            parametros[2] = new SqlParameter("senha", usuarioPessoa.Senha);
            parametros[3] = new SqlParameter("email", usuarioPessoa.Email);
            parametros[4] = new SqlParameter("cpf", usuarioPessoa.CPF);
            return parametros;
        }
        private UsuarioPessoa MontaUsuarioPessoa(DataRow registro)
        {
            UsuarioPessoa uP = new UsuarioPessoa();
            uP.UsuarioPessoaId = Convert.ToInt32(registro["usuarioPessoaId"]);
            uP.Nome = registro["nome"].ToString();
            uP.Senha = registro["senha"].ToString();
            uP.Email = registro["email"].ToString();
            uP.CPF = registro["cpf"].ToString();
            return uP;
        }

        public void Inserir(UsuarioPessoa usuarioPessoa)
        {
            string sql =
            "insert into UsuarioPessoas (usuarioPessoaId, nome, senha, email, cpf)" +
            "values ( @usuarioPessoaId, @nome, @senha, @email, @cpf)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioPessoa));
        }
        public void Alterar(UsuarioPessoa usuarioPessoa)
        {
            string sql =
            "update UsuarioPessoas set nome = @nome, " +
            "senha = @senha, " +
            "email = @email," +
            "cpf = @cpf where usuarioPessoaId = @usuarioPessoaId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioPessoa));
        }
        public void Excluir(int id)
        {
            string sql = "delete UsuarioPessoas where usuarioPessoaId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public UsuarioPessoa Consulta(int id)
        {
            string sql = "select * from UsuarioPessoas where usuarioPessoaId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUsuarioPessoa(tabela.Rows[0]);
        }
        public List<UsuarioPessoa> Listagem()
        {
            List<UsuarioPessoa> lista = new List<UsuarioPessoa>();
            string sql = "select * from UsuarioPessoas order by nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioPessoa(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(usuarioPessoaId) +1, 1) as 'MAIOR' from UsuarioPessoas";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
