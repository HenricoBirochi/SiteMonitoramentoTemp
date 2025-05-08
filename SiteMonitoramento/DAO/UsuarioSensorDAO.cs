using SiteMonitoramento.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class UsuarioSensorDAO : PadraoDAO<UsuarioSensor>
    {
        protected override SqlParameter[] CriaParametros(UsuarioSensor usuarioSensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("usuarioSensorId", usuarioSensor.UsuarioSensorId);
            parametros[1] = new SqlParameter("usuarioId", usuarioSensor.UsuarioId);
            parametros[2] = new SqlParameter("sensorId", usuarioSensor.SensorId);
            return parametros;
        }
        protected override UsuarioSensor MontaModel(DataRow registro)
        {
            UsuarioSensor uS = new UsuarioSensor();
            uS.UsuarioSensorId = Convert.ToInt32(registro["usuarioSensorId"]);
            uS.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            uS.SensorId = Convert.ToInt32(registro["sensorId"]);
            return uS;
        }
        protected override void SetTabela()
        {
            Tabela = "UsuarioSensores";
        }

        protected override void SetNomeDoCampoId()
        {
            NomeDoCampoId = "usuarioSensorId";
        }

        //metodos importantes abaixo, um vai trazer o object DTO(Data Transfer Object) para usa-lo no outro metodo que
        public UsuarioSensorDTO MontaUsuarioSensorDTO(DataRow registro)
        {
            UsuarioSensorDTO uSD = new UsuarioSensorDTO();
            uSD.UsuarioSensor.UsuarioSensorId = Convert.ToInt32(registro["usuarioSensorId"]);
            uSD.UsuarioSensor.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            uSD.UsuarioSensor.SensorId = Convert.ToInt32(registro["sensorId"]);
            uSD.UsuarioNome = registro["usuarioNome"].ToString();
            uSD.Email = registro["email"].ToString();
            uSD.CPF = registro["cpf"].ToString();
            uSD.SensorNome = registro["sensorNome"].ToString();
            uSD.TipoSensorId = Convert.ToInt32(registro["tipoSensorId"]);
            return uSD;
        }
        //vai retornar a relacao entre as tabelas Sensores, Usuarios e UsuarioSensores
        public List<UsuarioSensorDTO> ListagemComJoin()
        {
            List<UsuarioSensorDTO> lista = new List<UsuarioSensorDTO>();
            string sql = "spListarUsuarioSensorJoin";
            DataTable tabela = HelperDAO.ExecutaProcSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioSensorDTO(registro));
            return lista;
        }
    }
}