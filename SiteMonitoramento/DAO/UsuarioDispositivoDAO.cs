using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class UsuarioDispositivoDAO : PadraoDAO<UsuarioDispositivo>
    {
        protected override SqlParameter[] CriaParametros(UsuarioDispositivo usuarioDispositivo)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("usuarioDispositivoId", usuarioDispositivo.UsuarioDispositivoId);
            parametros[1] = new SqlParameter("usuarioId", usuarioDispositivo.UsuarioId);
            parametros[2] = new SqlParameter("dispositivoId", usuarioDispositivo.DispositivoId);
            return parametros;
        }
        protected override UsuarioDispositivo MontaModel(DataRow registro)
        {
            UsuarioDispositivo ud = new UsuarioDispositivo();
            ud.UsuarioDispositivoId = Convert.ToInt32(registro["usuarioDispositivoId"]);
            ud.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            ud.DispositivoId = Convert.ToInt32(registro["dispositivoId"]);
            return ud;
        }
        protected override void SetTabela()
        {
            Tabela = "UsuarioDispositivo";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "usuarioDispositivoId";
        }

        //metodos importantes abaixo, um vai trazer o object DTO(Data Transfer Object) para usa-lo no outro metodo que
        public UsuarioDispositivoDTO MontaUsuarioDispositivoDTO(DataRow registro)
        {
            UsuarioDispositivoDTO ud = new UsuarioDispositivoDTO();
            ud.UsuarioDispositivo.UsuarioDispositivoId = Convert.ToInt32(registro["usuarioDispositivoId"]);
            ud.UsuarioDispositivo.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            ud.UsuarioDispositivo.DispositivoId = Convert.ToInt32(registro["dispositivoId"]);
            ud.UsuarioNome = registro["usuarioNome"].ToString();
            ud.Email = registro["email"].ToString();
            ud.CPF = registro["cpf"].ToString();
            if (registro["imagem"] != DBNull.Value)
                ud.ImagemEmByte = registro["imagem"] as byte[];
            ud.DispositivoNome = registro["dispositivoNome"].ToString();
            return ud;
        }
        //vai retornar a relacao entre as tabelas Sensores, Usuarios e UsuarioSensores
        public List<UsuarioDispositivoDTO> ListagemComJoin()
        {
            List<UsuarioDispositivoDTO> lista = new List<UsuarioDispositivoDTO>();
            string sql = "spListarUsuarioDispositivoJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioDispositivoDTO(registro));
            return lista;
        }
    }
}