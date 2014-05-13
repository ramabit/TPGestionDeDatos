IF OBJECT_ID('Pregunta', 'U') IS NOT NULL
DROP TABLE Pregunta
create table Pregunta
(
codigo numeric(18,0),
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
codigo_publicacion numeric(18,0) foreign key references publicacion (codigo),
PRIMARY KEY (codigo)
)