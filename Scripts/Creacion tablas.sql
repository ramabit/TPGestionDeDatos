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

IF OBJECT_ID('LOS_SUPER_AMIGOS.Direccion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Direccion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Usuario', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.Factura', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Factura

IF OBJECT_ID('LOS_SUPER_AMIGOS.Forma_Pago', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Forma_Pago

IF (OBJECT_ID('LOS_SUPER_AMIGOS.crear_usuario') IS NOT NULL)
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_usuario
GO

IF (OBJECT_ID('LOS_SUPER_AMIGOS.agregar_id_publ') IS NOT NULL)
DROP FUNCTION LOS_SUPER_AMIGOS.agregar_id_publ
GO

DROP SCHEMA LOS_SUPER_AMIGOS;
GO

CREATE SCHEMA [LOS_SUPER_AMIGOS] AUTHORIZATION [gd]
GO

create table LOS_SUPER_AMIGOS.Usuario
(
id numeric(18,0) IDENTITY(1,1),
username as isnull('USER' + CAST(ID AS NVARCHAR(10)),'X'),--nvarchar(50)
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

create table LOS_SUPER_AMIGOS.Cliente
(
id numeric(18,0) identity(1,1),
dni numeric(18,0),
apellido nvarchar(255),
nombre nvarchar(255),
fecha_nacimiento datetime,
mail nvarchar(255),
habilitado bit default 1,
usuario_id numeric(18,0),
direccion numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
FOREIGN KEY (direccion) REFERENCES LOS_SUPER_AMIGOS.Direccion (id)
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

