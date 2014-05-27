IF OBJECT_ID('Forma_Pago', 'U') IS NOT NULL
DROP TABLE Forma_Pago

create table Forma_Pago
(
id int,
Descripcion varchar(255),
PRIMARY KEY (id),
)