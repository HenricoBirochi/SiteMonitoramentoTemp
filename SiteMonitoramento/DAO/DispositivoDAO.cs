using Microsoft.Data.SqlClient;
using SiteMonitoramento.Models;
using SiteMonitoramento.ViewModels;
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

        //Lista os as medidas que estão relacionadas com um dispositivo
        private DispositivoMedidasViewModel MontaDispositivoMedidas(DataRow registro)
        {
            DispositivoMedidasViewModel dm = new DispositivoMedidasViewModel();
            dm.DispositivoNome = registro["dispositivoNome"].ToString();
            dm.ValorMedido = Convert.ToDouble(registro["valorMedido"]);
            dm.HorarioMedicao = Convert.ToDateTime(registro["horarioMedicao"]);
            return dm;
        }
        public List<DispositivoMedidasViewModel> ListagemDispositivoMedidasJoin(int dispositivoId)
        {
            var lista = new List<DispositivoMedidasViewModel>();
            var parametros = new SqlParameter[]
            {
                new SqlParameter("dispositivoId", dispositivoId),
            };
            string sql = "spListarDispositivoMedidasJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, parametros);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaDispositivoMedidas(registro));
            return lista;
        }
    }
}
