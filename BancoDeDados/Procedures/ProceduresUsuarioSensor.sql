-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuarioSensor
-- Descrição: Insere um novo vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
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
-- Descrição: Atualiza o vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
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
-- Descrição: Retorna a relação entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem parâmetros
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