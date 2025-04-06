-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirUsuario
-- Descri��o: Insere um novo registro na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spInserirUsuario
    @usuarioId INT,
    @usuarioNome NVARCHAR(100),
    @senha NVARCHAR(100),
    @email NVARCHAR(100),
    @cpf VARCHAR(20)
AS
BEGIN
    INSERT INTO Usuarios (usuarioId, usuarioNome, senha, email, cpf)
    VALUES (@usuarioId, @usuarioNome, @senha, @email, @cpf)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuario
-- Descri��o: Atualiza os dados de um usu�rio existente na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarUsuario
    @usuarioId INT,
    @usuarioNome NVARCHAR(100),
    @senha NVARCHAR(100),
    @email NVARCHAR(100),
    @cpf VARCHAR(20)
AS
BEGIN
    UPDATE Usuarios
    SET usuarioNome = @usuarioNome,
        senha = @senha,
        email = @email,
        cpf = @cpf
    WHERE usuarioId = @usuarioId
END
GO

-- ========================================
-- PROCEDURE: spExcluirUsuario
-- Descri��o: Exclui um usu�rio com base no ID informado
-- Par�metro: usuarioId (INT)
-- ========================================
CREATE PROCEDURE spExcluirUsuario
    @usuarioId INT
AS
BEGIN
    DELETE FROM Usuarios
    WHERE usuarioId = @usuarioId
END
GO

-- ========================================
-- PROCEDURE: spConsultarUsuario
-- Descri��o: Retorna um usu�rio espec�fico pelo seu ID
-- Par�metro: usuarioId (INT)
-- ========================================
CREATE PROCEDURE spConsultarUsuario
    @usuarioId INT
AS
BEGIN
    SELECT * FROM Usuarios
    WHERE usuarioId = @usuarioId
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarios
-- Descri��o: Lista todos os usu�rios cadastrados na tabela Usuarios, ordenados por nome
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarUsuarios
AS
BEGIN
    SELECT * FROM Usuarios
    ORDER BY usuarioNome
END
GO