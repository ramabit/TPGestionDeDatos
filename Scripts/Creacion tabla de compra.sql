drop table compra
create table compra
(
codigo numeric(18,0) identity(0,1),
cantidad numeric(18,0),
fecha datetime,
username nvarchar(255) default null,
codigo_publicacion numeric(18,0) foreign key references publicacion (codigo),
codigo_calificacion numeric(18,0) foreign key references calificacion (codigo) default null,
)

insert into compra
([cantidad], [fecha], [codigo_publicacion])
select Compra_Cantidad, Compra_Fecha, Publicacion_Cod from gd_esquema.Maestra
where ISNULL(Compra_Cantidad, 0) != 0