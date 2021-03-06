--CREATE SCHEMA MATE_LAVADO
--GO

--drop schema MATE_LAVADO

DROP TABLE MATE_LAVADO.UsuarioXRol
DROP TABLE MATE_LAVADO.FuncionalidadXRol
DROP TABLE MATE_LAVADO.Roles
DROP TABLE MATE_LAVADO.Funcionalidades
DROP TABLE MATE_LAVADO.Premios
DROP TABLE MATE_LAVADO.Puntos
DROP TABLE MATE_LAVADO.UbicacionXEspectaculo
DROP TABLE MATE_LAVADO.Espectaculos
DROP TABLE MATE_LAVADO.Ubicaciones
DROP TABLE MATE_LAVADO.TiposDeUbicacion
DROP TABLE MATE_LAVADO.Compras
DROP TABLE MATE_LAVADO.Facturas
DROP TABLE MATE_LAVADO.Publicaciones
DROP TABLE MATE_LAVADO.Rubros
DROP TABLE MATE_LAVADO.Grados_publicacion
DROP TABLE MATE_LAVADO.Empresas
DROP TABLE MATE_LAVADO.Medios_de_pago
DROP TABLE MATE_LAVADO.Clientes
DROP TABLE MATE_LAVADO.Usuarios
GO

---------------CREACION DE TABLAS---------------

CREATE TABLE MATE_LAVADO.Usuarios(
id_usuario INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.UsuarioXRol(
id_usuarioXrol INT IDENTITY(1,1) PRIMARY KEY,
)
GO

CREATE TABLE MATE_LAVADO.Roles(
id_rol INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.FuncionalidadXRol(
id_funcionalidadXrol INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Funcionalidades(
id_funcionalidad INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Clientes(
id_cliente INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Medios_de_pago(
id_medio_de_pago INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Puntos(
id_punto INT IDENTITY(1,1) PRIMARY KEY,
)
GO

CREATE TABLE MATE_LAVADO.Empresas(
id_empresa INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Facturas(
id_factura INT PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Compras(
id_compra INT IDENTITY PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Publicaciones(
id_publicacion INT IDENTITY PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Espectaculos(
id_espectaculo INT PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.UbicacionXEspectaculo(
id_ubicacion_espectaculo INT IDENTITY PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Rubros(
id_rubro INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Grados_publicacion(
id_grado_publicacion INT IDENTITY(1,1) PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.TiposDeUbicacion(
id_tipo_ubicacion INT IDENTITY PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Ubicaciones(
id_ubicacion INT IDENTITY PRIMARY KEY
)
GO

CREATE TABLE MATE_LAVADO.Premios(
id_premio INT IDENTITY(1,1) PRIMARY KEY
)
GO

-----ALTER TABLES-----

ALTER TABLE MATE_LAVADO.Usuarios ADD
username VARCHAR(255),
password VARCHAR(255),
habilitado BIT,
alta_logica DATETIME,
intentos_fallidos INT,
debe_cambiar_pass BIT
GO

ALTER TABLE MATE_LAVADO.UsuarioXRol ADD
id_usuario INT REFERENCES MATE_LAVADO.Usuarios,
id_rol INT REFERENCES MATE_LAVADO.Roles
GO

ALTER TABLE MATE_LAVADO.Roles ADD
nombre CHAR(40),
habilitado BIT
GO

ALTER TABLE MATE_LAVADO.FuncionalidadXRol ADD
id_funcionalidad INT REFERENCES MATE_LAVADO.Funcionalidades,
id_rol INT REFERENCES MATE_LAVADO.Roles
GO

ALTER TABLE MATE_LAVADO.Funcionalidades ADD
nombre VARCHAR(100)
GO

ALTER TABLE MATE_LAVADO.Clientes ADD
id_usuario INT NOT NULL REFERENCES MATE_LAVADO.Usuarios,
nombre NVARCHAR(255),
apellido NVARCHAR(255),
tipo_documento CHAR(3),
documento NUMERIC(18,0),
cuil NUMERIC(18,0),
mail NVARCHAR(50),
telefono NUMERIC(15),
fecha_creacion DATETIME,
fecha_nacimiento DATETIME,
calle NVARCHAR(255),
numero_calle NUMERIC(18,0),
piso NUMERIC(18,0),
depto NVARCHAR(255),
codigo_postal NVARCHAR(50)
GO

ALTER TABLE MATE_LAVADO.Medios_de_pago ADD
id_cliente INT REFERENCES MATE_LAVADO.Clientes,
descripcion VARCHAR(10) CHECK(descripcion IN ('Efectivo', 'Tarjeta')),
nro_tarjeta NUMERIC(30),
titular NVARCHAR(50)
GO

ALTER TABLE MATE_LAVADO.Puntos ADD
id_cliente INT REFERENCES MATE_LAVADO.Clientes,
cantidad_puntos INT,
fecha_vencimiento DATE
GO

ALTER TABLE MATE_LAVADO.Empresas ADD
id_usuario INT REFERENCES MATE_LAVADO.Usuarios,
razon_social NVARCHAR(255),
mail NVARCHAR(50),
cuit NVARCHAR(255),
fecha_creacion DATETIME,
calle NVARCHAR(50),
numero_calle NUMERIC(18,0),
piso NUMERIC(18,0),
depto NVARCHAR(50),
codigo_postal NVARCHAR(50)
GO

ALTER TABLE MATE_LAVADO.Facturas ADD
id_empresa INT REFERENCES MATE_LAVADO.Empresas,
fecha_facturacion DATETIME,
importe_total NUMERIC(18,2)
GO

ALTER TABLE MATE_LAVADO.Compras ADD
id_cliente INT REFERENCES MATE_LAVADO.Clientes,
id_medio_de_pago INT REFERENCES MATE_LAVADO.Medios_de_pago,
id_factura INT REFERENCES MATE_LAVADO.Facturas,
comision NUMERIC(3,3),
fecha DATETIME,
importe INT
GO

ALTER TABLE MATE_LAVADO.Publicaciones ADD
id_empresa INT REFERENCES MATE_LAVADO.Empresas,
id_grado_publicacion INT REFERENCES MATE_LAVADO.Grados_publicacion,
id_rubro INT REFERENCES MATE_LAVADO.Rubros,
descripcion NVARCHAR(255),
direccion VARCHAR(80)
GO

ALTER TABLE MATE_LAVADO.Espectaculos ADD
id_publicacion INT REFERENCES MATE_LAVADO.Publicaciones,
fecha_inicio DATETIME,
fecha_evento DATETIME,
estado_espectaculo CHAR(15) CHECK(estado_espectaculo IN ('Borrador', 'Publicada', 'Finalizada'))
GO

ALTER TABLE MATE_LAVADO.UbicacionXEspectaculo ADD
id_espectaculo INT REFERENCES MATE_LAVADO.Espectaculos,
id_ubicacion INT REFERENCES MATE_LAVADO.Ubicaciones,
id_compra INT REFERENCES MATE_LAVADO.Compras
GO

ALTER TABLE MATE_LAVADO.Grados_publicacion ADD
comision NUMERIC(3,3),
nombre NVARCHAR(20)
GO

ALTER TABLE MATE_LAVADO.Rubros ADD
descripcion NVARCHAR(100)
GO

ALTER TABLE MATE_LAVADO.TiposDeUbicacion ADD
descripcion NVARCHAR(255)
GO

ALTER TABLE MATE_LAVADO.Ubicaciones ADD
codigo_tipo_ubicacion INT,
tipo_ubicacion NVARCHAR(20),
fila VARCHAR(3),
asiento NUMERIC(18),
sin_numerar BIT,
precio NUMERIC(18)
GO

ALTER TABLE MATE_LAVADO.Premios ADD
descripcion VARCHAR(110),
puntos BIGINT
GO

--.--.--.--.--.--.--ROLES--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Roles(nombre, habilitado)
VALUES ('Administrativo', 1),('Empresa', 1),('Cliente', 1),('adminOP', 1)
GO

--.--.--.--.--.--.--FUNCIONALIDADES--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Funcionalidades
VALUES
('Login y seguridad'),
('ABM de rol'),
('Registro de usuario'),
('ABM de cliente'),
('ABM de empresa de espectaculos'),
('ABM de rubro'),
('ABM grado de publicacion'),
('Generar publicacion'),
('Editar publicacion'),
('Comprar'),
('Historial del cliente'),
('Canje y administracion de puntos'),
('Generar pago de comisiones'),
('Listado estadistico')
GO

--.--.--.--.--.--.--USUARIOS--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Usuarios(username, password, habilitado, alta_logica, intentos_fallidos, debe_cambiar_pass)
VALUES('admin', 'w23e', 1, GETDATE(), 0, 0)
GO

INSERT INTO MATE_LAVADO.Usuarios(username, password, habilitado, alta_logica, intentos_fallidos, debe_cambiar_pass)
VALUES('sa', 'gestiondedatos', 1, GETDATE(), 0, 0)
GO

INSERT INTO MATE_LAVADO.Usuarios (username, password, habilitado, alta_logica, intentos_fallidos, debe_cambiar_pass)
SELECT DISTINCT Cli_Dni, Cli_Dni, 1, GETDATE(), 0, 0
FROM gd_esquema.Maestra
WHERE Cli_Dni IS NOT NULL
GO

INSERT INTO MATE_LAVADO.Usuarios (username, password, habilitado, alta_logica, intentos_fallidos, debe_cambiar_pass)
SELECT DISTINCT Espec_Empresa_Cuit, Espec_Empresa_Cuit, 1, GETDATE(), 0, 0
FROM gd_esquema.Maestra
GO

--.--.--.--.--.--.--CLIENTES--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Clientes(id_usuario, nombre, apellido, tipo_documento, documento, cuil, mail, fecha_creacion, fecha_nacimiento, calle, numero_calle, piso, depto, codigo_postal)
SELECT DISTINCT u.id_usuario, Cli_Nombre, Cli_Apeliido, 'DNI' as Tipo_DNI, Cli_Dni, NULL, Cli_Mail, GETDATE() AS fecha_creacion, Cli_Fecha_Nac, Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Usuarios u ON(u.username = CAST(gd.Cli_Dni as varchar))
WHERE Cli_Dni IS NOT NULL
GO

--.--.--.--.--.--.--EMPRESAS--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Empresas(id_usuario, razon_social, mail, cuit, fecha_creacion, calle, numero_calle, piso, depto, codigo_postal)
SELECT DISTINCT u.id_usuario, Espec_Empresa_Razon_Social ,Espec_Empresa_Mail, REPLACE(Espec_Empresa_Cuit,'-',''), Espec_Empresa_Fecha_Creacion,
				Espec_Empresa_Dom_Calle, Espec_Empresa_Nro_Calle, Espec_Empresa_Piso, Espec_Empresa_Depto,
				Espec_Empresa_Cod_Postal
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Usuarios u ON(u.username = gd.Espec_Empresa_Cuit)
WHERE Espec_Empresa_Cuit IS NOT NULL
GO

--.--.--.--.--.--.--ROLXUSUARIO--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.UsuarioXRol(id_usuario, id_rol)
SELECT c.id_usuario, r.id_rol
FROM MATE_LAVADO.Clientes c, MATE_LAVADO.Roles r
WHERE r.nombre = 'Cliente'
GO

INSERT INTO MATE_LAVADO.UsuarioXRol(id_usuario, id_rol)
SELECT e.id_usuario, r.id_rol
FROM MATE_LAVADO.Empresas e, MATE_LAVADO.Roles r
WHERE r.nombre = 'Empresa'
GO

INSERT INTO MATE_LAVADO.UsuarioXRol(id_usuario, id_rol) VALUES((SELECT id_usuario FROM MATE_LAVADO.Usuarios WHERE username like 'admin'), 4)
GO
INSERT INTO MATE_LAVADO.UsuarioXRol(id_usuario, id_rol) VALUES((SELECT id_usuario FROM MATE_LAVADO.Usuarios WHERE username like 'sa'), 1)
GO

--.--.--.--.--.--.--FUNCIONALIDADXROL--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.FuncionalidadXRol(id_rol, id_funcionalidad)
SELECT 1, id_funcionalidad
FROM MATE_LAVADO.Funcionalidades
WHERE nombre IN('ABM de cliente', 'ABM de empresa de espectaculos', 'Generar pago de comisiones', 'Listado estadistico', 'Registro de usuario')
GO

INSERT INTO MATE_LAVADO.FuncionalidadXRol(id_rol, id_funcionalidad)
SELECT 2, id_funcionalidad
FROM MATE_LAVADO.Funcionalidades
WHERE nombre IN('ABM de categoria', 'ABM grado de publicacion', 'Editar publicacion', 'Generar publicacion')
GO

INSERT INTO MATE_LAVADO.FuncionalidadXRol(id_rol, id_funcionalidad)
SELECT 3, id_funcionalidad
FROM MATE_LAVADO.Funcionalidades
WHERE nombre IN('Canje y administracion de puntos', 'Comprar', 'Historial del cliente')
GO

INSERT INTO MATE_LAVADO.FuncionalidadXRol(id_rol, id_funcionalidad)
SELECT 4, id_funcionalidad
FROM MATE_LAVADO.Funcionalidades
GO

--.--.--.--.--.--.--RUBROS--.--.--.--.--.--.--
INSERT INTO MATE_LAVADO.Rubros
SELECT DISTINCT Espectaculo_Rubro_Descripcion
FROM gd_esquema.Maestra
GO

INSERT INTO MATE_LAVADO.Rubros
VALUES
('Concierto'),
('Obra infantil'),
('Musical'),
('Stand-up'),
('Familiar')
GO

--.--.--.--.--.--.--GRADOS--.--.--.--.--.--.--
INSERT INTO MATE_LAVADO.Grados_publicacion(comision, nombre)
VALUES
(0.5, 'Alto'),
(0.35, 'Medio'),
(0.2, 'Bajo')
GO

--.--.--.--.--.--.--PUBLICACION--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Publicaciones(id_empresa, id_grado_publicacion, id_rubro, descripcion,
			direccion)
SELECT DISTINCT e.id_empresa, NULL, 1, Espectaculo_Descripcion, NULL
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Empresas e ON (e.razon_social = gd.Espec_Empresa_Razon_Social)
GO

--.--.--.--.--.--.--ESPECT�CULOS--.--.--.--.--.--.--
INSERT INTO MATE_LAVADO.Espectaculos(id_espectaculo, id_publicacion, fecha_inicio, fecha_evento, estado_espectaculo)
SELECT DISTINCT gd.Espectaculo_Cod, p.id_publicacion, Espectaculo_Fecha, Espectaculo_Fecha_Venc, Espectaculo_Estado
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Publicaciones p ON (p.descripcion = gd.Espectaculo_Descripcion)
GO

--.--.--.--.--.--.--TIPOSDEUBICACION--.--.--.--.--.--.--

SET IDENTITY_INSERT MATE_LAVADO.TiposDeUbicacion ON
INSERT INTO MATE_LAVADO.TiposDeUbicacion(id_tipo_ubicacion, descripcion)
SELECT DISTINCT Ubicacion_Tipo_Codigo, Ubicacion_Tipo_Descripcion
FROM gd_esquema.Maestra
SET IDENTITY_INSERT MATE_LAVADO.TiposDeUbicacion OFF
GO

--.--.--.--.--.--.--UBICACIONES--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Ubicaciones(codigo_tipo_ubicacion, fila, asiento, sin_numerar, precio)
SELECT DISTINCT gd.Ubicacion_Tipo_Codigo, gd.Ubicacion_Fila, gd.Ubicacion_Asiento, Ubicacion_Sin_numerar, Ubicacion_Precio
FROM gd_esquema.Maestra gd
GO

--.--.--.--.--.--.--FACTURAS--.--.--.--.--.--.--
INSERT INTO MATE_LAVADO.Facturas(id_factura, id_empresa, fecha_facturacion, importe_total)
SELECT DISTINCT Factura_Nro, e.id_empresa, Factura_Fecha, Factura_Total
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Empresas e ON(e.razon_social = gd.Espec_Empresa_Razon_Social)
WHERE Factura_Nro IS NOT NULL
GO

--.--.--.--.--.--.--MEDIODEPAGO--.--.--.--.--.--.--
INSERT INTO MATE_LAVADO.Medios_de_pago(c.id_cliente, descripcion, nro_tarjeta, titular)
SELECT DISTINCT c.id_cliente, Forma_Pago_Desc, NULL, NULL
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Clientes c ON(c.documento = gd.Cli_Dni)
WHERE gd.Item_Factura_Monto IS NOT NULL
GO

--.--.--.--.--.--.--COMPRA--.--.--.--.--.--.--

CREATE TABLE MATE_LAVADO.#ComprasTemp(
id_compra NUMERIC IDENTITY(1,1) PRIMARY KEY,
id_cliente INT,
id_espectaculo INT,
id_medio_de_pago INT,
id_factura INT,
fecha DATETIME,
asiento INT,
fila CHAR(1),
tipo_codigo INT
)
GO

INSERT INTO MATE_LAVADO.#ComprasTemp(id_cliente, id_espectaculo, id_medio_de_pago, id_factura, fecha, asiento, fila, tipo_codigo)
SELECT c.id_cliente, gd.Espectaculo_Cod, mp.id_medio_de_pago, f.id_factura, gd.Compra_Fecha, gd.Ubicacion_Asiento, gd.Ubicacion_Fila, gd.Ubicacion_Tipo_Codigo
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Clientes c ON(gd.Cli_Dni = c.documento)
JOIN MATE_LAVADO.Facturas f ON(f.id_factura = gd.Factura_Nro)
JOIN MATE_LAVADO.Medios_de_pago mp ON(c.id_cliente = mp.id_cliente)
WHERE gd.Forma_Pago_Desc IS NOT NULL
GO

--.--.--.--.--.--.--UBICACIONXESPECTACULO--.--.--.--.--.--.--

SET IDENTITY_INSERT MATE_LAVADO.Compras ON
INSERT INTO MATE_LAVADO.Compras(id_compra, id_cliente, id_factura, id_medio_de_pago, fecha)
SELECT DISTINCT id_compra, id_cliente, id_factura, id_medio_de_pago, fecha
FROM MATE_LAVADO.#ComprasTemp
SET IDENTITY_INSERT MATE_LAVADO.Compras OFF
GO

INSERT INTO MATE_LAVADO.UbicacionXEspectaculo(id_espectaculo, id_ubicacion, id_compra)
SELECT DISTINCT gd.Espectaculo_Cod, u.id_ubicacion, ct.id_compra
FROM gd_esquema.Maestra gd
JOIN MATE_LAVADO.Ubicaciones u ON (gd.Ubicacion_Tipo_Codigo = u.codigo_tipo_ubicacion
	AND gd.Ubicacion_Fila = u.fila
	AND gd.Ubicacion_Asiento = u.asiento AND gd.Ubicacion_Sin_numerar = u.sin_numerar
	AND gd.Ubicacion_Precio = u.precio)
LEFT JOIN MATE_LAVADO.#ComprasTemp ct ON(ct.id_espectaculo = gd.Espectaculo_Cod
		AND ct.asiento = gd.Ubicacion_Asiento
		AND ct.fila = gd.Ubicacion_Fila
		AND ct.tipo_codigo = gd.Ubicacion_Tipo_Codigo)
GO

DROP TABLE MATE_LAVADO.#ComprasTemp


UPDATE MATE_LAVADO.Compras
SET importe = u.precio
FROM MATE_LAVADO.Compras c
JOIN MATE_LAVADO.UbicacionXEspectaculo uxe ON(uxe.id_compra = c.id_compra)
JOIN MATE_LAVADO.Ubicaciones u ON(u.id_ubicacion = uxe.id_ubicacion)
WHERE c.id_compra = uxe.id_compra
GO

--.--.--.--.--.--.--PUNTOS--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Puntos(id_cliente, cantidad_puntos, fecha_vencimiento)
SELECT id_cliente, importe, DATEADD(year, 1, fecha)
FROM MATE_LAVADO.Compras
GO

--.--.--.--.--.--.--PREMIOS--.--.--.--.--.--.--

INSERT INTO MATE_LAVADO.Premios(descripcion, puntos)
VALUES
('Plancha', 800),
('2x1 en la proxima compra', 300),
('Set de 6 platos', 500),
('SEGA', 2000),
('Fin de semana en Tandil', 8000),
('Batidora', 1000)
GO

--.--.--.--.--.--.--ENCRIPTACION--.--.--.--.--.--.--
BEGIN TRANSACTION
UPDATE MATE_LAVADO.Usuarios set password = LOWER(CONVERT(char(100),HASHBYTES('SHA2_256', password),2))
COMMIT
GO

--delete from MATE_LAVADO.UsuarioXRol
--delete from MATE_LAVADO.Clientes where nombre = 'Ro'
--delete from MATE_LAVADO.Empresas where razon_social = 'RoCo'
--delete from MATE_LAVADO.Usuarios where username = 'ro'



-----TESTING-----
--ro lol Cliente--
INSERT INTO MATE_LAVADO.Usuarios VALUES('ro', 'lol', 1, '12-12-2016 02:03:02', 0, 0)
DECLARE @id_ro INT
SET @id_ro = SCOPE_IDENTITY()
INSERT INTO MATE_LAVADO.Clientes VALUES(@id_ro, 'Ro', 'Chi', 'DNI', NULL, 290151473212, 'ro@ro.com', 15423681, '11-11-2011 03:03:30', '10-10-2010 04:04:04', 'Gu', 12, 5, 'C', 1412)

--INSERT INTO MATE_LAVADO.Empresas VALUES(@id_ro, 'RoCo', 'ro@co.com', 154843, '11-11-2012 03:03:30', 'Gur', 12, 3, 'C', 5486)

INSERT INTO MATE_LAVADO.UsuarioXRol VALUES(@id_ro, 3)
--INSERT INTO MATE_LAVADO.UsuarioXRol VALUES(@id_ro, 2)


INSERT INTO MATE_LAVADO.Usuarios VALUES('roco', 'lol', 1, '12-12-2016 02:03:02', 0, 0)
DECLARE @id_roo INT
SET @id_roo = SCOPE_IDENTITY()
INSERT INTO MATE_LAVADO.Empresas VALUES(@id_roo, 'RoCo', 'rorro@lo.com', 4853, '12-12-2016 02:03:04', 'Gs', 84, 1, 'G', 548)

INSERT INTO MATE_LAVADO.UsuarioXRol VALUES(@id_roo, 2)


select * from MATE_LAVADO.Empresas

--.--.--.--.--.--.--ENCRIPTACION--.--.--.--.--.--.--
BEGIN TRANSACTION
UPDATE MATE_LAVADO.Usuarios set password = LOWER(CONVERT(char(100),HASHBYTES('SHA2_256', password),2))
COMMIT
GO




--mati 123 Cliente--
INSERT INTO MATE_LAVADO.Usuarios VALUES('mati', '123', 1, '12-12-2016 02:03:02', 0, 0)
DECLARE @id_mati INT
SET @id_mati = SCOPE_IDENTITY()
INSERT INTO MATE_LAVADO.Clientes VALUES(@id_mati, NULL, NULL, NULL, NULL, NULL, NULL, 15423681, '11-11-2011 03:03:30', '10-10-2010 04:04:04', NULL, NULL, NULL, NULL, NULL)
INSERT INTO MATE_LAVADO.UsuarioXRol VALUES(@id_mati, 3)

--select * from MATE_LAVADO.Usuarios ORDER BY id_usuario desc
--select * from MATE_LAVADO.Roles
--select * from MATE_LAVADO.clientes order by id_cliente desc
select * from MATE_LAVADO.Clientes WHERE id_usuario = 785
select * from MATE_LAVADO.Empresas WHERE id_usuario = 785

--.--.--.--.--.--.--ENCRIPTACION--.--.--.--.--.--.--
BEGIN TRANSACTION
UPDATE MATE_LAVADO.Usuarios set password = LOWER(CONVERT(char(100),HASHBYTES('SHA2_256', password),2))
COMMIT




select * from MATE_LAVADO.Compras order by id_compra desc
select * from MATE_LAVADO.UbicacionXEspectaculo order by id_ubicacion_espectaculo desc