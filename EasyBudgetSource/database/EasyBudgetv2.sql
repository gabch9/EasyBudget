USE master;
CREATE DATABASE EasyBudget2;
USE EasyBudget2;

CREATE TABLE LOGINS(
	LoginId INT PRIMARY KEY IDENTITY(1,1),
	LoginUsername VARCHAR(250) NOT NULL,
	LoginPassword VARCHAR(250) NOT NULL
)

CREATE TABLE PERSONAS(
	PersonaId INT PRIMARY KEY IDENTITY(1,1),
	PersonaNombre VARCHAR(50) NOT NULL,
	PersonaApellido VARCHAR(50) NOT NULL,
	PersonaFechaNacimiento DATE NOT NULL,
	PersonaOcupacion VARCHAR(100) NOT NULL,
	PersonaPais VARCHAR(250) NOT NULL,
	FkLoginId INT FOREIGN KEY REFERENCES LOGINS(LoginId)
)

CREATE TABLE TIPOOBJETIVO(
	TipoObjetivoId INT PRIMARY KEY IDENTITY(1,1),
	TipoObjetivoDescripcion VARCHAR (150) NOT NULL
)

CREATE TABLE OBJETIVOS(
	ObjetivoId INT PRIMARY KEY IDENTITY(1,1),
	ObjetivoFechaInicio DATE,
	ObjetivoFechaFin DATE,
	ObjetivoDescripcion VARCHAR (250),
	ObjetivoCompleto BIT,
	ObjetivoValor DECIMAL(10, 5),
	ObjetivoFechaModificacion TIMESTAMP,
	FkPersonaId INT FOREIGN KEY REFERENCES PERSONAS(PersonaId)
)

CREATE TABLE COMERCIOS(
	ComercioId INT PRIMARY KEY IDENTITY(1,1),
	ComercioNombre VARCHAR(250),
	ComercioUbicacion VARCHAR(250)
)

CREATE TABLE COMPROBANTES(
	ComprobanteId INT PRIMARY KEY IDENTITY(1,1),
	ComprobanteUrl VARCHAR(250),
	FkPersonaId INT FOREIGN KEY REFERENCES PERSONAS(PersonaId)
)

CREATE TABLE TIPOTRANSACCION(
	TipoTransaccionId INT PRIMARY KEY IDENTITY(1,1),
	TransaccionDescripcion VARCHAR(250)
)

CREATE TABLE TRANSACCIONES(
	TransaccionId INT PRIMARY KEY IDENTITY(1,1),
	TransaccionDescripcion VARCHAR(250),
	TransaccionValor DECIMAL(10,5),
	TransaccionFecha DATE,
	TransaccionFechaModificacion TIMESTAMP,
	FkIdComercio INT FOREIGN KEY REFERENCES COMERCIOS(ComercioId),
	FkIdPersona INT FOREIGN KEY REFERENCES PERSONAS(PersonaId),
	FkIdComprobante INT FOREIGN KEY REFERENCES COMPROBANTES(ComprobanteId),
	FkIdTipoTransaccion INT FOREIGN KEY REFERENCES TIPOTRANSACCION(TipoTransaccionId) 
)

