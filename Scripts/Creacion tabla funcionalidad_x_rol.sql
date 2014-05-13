create table Funcionalidad_x_Rol
(funcionalidad_id int foreign key references funcionalidad(id),
rol_id int foreign key references rol(id),
primary key(funcionalidad_id,rol_id));

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

select funcionalidad_id,rol_id from Funcionalidad_x_Rol