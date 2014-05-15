IF OBJECT_ID('Pregunta', 'U') IS NOT NULL
DROP TABLE Pregunta

create table Pregunta
(
id numeric(18,0),
descripcion nvarchar(255) not null,
respuesta nvarchar(255) default '',
respuesta_fecha datetime default null,
id_publicacion numeric(18,0),
PRIMARY KEY (id),
FOREIGN KEY (id_publicacion) REFERENCES Publicacion (id)
)