-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirTipoSensor
-- Descrição: Insere um novo registro na tabela TipoSensores
-- Parâmetros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
-- ========================================
CREATE PROCEDURE spInserirTipoSensor
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
CREATE PROCEDURE spAlterarTipoSensor
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

-- ========================================
-- PROCEDURE: spExcluirTipoSensor
-- Descrição: Exclui um tipo de sensor com base no ID informado
-- Parâmetro: tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spExcluirTipoSensor
    @tipoSensorId INT
AS
BEGIN
    DELETE FROM TipoSensores
    WHERE tipoSensorId = @tipoSensorId
END
GO

-- ========================================
-- PROCEDURE: spConsultarTipoSensor
-- Descrição: Retorna um tipo de sensor específico pelo seu ID
-- Parâmetro: tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spConsultarTipoSensor
    @tipoSensorId INT
AS
BEGIN
    SELECT * FROM TipoSensores
    WHERE tipoSensorId = @tipoSensorId
END
GO

-- ========================================
-- PROCEDURE: spListarTipoSensores
-- Descrição: Lista todos os tipos de sensores cadastrados na tabela TipoSensores
-- Sem parâmetros
-- ========================================
CREATE PROCEDURE spListarTipoSensores
AS
BEGIN
    SELECT * FROM TipoSensores
END
GO