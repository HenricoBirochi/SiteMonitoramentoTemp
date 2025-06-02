using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SiteMonitoramento.DAO
{
    public class MedidaDAO : PadraoDAO<Medida>
    {
        protected override SqlParameter[] CriaParametros(Medida medida)
        {
            SqlParameter[] parametros = new SqlParameter[4];
            parametros[0] = new SqlParameter("medidaId", medida.MedidaId);
            parametros[1] = new SqlParameter("valorMedido", medida.ValorMedido);
            parametros[2] = new SqlParameter("horarioMedicao", medida.HorarioMedicao);
            parametros[3] = new SqlParameter("dispositivoId", medida.DispositivoId);
            return parametros;
        }

        protected override Medida MontaModel(DataRow registro)
        {
            Medida m = new Medida();
            m.MedidaId = Convert.ToInt32(registro["medidaId"]);
            m.ValorMedido = Convert.ToDouble(registro["valorMedido"]);
            m.HorarioMedicao = Convert.ToDateTime(registro["horarioMedicao"]);
            m.DispositivoId = Convert.ToInt32(registro["dispositivoId"]);
            return m;
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "medidaId";
        }

        protected override void SetTabela()
        {
            Tabela = "Medidas";
        }

        //Serve para fazer a consulta avançada
        public List<Medida> ConsultaAvancadaMedidas(double valorMedido, DateTime dataInicial, DateTime dataFinal)
        {
            SqlParameter[] p = 
            {
                new SqlParameter("valorMedido", valorMedido),
                new SqlParameter("dataInicial", dataInicial),
                new SqlParameter("dataFinal", dataFinal),
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsultaAvancadaMedidas", p);
            var lista = new List<Medida>();
            foreach (DataRow dr in tabela.Rows)
                lista.Add(MontaModel(dr));
            return lista;
        }
    }
}
