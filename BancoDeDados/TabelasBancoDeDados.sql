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
-- CRIA A TABELA DE TIPOS DE SENSORES
-- ========================================
CREATE TABLE TipoSensores(
	tipoSensorId INT PRIMARY KEY,                                    -- Identificador único do tipo de sensor
	nomeTecnico VARCHAR(50),                                         -- Nome técnico do sensor
	parametroMedido VARCHAR(50)                                      -- Parâmetro que o sensor mede
)

-- ========================================
-- CRIA A TABELA DE SENSORES
-- ========================================
CREATE TABLE Sensores(
	sensorId INT PRIMARY KEY,                                        -- Identificador único do sensor
	sensorNome VARCHAR(50),                                          -- Nome do sensor
	tipoSensorId INT,                                                -- Tipo do sensor (chave estrangeira)

	FOREIGN KEY (tipoSensorId) REFERENCES TipoSensores(tipoSensorId) -- Relacionamento com TipoSensores
)

-- ========================================
-- CRIA A TABELA DE MEDIDAS
-- ========================================
CREATE TABLE Medidas(
	medidaId INT PRIMARY KEY,
	valorMedido DECIMAL(18,2),
	horarioMedicao DATETIME,
	sensorId INT,

	FOREIGN KEY (sensorId) REFERENCES Sensores(sensorId)
)

-- ========================================
-- CRIA A TABELA DE USUÁRIOS COM SEUS SENSORES
-- ========================================
CREATE TABLE UsuarioSensores(
	usuarioSensorId INT PRIMARY KEY,                                 -- Identificador único da relação
	usuarioId INT,                                                   -- ID do usuário (chave estrangeira)
	sensorId INT,                                                    -- ID do sensor (chave estrangeira)

	FOREIGN KEY (usuarioId) REFERENCES Usuarios(usuarioId),          -- Relacionamento com Usuarios
	FOREIGN KEY (sensorId) REFERENCES Sensores(sensorId)             -- Relacionamento com Sensores
)
