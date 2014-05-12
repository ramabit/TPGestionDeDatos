create table publicacion
(codigo numeric(18,0),
descripcion nvarchar(255),
stock numeric(18,0),
fecha_inicio datetime,
fecha_vencimiento datetime,
precio numeric(18,0),
rubro nvarchar(255),
codigo_visibilidad numeric(18,0),
codigo_usuario numeric(18,0) default null,
estado nvarchar(255),
tipo nvarchar(255),
se_realizan_preguntas bit default 0,
habilitado bit default 1)

insert into publicacion
([codigo], [descripcion], [stock], [fecha_inicio], [fecha_vencimiento], [precio], [rubro], [codigo_visibilidad], [estado], [tipo])
select distinct Publicacion_Cod, Publicacion_Descripcion, Publicacion_Stock, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, Publicacion_Rubro_Descripcion, Publicacion_Visibilidad_Cod, Publicacion_Estado,Publicacion_Tipo from gd_esquema.Maestra
where ISNULL(Publicacion_Rubro_Descripcion, '') != ''