-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirTipoSensor
-- Descrição: Insere um novo registro na tabela TipoSensores
-- Parâmetros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
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
-- Descrição: Atualiza os dados de um tipo de sensor existente
-- Parâmetros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
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