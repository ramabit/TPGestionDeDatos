create table Rol
(id int identity(1,1) primary key ,
nombre varchar(45) NOT NULL);

insert into Rol(nombre)
values('Administrador')

insert into Rol(nombre)
values('Cliente')

insert into Rol(nombre)
values('Empresa')


