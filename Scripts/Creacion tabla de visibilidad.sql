drop table visibilidad
create table visibilidad
(codigo numeric(18,0),
descripcion nvarchar(255),
precio numeric(18,2),
porcentaje numeric(18,0),
habilitado bit default 1)

insert into visibilidad
([codigo],[descripcion],[precio],[porcentaje])
select distinct Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Porcentaje, Publicacion_Visibilidad_Precio from gd_esquema.Maestra