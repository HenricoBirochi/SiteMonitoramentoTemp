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
CREATE PROCEDURE spInserirAmbientes
    @ambienteId INT,
    @ambienteNome NVARCHAR(40)
AS
BEGIN
    INSERT INTO Ambientes (ambienteId, ambienteNome)
    VALUES (@ambienteId, @ambienteNome)
END
GO

-- ========================================
-- PROCEDURE: spAlterarSensor
-- Descri��o: Atualiza os dados de um sensor existente na tabela Sensores
-- Par�metros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
-- ========================================
CREATE PROCEDURE spAlterarAmbientes
    @ambienteId INT,
    @ambienteNome NVARCHAR(40)
AS
BEGIN
    UPDATE Ambientes
    SET ambienteNome = @ambienteNome
    WHERE ambienteId = @ambienteId
END
GO

-- ========================================
-- PROCEDURE: spListarUsuarioSensorJoin
-- Descri��o: Retorna a rela��o entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem par�metros
-- ========================================
CREATE PROCEDURE spListarAmbienteSensorTipoSensorJoin
	@ambienteId INT
AS
BEGIN
    SELECT 
        s.sensorNome, ts.nomeTecnico, ts.parametroMedido
    FROM Sensores s
	INNER JOIN TipoSensores ts ON s.tipoSensorId = ts.tipoSensorId
	where s.ambienteId = @ambienteId
END
GO