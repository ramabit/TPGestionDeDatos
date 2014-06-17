DROP FUNCTION LOS_SUPER_AMIGOS.calcular_productos_no_vendidos 
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