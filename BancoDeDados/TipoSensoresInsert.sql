-- ========================================
-- USA O BANCO DE DADOS CRIADO
-- ========================================
USE SiteMonitoramentoSensores
GO

-- ========================================
-- Fazendo os "INSERT" dos Sensores Base:
-- ========================================

-- Inser��o de tipo de sensor DHT22, que mede temperatura e umidade com alta precis�o
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (1, 'DHT22', 'Temperatura e Umidade');

-- Inser��o de tipo de sensor DHT11, uma vers�o mais simples e econ�mica que tamb�m mede temperatura e umidade
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (2, 'DHT11', 'Temperatura e Umidade');

-- Inser��o de tipo de sensor FotoSensor, utilizado para detectar a intensidade da luz (luminosidade do ambiente)
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (3, 'FotoSensor', 'Luminosidade');

-- Inser��o de tipo de sensor RTC (Real Time Clock), respons�vel por fornecer data e hor�rio precisos ao sistema
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (4, 'RTC', 'Data e o hor�rio');

-- Inser��o de tipo de sensor DHT33, outra variante para medir temperatura e umidade, possivelmente com melhorias sobre vers�es anteriores
INSERT INTO TipoSensores (tipoSensorId, nomeTecnico, parametroMedido) 
VALUES (5, 'DHT33', 'Temperatura e Umidade');