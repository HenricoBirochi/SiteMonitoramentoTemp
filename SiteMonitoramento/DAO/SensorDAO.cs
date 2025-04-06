using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
            string sql = "spInserirSensor";
            HelperDAO.ExecutaProc(sql, CriaParametros(sensor));
        }

        public void Alterar(Sensor sensor)
        {
            string sql = "spAlterarSensor";
            HelperDAO.ExecutaProc(sql, CriaParametros(sensor));
        }

        public void Excluir(int id)
        {
            string sql = "spExcluirSensor";
            SqlParameter[] parametros = { new SqlParameter("@sensorId", id) };
            HelperDAO.ExecutaProc(sql, parametros);
        }

        public Sensor Consulta(int id)
        {
            string sql = "spConsultarSensor";
            SqlParameter[] parametros = { new SqlParameter("@sensorId", id) };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaSensor(tabela.Rows[0]);
        }

        public List<Sensor> Listagem()
        {
            List<Sensor> lista = new List<Sensor>();
            string sql = "spListarSensores";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaSensor(registro));
            return lista;
        }

        public int ProximoId()
        {
            string sql = "spProximoId";
            SqlParameter[] parametros = 
            { 
                new SqlParameter("@tabela", "Sensores"),
                new SqlParameter("@nomeDoCampoId", "sensorId") 
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}