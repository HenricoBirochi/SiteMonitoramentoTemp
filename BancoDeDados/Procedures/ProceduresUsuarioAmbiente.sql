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
CREATE PROCEDURE spInserirUsuarioAmbiente
    @usuarioAmbienteId INT,
    @usuarioId INT,
    @ambienteId INT
AS
BEGIN
    INSERT INTO UsuarioAmbiente (usuarioAmbienteId, usuarioId, ambienteId)
    VALUES (@usuarioAmbienteId, @usuarioId, @ambienteId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuarioSensor
-- Descri��o: Atualiza o v�nculo entre usu�rio e sensor na tabela UsuarioSensores
-- Par�metros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarUsuarioAmbiente
    @usuarioAmbienteId INT,
    @usuarioId INT,
    @ambienteId INT
AS
BEGIN
    UPDATE UsuarioAmbiente
    SET usuarioId = @usuarioId,
        ambienteId = @ambienteId
    WHERE usuarioAmbienteId = @usuarioAmbienteId
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarioSensorJoin
-- Descri��o: Retorna a rela��o entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarUsuarioAmbienteJoin
AS
BEGIN
    SELECT 
        us.usuarioAmbienteId, us.usuarioId, us.ambienteId,
        u.usuarioNome, u.email, u.cpf, u.imagem,
        a.ambienteNome
    FROM UsuarioAmbiente us
    INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId
    INNER JOIN Ambientes a ON us.ambienteId = a.ambienteId
END
GO