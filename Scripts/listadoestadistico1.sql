IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades
GO
IF OBJECT_ID('LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades
GO
IF OBJECT_ID('LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades
AS
BEGIN
	CREATE TABLE LOS_SUPER_AMIGOS.usuarios_por_visibilidad
		(
			mes int,
			visibilidad numeric(18,0),
			usuario numeric(18,0),
			cantidad numeric(18,0),
			PRIMARY KEY(mes, visibilidad, usuario)
		)
END
GO

DROP FUNCTION LOS_SUPER_AMIGOS.calcular_productos_no_vendidos 
GO




-------------------------------------------------------------------------



CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades
AS
BEGIN
	CREATE TABLE LOS_SUPER_AMIGOS.usuarios_por_visibilidad
		(
			mes int,
			visibilidad numeric(18,0),
			usuario numeric(18,0),
			cantidad numeric(18,0),
			PRIMARY KEY(mes, visibilidad, usuario)
		)
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.borrar_tabla_usuarios_por_visibilidades
AS
BEGIN
DROP TABLE LOS_SUPER_AMIGOS.usuarios_por_visibilidad
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.calcular_productos_no_vendidos 
(@usuario_id numeric(18,0), @visibilidad_id numeric(18,0), @fecha_inicio datetime, @fecha_fin datetime) 
RETURNS numeric(18,0) 
AS 
BEGIN 
	DECLARE @stock_total numeric(18,0), @stock_vendido numeric(18,0) 
	
	SELECT @stock_total = SUM(publicacion.stock) 
	FROM LOS_SUPER_AMIGOS.Publicacion publicacion
	WHERE publicacion.usuario_id = @usuario_id 
	AND publicacion.visibilidad_id = @visibilidad_id 
	AND publicacion.fecha_vencimiento >= @fecha_inicio
	AND publicacion.fecha_inicio < @fecha_fin 

	SELECT @stock_vendido = SUM(compra.cantidad) 
	FROM LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Compra compra
	WHERE publicacion.usuario_id = @usuario_id 
	AND publicacion.visibilidad_id = @visibilidad_id 
	AND compra.publicacion_id = publicacion.id 
	AND publicacion.fecha_vencimiento >= @fecha_inicio 
	AND publicacion.fecha_inicio < @fecha_fin
	AND compra.fecha BETWEEN @fecha_inicio AND @fecha_fin
	
	RETURN @stock_total - @stock_vendido
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender
(
@fecha_inicio datetime,
@fecha_media datetime,
@fecha_fin datetime
)
	
RETURNS @mi_tabla TABLE (mes int, visibilidad numeric(18,0), usuario numeric(18,0), cantidad numeric(18,0))
AS
BEGIN

	EXEC LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades
	
	EXEC LOS_SUPER_AMIGOS.llenar_tabla
	
	INSERT @mi_tabla
	SELECT *
	FROM LOS_SUPER_AMIGOS.usuarios_por_visibilidad u
	ORDER BY u.mes, u.visibilidad, u.cantidad DESC
	
	EXEC LOS_SUPER_AMIGOS.borrar_tabla_usuarios_por_visibilidades
	
	RETURN
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.llenar_tabla
AS
BEGIN
DECLARE mi_cursor CURSOR FOR
	SELECT DATEPART(month, fecha) Mes, visibilidad.id Visibilidad 
	FROM (VALUES('01/01/2013'), ('01/02/2013'), ('01/03/2013')) as F(fecha), LOS_SUPER_AMIGOS.Visibilidad visibilidad
	ORDER BY Mes, Visibilidad

	DECLARE @mes int, @visibilidad numeric(18,0)
	OPEN mi_cursor
	FETCH FROM mi_cursor INTO @mes, @visibilidad
	WHILE  @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO LOS_SUPER_AMIGOS.usuarios_por_visibilidad ([mes], [visibilidad], [usuario], [cantidad])
		SELECT TOP 5 @mes, @visibilidad, usuario.id, LOS_SUPER_AMIGOS.calcular_productos_no_vendidos(usuario.id, @visibilidad, '01/01/2013', '01/03/2013') Cantidad
		FROM LOS_SUPER_AMIGOS.Usuario usuario
		ORDER BY Cantidad DESC
		FETCH FROM mi_cursor INTO @mes, @visibilidad
	END
	CLOSE mi_cursor
	DEALLOCATE mi_cursor
END
GO


SELECT * 
		FROM LOS_SUPER_AMIGOS.usuarios_por_visibilidad u
		ORDER BY u.mes, u.visibilidad, u.cantidad DESC
		
		
insert into LOS_SUPER_AMIGOS.Publicacion
(usuario_id,visibilidad_id,costo_pagado,estado,fecha_inicio,fecha_vencimiento,precio,tipo)
values(74,10003,0,'Publicada','01/01/2013','01/04/2013',150,'Compra Inmediata')
















CREATE PROCEDURE LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades

AS
BEGIN
	
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender
(
@fecha_inicio datetime,
@fecha_media datetime,
@fecha_fin datetime
)
	
RETURNS @mi_tabla TABLE (mes int, visibilidad numeric(18,0), usuario numeric(18,0), cantidad numeric(18,0))
AS
BEGIN

	CREATE TABLE LOS_SUPER_AMIGOS.usuarios_por_visibilidad
		(
			mes int,
			visibilidad numeric(18,0),
			usuario numeric(18,0),
			cantidad numeric(18,0),
			PRIMARY KEY(mes, visibilidad, usuario)
		)
	
	DECLARE mi_cursor CURSOR FOR
	SELECT DATEPART(month, fecha) Mes, visibilidad.id Visibilidad 
	FROM (VALUES(@fecha_inicio), (@fecha_media), (@fecha_fin)) as F(fecha), LOS_SUPER_AMIGOS.Visibilidad visibilidad
	ORDER BY Mes, Visibilidad

	DECLARE @mes int, @visibilidad numeric(18,0)
	OPEN mi_cursor
	FETCH FROM mi_cursor INTO @mes, @visibilidad
	WHILE  @@FETCH_STATUS = 0
	BEGIN
		INSERT INTO LOS_SUPER_AMIGOS.usuarios_por_visibilidad ([mes], [visibilidad], [usuario], [cantidad])
		SELECT TOP 5 @mes, @visibilidad, usuario.id, LOS_SUPER_AMIGOS.calcular_productos_no_vendidos(usuario.id, @visibilidad, @fecha_inicio, @fecha_fin) Cantidad
		FROM LOS_SUPER_AMIGOS.Usuario usuario
		ORDER BY Cantidad DESC
		FETCH FROM mi_cursor INTO @mes, @visibilidad
	END
	CLOSE mi_cursor
	DEALLOCATE mi_cursor
	
	INSERT @mi_tabla
		SELECT * 
		FROM usuarios_por_visibilidad u
		ORDER BY u.mes, u.visibilidad, u.cantidad DESC
	RETURN
END
GO
