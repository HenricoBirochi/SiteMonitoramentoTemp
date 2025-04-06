using System.Data;
using Microsoft.Data.SqlClient;

namespace SiteMonitoramento.DAO
{
    public class HelperDAO
    {
        //public static void ExecutaSQL(string sql, SqlParameter[] parametros)
        //{
        //    using (SqlConnection cnx = ConexaoBD.GetConexao())
        //    {
        //        using (SqlCommand cmd = new SqlCommand(sql, cnx))
        //        {
        //            if (parametros != null)
        //                cmd.Parameters.AddRange(parametros);
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        //public static DataTable ExecutaSelect(string sql, SqlParameter[] parametros)
        //{
        //    using (SqlConnection cnx = ConexaoBD.GetConexao())
        //    {
        //        using (SqlDataAdapter adapter = new SqlDataAdapter(sql, cnx))
        //        {
        //            if (parametros != null)
        //                adapter.SelectCommand.Parameters.AddRange(parametros);
        //            DataTable tabela = new DataTable();
        //            adapter.Fill(tabela);
        //            return tabela;
        //        }
        //    }
        //}
        public static void ExecutaProc(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection cnx = ConexaoBD.GetConexao())
            {
                using (SqlCommand comando = new SqlCommand(nomeProc, cnx))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        comando.Parameters.AddRange(parametros);
                    comando.ExecuteNonQuery();
                }
            }
        }
        public static DataTable ExecutaProcSelect(string nomeProc, SqlParameter[] parametros)
        {
            using (SqlConnection cnx = ConexaoBD.GetConexao())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(nomeProc, cnx))
                {
                    if (parametros != null)
                        adapter.SelectCommand.Parameters.AddRange(parametros);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    return tabela;
                }
            }
        }
    }
}
