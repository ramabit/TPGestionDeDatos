IF OBJECT_ID('Empresa', 'U') IS NOT NULL
DROP TABLE Empresa

create table Empresa
(
razon_social nvarchar(255),
cuit nvarchar(50),
fecha_creacion datetime,
mail nvarchar(50),
domicilio_calle nvarchar(100),
domicilio_numero numeric(18,0),
domicilio_piso numeric(18,0),
domicilio_depto nvarchar(50),
domicilio_id_postal nvarchar(50),
habilitado bit default 1,
username nvarchar(45) foreign key references Usuario(username),
PRIMARY KEY (razon_social,cuit)
)

-- FALTA INSERTAR LOS USERNAME POR DEFECTO

insert into Empresa
   ( [razon_social], [cuit], [fecha_creacion], [mail], [domicilio_calle], [domicilio_numero], [domicilio_piso], [domicilio_depto], [domicilio_id_postal])
select distinct Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Fecha_Creacion, Publ_Empresa_Mail, Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Piso, Publ_Empresa_Depto, Publ_Empresa_Cod_Postal 
from gd_esquema.Maestra 
where ISNULL(Publ_Empresa_Razon_Social, '') != ''