CREATE PROCEDURE crear_usuario
	@usuario_id numeric(18,0) output
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Usuario default values
	SET @usuario_id = SCOPE_IDENTITY();
END
GO

INSERT INTO Empresa
   ( [razon_social], [cuit], [fecha_creacion], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_id_postal])
SELECT DISTINCT Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Fecha_Creacion, Publ_Empresa_Mail, Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Piso, Publ_Empresa_Depto, Publ_Empresa_Cod_Postal 
FROM gd_esquema.Maestra 
WHERE ISNULL(Publ_Empresa_Razon_Social, '') != ''

DECLARE @row_pos numeric(18,0)
DECLARE @row_count numeric(18,0)
select @row_count = COUNT(*) from Empresa
set @row_pos = 1

while (@row_pos <= @row_count)
begin
	declare @id_e numeric(18,0)
	exec crear_usuario @id_e output
	update Empresa set usuario_id = @id_e where id = @row_pos
	set @row_pos = @row_pos + 1
end

-- Todos los clientes que compraron
insert into Cliente
   ( [dni], [apellido], [nombre], [fecha_nacimiento], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_id_postal])
select distinct Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Fecha_Nac, Cli_Mail, Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal 
from gd_esquema.Maestra 
where ISNULL(Cli_DNI, 0) != 0

-- Todos los clientes que vendieron

insert into Cliente
   ( [dni], [apellido], [nombre], [fecha_nacimiento], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_id_postal])
select distinct Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Fecha_Nac, Publ_Cli_Mail, Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Piso, Publ_Cli_Depto, Publ_Cli_Cod_Postal 
from gd_esquema.Maestra as m
where ISNULL(Publ_Cli_DNI, 0) != 0 and not exists (select * from Cliente as c where m.Publ_Cli_Dni = c.dni)

select @row_count = COUNT(*) from Cliente
set @row_pos = 1

while (@row_pos <= @row_count)
begin
	declare @id_c numeric(18,0)
	exec crear_usuario @id_c output
	update Cliente set usuario_id = @id_c where id = @row_pos
	set @row_pos = @row_pos + 1
end
