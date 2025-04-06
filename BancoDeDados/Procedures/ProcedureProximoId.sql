-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: sp_ProximoId
-- Descri��o: Retorna o pr�ximo ID dispon�vel para uma tabela qualquer
-- Par�metro: @tabela (nome da tabela como string)
-- Observa��o: A procedure � gen�rica e usa EXEC din�mico
-- ========================================
CREATE PROCEDURE spProximoId
    @tabela VARCHAR(MAX),
	@nomeDoCampoId VARCHAR(MAX)
AS
BEGIN
    EXEC('SELECT ISNULL(MAX(' + @nomeDoCampoId + ') + 1, 1) AS MAIOR FROM ' + @tabela)
END
GO