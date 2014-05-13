IF OBJECT_ID('Oferta', 'U') IS NOT NULL
DROP TABLE Oferta

create table Oferta
(
id numeric(18,0) identity(0,1),
monto numeric(18,0),
gano_subasta bit default 0,
fecha datetime,
username nvarchar(255) default null,
id_publicacion numeric(18,0) foreign key references publicacion (id),
id_calificacion numeric(18,0) foreign key references calificacion (id) default null,
PRIMARY KEY (id)
)

insert into Oferta
([monto], [fecha], [id_publicacion])
select Oferta_Monto, Oferta_Fecha, Publicacion_Cod from gd_esquema.Maestra
where ISNULL(Oferta_Monto, 0) != 0