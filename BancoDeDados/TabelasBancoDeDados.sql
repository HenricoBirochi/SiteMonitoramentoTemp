create table Usuarios(
	usuarioId int primary key,
	usuarioNome varchar(40),
	senha varchar(40),
	email varchar(100),
	cpf varchar(30)
)
create table UsuarioSensores(
	usuarioSensorId int primary key,
	usuarioId int,
	sensorId int, 

	foreign key (usuarioId) references Usuarios(usuarioId),
	foreign key (sensorId) references Sensores(sensorId)
)
create table Sensores(
	sensorId int primary key,
	sensorNome varchar(50),
	tipoSensorId int,

	foreign key (tipoSensorId) references TipoSensores(tipoSensorId)
)
create table TipoSensores(
	tipoSensorId int primary key,
	nomeTecnico varchar(50),
	parametroMedido varchar(50)
)