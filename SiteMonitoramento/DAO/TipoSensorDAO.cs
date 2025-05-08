using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class TipoSensorDAO : PadraoDAO<TipoSensor>
    {
        protected override SqlParameter[] CriaParametros(TipoSensor tipoSensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("tipoSensorId", tipoSensor.TipoSensorId);
            parametros[1] = new SqlParameter("nomeTecnico", tipoSensor.NomeTecnico);
            parametros[2] = new SqlParameter("parametroMedido", tipoSensor.ParametroMedido);
            return parametros;
        }
        protected override TipoSensor MontaModel(DataRow registro)
        {
            TipoSensor tS = new TipoSensor();
            tS.TipoSensorId = Convert.ToInt32(registro["tipoSensorId"]);
            tS.NomeTecnico = registro["nomeTecnico"].ToString();
            tS.ParametroMedido = registro["parametroMedido"].ToString();
            return tS;
        }
        protected override void SetTabela()
        {
            Tabela = "TipoSensores";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "tipoSensorId";
        }
    }
}