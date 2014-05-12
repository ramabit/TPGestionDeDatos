drop table pregunta
create table pregunta
(
codigo numeric(18,0) primary key,
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
codigo_publicacion numeric(18,0) foreign key references publicacion (codigo)
)