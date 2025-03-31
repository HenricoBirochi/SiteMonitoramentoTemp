using SiteMonitoramento.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class SensorDAO
    {
        private SqlParameter[] CriaParametros(Sensor sensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("sensorId", sensor.SensorId);
            parametros[1] = new SqlParameter("sensorNome", sensor.SensorNome);
            parametros[2] = new SqlParameter("tipoSensorId", sensor.TipoSensorId);
            return parametros;
        }
        private Sensor MontaSensor(DataRow registro)
        {
            Sensor s = new Sensor();
            s.SensorId = Convert.ToInt32(registro["sensorId"]);
            s.SensorNome = registro["sensorNome"].ToString();
            s.TipoSensorId = Convert.ToInt32(registro["tipoSensorId"]);
            return s;
        }

        public void Inserir(Sensor sensor)
        {
            string sql =
            "insert into Sensores (sensorId, sensorNome, tipoSensorId)" +
            "values ( @sensorId, @sensorNome, @tipoSensorId)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(sensor));
        }
        public void Alterar(Sensor sensor)
        {
            string sql =
            "update Sensores set sensorNome = @sensorNome, " +
            "tipoSensorId = @tipoSensorId " +
            "where sensorId = @sensorId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(sensor));
        }
        public void Excluir(int id)
        {
            string sql = "delete Sensores where sensorId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public Sensor Consulta(int id)
        {
            string sql = "select * from Sensores where sensorId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaSensor(tabela.Rows[0]);
        }
        public List<Sensor> Listagem()
        {
            List<Sensor> lista = new List<Sensor>();
            string sql = "select * from Sensores order by sensorNome";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaSensor(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(sensorId) +1, 1) as 'MAIOR' from Sensores";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}
