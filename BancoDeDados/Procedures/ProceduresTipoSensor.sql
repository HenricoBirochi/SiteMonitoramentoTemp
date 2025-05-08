-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirTipoSensor
-- Descri��o: Insere um novo registro na tabela TipoSensores
-- Par�metros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
-- ========================================
CREATE PROCEDURE spInserirTipoSensores
    @tipoSensorId INT,
    @nomeTecnico NVARCHAR(100),
    @parametroMedido NVARCHAR(100)
AS
BEGIN
    INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido)
    VALUES (@tipoSensorId, @nomeTecnico, @parametroMedido)
END
GO

-- ========================================
-- PROCEDURE: spAlterarTipoSensor
-- Descri��o: Atualiza os dados de um tipo de sensor existente
-- Par�metros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarTipoSensores
    @tipoSensorId INT,
    @nomeTecnico NVARCHAR(100),
    @parametroMedido NVARCHAR(100)
AS
BEGIN
    UPDATE TipoSensores
    SET nomeTecnico = @nomeTecnico,
        parametroMedido = @parametroMedido
    WHERE tipoSensorId = @tipoSensorId
END
GO