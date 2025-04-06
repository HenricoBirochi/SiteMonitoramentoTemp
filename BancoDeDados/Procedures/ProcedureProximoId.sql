-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: sp_ProximoId
-- Descrição: Retorna o próximo ID disponível para uma tabela qualquer
-- Parâmetro: @tabela (nome da tabela como string)
-- Observação: A procedure é genérica e usa EXEC dinâmico
-- ========================================
CREATE PROCEDURE spProximoId
    @tabela VARCHAR(MAX),
	@nomeDoCampoId VARCHAR(MAX)
AS
BEGIN
    EXEC('SELECT ISNULL(MAX(' + @nomeDoCampoId + ') + 1, 1) AS MAIOR FROM ' + @tabela)
END
GO