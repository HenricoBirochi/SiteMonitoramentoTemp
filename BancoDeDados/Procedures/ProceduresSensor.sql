-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- PROCEDURE: spInserirSensor
-- Descri��o: Insere um novo registro na tabela Sensores
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spInserirSensor
    @sensorId INT,
    @sensorNome NVARCHAR(100),
    @tipoSensorId INT
AS
BEGIN
    INSERT INTO Sensores (sensorId, sensorNome, tipoSensorId)
    VALUES (@sensorId, @sensorNome, @tipoSensorId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarSensor
-- Descri��o: Atualiza os dados de um sensor existente na tabela Sensores
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarSensor
    @sensorId INT,
    @sensorNome NVARCHAR(100),
    @tipoSensorId INT
AS
BEGIN
    UPDATE Sensores
    SET sensorNome = @sensorNome,
        tipoSensorId = @tipoSensorId
    WHERE sensorId = @sensorId
END
GO

-- ========================================
-- PROCEDURE: spExcluirSensor
-- Descri��o: Exclui um sensor com base no ID informado
-- Par�metro: sensorId (INT)
-- ========================================
CREATE PROCEDURE spExcluirSensor
    @sensorId INT
AS
BEGIN
    DELETE FROM Sensores
    WHERE sensorId = @sensorId
END
GO

-- ========================================
-- PROCEDURE: spConsultarSensor
-- Descri��o: Retorna um sensor espec�fico pelo seu ID
-- Par�metro: sensorId (INT)
-- ========================================
CREATE PROCEDURE spConsultarSensor
    @sensorId INT
AS
BEGIN
    SELECT * FROM Sensores
    WHERE sensorId = @sensorId
END
GO

-- ========================================
-- PROCEDURE: spListarSensores
-- Descri��o: Lista todos os sensores cadastrados na tabela Sensores
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarSensores
AS
BEGIN
    SELECT * FROM Sensores
END
GO