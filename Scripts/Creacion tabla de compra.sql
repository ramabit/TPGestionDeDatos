IF OBJECT_ID('Compra', 'U') IS NOT NULL
DROP TABLE Compra

create table Compra
(
id numeric(18,0) identity(0,1),
cantidad numeric(18,0),
fecha datetime,
username nvarchar(255) default null,
id_publicacion numeric(18,0) foreign key references publicacion (id),
id_calificacion numeric(18,0) foreign key references calificacion (id) default null,
-- PRIMARY KEY ()
)

insert into Compra
([cantidad], [fecha], [id_publicacion])
select Compra_Cantidad, Compra_Fecha, Publicacion_Cod from gd_esquema.Maestra
where ISNULL(Compra_Cantidad, 0) != 0