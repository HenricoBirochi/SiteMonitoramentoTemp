-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirTipoSensor
-- Descri��o: Insere um novo registro na tabela TipoSensores
-- Par�metros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
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
-- Descri��o: Atualiza os dados de um tipo de sensor existente
-- Par�metros: tipoSensorId (INT), nomeTecnico (NVARCHAR), parametroMedido (NVARCHAR)
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
-- Descri��o: Exclui um tipo de sensor com base no ID informado
-- Par�metro: tipoSensorId (INT)
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
-- Descri��o: Retorna um tipo de sensor espec�fico pelo seu ID
-- Par�metro: tipoSensorId (INT)
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
-- Descri��o: Lista todos os tipos de sensores cadastrados na tabela TipoSensores
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarTipoSensores
AS
BEGIN
    SELECT * FROM TipoSensores
END
GO