-- ========================================
-- CRIA O BANCO DE DADOS
-- ========================================
CREATE DATABASE HephaiTech
GO

-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- CRIA A TABELA DE USU�RIOS
-- ========================================
CREATE TABLE Usuarios(
	usuarioId INT PRIMARY KEY,                                       -- Identificador �nico do usu�rio
	usuarioNome VARCHAR(40),                                         -- Nome do usu�rio
	senha VARCHAR(40),                                               -- Senha de acesso
	email VARCHAR(100),                                              -- Email do usu�rio
	cpf VARCHAR(30),                                                 -- CPF do usu�rio
	imagem varbinary(max)										     -- Imagem do usu�rio
)

-- ========================================
-- CRIA A TABELA DE Dispositivos
-- ========================================
CREATE TABLE Dispositivos(
	dispositivoId INT PRIMARY KEY,									 -- Identificador �nico de dispositivo
	dispositivoNome varchar(40)										 -- Nome do ambiente
)

-- ========================================
-- CRIA A TABELA DE USU�RIOS COM SEUS SENSORES
-- ========================================
CREATE TABLE UsuarioDispositivo(
	usuarioDispositivoId INT PRIMARY KEY,                               -- Identificador �nico da rela��o
	usuarioId INT,                                                   -- ID do usu�rio (chave estrangeira)
	dispositivoId INT,                                               -- ID do dispositivo (chave estrangeira)

	FOREIGN KEY (usuarioId) REFERENCES Usuarios(usuarioId),          -- Relacionamento com Usuarios
	FOREIGN KEY (dispositivoId) REFERENCES Dispositivos(dispositivoId)        -- Relacionamento com Dispositivos
)

-- ========================================
-- CRIA A TABELA DE MEDIDAS
-- ========================================
CREATE TABLE Medidas(
	medidaId INT PRIMARY KEY,
	valorMedido DECIMAL(18,2),
	horarioMedicao DATETIME,
	dispositivoId INT,

	FOREIGN KEY (dispositivoId) REFERENCES Dispositivos(dispositivoId) ON DELETE CASCADE
)