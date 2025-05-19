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
-- Descri��o: Atualiza os dados de um usu�rio existente na tabela Usuarios
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
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