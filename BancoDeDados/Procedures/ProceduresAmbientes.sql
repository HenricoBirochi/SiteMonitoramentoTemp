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
-- Descrição: Atualiza os dados de um sensor existente na tabela Sensores
-- Parâmetros: sensorId (INT), sensorNome (NVARCHAR), tipoSensorId (INT)
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
-- Descrição: Retorna a relação entre as tabelas UsuarioSensores, Usuarios e Sensores com dados detalhados
-- Sem parâmetros
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