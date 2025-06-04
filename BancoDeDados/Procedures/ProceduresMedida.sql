-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirMedidas
-- Descri��o: Insere um novo registro na tabela Medidas
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spInserirMedidas
    @medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
	@dispositivoId int
AS
BEGIN
    INSERT INTO Medidas (medidaId, valorMedido, horarioMedicao, dispositivoId)
    VALUES (@medidaId, @valorMedido, @horarioMedicao, @dispositivoId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarMedidas
-- Descri��o: Atualiza os dados de um usu�rio existente na tabela Medidas
-- Par�metros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarMedidas
	@medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
    @dispositivoId INT
AS
BEGIN
    UPDATE Medidas
    SET valorMedido = @valorMedido,
        horarioMedicao = @horarioMedicao,
        dispositivoId = @dispositivoId
    WHERE medidaId = @medidaId
END
GO

CREATE PROCEDURE spConsultaAvancadaMedidas
(
@valorMedido decimal(18,2),
@dataInicial datetime,
@dataFinal datetime
)
as
begin
	declare @valorMedidoIni int
	declare @valorMedidoFim int

	set @valorMedidoIni = case @valorMedido when 0 then 0 else @valorMedido end
	set @valorMedidoFim = case @valorMedido when 0 then 999999 else @valorMedido end

	select m.*
	from Medidas m
	where m.horarioMedicao between @dataInicial and @dataFinal and
	m.valorMedido between @valorMedidoIni and @valorMedidoFim
	order by m.horarioMedicao
end
GO