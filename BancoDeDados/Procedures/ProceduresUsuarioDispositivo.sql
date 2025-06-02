-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuarioDispositivo
-- Descrição: Insere um novo vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioDispositivoId (INT), usuarioId (INT), dispositivoId (INT)
-- ========================================
CREATE PROCEDURE spInserirUsuarioDispositivo
    @usuarioDispositivoId INT,
    @usuarioId INT,
    @dispositivoId INT
AS
BEGIN
    INSERT INTO UsuarioDispositivo (usuarioDispositivoId, usuarioId, dispositivoId)
    VALUES (@usuarioDispositivoId, @usuarioId, @dispositivoId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuarioDispositivo
-- Descrição: Atualiza o vínculo entre usuário e sensor na tabela UsuarioSensores
-- Parâmetros: usuarioSensorId (INT), usuarioId (INT), sensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarUsuarioDispositivo
    @usuarioDispositivoId INT,
    @usuarioId INT,
    @dispositivoId INT
AS
BEGIN
    UPDATE UsuarioDispositivo
    SET usuarioId = @usuarioId,
        dispositivoId = @dispositivoId
    WHERE usuarioDispositivoId = @usuarioDispositivoId
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarioDispositivoJoin
-- Descrição: Retorna a relação entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem parâmetros
-- ========================================
CREATE PROCEDURE spListarUsuarioDispositivoJoin
AS
BEGIN
    SELECT 
        us.usuarioDispositivoId, us.usuarioId, us.dispositivoId,
        u.usuarioNome, u.email, u.cpf, u.imagem,
        d.dispositivoNome
    FROM UsuarioDispositivo us
    INNER JOIN Usuarios u ON us.usuarioId = u.usuarioId
    INNER JOIN Dispositivos d ON us.dispositivoId = d.dispositivoId
END
GO