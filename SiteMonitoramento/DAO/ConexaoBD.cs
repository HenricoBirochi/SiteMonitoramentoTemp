using Microsoft.Data.SqlClient;

namespace SiteMonitoramento.DAO
{
    public class ConexaoBD
    {
        public static SqlConnection GetConexao()
        {
            string strCon = "Data Source=LOCALHOST;Initial Catalog=HephaiTech;User Id=sa;Password=Password123!;TrustServerCertificate=True;";
            SqlConnection conexao = new SqlConnection(strCon);
            conexao.Open();
            return conexao;
        }
    }
}
