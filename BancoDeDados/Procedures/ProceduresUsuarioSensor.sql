-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirUsuarioSensor
-- Descrição: Insere um novo vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
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
-- Descrição: Atualiza o vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
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
-- Descrição: Exclui um vínculo entre usuário e sensor com base no ID informado
-- Parâmetro: usuarioSensorId (INT)
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
-- Descrição: Retorna um vínculo específico entre usuário e sensor pelo ID
-- Parâmetro: usuarioSensorId (INT)
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
-- Descrição: Lista todos os vínculos entre usuários e sensores
-- Sem parâmetros
-- ========================================
CREATE PROCEDURE spListarUsuarioSensores
AS
BEGIN
    SELECT * FROM UsuarioSensores
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
        u.usuarioNome, u.email, u.cpf,
        s.sensorNome, s.tipoSensorId
    FROM UsuarioSensores us
    INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId
    INNER JOIN Sensores s ON us.sensorId = s.sensorId
END
GO