using Microsoft.AspNetCore.Mvc;
using SiteMonitoramento.DAO;
using SiteMonitoramento.Models;
using System;

namespace SiteMonitoramento.Controllers
{
    public class MedidaController : PadraoController
    {
        public MedidaController()
        {
            ExigeAutenticacao = true;
        }
        public void Save(Medida model)
        {
            try
            {
                var dao = new MedidaDAO();
                dao.Inserir(model);
            }
            catch (Exception erro)
            {
                throw new Exception("Erro ao salvar medida: " + erro.Message);
            }
        }
    }
}
