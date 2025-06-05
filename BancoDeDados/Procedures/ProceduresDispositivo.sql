-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirDispositivos
-- Descrição: Insere um novo registro na tabela Dispositivos
-- Parâmetros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spInserirDispositivos
    @dispositivoId INT,
    @dispositivoNome NVARCHAR(40)
AS
BEGIN
    INSERT INTO Dispositivos (dispositivoId, dispositivoNome)
    VALUES (@dispositivoId, @dispositivoNome)
END
GO

-- ========================================
-- PROCEDURE: spAlterarSensor
-- Descrição: Atualiza os dados de um sensor existente na tabela Sensores
-- Parâmetros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarDispositivos
    @dispositivoId INT,
    @dispositivoNome NVARCHAR(40)
AS
BEGIN
    UPDATE Dispositivos
    SET dispositivoNome = @dispositivoNome
    WHERE dispositivoId = @dispositivoId
END
GO