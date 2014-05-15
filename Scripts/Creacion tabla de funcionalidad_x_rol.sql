IF OBJECT_ID('Funcionalidad_x_Rol', 'U') IS NOT NULL
DROP TABLE Funcionalidad_x_Rol

create table Funcionalidad_x_Rol
(
funcionalidad_id numeric(18, 0),
rol_id numeric(18, 0),
PRIMARY KEY(funcionalidad_id, rol_id),
FOREIGN KEY (funcionalidad_id) REFERENCES Funcionalidad (id),
FOREIGN KEY (rol_id) REFERENCES Rol (id)
)

-- Agrego a administrador todas las funcionalidades
begin transaction
	declare @i int;
	set @i = 0;
	
	while((select COUNT(*) from Funcionalidad) > @i)
	begin
		set @i = @i + 1;
		insert into Funcionalidad_x_Rol
		(funcionalidad_id,rol_id)
		values(@i, 1);
	end
commit

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(1,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(2,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(3,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(4,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(5,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(13,2);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(3,3);

insert into Funcionalidad_x_Rol
(funcionalidad_id,rol_id)
values(13,3);