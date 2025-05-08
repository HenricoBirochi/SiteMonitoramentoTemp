-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuario
-- Descri��o: Insere um novo registro na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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
-- Descri��o: Atualiza os dados de um usu�rio existente na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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