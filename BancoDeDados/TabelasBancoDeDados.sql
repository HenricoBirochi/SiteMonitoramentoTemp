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
-- CRIA A TABELA DE Ambientes
-- ========================================
CREATE TABLE Ambientes(
	ambienteId INT PRIMARY KEY,										 -- Identificador �nico de ambiente
	ambienteNome varchar(40)										 -- Nome do ambiente
)

-- ========================================
-- CRIA A TABELA DE USU�RIOS COM SEUS SENSORES
-- ========================================
CREATE TABLE UsuarioAmbiente(
	usuarioAmbienteId INT PRIMARY KEY,                               -- Identificador �nico da rela��o
	usuarioId INT,                                                   -- ID do usu�rio (chave estrangeira)
	ambienteId INT,                                                  -- ID do ambiente (chave estrangeira)

	FOREIGN KEY (usuarioId) REFERENCES Usuarios(usuarioId),          -- Relacionamento com Usuarios
	FOREIGN KEY (ambienteId) REFERENCES Ambientes(ambienteId)        -- Relacionamento com Ambientes
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
	ambienteId INT,

	FOREIGN KEY (tipoSensorId) REFERENCES TipoSensores(tipoSensorId),-- Relacionamento com TipoSensores
	FOREIGN KEY (ambienteId) REFERENCES Ambientes(ambienteId)		 -- Relacionamento com Ambientes
)

-- ========================================
-- CRIA A TABELA DE MEDIDAS
-- ========================================
CREATE TABLE Medidas(
	medidaId INT PRIMARY KEY,
	valorMedido DECIMAL(18,2),
	horarioMedicao DATETIME,
	parametro varchar(20),
	sensorId INT,

	FOREIGN KEY (sensorId) REFERENCES Sensores(sensorId)
)