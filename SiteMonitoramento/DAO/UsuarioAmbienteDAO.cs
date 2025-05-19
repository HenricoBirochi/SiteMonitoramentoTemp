using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class UsuarioAmbienteDAO : PadraoDAO<UsuarioAmbiente>
    {
        protected override SqlParameter[] CriaParametros(UsuarioAmbiente usuarioAmbiente)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("usuarioAmbienteId", usuarioAmbiente.UsuarioAmbienteId);
            parametros[1] = new SqlParameter("usuarioId", usuarioAmbiente.UsuarioId);
            parametros[2] = new SqlParameter("ambienteId", usuarioAmbiente.AmbienteId);
            return parametros;
        }
        protected override UsuarioAmbiente MontaModel(DataRow registro)
        {
            UsuarioAmbiente uA = new UsuarioAmbiente();
            uA.UsuarioAmbienteId = Convert.ToInt32(registro["usuarioAmbienteId"]);
            uA.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            uA.AmbienteId = Convert.ToInt32(registro["ambienteId"]);
            return uA;
        }
        protected override void SetTabela()
        {
            Tabela = "UsuarioAmbiente";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "usuarioAmbienteId";
        }

        //metodos importantes abaixo, um vai trazer o object DTO(Data Transfer Object) para usa-lo no outro metodo que
        public UsuarioAmbienteDTO MontaUsuarioSensorDTO(DataRow registro)
        {
            UsuarioAmbienteDTO uSD = new UsuarioAmbienteDTO();
            uSD.UsuarioAmbiente.UsuarioAmbienteId = Convert.ToInt32(registro["usuarioAmbienteId"]);
            uSD.UsuarioAmbiente.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            uSD.UsuarioAmbiente.AmbienteId = Convert.ToInt32(registro["ambienteID"]);
            uSD.UsuarioNome = registro["usuarioNome"].ToString();
            uSD.Email = registro["email"].ToString();
            uSD.CPF = registro["cpf"].ToString();
            if (registro["imagem"] != DBNull.Value)
                uSD.ImagemEmByte = registro["imagem"] as byte[];
            uSD.AmbienteNome = registro["ambienteNome"].ToString();
            return uSD;
        }
        //vai retornar a relacao entre as tabelas Sensores, Usuarios e UsuarioSensores
        public List<UsuarioAmbienteDTO> ListagemComJoin()
        {
            List<UsuarioAmbienteDTO> lista = new List<UsuarioAmbienteDTO>();
            string sql = "spListarUsuarioSensorJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioSensorDTO(registro));
            return lista;
        }
    }
}