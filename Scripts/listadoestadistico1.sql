IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_tabla_usuarios_por_visibilidades
GO
IF OBJECT_ID('LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades
GO
IF OBJECT_ID('LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender', 'F') IS NOT NULL
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

CREATE PROCEDURE LOS_SUPER_AMIGOS.cargar_valores_a_tabla_usuarios_por_visibilidades
	@fecha_inicio datetime,
	@fecha_media datetime,
	@fecha_fin datetime
AS
BEGIN
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
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_cantidad_de_publicaciones_sin_vender
()
RETURNS @mi_tabla TABLE (mes int, visibilidad numeric(18,0), usuario numeric(18,0), cantidad numeric(18,0))
AS
BEGIN
	INSERT @mi_tabla
		SELECT * 
		FROM usuarios_por_visibilidad u
		ORDER BY u.mes, u.visibilidad, u.cantidad DESC
	RETURN
END
GO