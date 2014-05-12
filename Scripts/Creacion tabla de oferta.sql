drop table oferta
create table oferta
(
codigo numeric(18,0) identity(0,1),
monto numeric(18,0),
gano_subasta bit default 0,
fecha datetime,
username nvarchar(255) default null,
codigo_publicacion numeric(18,0) foreign key references publicacion (codigo),
codigo_calificacion numeric(18,0) default null,
)

insert into oferta
([monto], [fecha], [codigo_publicacion])
select Oferta_Monto, Oferta_Fecha, Publicacion_Cod from gd_esquema.Maestra
where ISNULL(Oferta_Monto, 0) != 0