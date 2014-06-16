CREATE FUNCTION LOS_SUPER_AMIGOS.calcular_productos_no_vendidos 
(@usuario_id numeric(18,0), @visibilidad_id numeric(18,0), @fecha_fin datetime) 
RETURNS numeric(18,0) 
AS 
BEGIN 
	DECLARE @stock_total numeric(18,0), @stock_vendido numeric(18,0) 
	
	SELECT @stock_total = SUM(p.stock) 
	FROM LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v
	WHERE p.usuario_id = @usuario_id and p.visibilidad_id = v.id
	and v.id = @visibilidad_id and p.fecha_vencimiento >= @fecha_fin
	and p.fecha_inicio < @fecha_fin and p.estado != 'Finalizada'

	SELECT @stock_vendido = SUM(c.cantidad) 
	FROM LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Visibilidad v 
	WHERE p.usuario_id = @usuario_id and c.publicacion_id = p.id and p.visibilidad_id = v.id
	and v.id = @visibilidad_id and p.fecha_vencimiento >= @fecha_fin and p.estado != 'Finalizada'
	and p.fecha_inicio < @fecha_fin
	
	RETURN @stock_total - @stock_vendido
END
GO