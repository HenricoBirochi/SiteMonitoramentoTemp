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
-- CRIA A TABELA DE USUÁRIOS
-- ========================================
CREATE TABLE Usuarios(
	usuarioId INT PRIMARY KEY,                                       -- Identificador único do usuário
	usuarioNome VARCHAR(40),                                         -- Nome do usuário
	senha VARCHAR(40),                                               -- Senha de acesso
	email VARCHAR(100),                                              -- Email do usuário
	cpf VARCHAR(30),                                                 -- CPF do usuário
	imagem varbinary(max)										     -- Imagem do usuário
)

-- ========================================
-- CRIA A TABELA DE Dispositivos
-- ========================================
CREATE TABLE Dispositivos(
	dispositivoId INT PRIMARY KEY,									 -- Identificador único de dispositivo
	dispositivoNome varchar(40)										 -- Nome do ambiente
)

-- ========================================
-- CRIA A TABELA DE USUÁRIOS COM SEUS SENSORES
-- ========================================
CREATE TABLE UsuarioDispositivo(
	usuarioDispositivoId INT PRIMARY KEY,                               -- Identificador único da relação
	usuarioId INT,                                                   -- ID do usuário (chave estrangeira)
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

	FOREIGN KEY (dispositivoId) REFERENCES Dispositivos(dispositivoId)
)