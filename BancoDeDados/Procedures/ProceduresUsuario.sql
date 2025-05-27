-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuarios
-- Descrição: Insere um novo registro na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spInserirUsuarios
    @usuarioId INT,
    @usuarioNome NVARCHAR(100),
    @senha NVARCHAR(100),
    @email NVARCHAR(100),
    @cpf VARCHAR(20),
	@imagem VARBINARY(max)
AS
BEGIN
    INSERT INTO Usuarios (usuarioId, usuarioNome, senha, email, cpf, imagem)
    VALUES (@usuarioId, @usuarioNome, @senha, @email, @cpf, @imagem)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuarios
-- Descrição: Atualiza os dados de um usuário existente na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarUsuarios
    @usuarioId INT,
    @usuarioNome NVARCHAR(100),
    @senha NVARCHAR(100),
    @email NVARCHAR(100),
    @cpf VARCHAR(20),
	@imagem VARBINARY(max)
AS
BEGIN
    UPDATE Usuarios
    SET usuarioNome = @usuarioNome,
        senha = @senha,
        email = @email,
        cpf = @cpf,
		imagem = @imagem
    WHERE usuarioId = @usuarioId
END
GO