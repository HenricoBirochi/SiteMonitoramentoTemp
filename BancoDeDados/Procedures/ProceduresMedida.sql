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
CREATE PROCEDURE spInserirMedidas
    @medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
	@parametro varchar(20),
    @sensorId INT
AS
BEGIN
    INSERT INTO Medidas(medidaId, valorMedido, horarioMedicao, parametro, sensorId)
    VALUES (@medidaId, @valorMedido, @horarioMedicao, @parametro, @sensorId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarUsuario
-- Descrição: Atualiza os dados de um usuário existente na tabela Usuarios
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarMedidas
	@medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
	@parametro varchar(20),
    @sensorId INT
AS
BEGIN
    UPDATE Medidas
    SET valorMedido = @valorMedido,
        horarioMedicao = @horarioMedicao,
		parametro = @parametro,
        sensorId = @sensorId
    WHERE medidaId = @medidaId
END
GO