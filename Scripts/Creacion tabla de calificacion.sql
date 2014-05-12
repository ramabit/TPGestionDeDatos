drop table calificacion
create table calificacion
(
codigo numeric(18,0) primary key,
cantidad_estrellas numeric(18,2),
descripcion nvarchar(255)
)