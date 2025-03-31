using SiteMonitoramento.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class TipoSensorDAO
    {
        private SqlParameter[] CriaParametros(TipoSensor tipoSensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("tipoSensorId", tipoSensor.TipoSensorId);
            parametros[1] = new SqlParameter("nomeTecnico", tipoSensor.NomeTecnico);
            parametros[2] = new SqlParameter("parametroMedido", tipoSensor.ParametroMedido);
            return parametros;
        }
        private TipoSensor MontaTipoSensor(DataRow registro)
        {
            TipoSensor tS = new TipoSensor();
            tS.TipoSensorId = Convert.ToInt32(registro["tipoSensorId"]);
            tS.NomeTecnico = registro["nomeTecnico"].ToString();
            tS.ParametroMedido = registro["parametroMedido"].ToString();
            return tS;
        }

        public void Inserir(TipoSensor tipoSensor)
        {
            string sql =
            "insert into TipoSensores (tipoSensorId, nomeTecnico, parametroMedido)" +
            "values ( @tipoSensorId, @nomeTecnico, @parametroMedido)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(tipoSensor));
        }
        public void Alterar(TipoSensor tipoSensor)
        {
            string sql =
            "update Sensores set nomeTecnico = @nomeTecnico, " +
            "parametroMedido = @parametroMedido " +
            "where tipoSensorId = @tipoSensorId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(tipoSensor));
        }
        public void Excluir(int id)
        {
            string sql = "delete TipoSensores where tipoSensorId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public TipoSensor Consulta(int id)
        {
            string sql = "select * from TipoSensores where tipoSensorId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaTipoSensor(tabela.Rows[0]);
        }
        public List<TipoSensor> Listagem()
        {
            List<TipoSensor> lista = new List<TipoSensor>();
            string sql = "select * from TipoSensores order by nomeTecnico";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaTipoSensor(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(tipoSensorId) +1, 1) as 'MAIOR' from TipoSensores";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
