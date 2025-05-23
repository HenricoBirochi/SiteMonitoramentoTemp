using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using SiteMonitoramento.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;

namespace SiteMonitoramento.DAO
{
    public class AmbienteDAO : PadraoDAO<Ambiente>
    {
        protected override SqlParameter[] CriaParametros(Ambiente model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("ambienteId", model.AmbienteId);
            parametros[1] = new SqlParameter("ambienteNome", model.AmbienteNome);
            return parametros;
        }

        protected override Ambiente MontaModel(DataRow registro)
        {
            Ambiente a = new Ambiente();
            a.AmbienteId = Convert.ToInt32(registro["ambienteId"]);
            a.AmbienteNome = registro["ambienteNome"].ToString();
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

        //Lista os sensores que são de um ambiente específico
        private SensoresAmbienteViewModel MontaSensores(DataRow registro)
        {
            SensoresAmbienteViewModel sa = new SensoresAmbienteViewModel();
            sa.SensorNome = registro["sensorNome"].ToString();
            sa.NomeTecnico = registro["nomeTecnico"].ToString();
            sa.ParametroMedido = registro["parametroMedido"].ToString();
            return sa;
        }
        public List<SensoresAmbienteViewModel> ListagemAmbienteSensoresTipoSensoresJoin(int ambienteId)
        {
            var lista = new List<SensoresAmbienteViewModel>();
            var parametros = new SqlParameter[]
            {
                new SqlParameter("ambienteId", ambienteId),
            };
            string sql = "spListarAmbienteSensorTipoSensorJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaSensores(registro));
            return lista;
        }
    }
}
