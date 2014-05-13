IF OBJECT_ID('Calificacion', 'U') IS NOT NULL
DROP TABLE Calificacion

create table Calificacion
(
id numeric(18,0),
cantidad_estrellas numeric(18,2),
descripcion nvarchar(255),
PRIMARY KEY (id)
)