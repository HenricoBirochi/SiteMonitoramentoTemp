using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using System;
using System.Data;

namespace SiteMonitoramento.DAO
{
    public class AmbienteDAO : PadraoDAO<Ambiente>
    {
        protected override SqlParameter[] CriaParametros(Ambiente model)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("ambienteId", model.AmbienteId);
            parametros[1] = new SqlParameter("ambienteNome", model.AmbienteNome);
            parametros[2] = new SqlParameter("sensorId", model.SensorId);
            return parametros;
        }

        protected override Ambiente MontaModel(DataRow registro)
        {
            Ambiente a = new Ambiente();
            a.AmbienteId = Convert.ToInt32(registro["ambienteId"]);
            a.AmbienteNome = registro["ambienteNome"].ToString();
            a.SensorId = Convert.ToInt32(registro["sensorId"]);
            return a;
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "ambienteId";
        }

        protected override void SetTabela()
        {
            Tabela = "Ambientes";
        }
    }
}
