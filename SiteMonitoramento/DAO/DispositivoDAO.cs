using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace SiteMonitoramento.DAO
{
    public class DispositivoDAO : PadraoDAO<Dispositivo>
    {
        protected override SqlParameter[] CriaParametros(Dispositivo model)
        {
            SqlParameter[] parametros = new SqlParameter[2];
            parametros[0] = new SqlParameter("dispositivoId", model.DispositivoId);
            parametros[1] = new SqlParameter("dispositivoNome", model.DispositivoNome);
            return parametros;
        }

        protected override Dispositivo MontaModel(DataRow registro)
        {
            Dispositivo d = new Dispositivo();
            d.DispositivoId = Convert.ToInt32(registro["dispositivoId"]);
            d.DispositivoNome = registro["dispositivoNome"].ToString();
            return d;
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "dispositivoId";
        }

        protected override void SetTabela()
        {
            Tabela = "Dispositivos";
        }
    }
}
