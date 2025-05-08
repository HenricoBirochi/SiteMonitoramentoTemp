-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuario
-- Descrição: Insere um novo registro na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spInserirUsuarios
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
CREATE PROCEDURE spAlterarUsuarios
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