-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirSensor
-- Descrição: Insere um novo registro na tabela Sensores
-- Parâmetros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
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
-- Descrição: Atualiza os dados de um sensor existente na tabela Sensores
-- Parâmetros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
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
-- Descrição: Exclui um sensor com base no ID informado
-- Parâmetro: sensorId (INT)
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
-- Descrição: Retorna um sensor específico pelo seu ID
-- Parâmetro: sensorId (INT)
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
-- Descrição: Lista todos os sensores cadastrados na tabela Sensores
-- Sem parâmetros
-- ========================================
CREATE PROCEDURE spListarSensores
AS
BEGIN
    SELECT * FROM Sensores
END
GO

-- ========================================
-- PROCEDURE: spListarSensoresTipoSensoresJoin
-- Descrição: Retorna a relação entre as tabelas Sensores e TipoSensores com dados detalhados
-- Sem parâmetros
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