IF OBJECT_ID('Rol_Por_Usuario', 'U') IS NOT NULL
DROP TABLE Rol_Por_Usuario

create table Rol_Por_Usuario
(
rol_id numeric(18,0),
username nvarchar(45),
PRIMARY KEY (rol_id, username),
FOREIGN KEY (rol_id) REFERENCES Rol (id),
FOREIGN KEY (username) REFERENCES Usuario (username),
)