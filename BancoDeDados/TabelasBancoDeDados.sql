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
-- CRIA A TABELA DE TIPOS DE SENSORES
-- ========================================
CREATE TABLE TipoSensores(
	tipoSensorId INT PRIMARY KEY,                                    -- Identificador �nico do tipo de sensor
	nomeTecnico VARCHAR(50),                                         -- Nome t�cnico do sensor
	parametroMedido VARCHAR(50)                                      -- Par�metro que o sensor mede
)

-- ========================================
-- CRIA A TABELA DE SENSORES
-- ========================================
CREATE TABLE Sensores(
	sensorId INT PRIMARY KEY,                                        -- Identificador �nico do sensor
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
-- CRIA A TABELA DE USU�RIOS COM SEUS SENSORES
-- ========================================
CREATE TABLE UsuarioSensores(
	usuarioSensorId INT PRIMARY KEY,                                 -- Identificador �nico da rela��o
	usuarioId INT,                                                   -- ID do usu�rio (chave estrangeira)
	sensorId INT,                                                    -- ID do sensor (chave estrangeira)

	FOREIGN KEY (usuarioId) REFERENCES Usuarios(usuarioId),          -- Relacionamento com Usuarios
	FOREIGN KEY (sensorId) REFERENCES Sensores(sensorId)             -- Relacionamento com Sensores
)
