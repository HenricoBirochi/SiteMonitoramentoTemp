﻿using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using System;
using System.Data;

namespace SiteMonitoramento.DAO
{
    public class MedidaDAO : PadraoDAO<Medida>
    {
        protected override SqlParameter[] CriaParametros(Medida medida)
        {
            SqlParameter[] parametros = new SqlParameter[5];
            parametros[0] = new SqlParameter("medidaId", medida.MedidaId);
            parametros[1] = new SqlParameter("valorMedido", medida.ValorMedido);
            parametros[2] = new SqlParameter("horarioMedicao", medida.HorarioMedicao);
            parametros[3] = new SqlParameter("parametro", medida.Parametro);
            parametros[4] = new SqlParameter("sensorId", medida.SensorId);
            return parametros;
        }

        protected override Medida MontaModel(DataRow registro)
        {
            Medida m = new Medida();
            m.MedidaId = Convert.ToInt32(registro["medidaId"]);
            m.ValorMedido = Convert.ToDouble(registro["valorMedido"]);
            m.HorarioMedicao = Convert.ToDateTime(registro["horarioMedicao"]);
            m.Parametro = registro["parametro"].ToString();
            m.SensorId = Convert.ToInt32(registro["sensorId"]);
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
    }
}
