USE HephaiTech

GO

create procedure spDelete
(
 @id int ,
 @nomeDoCampoId varchar(max),
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = ' delete ' + @tabela +
 ' where '+ @nomeDoCampoId +' = ' + cast(@id as varchar(max))
 exec(@sql)
end
GO
create procedure spConsulta
(
 @id int ,
 @nomeDoCampoId varchar(max),
 @tabela varchar(max)
)
as
begin
 declare @sql varchar(max);
 set @sql = 'select * from ' + @tabela +
 ' where '+ @nomeDoCampoId +' = ' + cast(@id as varchar(max))
 exec(@sql)
end
GO
create procedure spListagem
(
 @tabela varchar(max),
 @ordem varchar(max))
as
begin
 exec('select * from ' + @tabela +
 ' order by ' + @ordem)
end
GO
CREATE PROCEDURE spProximoId
    @tabela VARCHAR(MAX),
	@nomeDoCampoId VARCHAR(MAX)
AS
BEGIN
    EXEC('SELECT ISNULL(MAX(' + @nomeDoCampoId + ') + 1, 1) AS MAIOR FROM ' + @tabela)
END
GO