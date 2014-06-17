
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



		SELECT * 
		FROM usuarios_por_visibilidad u
		ORDER BY u.mes, u.visibilidad, u.cantidad DESC