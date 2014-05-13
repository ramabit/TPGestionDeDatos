IF OBJECT_ID('Visibilidad', 'U') IS NOT NULL
DROP TABLE Visibilidad
create table Visibilidad
(
id numeric(18,0),
descripcion nvarchar(255),
precio numeric(18,2),
porcentaje numeric(18,0),
habilitado bit default 1,
PRIMARY KEY (id)
)

insert into Visibilidad
([id],[descripcion],[precio],[porcentaje])
select distinct Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Porcentaje, Publicacion_Visibilidad_Precio from gd_esquema.Maestra