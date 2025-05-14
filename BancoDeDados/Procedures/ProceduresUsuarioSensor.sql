-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuarioSensor
-- Descri��o: Insere um novo v�nculo entre usu�rio e sensor na tabela UsuarioSensores
-- Par�metros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
-- ========================================
CREATE PROCEDURE spInserirUsuarioSensores
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
CREATE PROCEDURE spAlterarUsuarioSensores
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
-- PROCEDURE: spListarUsuarioSensorJoin
-- Descri��o: Retorna a rela��o entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarUsuarioSensorJoin
AS
BEGIN
    SELECT 
        us.usuarioSensorId, us.usuarioId, us.sensorId,
        u.usuarioNome, u.email, u.cpf, u.imagem,
        s.sensorNome, s.tipoSensorId
    FROM UsuarioSensores us
    INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId
    INNER JOIN Sensores s ON us.sensorId = s.sensorId
END
GO