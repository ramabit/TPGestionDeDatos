IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'LOS_SUPER_AMIGOS')
BEGIN
	EXEC ('CREATE SCHEMA LOS_SUPER_AMIGOS AUTHORIZATION gd')
END
GO

IF OBJECT_ID('LOS_SUPER_AMIGOS.Funcionalidad_x_Rol', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Funcionalidad_x_Rol

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rol_x_Usuario', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rol_x_Usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.Pregunta', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Pregunta

IF OBJECT_ID('LOS_SUPER_AMIGOS.Oferta', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Oferta

IF OBJECT_ID('LOS_SUPER_AMIGOS.Compra', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Compra

IF OBJECT_ID('LOS_SUPER_AMIGOS.Calificacion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Calificacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Item_Factura', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Item_Factura

IF OBJECT_ID('LOS_SUPER_AMIGOS.Publicacion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Publicacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rubro', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rubro

IF OBJECT_ID('LOS_SUPER_AMIGOS.Visibilidad', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Visibilidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rol', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rol

IF OBJECT_ID('LOS_SUPER_AMIGOS.Funcionalidad', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Funcionalidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.Empresa', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Empresa

IF OBJECT_ID('LOS_SUPER_AMIGOS.Cliente', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Cliente

IF OBJECT_ID('LOS_SUPER_AMIGOS.TipoDeDocumento', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.TipoDeDocumento

IF OBJECT_ID('LOS_SUPER_AMIGOS.Direccion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Direccion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Usuario', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.Factura', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Factura

IF OBJECT_ID('LOS_SUPER_AMIGOS.Forma_Pago', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Forma_Pago

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_usuario', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_usuario
GO

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_usuario_con_valores', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_usuario_con_valores
GO

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_direccion', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_direccion
GO

IF OBJECT_ID('LOS_SUPER_AMIGOS.agregar_id_publ') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.agregar_id_publ
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_usuario_con_valores
	@username nvarchar(50),
	@password nvarchar(150),
	@usuario_id numeric(18,0) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO LOS_SUPER_AMIGOS.Usuario
	(username, password) 
	values (@username, @password)
	SET @usuario_id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_usuario
	@usuario_id numeric(18,0) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO LOS_SUPER_AMIGOS.Usuario
	(username) 
	values (isnull('USER' + CAST(((select COUNT(*) from LOS_SUPER_AMIGOS.Usuario)+ 1) AS NVARCHAR(10)),''))
	SET @usuario_id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_direccion
	@calle nvarchar(100),
	@numero numeric(18,0),
	@piso numeric(18,0),
	@depto nvarchar(5),
	@cod_postal nvarchar(50),
	@localidad nvarchar(50),
	@direccion_id numeric(18,0) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO LOS_SUPER_AMIGOS.Direccion (calle, numero, piso, depto, cod_postal, localidad) values (@calle, @numero, @piso, @depto, @cod_postal, @localidad);
	SET @direccion_id = SCOPE_IDENTITY();
END
GO


create table LOS_SUPER_AMIGOS.Usuario
(
id numeric(18,0) IDENTITY(1,1),
username nvarchar(50) ,--
password nvarchar(150) default '559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd', -- hash de 'A'
habilitado bit default 1,
login_fallidos int default 0,
PRIMARY KEY (id)
)

create table LOS_SUPER_AMIGOS.Direccion
(
id numeric(18,0) identity(1,1),
calle nvarchar(100),
numero numeric(18,0),
piso numeric(18,0),
depto nvarchar(5),
cod_postal nvarchar(50),
localidad nvarchar(50),
PRIMARY KEY(id)
)

create table LOS_SUPER_AMIGOS.Empresa
(
id numeric(18,0) identity(1,1),
razon_social nvarchar(255),
cuit nvarchar(50),
fecha_creacion datetime,
mail nvarchar(50),
habilitado bit default 1,
usuario_id numeric(18,0),
direccion numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (direccion) REFERENCES LOS_SUPER_AMIGOS.Direccion (id)
)

create table LOS_SUPER_AMIGOS.TipoDeDocumento
(
id numeric(18,0) identity(1,1),
nombre nvarchar(50),
PRIMARY KEY(id)
)

create table LOS_SUPER_AMIGOS.Cliente
(
id numeric(18,0) identity(1,1),
nombre nvarchar(255),
apellido nvarchar(255),
tipo_de_documento_id numeric(18,0),
documento numeric(18,0),
fecha_nacimiento datetime,
mail nvarchar(255),
telefono numeric(18,0),
habilitado bit default 1,
usuario_id numeric(18,0),
direccion_id numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (tipo_de_documento_id) REFERENCES LOS_SUPER_AMIGOS.TipoDeDocumento (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (direccion_id) REFERENCES LOS_SUPER_AMIGOS.Direccion (id)
)

create table LOS_SUPER_AMIGOS.Rol
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) NOT NULL,
habilitado bit NOT NULL default 1,
PRIMARY KEY (id)
)

create table LOS_SUPER_AMIGOS.Rol_x_Usuario
(
rol_id numeric(18,0),
usuario_id numeric(18,0),
PRIMARY KEY (rol_id, usuario_id),
FOREIGN KEY (rol_id) REFERENCES LOS_SUPER_AMIGOS.Rol (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
)

create table LOS_SUPER_AMIGOS.Funcionalidad
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) not null,
PRIMARY KEY (id)
)

create table LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
(
funcionalidad_id numeric(18, 0),
rol_id numeric(18, 0),
PRIMARY KEY(funcionalidad_id, rol_id),
FOREIGN KEY (funcionalidad_id) REFERENCES LOS_SUPER_AMIGOS.Funcionalidad (id),
FOREIGN KEY (rol_id) REFERENCES LOS_SUPER_AMIGOS.Rol (id)
)

create table LOS_SUPER_AMIGOS.Visibilidad
(
id numeric(18,0),
descripcion nvarchar(255),
precio numeric(18,2),
porcentaje numeric(18,0),
habilitado bit default 1,
PRIMARY KEY (id)
)

create table LOS_SUPER_AMIGOS.Rubro
(
id numeric(18,0) identity(1,1),
descripcion nvarchar(255),
habilitado bit default 1,
PRIMARY KEY(id)
)

create table LOS_SUPER_AMIGOS.Publicacion
(
id numeric(18,0) identity(1,1),
descripcion nvarchar(255),
stock numeric(18,0),
fecha_inicio datetime,
fecha_vencimiento datetime,
precio numeric(18,0),
rubro_id numeric(18,0),
visibilidad_id numeric(18,0),
usuario_id numeric(18,0),
estado nvarchar(255),
tipo nvarchar(255),
se_realizan_preguntas bit default 1,
habilitado bit default 1,
PRIMARY KEY (id),
FOREIGN KEY (visibilidad_id) REFERENCES LOS_SUPER_AMIGOS.Visibilidad (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (rubro_id) REFERENCES LOS_SUPER_AMIGOS.Rubro(id),
)

create table LOS_SUPER_AMIGOS.Pregunta
(
id numeric(18,0),
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
respuesta_fecha datetime default null,
publicacion_id numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id)
)

create table LOS_SUPER_AMIGOS.Calificacion
(
id numeric(18,0) identity(1,1),
cantidad_estrellas numeric(18,0),
descripcion nvarchar(255),
PRIMARY KEY (id)
)

create table LOS_SUPER_AMIGOS.Oferta
(
id numeric(18,0) identity(1,1),
monto numeric(18,0),
gano_subasta bit default 0,
fecha datetime,
usuario_id numeric(18,0),
publicacion_id numeric(18,0),
calificacion_id numeric(18,0) default null,
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id),
FOREIGN KEY (calificacion_id) REFERENCES LOS_SUPER_AMIGOS.Calificacion (id)
)

create table LOS_SUPER_AMIGOS.Compra
(
id numeric(18,0) identity(1,1),
cantidad numeric(18,0),
fecha datetime,
usuario_id numeric(18,0),
publicacion_id numeric(18,0),
calificacion_id numeric(18,0) default null,
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id),
FOREIGN KEY (calificacion_id) REFERENCES LOS_SUPER_AMIGOS.Calificacion (id),
)

create table LOS_SUPER_AMIGOS.Forma_Pago
(
id numeric(18,0) identity(1,1),
descripcion nvarchar(255),
PRIMARY KEY (id),
)

create table LOS_SUPER_AMIGOS.Factura
(
nro numeric(18,0),
fecha DATETIME,
total numeric(18,2),
forma_pago_id numeric(18,0),
PRIMARY KEY (nro),
FOREIGN KEY (forma_pago_id) REFERENCES LOS_SUPER_AMIGOS.Forma_Pago (id)
)

create table LOS_SUPER_AMIGOS.Item_Factura
(
id numeric(18,0) identity(1,1),
monto numeric(18,2),
cantidad numeric(18,0),
factura_nro numeric(18,0),
publicacion_id numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (factura_nro) REFERENCES LOS_SUPER_AMIGOS.Factura (nro),
FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id)
)

-- INSERTAR Usuario


-- INSERTAR Direcciones de Empresas
INSERT INTO LOS_SUPER_AMIGOS.Direccion
([calle],[numero],[piso],[depto],[cod_postal])
SELECT DISTINCT Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Piso, Publ_Empresa_Depto, Publ_Empresa_Cod_Postal
FROM gd_esquema.Maestra
WHERE ISNULL(Publ_Empresa_Dom_Calle,'')!=''

-- INSERTAR Direcciones de Clientes que vendieron
INSERT INTO LOS_SUPER_AMIGOS.Direccion
([calle],[numero],[piso],[depto],[cod_postal])
SELECT DISTINCT Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Piso, Publ_Cli_Depto, Publ_Cli_Cod_Postal
FROM gd_esquema.Maestra
WHERE ISNULL(Publ_Cli_Dom_Calle,'')!=''

-- INSERTAR Direcciones de Clientes que compraron
INSERT INTO LOS_SUPER_AMIGOS.Direccion
([calle],[numero],[piso],[depto],[cod_postal])
SELECT DISTINCT Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal
FROM gd_esquema.Maestra
WHERE ISNULL(Cli_Dom_Calle,'')!='' and not exists (SELECT * FROM LOS_SUPER_AMIGOS.Direccion dir WHERE Cli_Dom_Calle = dir.calle and Cli_Nro_Calle = dir.numero and Cli_Piso = dir.piso and Cli_Depto = dir.depto and Cli_Cod_Postal = dir.cod_postal)

-- INSERTAR Empresas
INSERT INTO LOS_SUPER_AMIGOS.Empresa
   ( [razon_social], [cuit], [fecha_creacion], [mail], [direccion])
SELECT DISTINCT Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Fecha_Creacion, Publ_Empresa_Mail,
	(SELECT DISTINCT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Publ_Empresa_Dom_Calle = d.calle and Publ_Empresa_Nro_Calle = d.numero and Publ_Empresa_Piso = d.piso and Publ_Empresa_Depto = d.depto and Publ_Empresa_Cod_Postal = d.cod_postal)) 
FROM gd_esquema.Maestra 
WHERE ISNULL(Publ_Empresa_Razon_Social, '') != ''

DECLARE @row_pos numeric(18,0)
DECLARE @row_count numeric(18,0)
SELECT @row_count = COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa
SET @row_pos = 1

WHILE (@row_pos <= @row_count)
BEGIN
	DECLARE @id_e numeric(18,0)
	EXEC LOS_SUPER_AMIGOS.crear_usuario @id_e OUTPUT
	UPDATE LOS_SUPER_AMIGOS.Empresa SET usuario_id = @id_e WHERE id = @row_pos
	SET @row_pos = @row_pos + 1
END

-- INSERTAR Tipo de documentos
INSERT INTO LOS_SUPER_AMIGOS.TipoDeDocumento (nombre) values ('DNI'), ('OTRO')

-- INSERTAR Clientes
-- Todos los clientes que compraron
INSERT INTO LOS_SUPER_AMIGOS.Cliente
   ( [tipo_de_documento_id], [documento], [apellido], [nombre], [fecha_nacimiento], [mail], [direccion_id])
SELECT DISTINCT 1, Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Fecha_Nac, Cli_Mail,
	(SELECT DISTINCT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Cli_Dom_Calle = d.calle and Cli_Nro_Calle = d.numero and Cli_Piso = d.piso and Cli_Depto = d.depto and Cli_Cod_Postal = d.cod_postal)) 
FROM gd_esquema.Maestra 
WHERE ISNULL(Cli_DNI, 0) != 0

-- Todos los clientes que vendieron
INSERT INTO LOS_SUPER_AMIGOS.Cliente
   ( [tipo_de_documento_id], [documento], [apellido], [nombre], [fecha_nacimiento], [mail], [direccion_id])
SELECT DISTINCT 1, Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Fecha_Nac, Publ_Cli_Mail,
	(SELECT DISTINCT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Publ_Cli_Dom_Calle = d.calle and Publ_Cli_Nro_Calle = d.numero and Publ_Cli_Piso = d.piso and Publ_Cli_Depto = d.depto and Publ_Cli_Cod_Postal = d.cod_postal)) 
FROM gd_esquema.Maestra as m
WHERE ISNULL(Publ_Cli_DNI, 0) != 0 and not exists (SELECT * FROM LOS_SUPER_AMIGOS.Cliente as c WHERE m.Publ_Cli_Dni = c.documento)

SELECT @row_count = COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente
SET @row_pos = 1

WHILE (@row_pos <= @row_count)
BEGIN
	DECLARE @id_c numeric(18,0)
	EXEC LOS_SUPER_AMIGOS.crear_usuario @id_c OUTPUT
	UPDATE LOS_SUPER_AMIGOS.Cliente SET usuario_id = @id_c WHERE id = @row_pos
	SET @row_pos = @row_pos + 1
END

-- INSERTAR Roles
INSERT INTO LOS_SUPER_AMIGOS.Rol(nombre)
VALUES('Administrador')

INSERT INTO LOS_SUPER_AMIGOS.Rol(nombre)
VALUES('Cliente')

INSERT INTO LOS_SUPER_AMIGOS.Rol(nombre)
VALUES('Empresa')

-- INSERTAR Roles_x_Usuario
INSERT INTO LOS_SUPER_AMIGOS.Rol_x_Usuario
(
	[rol_id],
	[usuario_id]
)
SELECT (SELECT id FROM LOS_SUPER_AMIGOS.Rol WHERE nombre = 'Empresa'), usuario_id FROM LOS_SUPER_AMIGOS.Empresa
INSERT INTO LOS_SUPER_AMIGOS.Rol_x_Usuario
(
	[rol_id],
	[usuario_id]
)
SELECT (SELECT id FROM LOS_SUPER_AMIGOS.Rol WHERE nombre = 'Cliente'), usuario_id FROM LOS_SUPER_AMIGOS.Cliente

-- INSERTAR Funcionalidad
INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad
(nombre)
VALUES ('Comprar / Ofertar'),('Generar publicacion'),('Editar publicacion'),
('Calificar vendedor'),('Preguntar'),('Responder preguntas'),('Gestionar roles'),
('Gestionar usuarios'),('Generar factura'),('Crear empresa'),('Editar empresa'),
('Crear cliente'),('Editar cliente'),('Agregar visibilidad'),('Editar visibilidad'),
('Agregar rubro'),('Editar rubro'),('Obtener estadisticas'),('Ver historial')


-- INSERTAR Funcionalidad_por_Rol
-- Agrego a administrador todas las funcionalidades
BEGIN TRANSACTION
	DECLARE @i int;
	SET @i = 0;
	
	WHILE((SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Funcionalidad) > @i)
	BEGIN
		SET @i = @i + 1;
		INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
		(funcionalidad_id,rol_id)
		VALUES(@i, 1);
	END
COMMIT

INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
VALUES(1,2),(2,2),(3,2),(4,2),(5,2),(6,2),(9,2),(18,2),(19,2),(2,3),(3,3),
(6,3),(9,3),(18,3)

-- INSERTAR Visibilidad
INSERT INTO LOS_SUPER_AMIGOS.Visibilidad
([id],[descripcion],[precio],[porcentaje])
SELECT DISTINCT Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Porcentaje, Publicacion_Visibilidad_Precio FROM gd_esquema.Maestra
GO

-- INSERTAR Rubro
INSERT INTO LOS_SUPER_AMIGOS.Rubro
([descripcion])
SELECT DISTINCT Publicacion_Rubro_Descripcion FROM gd_esquema.Maestra WHERE ISNULL(Publicacion_Rubro_Descripcion, '') != ''
GO

-- INSERTAR Publicacion
CREATE FUNCTION LOS_SUPER_AMIGOS.agregar_id_publ
(
	@documento numeric(18,0),
	@razon_social nvarchar(255)
)
RETURNS numeric(18,0)
AS
BEGIN
	DECLARE @id numeric(18,0)
	IF @documento IS NULL
		BEGIN
			SELECT @id = usuario_id FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razon_social
		END
	ELSE
		BEGIN
			SELECT @id = usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = @documento
		END
	RETURN @id
END
GO

-- SET IDENTITY_INSERT to ON.
SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Publicacion ON;
GO

INSERT INTO LOS_SUPER_AMIGOS.Publicacion
([id], [descripcion], [stock], [fecha_inicio], [fecha_vencimiento], [precio], [rubro_id], [visibilidad_id], [usuario_id], [estado], [tipo])
SELECT DISTINCT Publicacion_Cod, Publicacion_Descripcion, Publicacion_Stock, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, (SELECT id FROM LOS_SUPER_AMIGOS.Rubro r WHERE Publicacion_Rubro_Descripcion = r.descripcion), Publicacion_Visibilidad_Cod, LOS_SUPER_AMIGOS.agregar_id_publ(Publ_Cli_Dni, Publ_Empresa_Razon_Social),Publicacion_Estado,Publicacion_Tipo FROM gd_esquema.Maestra
WHERE ISNULL(Publicacion_Rubro_Descripcion, '') != ''

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Publicacion OFF;
GO

-- INSERTAR Calificaciones
SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Calificacion ON;
GO

INSERT INTO LOS_SUPER_AMIGOS.Calificacion
([id], [cantidad_estrellas], [descripcion])
SELECT DISTINCT Calificacion_Codigo, Calificacion_Cant_Estrellas, Calificacion_Descripcion FROM gd_esquema.Maestra WHERE ISNULL(Calificacion_Codigo, -1) != -1

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Calificacion OFF;
GO

-- INSERTAR Ofertas
INSERT INTO LOS_SUPER_AMIGOS.Oferta
([monto], [fecha], [usuario_id], [publicacion_id], [calificacion_id])
SELECT Oferta_Monto, Oferta_Fecha, (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = Cli_Dni), Publicacion_Cod, Calificacion_Codigo FROM gd_esquema.Maestra
WHERE ISNULL(Oferta_Monto, 0) != 0

-- INSERTAR Compras
INSERT INTO LOS_SUPER_AMIGOS.Compra
([cantidad], [fecha], [usuario_id], [publicacion_id], [calificacion_id])
SELECT Compra_Cantidad, Compra_Fecha, (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = Cli_Dni), Publicacion_Cod, Calificacion_Codigo FROM gd_esquema.Maestra
WHERE ISNULL(Compra_Cantidad, 0) != 0

-- INSERTAR Formas_Pago
INSERT INTO LOS_SUPER_AMIGOS.Forma_Pago
   ( [descripcion])
SELECT DISTINCT Forma_Pago_Desc
FROM gd_esquema.Maestra 
WHERE ISNULL(Forma_Pago_Desc,'') != ''

-- INSERTAR Facturas
INSERT INTO LOS_SUPER_AMIGOS.Factura
   ( [nro], [fecha], [total], [forma_pago_id])
SELECT DISTINCT Factura_Nro, Factura_Fecha, Factura_Total, (SELECT id FROM LOS_SUPER_AMIGOS.Forma_Pago WHERE descripcion = Forma_Pago_Desc)
FROM gd_esquema.Maestra 
WHERE ISNULL(Factura_Nro,-1) != -1

-- INSERTAR Items_Factura
INSERT INTO LOS_SUPER_AMIGOS.Item_Factura
   ( [monto], [cantidad], [factura_nro], [publicacion_id])
SELECT DISTINCT Item_Factura_Monto, Item_Factura_Cantidad, Factura_Nro, Publicacion_Cod
FROM gd_esquema.Maestra 
WHERE ISNULL(Factura_Nro,-1) != -1