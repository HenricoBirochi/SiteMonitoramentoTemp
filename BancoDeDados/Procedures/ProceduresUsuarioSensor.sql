-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirUsuarioSensor
-- Descri��o: Insere um novo v�nculo entre usu�rio e sensor na tabela UsuarioSensores
-- Par�metros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
-- ========================================
CREATE PROCEDURE spInserirUsuarioSensor
    @usuarioSensorId INT,
    @usuarioId INT,
    @sensorId INT
AS
BEGIN
    INSERT INTO UsuarioSensores (usuarioSensorId, usuarioId, sensorId)
    VALUES (@usuarioSensorId, @usuarioId, @sensorId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuarioSensor
-- Descri��o: Atualiza o v�nculo entre usu�rio e sensor na tabela UsuarioSensores
-- Par�metros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarUsuarioSensor
    @usuarioSensorId INT,
    @usuarioId INT,
    @sensorId INT
AS
BEGIN
    UPDATE UsuarioSensores
    SET usuarioId = @usuarioId,
        sensorId = @sensorId
    WHERE usuarioSensorId = @usuarioSensorId
END
GO

-- ========================================
-- PROCEDURE: spExcluirUsuarioSensor
-- Descri��o: Exclui um v�nculo entre usu�rio e sensor com base no ID informado
-- Par�metro: usuarioSensorId (INT)
-- ========================================
CREATE PROCEDURE spExcluirUsuarioSensor
    @usuarioSensorId INT
AS
BEGIN
    DELETE FROM UsuarioSensores
    WHERE usuarioSensorId = @usuarioSensorId
END
GO

-- ========================================
-- PROCEDURE: spConsultarUsuarioSensor
-- Descri��o: Retorna um v�nculo espec�fico entre usu�rio e sensor pelo ID
-- Par�metro: usuarioSensorId (INT)
-- ========================================
CREATE PROCEDURE spConsultarUsuarioSensor
    @usuarioSensorId INT
AS
BEGIN
    SELECT * FROM UsuarioSensores
    WHERE usuarioSensorId = @usuarioSensorId
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarioSensores
-- Descri��o: Lista todos os v�nculos entre usu�rios e sensores
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarUsuarioSensores
AS
BEGIN
    SELECT * FROM UsuarioSensores
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarioSensorJoin
-- Descri��o: Retorna a rela��o entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarUsuarioSensorJoin
AS
BEGIN
    SELECT 
        us.usuarioSensorId, us.usuarioId, us.sensorId,
        u.usuarioNome, u.email, u.cpf,
        s.sensorNome, s.tipoSensorId
    FROM UsuarioSensores us
    INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId
    INNER JOIN Sensores s ON us.sensorId = s.sensorId
END
GO