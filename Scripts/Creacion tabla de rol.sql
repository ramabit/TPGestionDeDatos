IF OBJECT_ID('Rol', 'U') IS NOT NULL
DROP TABLE Rol

create table Rol
(
id numeric(18, 0) identity(1,1),
nombre nvarchar(45) NOT NULL,
habilitado bit NOT NULL default 1,
PRIMARY KEY (id)
)

insert into Rol(nombre)
values('Administrador')

insert into Rol(nombre)
values('Cliente')

insert into Rol(nombre)
values('Empresa')
