-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirUsuario
-- Descrição: Insere um novo registro na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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
-- Descrição: Atualiza os dados de um usuário existente na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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
-- Descrição: Exclui um usuário com base no ID informado
-- Parâmetro: usuarioId (INT)
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
-- Descrição: Retorna um usuário específico pelo seu ID
-- Parâmetro: usuarioId (INT)
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
-- Descrição: Lista todos os usuários cadastrados na tabela Usuarios, ordenados por nome
-- Sem parâmetros
-- ========================================
CREATE PROCEDURE spListarUsuarios
AS
BEGIN
    SELECT * FROM Usuarios
    ORDER BY usuarioNome
END
GO