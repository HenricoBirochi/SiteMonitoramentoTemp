-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirUsuarios
-- Descri��o: Insere um novo registro na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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
-- Descri��o: Atualiza os dados de um usu�rio existente na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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