using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.Models;

namespace SiteMonitoramento.DAO
{
    public class UsuarioEmpresaDAO
    {
        private SqlParameter[] CriaParametros(UsuarioEmpresa usuarioEmpresa)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("usuarioEmpresaId", usuarioEmpresa.UsuarioEmpresaId);
            parametros[1] = new SqlParameter("nome", usuarioEmpresa.Nome);
            parametros[2] = new SqlParameter("senha", usuarioEmpresa.Senha);
            parametros[3] = new SqlParameter("email", usuarioEmpresa.Email);
            parametros[4] = new SqlParameter("cnpj", usuarioEmpresa.CNPJ);
            return parametros;
        }
        private UsuarioEmpresa MontaUsuarioEmpresa(DataRow registro)
        {
            UsuarioEmpresa uE = new UsuarioEmpresa();
            uE.UsuarioEmpresaId = Convert.ToInt32(registro["usuarioEmpresaId"]);
            uE.Nome = registro["nome"].ToString();
            uE.Senha = registro["senha"].ToString();
            uE.Email = registro["email"].ToString();
            uE.CNPJ = registro["cnpj"].ToString();
            return uE;
        }

        public void Inserir(UsuarioEmpresa usuarioEmpresa)
        {
            string sql =
            "insert into UsuarioEmpresas (usuarioEmpresaId, nome, senha, email, cnpj)" +
            "values ( @usuarioEmpresaId, @nome, @senha, @email, @cnpj)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioEmpresa));
        }
        public void Alterar(UsuarioEmpresa usuarioEmpresa)
        {
            string sql =
            "update UsuarioEmpresas set nome = @nome, " +
            "senha = @senha, " +
            "email = @email," +
            "cnpj = @cnpj where usuarioEmpresaId = @usuarioEmpresaId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioEmpresa));
        }
        public void Excluir(int id)
        {
            string sql = "delete UsuarioEmpresas where usuarioEmpresaId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public UsuarioEmpresa Consulta(int id)
        {
            string sql = "select * from UsuarioEmpresas where usuarioEmpresaId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUsuarioEmpresa(tabela.Rows[0]);
        }
        public List<UsuarioEmpresa> Listagem()
        {
            List<UsuarioEmpresa> lista = new List<UsuarioEmpresa>();
            string sql = "select * from UsuarioEmpresas order by nome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioEmpresa(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(usuarioEmpresaId) +1, 1) as 'MAIOR' from UsuarioEmpresas";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
