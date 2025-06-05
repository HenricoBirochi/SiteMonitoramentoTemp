-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirDispositivos
-- Descri��o: Insere um novo registro na tabela Dispositivos
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
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
-- Descri��o: Atualiza os dados de um sensor existente na tabela Sensores
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
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