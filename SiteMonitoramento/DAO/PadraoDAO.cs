﻿using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public abstract class PadraoDAO<T>
    {
        public PadraoDAO()
        {
            SetTabela();
            SetNomeDoCampoId();
        }

        protected string Tabela { get; set; }
        protected string NomeDoCampoId { get; set; }
        protected string NomeSpListagem { get; set; } = "spListagem";
        protected abstract SqlParameter[] CriaParametros(T model);
        protected abstract T MontaModel(DataRow registro);
        protected abstract void SetTabela();
        protected abstract void SetNomeDoCampoId();

        public virtual void Inserir(T model)
        {
            HelperDAO.ExecutaProc("spInserir" + Tabela, CriaParametros(model));
        }
        public virtual void Alterar(T model)
        {
            HelperDAO.ExecutaProc("spAlterar" + Tabela, CriaParametros(model));
        }
        public virtual void Delete(int id)
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("id", id),
                 new SqlParameter("nomeDoCampoId", NomeDoCampoId),
                 new SqlParameter("tabela", Tabela)
            };
            HelperDAO.ExecutaProc("spDelete", p);
        }
        public virtual T Consulta(int id)
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("id", id),
                 new SqlParameter("nomeDoCampoId", NomeDoCampoId),
                 new SqlParameter("tabela", Tabela)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spConsulta", p);
            if (tabela.Rows.Count == 0)
                return default;
            else
                return MontaModel(tabela.Rows[0]);
        }
        public virtual int ProximoId()
        {
            var p = new SqlParameter[]
            {
                new SqlParameter("tabela", Tabela),
                new SqlParameter("nomeDoCampoId", NomeDoCampoId)
            };
            var tabela = HelperDAO.ExecutaProcSelect("spProximoId", p);
            return Convert.ToInt32(tabela.Rows[0][0]);
        }
        public virtual List<T> Listagem()
        {
            var p = new SqlParameter[]
            {
                 new SqlParameter("tabela", Tabela),
                 new SqlParameter("ordem", "1") // 1 é o primeiro campo da tabela
            };
            var tabela = HelperDAO.ExecutaProcSelect(NomeSpListagem, p);
            List<T> lista = new List<T>();
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaModel(registro));
            return lista;
        }
    }
}
