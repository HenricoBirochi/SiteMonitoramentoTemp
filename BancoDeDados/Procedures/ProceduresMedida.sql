-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- PROCEDURE: spInserirMedidas
-- Descrição: Insere um novo registro na tabela Medidas
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spInserirMedidas
    @medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
	@estado VARCHAR(40),
	@dispositivoId int
AS
BEGIN
    INSERT INTO Medidas (medidaId, valorMedido, horarioMedicao, estado, dispositivoId)
    VALUES (@medidaId, @valorMedido, @horarioMedicao, @estado, @dispositivoId)
END
GO

-- ========================================
-- PROCEDURE: spAlterarMedidas
-- Descrição: Atualiza os dados de um usuário existente na tabela Medidas
-- Parâmetros: usuarioId (INT), usuarioNome (NVARCHAR), senha (NVARCHAR), email (NVARCHAR), cpf (VARCHAR)
-- ========================================
CREATE PROCEDURE spAlterarMedidas
	@medidaId INT,
    @valorMedido DECIMAL(18,2),
    @horarioMedicao DATETIME,
	@estado VARCHAR(40),
    @dispositivoId INT
AS
BEGIN
    UPDATE Medidas
    SET valorMedido = @valorMedido,
        horarioMedicao = @horarioMedicao,
		estado = @estado,
        dispositivoId = @dispositivoId
    WHERE medidaId = @medidaId
END
GO

CREATE PROCEDURE spConsultaAvancadaMedidas
(
@valorMedido decimal(18,2),
@dataInicial datetime,
@dataFinal datetime,
@estado varchar(40)
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
	m.valorMedido between @valorMedidoIni and @valorMedidoFim and
	m.estado like '%' + @estado + '%'
	order by m.horarioMedicao desc
end
GO