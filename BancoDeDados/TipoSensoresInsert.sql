-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE HephaiTech
GO

-- ========================================
-- Fazendo os "INSERT" dos Sensores Base:
-- ========================================

-- Inserção de tipo de sensor DHT11, que mede temperatura e umidade com alta precisão
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (1, 'DHT11', 'Temperatura e Umidade');

-- Inserção de tipo de sensor DHT22, uma versão mais simples e econômica que também mede temperatura e umidade
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (2, 'DHT22', 'Temperatura e Umidade');

-- Inserção de tipo de sensor RTC (Real Time Clock), responsável por fornecer data e horário precisos ao sistema
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (3, 'RTC', 'Data e o horário');

-- Inserção de tipo de sensor FotoSensor, utilizado para detectar a intensidade da luz (luminosidade do ambiente)
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (4, 'FotoSensor', 'Luminosidade');