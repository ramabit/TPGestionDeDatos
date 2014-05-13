IF OBJECT_ID('Cliente', 'U') IS NOT NULL
DROP TABLE Cliente
create table Cliente
(
dni numeric(18,0),
apellido nvarchar(255),
nombre nvarchar(255),
fecha_nacimiento datetime,
mail nvarchar(255),
domicilio_calle nvarchar(255),
domicilio_numero numeric(18,0),
domicilio_piso numeric(18,0),
domicilio_depto nvarchar(50),
domicilio_codigo_postal nvarchar(50),
habilitado bit default 1,
-- PRIMARY KEY ()
)

-- Todos los clientes que compraron
insert into Cliente
   ( [dni], [apellido], [nombre], [fecha_nacimiento], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_codigo_postal])
select distinct Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Fecha_Nac, Cli_Mail, Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal 
from gd_esquema.Maestra 
where ISNULL(Cli_DNI, 0) != 0

-- Todos los clientes que vendieron
insert into CLIENTE
   ( [dni], [apellido], [nombre], [fecha_nacimiento], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_codigo_postal])
select distinct Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Fecha_Nac, Publ_Cli_Mail, Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Piso, Publ_Cli_Depto, Publ_Cli_Cod_Postal 
from gd_esquema.Maestra 
where ISNULL(Publ_Cli_DNI, 0) != 0
