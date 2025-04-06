using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
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
            string sql = "spInserirTipoSensor";
            HelperDAO.ExecutaProc(sql, CriaParametros(tipoSensor));
        }

        public void Alterar(TipoSensor tipoSensor)
        {
            string sql = "spAlterarTipoSensor";
            HelperDAO.ExecutaProc(sql, CriaParametros(tipoSensor));
        }

        public void Excluir(int id)
        {
            string sql = "spExcluirTipoSensor";
            SqlParameter[] parametros = { new SqlParameter("@tipoSensorId", id) };
            HelperDAO.ExecutaProc(sql, parametros);
        }

        public TipoSensor Consulta(int id)
        {
            string sql = "spConsultarTipoSensor";
            SqlParameter[] parametros = { new SqlParameter("@tipoSensorId", id) };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaTipoSensor(tabela.Rows[0]);
        }

        public List<TipoSensor> Listagem()
        {
            List<TipoSensor> lista = new List<TipoSensor>();
            string sql = "spListarTipoSensores";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaTipoSensor(registro));
            return lista;
        }

        public int ProximoId()
        {
            string sql = "spProximoId";
            SqlParameter[] parametros =
            {
                new SqlParameter("@tabela", "TipoSensores"),
                new SqlParameter("@nomeDoCampoId", "tipoSensorId")
            };
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
        }
    }
}