IF OBJECT_ID('Funcionalidad_x_Rol', 'U') IS NOT NULL
DROP TABLE Funcionalidad_x_Rol

IF OBJECT_ID('Rol_x_Usuario', 'U') IS NOT NULL
DROP TABLE Rol_x_Usuario

IF OBJECT_ID('Pregunta', 'U') IS NOT NULL
DROP TABLE Pregunta

IF OBJECT_ID('Oferta', 'U') IS NOT NULL
DROP TABLE Oferta

IF OBJECT_ID('Compra', 'U') IS NOT NULL
DROP TABLE Compra

IF OBJECT_ID('Calificacion', 'U') IS NOT NULL
DROP TABLE Calificacion

IF OBJECT_ID('Item_Factura', 'U') IS NOT NULL
DROP TABLE Item_Factura

IF OBJECT_ID('Publicacion', 'U') IS NOT NULL
DROP TABLE Publicacion

IF OBJECT_ID('Rubro', 'U') IS NOT NULL
DROP TABLE Rubro

IF OBJECT_ID('Visibilidad', 'U') IS NOT NULL
DROP TABLE Visibilidad

IF OBJECT_ID('Rol', 'U') IS NOT NULL
DROP TABLE Rol

IF OBJECT_ID('Funcionalidad', 'U') IS NOT NULL
DROP TABLE Funcionalidad

IF OBJECT_ID('Empresa', 'U') IS NOT NULL
DROP TABLE Empresa

IF OBJECT_ID('Cliente', 'U') IS NOT NULL
DROP TABLE Cliente

IF OBJECT_ID('Direccion', 'U') IS NOT NULL
DROP TABLE Direccion

IF OBJECT_ID('Usuario', 'U') IS NOT NULL
DROP TABLE Usuario

IF OBJECT_ID('Factura', 'U') IS NOT NULL
DROP TABLE Factura

IF OBJECT_ID('Forma_Pago', 'U') IS NOT NULL
DROP TABLE Forma_Pago

create table Usuario
(
id numeric(18,0) IDENTITY(1,1),
username AS ISNULL('USER' + CAST(ID AS NVARCHAR(10)), 'X'),
password nvarchar(45) DEFAULT 'A',
habilitado bit default 1,
login_fallidos int default 0,
PRIMARY KEY (id)
)

create table Direccion
(
id numeric(18,0) identity(1,1),
calle nvarchar(100),
numero numeric(18,0),
piso numeric(18,0),
depto nvarchar(5),
cod_postal nvarchar(50),
PRIMARY KEY(id)
)

create table Empresa
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
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
FOREIGN KEY (direccion) REFERENCES Direccion (id)
)

create table Cliente
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
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
FOREIGN KEY (direccion) REFERENCES Direccion (id)
)

create table Rol
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) NOT NULL,
habilitado bit NOT NULL default 1,
PRIMARY KEY (id)
)

create table Rol_x_Usuario
(
rol_id numeric(18,0),
usuario_id numeric(18,0),
habilitado bit NOT NULL default 1,
PRIMARY KEY (rol_id, usuario_id),
FOREIGN KEY (rol_id) REFERENCES Rol (id),
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
)

create table Funcionalidad
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) not null,
PRIMARY KEY (id)
)

create table Funcionalidad_x_Rol
(
funcionalidad_id numeric(18, 0),
rol_id numeric(18, 0),
PRIMARY KEY(funcionalidad_id, rol_id),
FOREIGN KEY (funcionalidad_id) REFERENCES Funcionalidad (id),
FOREIGN KEY (rol_id) REFERENCES Rol (id)
)

create table Visibilidad
(
id numeric(18,0),
descripcion nvarchar(255),
precio numeric(18,2),
porcentaje numeric(18,0),
habilitado bit default 1,
PRIMARY KEY (id)
)

create table Rubro
(
id numeric(18,0) identity(1,1),
nombre nvarchar(255),
habilitado bit default 1,
PRIMARY KEY(id)
)

create table Publicacion
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
se_realizan_preguntas bit default 0,
habilitado bit default 1,
PRIMARY KEY (id),
FOREIGN KEY (visibilidad_id) REFERENCES Visibilidad (id),
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
FOREIGN KEY (rubro_id) REFERENCES Rubro(id),
)

create table Pregunta
(
id numeric(18,0),
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
respuesta_fecha datetime default null,
publicacion_id numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (publicacion_id) REFERENCES Publicacion (id)
)

create table Calificacion
(
id numeric(18,0),
cantidad_estrellas numeric(18,0),
descripcion nvarchar(255),
PRIMARY KEY (id)
)

create table Oferta
(
id numeric(18,0) identity(1,1),
monto numeric(18,0),
gano_subasta bit default 0,
fecha datetime,
usuario_id numeric(18,0),
publicacion_id numeric(18,0),
calificacion_id numeric(18,0) default null,
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
FOREIGN KEY (publicacion_id) REFERENCES Publicacion (id),
FOREIGN KEY (calificacion_id) REFERENCES Calificacion (id)
)

create table Compra
(
id numeric(18,0) identity(1,1),
cantidad numeric(18,0),
fecha datetime,
usuario_id numeric(18,0),
publicacion_id numeric(18,0),
calificacion_id numeric(18,0) default null,
PRIMARY KEY (id),
FOREIGN KEY (usuario_id) REFERENCES Usuario (id),
FOREIGN KEY (publicacion_id) REFERENCES Publicacion (id),
FOREIGN KEY (calificacion_id) REFERENCES Calificacion (id),
)

create table Forma_Pago
(
id numeric(18,0) identity(1,1),
descripcion nvarchar(255),
PRIMARY KEY (id),
)

create table Factura
(
nro numeric(18,0),
fecha DATETIME,
total numeric(18,2),
forma_pago_id numeric(18,0),
PRIMARY KEY (nro),
FOREIGN KEY (forma_pago_id) REFERENCES Forma_Pago (id)
)

create table Item_Factura
(
id numeric(18,0) identity(1,1),
monto numeric(18,2),
cantidad numeric(18,0),
factura_nro numeric(18,0),
publicacion_id numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (factura_nro) REFERENCES Factura (nro),
FOREIGN KEY (publicacion_id) REFERENCES Publicacion (id)
)

