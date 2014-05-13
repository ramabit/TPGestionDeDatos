create table Rol
(id int identity(1,1) primary key ,
nombre varchar(45) NOT NULL,
habilitado bit NOT NULL default 1);

insert into Rol(nombre)
values('Administrador')

insert into Rol(nombre)
values('Cliente')

insert into Rol(nombre)
values('Empresa')
