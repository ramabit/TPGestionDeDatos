IF OBJECT_ID('Pregunta', 'U') IS NOT NULL
DROP TABLE Pregunta

create table Pregunta
(
id numeric(18,0),
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
id_publicacion numeric(18,0) foreign key references publicacion (id),
PRIMARY KEY (id)
)