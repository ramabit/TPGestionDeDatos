IF OBJECT_ID('Funcionalidad', 'U') IS NOT NULL
DROP TABLE Funcionalidad

create table Funcionalidad
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) not null,
PRIMARY KEY (id)
)

insert into Funcionalidad
(nombre)
values ('Comprar');

insert into Funcionalidad
(nombre)
values ('Ofertar');

insert into Funcionalidad
(nombre)
values ('Vender');

insert into Funcionalidad
(nombre)
values ('Calificar vendedor');

insert into Funcionalidad
(nombre)
values ('Preguntar');

insert into Funcionalidad
(nombre)
values ('Agregar rol');

insert into Funcionalidad
(nombre)
values ('Deshabilitar rol');

insert into Funcionalidad
(nombre)
values ('Editar rol');

insert into Funcionalidad
(nombre)
values ('Crear usuario');

insert into Funcionalidad
(nombre)
values ('Editar usuario');

insert into Funcionalidad
(nombre)
values ('Habilitar usuario');

insert into Funcionalidad
(nombre)
values ('Deshabilitar usuario');

insert into Funcionalidad
(nombre)
values ('Generar factura');

