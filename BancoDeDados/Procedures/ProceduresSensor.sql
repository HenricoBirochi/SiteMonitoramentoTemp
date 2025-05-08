-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirSensor
-- Descri��o: Insere um novo registro na tabela Sensores
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spInserirSensores
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
CREATE PROCEDURE spAlterarSensores
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
-- PROCEDURE: spListarSensoresTipoSensoresJoin
-- Descri��o: Retorna a rela��o entre as tabelas Sensores e TipoSensores com dados detalhados
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarSensoresTipoSensoresJoin
AS
BEGIN
    SELECT 
        sensorId, sensorNome, nomeTecnico
    FROM Sensores s
    INNER JOIN TipoSensores ts ON ts.tipoSensorId = s.tipoSensorId
END
GO