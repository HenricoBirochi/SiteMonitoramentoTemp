using SiteMonitoramento.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace SiteMonitoramento.DAO
{
    public class UsuarioSensorDAO
    {
        private SqlParameter[] CriaParametros(UsuarioSensor usuarioSensor)
        {
            SqlParameter[] parametros = new SqlParameter[3];
            parametros[0] = new SqlParameter("usuarioSensorId", usuarioSensor.UsuarioSensorId);
            parametros[1] = new SqlParameter("usuarioId", usuarioSensor.UsuarioId);
            parametros[2] = new SqlParameter("sensorId", usuarioSensor.SensorId);
            return parametros;
        }
        private UsuarioSensor MontaUsuarioSensor(DataRow registro)
        {
            UsuarioSensor uS = new UsuarioSensor();
            uS.UsuarioSensorId = Convert.ToInt32(registro["usuarioSensorId"]);
            uS.UsuarioId = Convert.ToInt32(registro["usuarioId"]);
            uS.SensorId = Convert.ToInt32(registro["sensorId"]);
            return uS;
        }

        public void Inserir(UsuarioSensor usuarioSensor)
        {
            string sql =
            "insert into UsuarioSensores (usuarioSensorId, usuarioId, sensorId)" +
            "values ( @usuarioSensorId, @usuarioId, @sensorId)";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioSensor));
        }
        public void Alterar(UsuarioSensor usuarioSensor)
        {
            string sql =
            "update UsuarioSensores set usuarioId = @usuarioId, " +
            "sensorId = @sensorId " +
            "where usuarioSensorId = @usuarioSensorId";
            HelperDAO.ExecutaSQL(sql, CriaParametros(usuarioSensor));
        }
        public void Excluir(int id)
        {
            string sql = "delete UsuarioSensores where usuarioSensorId = " + id;
            HelperDAO.ExecutaSQL(sql, null);
        }
        public UsuarioSensor Consulta(int id)
        {
            string sql = "select * from UsuarioSensores where usuarioSensorId = " + id;
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            if (tabela.Rows.Count == 0)
                return null;
            else
                return MontaUsuarioSensor(tabela.Rows[0]);
        }
        public List<UsuarioSensor> Listagem()
        {
            List<UsuarioSensor> lista = new List<UsuarioSensor>();
            string sql = "select * from UsuarioSensores";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioSensor(registro));
            return lista;
        }
        public int ProximoId()
        {
            string sql = "select isnull(max(usuarioSensorId) +1, 1) as 'MAIOR' from UsuarioSensores";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            return Convert.ToInt32(tabela.Rows[0]["MAIOR"]);
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
            string sql = "SELECT us.usuarioSensorId, us.usuarioId, us.sensorId, " +
                "u.usuarioNome, u.email, u.cpf, " +
                "s.sensorNome, s.tipoSensorId " +
                "FROM UsuarioSensores us " +
                "INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId " +
                "INNER JOIN Sensores s ON us.sensorId = s.sensorId";
            DataTable tabela = HelperDAO.ExecutaSelect(sql, null);
            foreach (DataRow registro in tabela.Rows)
                lista.Add(MontaUsuarioSensorDTO(registro));
            return lista;
        }
    }
}
