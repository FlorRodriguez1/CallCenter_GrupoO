USE master;
GO

IF DB_ID('CALLCENTER_DB') IS NOT NULL
BEGIN
    ALTER DATABASE CALLCENTER_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE CALLCENTER_DB;
END
GO

CREATE DATABASE CALLCENTER_DB;
GO

USE CALLCENTER_DB;
GO

-- =============================================
-- 1. TABLAS DE ENUMS
-- =============================================

CREATE TABLE Perfiles (
    IdPerfil      INT          NOT NULL PRIMARY KEY,
    Descripcion   NVARCHAR(30) NOT NULL
);

CREATE TABLE EstadosIncidencia (
    IdEstado      INT          NOT NULL PRIMARY KEY,
    Descripcion   NVARCHAR(30) NOT NULL
);

CREATE TABLE EstadosSolicitud (
    IdEstadoSolicitud INT          NOT NULL PRIMARY KEY,
    Descripcion       NVARCHAR(30) NOT NULL
);
GO

-- =============================================
-- 2. TABLAS ADMINISTRABLES
-- =============================================

CREATE TABLE Usuarios (
    IdUsuario      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    NombreUsuario  NVARCHAR(50)  NOT NULL UNIQUE,
    Clave          NVARCHAR(256) NOT NULL,
    Nombre         NVARCHAR(50)  NOT NULL,
    Apellido       NVARCHAR(50)  NOT NULL,
    Email          NVARCHAR(100) NOT NULL,
    IdPerfil       INT           NOT NULL REFERENCES Perfiles(IdPerfil),
    Activo         BIT           NOT NULL DEFAULT 1
);

CREATE TABLE Clientes (
    IdCliente  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    DNI        NVARCHAR(15)  NOT NULL UNIQUE,
    Nombre     NVARCHAR(50)  NOT NULL,
    Apellido   NVARCHAR(50)  NOT NULL,
    Email      NVARCHAR(100) NOT NULL,
    Telefono   NVARCHAR(30)  NULL,
    Activo     BIT           NOT NULL DEFAULT 1
);

CREATE TABLE TiposIncidencia (
    IdTipo       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Descripcion  NVARCHAR(50) NOT NULL,
    Activo       BIT          NOT NULL DEFAULT 1
);

CREATE TABLE Prioridades (
    IdPrioridad  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Descripcion  NVARCHAR(50) NOT NULL,
    Activo       BIT          NOT NULL DEFAULT 1
);
GO

-- =============================================
-- 3. INCIDENCIAS Y TABLAS RELACIONADAS
-- =============================================

CREATE TABLE Incidencias (
    IdIncidencia        INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdCliente           INT            NOT NULL REFERENCES Clientes(IdCliente),
    IdTipo              INT            NOT NULL REFERENCES TiposIncidencia(IdTipo),
    IdPrioridad         INT            NOT NULL REFERENCES Prioridades(IdPrioridad),
    IdEstado            INT            NOT NULL REFERENCES EstadosIncidencia(IdEstado),
    IdUsuarioCreador    INT            NOT NULL REFERENCES Usuarios(IdUsuario),
    IdUsuarioAsignado   INT            NOT NULL REFERENCES Usuarios(IdUsuario),
    Problematica        NVARCHAR(2000) NOT NULL,
    FechaCreacion       DATETIME       NOT NULL DEFAULT GETDATE(),
    FechaResolucion     DATETIME       NULL,
    FechaCierre         DATETIME       NULL,
    ComentarioRespuesta NVARCHAR(1000) NULL,
    ComentarioCierre    NVARCHAR(1000) NULL
);

CREATE TABLE HistorialIncidencias (
    IdHistorial       INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdIncidencia      INT            NOT NULL REFERENCES Incidencias(IdIncidencia),
    Fecha             DATETIME       NOT NULL DEFAULT GETDATE(),
    IdUsuario         INT            NOT NULL REFERENCES Usuarios(IdUsuario),
    IdEstadoAnterior  INT            NULL REFERENCES EstadosIncidencia(IdEstado),
    IdEstadoNuevo     INT            NOT NULL REFERENCES EstadosIncidencia(IdEstado),
    Comentario        NVARCHAR(1000) NULL
);

CREATE TABLE SolicitudesReapertura (
    IdSolicitud          INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    IdIncidencia         INT            NOT NULL REFERENCES Incidencias(IdIncidencia),
    IdUsuarioSolicitante INT            NOT NULL REFERENCES Usuarios(IdUsuario),
    Motivo               NVARCHAR(1000) NOT NULL,
    FechaSolicitud       DATETIME       NOT NULL DEFAULT GETDATE(),
    IdEstadoSolicitud    INT            NOT NULL DEFAULT 1 REFERENCES EstadosSolicitud(IdEstadoSolicitud),
    IdUsuarioSupervisor  INT            NULL REFERENCES Usuarios(IdUsuario),
    FechaRespuesta       DATETIME       NULL,
    ComentarioRespuesta  NVARCHAR(1000) NULL
);
GO

-- =============================================
-- 4. DATOS FIJOS (ENUMS)
-- =============================================

INSERT INTO Perfiles (IdPerfil, Descripcion) VALUES
(1, N'Administrador'),
(2, N'Telefonista'),
(3, N'Supervisor');

INSERT INTO EstadosIncidencia (IdEstado, Descripcion) VALUES
(1, N'Abierto'),
(2, N'Asignado'),
(3, N'En análisis'),
(4, N'Resuelto'),
(5, N'Reabierto'),
(6, N'Cerrado');

INSERT INTO EstadosSolicitud (IdEstadoSolicitud, Descripcion) VALUES
(1, N'Pendiente'),
(2, N'Aprobada'),
(3, N'Rechazada');
GO

-- =============================================
-- 5. DATOS DE PRUEBA
-- =============================================

INSERT INTO TiposIncidencia (Descripcion) VALUES
(N'Facturación'),
(N'Problema técnico'),
(N'Consulta comercial'),
(N'Otro');

INSERT INTO Prioridades (Descripcion) VALUES
(N'Baja'),
(N'Media'),
(N'Alta'),
(N'Urgente');

INSERT INTO Usuarios (NombreUsuario, Clave, Nombre, Apellido, Email, IdPerfil) VALUES
(N'admin',        N'PENDIENTE_HASH', N'Ana',   N'Gómez', N'admin@callcenter.test',        1),
(N'telefonista1', N'PENDIENTE_HASH', N'Bruno', N'Pérez', N'telefonista1@callcenter.test', 2),
(N'telefonista2', N'PENDIENTE_HASH', N'Carla', N'Díaz',  N'telefonista2@callcenter.test', 2),
(N'supervisor',   N'PENDIENTE_HASH', N'Diego', N'López', N'supervisor@callcenter.test',   3);

INSERT INTO Clientes (DNI, Nombre, Apellido, Email, Telefono) VALUES
(N'30111222', N'Laura',   N'Fernández', N'laura.fernandez@example.com', N'11-4000-1111'),
(N'28333444', N'Martín',  N'Suárez',    N'martin.suarez@example.com',   N'11-4000-2222'),
(N'35555666', N'Sofía',   N'Romero',    N'sofia.romero@example.com',    N'11-4000-3333'),
(N'27777888', N'Javier',  N'Torres',    N'javier.torres@example.com',   N'11-4000-4444'),
(N'33999000', N'Valeria', N'Acosta',    N'valeria.acosta@example.com',  NULL);

INSERT INTO Incidencias (IdCliente, IdTipo, IdPrioridad, IdEstado, IdUsuarioCreador, IdUsuarioAsignado, Problematica) VALUES
(1, 1, 2, 1, 2, 2, N'La factura de octubre tiene un cargo duplicado.'),
(2, 2, 3, 3, 2, 2, N'Sin servicio de internet desde hace dos días.'),
(3, 3, 1, 2, 3, 2, N'Consulta por cambio de plan.');

INSERT INTO HistorialIncidencias (IdIncidencia, IdUsuario, IdEstadoAnterior, IdEstadoNuevo, Comentario) VALUES
(1, 2, NULL, 1, N'Alta de incidencia'),
(2, 2, NULL, 1, N'Alta de incidencia'),
(2, 2, 1,    3, N'Se derivó a soporte técnico'),
(3, 3, NULL, 1, N'Alta de incidencia'),
(3, 4, 1,    2, N'Reasignada a telefonista1');
GO

-- =============================================
-- 6. VISTA
-- =============================================

CREATE VIEW VW_Incidencias AS
SELECT
    I.IdIncidencia,
    I.Problematica,
    I.FechaCreacion,
    I.FechaResolucion,
    I.FechaCierre,
    I.IdCliente,
    C.Nombre + ' ' + C.Apellido   AS Cliente,
    I.IdTipo,
    T.Descripcion                 AS Tipo,
    I.IdPrioridad,
    P.Descripcion                 AS Prioridad,
    I.IdEstado,
    E.Descripcion                 AS Estado,
    I.IdUsuarioCreador,
    UC.Nombre + ' ' + UC.Apellido AS UsuarioCreador,
    I.IdUsuarioAsignado,
    UA.Nombre + ' ' + UA.Apellido AS UsuarioAsignado
FROM Incidencias I
INNER JOIN Clientes C           ON C.IdCliente   = I.IdCliente
INNER JOIN TiposIncidencia T    ON T.IdTipo      = I.IdTipo
INNER JOIN Prioridades P        ON P.IdPrioridad = I.IdPrioridad
INNER JOIN EstadosIncidencia E  ON E.IdEstado    = I.IdEstado
INNER JOIN Usuarios UC          ON UC.IdUsuario  = I.IdUsuarioCreador
INNER JOIN Usuarios UA          ON UA.IdUsuario  = I.IdUsuarioAsignado;
GO

-- =============================================
-- 7. PROCEDIMIENTOS ALMACENADOS 
-- =============================================

CREATE PROCEDURE SP_ListarClientes AS
BEGIN
    SELECT IdCliente, DNI, Nombre, Apellido, Email, Telefono, Activo
    FROM Clientes;
END
GO

CREATE PROCEDURE SP_ObtenerCliente
    @IdCliente INT
AS
BEGIN
    SELECT IdCliente, DNI, Nombre, Apellido, Email, Telefono, Activo
    FROM Clientes
    WHERE IdCliente = @IdCliente;
END
GO

CREATE PROCEDURE SP_AgregarCliente
    @DNI      NVARCHAR(15),
    @Nombre   NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @Email    NVARCHAR(100),
    @Telefono NVARCHAR(30)
AS
BEGIN
    INSERT INTO Clientes (DNI, Nombre, Apellido, Email, Telefono)
    VALUES (@DNI, @Nombre, @Apellido, @Email, @Telefono);
END
GO

CREATE PROCEDURE SP_ModificarCliente
    @IdCliente INT,
    @DNI       NVARCHAR(15),
    @Nombre    NVARCHAR(50),
    @Apellido  NVARCHAR(50),
    @Email     NVARCHAR(100),
    @Telefono  NVARCHAR(30),
    @Activo    BIT
AS
BEGIN
    UPDATE Clientes
    SET DNI      = @DNI,
        Nombre   = @Nombre,
        Apellido = @Apellido,
        Email    = @Email,
        Telefono = @Telefono,
        Activo   = @Activo
    WHERE IdCliente = @IdCliente;
END
GO

CREATE PROCEDURE SP_ListarTiposIncidencia AS
BEGIN
    SELECT IdTipo, Descripcion, Activo FROM TiposIncidencia;
END
GO

CREATE PROCEDURE SP_ListarPrioridades AS
BEGIN
    SELECT IdPrioridad, Descripcion, Activo FROM Prioridades;
END
GO
