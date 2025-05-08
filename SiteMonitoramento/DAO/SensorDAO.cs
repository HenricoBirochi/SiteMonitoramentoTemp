using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using SiteMonitoramento.ViewModels;

namespace SiteMonitoramento.DAO
{
    public class SensorDAO : PadraoDAO<Sensor>
    {
        protected override SqlParameter[] CriaParametros(Sensor sensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("sensorId", sensor.SensorId);
            parametros[1] = new SqlParameter("sensorNome", sensor.SensorNome);
            parametros[2] = new SqlParameter("tipoSensorId", sensor.TipoSensorId);
            return parametros;
        }
        protected override Sensor MontaModel(DataRow registro)
        {
            Sensor s = new Sensor();
            s.SensorId = Convert.ToInt32(registro["sensorId"]);
            s.SensorNome = registro["sensorNome"].ToString();
            s.TipoSensorId = Convert.ToInt32(registro["tipoSensorId"]);
            return s;
        }
        protected override void SetTabela()
        {
            Tabela = "Sensores";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "sensorId";
        }

        //Join das tabelas Sensores e TipoSensores
        public ListaSensorViewModel MontaListaSensorViewModel(DataRow registro)
        {
            ListaSensorViewModel ls = new ListaSensorViewModel();
            ls.SensorId = Convert.ToInt32(registro["sensorId"]);
            ls.NomeSensor = registro["sensorNome"].ToString();
            ls.NomeTecnicoSensor = registro["nomeTecnico"].ToString();
            return ls;
        }
        public List<ListaSensorViewModel> ListagemSensoresTipoSensoresJoin()
        {
            var lista = new List<ListaSensorViewModel>();
            string sql = "spListarSensoresTipoSensoresJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaListaSensorViewModel(registro));
            return lista;
        }
    }
}