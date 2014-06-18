-- CREACION DE SCHEMA
-- Comprueba si no existe ninguno, sino existe lo crea.

IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'LOS_SUPER_AMIGOS')
BEGIN
	EXEC ('CREATE SCHEMA LOS_SUPER_AMIGOS AUTHORIZATION gd')
END

-- FIN CREACION DE SCHEMA

-- ELIMINACION DE TABLAS NECESARIAS
-- Si existe, lo elimina

IF OBJECT_ID('LOS_SUPER_AMIGOS.Funcionalidad_x_Rol', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Funcionalidad_x_Rol

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rol_x_Usuario', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rol_x_Usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.Pregunta', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Pregunta

IF OBJECT_ID('LOS_SUPER_AMIGOS.Oferta', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Oferta

IF OBJECT_ID('LOS_SUPER_AMIGOS.Item_Factura', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Item_Factura

IF OBJECT_ID('LOS_SUPER_AMIGOS.Compra', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Compra

IF OBJECT_ID('LOS_SUPER_AMIGOS.Calificacion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Calificacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Publicacion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Publicacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rubro', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rubro

IF OBJECT_ID('LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.Visibilidad', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Visibilidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.Rol', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Rol

IF OBJECT_ID('LOS_SUPER_AMIGOS.Funcionalidad', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Funcionalidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.Empresa', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Empresa

IF OBJECT_ID('LOS_SUPER_AMIGOS.Cliente', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Cliente

IF OBJECT_ID('LOS_SUPER_AMIGOS.TipoDeDocumento', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.TipoDeDocumento

IF OBJECT_ID('LOS_SUPER_AMIGOS.Direccion', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Direccion

IF OBJECT_ID('LOS_SUPER_AMIGOS.Usuario', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.Factura', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Factura

IF OBJECT_ID('LOS_SUPER_AMIGOS.Forma_Pago', 'U') IS NOT NULL
DROP TABLE LOS_SUPER_AMIGOS.Forma_Pago

-- FIN ELIMINACION DE TABLAS

-- ELIMINACION DE PROCESOS, FUNCIONES, VISTAS Y TRIGGERS NECESARIOS
-- Si existe, lo elimina

IF OBJECT_ID('LOS_SUPER_AMIGOS.Hacer_Facturacion', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.Hacer_Facturacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_cliente', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_cliente

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_empresa', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_empresa

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_usuario', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_usuario

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_usuario_con_valores', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_usuario_con_valores

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_direccion', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_direccion

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_visibilidad', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_visibilidad

IF OBJECT_ID('LOS_SUPER_AMIGOS.crear_publicacion', 'P') IS NOT NULL
DROP PROCEDURE LOS_SUPER_AMIGOS.crear_publicacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.agregar_id_publ') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.agregar_id_publ

IF OBJECT_ID('LOS_SUPER_AMIGOS.gano_subasta') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.gano_subasta

IF OBJECT_ID('LOS_SUPER_AMIGOS.vendedores_con_mayor_facturacion') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_facturacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.vendedores_con_mayor_calificacion') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_Calificacion

IF OBJECT_ID('LOS_SUPER_AMIGOS.clientes_con_publicaciones_sin_calificar') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.clientes_con_publicaciones_sin_calificar

IF OBJECT_ID('LOS_SUPER_AMIGOS.calcular_productos_no_vendidos ') IS NOT NULL
DROP FUNCTION LOS_SUPER_AMIGOS.calcular_productos_no_vendidos 

IF OBJECT_ID('LOS_SUPER_AMIGOS.VistaCantidadVendida') IS NOT NULL
DROP VIEW LOS_SUPER_AMIGOS.VistaCantidadVendida

IF OBJECT_ID('LOS_SUPER_AMIGOS.VistaOfertaMax') IS NOT NULL
DROP VIEW LOS_SUPER_AMIGOS.VistaOfertaMax

IF OBJECT_ID('LOS_SUPER_AMIGOS.finalizar_x_fin_stock') IS NOT NULL
DROP TRIGGER LOS_SUPER_AMIGOS.finalizar_x_fin_stock

IF OBJECT_ID('LOS_SUPER_AMIGOS.agregar_valor_default_de_nueva_visiblidad_en_comisiones') IS NOT NULL
DROP TRIGGER LOS_SUPER_AMIGOS.agregar_valor_default_de_nueva_visiblidad_en_comisiones

IF OBJECT_ID('LOS_SUPER_AMIGOS.agregar_valor_default_de_nuevo_usuario_en_comisiones') IS NOT NULL
DROP TRIGGER LOS_SUPER_AMIGOS.agregar_valor_default_de_nueva_visiblidad_en_comisiones

-- FIN DE ELIMINACION DE PROCEDIMIENTO, FUNCIONES, VISTAS Y TRIGGERS NECESARIOS

GO

-- CREACION DE PROCEDIMIENTOS

CREATE PROCEDURE LOS_SUPER_AMIGOS.Hacer_Facturacion
	@id numeric(18,0),
	@idF numeric(18,0),
	@contador_bonificaciones int OUTPUT,
	@monto_descontado numeric(18,2) OUTPUT
AS
BEGIN
DECLARE @vid numeric(18,0)
DECLARE @compra_publicacion numeric(18,0) 
DECLARE @compra_cantidad numeric(18,0) 
DECLARE @precio numeric(18,0) 
DECLARE @porcentaje numeric(18,2) 
DECLARE comision_cursor CURSOR FOR
(SELECT cc.compra_publicacion, cc.compra_cantidad,p.precio, v.porcentaje, v.id
	FROM LOS_SUPER_AMIGOS.Compra_Comision cc, LOS_SUPER_AMIGOS.Compra c,
	LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Visibilidad v
	WHERE cc.compra_id = c.id AND c.publicacion_id = p.id AND p.visibilidad_id = v.id)
SET @monto_descontado = 0
SET @contador_bonificaciones = 0
OPEN comision_cursor
FETCH NEXT FROM comision_cursor INTO @compra_publicacion, @compra_cantidad, @precio, @porcentaje, @vid

WHILE @@FETCH_STATUS = 0
BEGIN

	IF((SELECT c.contador_comisiones 
			FROM LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad c, LOS_SUPER_AMIGOS.Visibilidad v
			WHERE c.usuario_id = @id AND c.visibilidad_id = @vid AND v.habilitado = 1 AND c.visibilidad_id = v.id) >= 9)
	
	BEGIN
		INSERT LOS_SUPER_AMIGOS.Item_Factura
			(factura_nro, publicacion_id, cantidad, monto)
		VALUES
			(@idF, @compra_publicacion, @compra_cantidad, 0)
		
		UPDATE LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad	
			SET contador_comisiones = 0
			WHERE  visibilidad_id = @vid AND usuario_id = @id
		
		SET @monto_descontado = @monto_descontado + (@compra_cantidad * @precio * @porcentaje)

		SET @contador_bonificaciones = @contador_bonificaciones + 1		
	END
	ELSE
	BEGIN
		INSERT LOS_SUPER_AMIGOS.Item_Factura
			(factura_nro, publicacion_id, cantidad, monto)
		VALUES
			(@idF, @compra_publicacion, @compra_cantidad, @compra_cantidad * @precio * @porcentaje)
		
		UPDATE LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad
			SET contador_comisiones = contador_comisiones + 1
			WHERE visibilidad_id = @vid AND usuario_id = @id
	END
	
	FETCH NEXT FROM comision_cursor INTO @compra_publicacion, @compra_cantidad, @precio, @porcentaje, @vid
END
CLOSE comision_cursor
DEALLOCATE comision_cursor
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_cliente
	@nombre nvarchar(255),
	@apellido nvarchar(255),
	@tipo_de_documento_id numeric(18,0),
	@documento numeric(18,0),
	@fecha_nacimiento datetime,
	@mail nvarchar(255),
	@telefono numeric(18,0),
	@direccion_id numeric(18,0),
	@usuario_id numeric(18,0),
	@id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Cliente 
		(nombre, apellido, fecha_nacimiento, tipo_de_documento_id, documento, mail, telefono, direccion_id, usuario_id) 
	VALUES 
		(@nombre, @apellido, @fecha_nacimiento, @tipo_de_documento_id, @documento, @mail, @telefono, @direccion_id, @usuario_id);
	SET @id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_empresa
	@razon_social nvarchar(255),
	@nombre_de_contacto nvarchar(50),
	@cuit nvarchar(50),
	@fecha_creacion datetime,
	@mail nvarchar(50),
	@telefono numeric(18,0),
	@ciudad nvarchar(50),
	@direccion_id numeric(18,0),
	@usuario_id numeric(18,0),
	@id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Empresa 
		(razon_social, nombre_de_contacto, cuit, fecha_creacion, mail, telefono, ciudad, direccion_id, usuario_id) 
	VALUES 
		(@razon_social, @nombre_de_contacto, @cuit, @fecha_creacion, @mail, @telefono, @ciudad, @direccion_id, @usuario_id)
	SET @id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_usuario_con_valores
	@username nvarchar(50),
	@password nvarchar(150),
	@usuario_id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Usuario 
		(username, password) 
	VALUES 
		(@username, @password)
	SET @usuario_id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_usuario
	@usuario_id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Usuario 
		(username) 
	VALUES 
		(ISNULL('USER' + CAST(((SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Usuario)+ 1) AS NVARCHAR(10)),''))
	SET @usuario_id = SCOPE_IDENTITY();	
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_direccion
	@calle nvarchar(100),
	@numero numeric(18,0),
	@piso numeric(18,0),
	@depto nvarchar(5),
	@cod_postal nvarchar(50),
	@localidad nvarchar(50),
	@id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Direccion 
		(calle, numero, piso, depto, cod_postal, localidad)
	VALUES
		(@calle, @numero, @piso, @depto, @cod_postal, @localidad);
	SET @id = SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_visibilidad
	@descripcion nvarchar(255),
	@precio numeric(18,2),
	@porcentaje numeric(18,2),
	@duracion numeric(18,0),
	@id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Visibilidad
		(descripcion, precio, porcentaje, duracion)
	VALUES
		(@descripcion, @precio, @porcentaje, @duracion);
	SET @id = SCOPE_IDENTITY();
END
GO

CREATE PROCEDURE LOS_SUPER_AMIGOS.crear_publicacion
	@tipo nvarchar(255),
	@estado nvarchar(255),
	@descripcion nvarchar(255),
	@fecha_inicio datetime,
	@fecha_vencimiento datetime,
	@rubro_id numeric(18,0),
	@visibilidad_id numeric(18,0),
	@stock numeric(18,0),
	@precio numeric(18,0),
	@usuario_id numeric(18,0),
	@se_realizan_preguntas bit,
	@id numeric(18,0) OUTPUT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Publicacion
		(tipo, estado, descripcion, fecha_inicio, fecha_vencimiento, rubro_id, visibilidad_id, precio, stock, usuario_id, se_realizan_preguntas)
	VALUES
		(@tipo, @estado, @descripcion, @fecha_inicio, @fecha_vencimiento, @rubro_id, @visibilidad_id, @precio, @stock, @usuario_id, @se_realizan_preguntas);
	SET @id = SCOPE_IDENTITY();
END
GO

-- FIN DE CREACION DE PROCEDIMIENTO

-- CREACION DE FUNCIONES

CREATE FUNCTION LOS_SUPER_AMIGOS.gano_subasta
(
	@id numeric(18,0)
)
RETURNS bit
AS
BEGIN
	IF (EXISTS (
			SELECT compra.id 
			FROM LOS_SUPER_AMIGOS.Oferta oferta, LOS_SUPER_AMIGOS.Compra compra 
			WHERE oferta.publicacion_id = compra.publicacion_id 
				AND oferta.usuario_id = compra.usuario_id 
				AND oferta.id = @id)
		)
	BEGIN
		RETURN 1;
	END
	RETURN 0;
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_facturacion
(
	@fecha_inicio datetime,
	@fecha_fin datetime
)
RETURNS @mi_tabla TABLE (
							Usuario nvarchar(50),
							Facturacion numeric(18,0)
						)
AS
BEGIN
	INSERT @mi_tabla
		SELECT TOP 5 usuario.username, SUM(item.cantidad * item.monto) Facturacion
		FROM LOS_SUPER_AMIGOS.Usuario usuario, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Item_Factura item, LOS_SUPER_AMIGOS.Factura factura
		WHERE usuario.id = publicacion.usuario_id 
			AND publicacion.id = item.publicacion_id
			AND factura.nro = item.factura_nro
			AND factura.fecha BETWEEN @fecha_inicio AND @fecha_fin  
		GROUP BY usuario.username
		ORDER BY Facturacion DESC
	RETURN
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.vendedores_con_mayor_Calificacion
(
	@fecha_inicio datetime,
	@fecha_fin datetime
)
RETURNS @mi_tabla TABLE (
							Usuario nvarchar(50),
							Mayor_Calificacion numeric(18,2)
						)
AS
BEGIN
	INSERT @mi_tabla
		SELECT TOP 5 usuario.username, SUM(Calificacion.cantidad_estrellas) / COUNT(*) 
		FROM LOS_SUPER_AMIGOS.Usuario usuario, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Calificacion Calificacion
		WHERE usuario.id = publicacion.usuario_id
			AND compra.publicacion_id = publicacion.id
			AND compra.Calificacion_id = Calificacion.id
			AND compra.fecha BETWEEN @fecha_inicio AND @fecha_fin  
		GROUP BY usuario.username
		ORDER BY 2 DESC
	RETURN
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.clientes_con_publicaciones_sin_calificar
(
	@fecha_inicio datetime,
	@fecha_fin datetime
)
RETURNS @mi_tabla TABLE (
							Usuario nvarchar(50),
							Publicaciones_sin_calificar numeric(18,0)
						)
AS
BEGIN
	INSERT @mi_tabla
		SELECT TOP 5 usuario.username, COUNT(*) 'Cantidad de no calificadas'
		FROM LOS_SUPER_AMIGOS.Cliente cliente, LOS_SUPER_AMIGOS.Compra compra, LOS_SUPER_AMIGOS.Publicacion publicacion, LOS_SUPER_AMIGOS.Usuario usuario
		WHERE cliente.usuario_id = compra.usuario_id
			AND publicacion.id = compra.publicacion_id
			AND cliente.usuario_id = usuario.id
			AND ISNULL(compra.Calificacion_id, -1) = -1
			AND compra.fecha BETWEEN @fecha_inicio AND @fecha_fin  
		GROUP BY usuario.username
		ORDER BY 2 DESC
	RETURN
END
GO

CREATE FUNCTION LOS_SUPER_AMIGOS.agregar_id_publ
(
	@documento numeric(18,0),
	@razon_social nvarchar(255)
)
RETURNS numeric(18,0)
AS
BEGIN
	DECLARE @id numeric(18,0)
	IF @documento IS NULL
		BEGIN
			SELECT @id = usuario_id FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razon_social
		END
	ELSE
		BEGIN
			SELECT @id = usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = @documento
		END
	RETURN @id
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

-- FIN DE CREACION DE FUNCIONES

-- CREACION DE TABLAS

CREATE TABLE LOS_SUPER_AMIGOS.Usuario
(
	id numeric(18,0) IDENTITY(1,1),
	username nvarchar(50) ,--
	password nvarchar(150) DEFAULT '559aead08264d5795d3909718cdd05abd49572e84fe55590eef31a88a08fdffd', -- hash de 'A'
	habilitado bit DEFAULT 1,
	login_fallidos int DEFAULT 0,
	PRIMARY KEY (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Direccion
(
	id numeric(18,0) IDENTITY(1,1),
	calle nvarchar(100),
	numero numeric(18,0),
	piso numeric(18,0),
	depto nvarchar(5),
	cod_postal nvarchar(50),
	localidad nvarchar(50),
	PRIMARY KEY(id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Empresa
(
	id numeric(18,0) IDENTITY(1,1),
	razon_social nvarchar(255),
	nombre_de_contacto nvarchar(50),
	cuit nvarchar(50),
	fecha_creacion datetime,
	mail nvarchar(50),
	telefono numeric(18,0),
	ciudad nvarchar(50),
	direccion_id numeric(18,0),
	usuario_id numeric(18,0),
	habilitado bit DEFAULT 1,
	dado_de_baja bit DEFAULT 0,
	PRIMARY KEY (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (direccion_id) REFERENCES LOS_SUPER_AMIGOS.Direccion (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.TipoDeDocumento
(
	id numeric(18,0) IDENTITY(1,1),
	nombre nvarchar(50),
	PRIMARY KEY(id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Cliente
(
	id numeric(18,0) IDENTITY(1,1),
	nombre nvarchar(255),
	apellido nvarchar(255),
	tipo_de_documento_id numeric(18,0),
	documento numeric(18,0),
	fecha_nacimiento datetime,
	mail nvarchar(255),
	telefono numeric(18,0),
	direccion_id numeric(18,0),
	usuario_id numeric(18,0),
	habilitado bit DEFAULT 1,
	dado_de_baja bit DEFAULT 0,
	PRIMARY KEY (id),
	FOREIGN KEY (tipo_de_documento_id) REFERENCES LOS_SUPER_AMIGOS.TipoDeDocumento (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (direccion_id) REFERENCES LOS_SUPER_AMIGOS.Direccion (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Rol
(
	id numeric(18, 0) IDENTITY(1,1),
	nombre nvarchar(45) NOT NULL,
	habilitado bit NOT NULL DEFAULT 1,
	PRIMARY KEY (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Rol_x_Usuario
(
	rol_id numeric(18,0),
	usuario_id numeric(18,0),
	PRIMARY KEY (rol_id, usuario_id),
	FOREIGN KEY (rol_id) REFERENCES LOS_SUPER_AMIGOS.Rol (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
)

CREATE TABLE LOS_SUPER_AMIGOS.Funcionalidad
(
	id numeric(18, 0) IDENTITY(1,1),
	nombre nvarchar(45) NOT NULL,
	PRIMARY KEY (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
(
	funcionalidad_id numeric(18, 0),
	rol_id numeric(18, 0),
	PRIMARY KEY(funcionalidad_id, rol_id),
	FOREIGN KEY (funcionalidad_id) REFERENCES LOS_SUPER_AMIGOS.Funcionalidad (id),
	FOREIGN KEY (rol_id) REFERENCES LOS_SUPER_AMIGOS.Rol (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Visibilidad
(
	id numeric(18,0) IDENTITY(1,1),
	descripcion nvarchar(255),
	precio numeric(18,2),
	porcentaje numeric(18,2),
	duracion numeric(18,0),
	habilitado bit DEFAULT 1,
	dado_de_baja bit DEFAULT 0,
	PRIMARY KEY (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Rubro
(
	id numeric(18,0) IDENTITY(1,1),
	descripcion nvarchar(255),
	habilitado bit DEFAULT 1,
	PRIMARY KEY(id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Publicacion
(
	id numeric(18,0) IDENTITY(1,1),
	tipo nvarchar(255),
	estado nvarchar(255),
	descripcion nvarchar(255),
	fecha_inicio datetime,
	fecha_vencimiento datetime,
	rubro_id numeric(18,0),
	visibilidad_id numeric(18,0),
	usuario_id numeric(18,0),
	costo_pagado bit DEFAULT 0,
	se_realizan_preguntas bit,
	stock numeric(18,0),
	precio numeric(18,0),
	habilitado bit DEFAULT 1,
	PRIMARY KEY (id),
	FOREIGN KEY (visibilidad_id) REFERENCES LOS_SUPER_AMIGOS.Visibilidad (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (rubro_id) REFERENCES LOS_SUPER_AMIGOS.Rubro(id),
)

CREATE TABLE LOS_SUPER_AMIGOS.Pregunta
(
	id numeric(18,0)IDENTITY(1,1),
	descripcion nvarchar(255) NOT NULL,
	respuesta nvarchar(255) DEFAULT '',
	respuesta_fecha datetime DEFAULT NULL,
	usuario_id numeric(18,0),
	publicacion_id numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario(id),
	FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Calificacion
(
	id numeric(18,0) IDENTITY(1,1),
	cantidad_estrellas numeric(18,0),
	descripcion nvarchar(255),
	PRIMARY KEY (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Oferta
(
	id numeric(18,0) IDENTITY(1,1),
	monto numeric(18,0),
	fecha datetime,
	usuario_id numeric(18,0),
	publicacion_id numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Compra
(
	id numeric(18,0) IDENTITY(1,1),
	cantidad numeric(18,0),
	fecha datetime,
	usuario_id numeric(18,0),
	publicacion_id numeric(18,0),
	calificacion_id numeric(18,0) DEFAULT NULL,
	facturada bit DEFAULT 0,
	PRIMARY KEY (id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id),
	FOREIGN KEY (calificacion_id) REFERENCES LOS_SUPER_AMIGOS.Calificacion (id),
)

CREATE TABLE LOS_SUPER_AMIGOS.Forma_Pago
(
	id numeric(18,0) IDENTITY(1,1),
	descripcion nvarchar(255),
	PRIMARY KEY (id),
)

CREATE TABLE LOS_SUPER_AMIGOS.Factura
(
	nro numeric(18,0) IDENTITY(1,1),
	fecha DATETIME,
	total numeric(18,2),
	forma_pago_id numeric(18,0),
	PRIMARY KEY (nro),
	FOREIGN KEY (forma_pago_id) REFERENCES LOS_SUPER_AMIGOS.Forma_Pago (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Item_Factura
(
	id numeric(18,0) IDENTITY(1,1),
	monto numeric(18,2),
	cantidad numeric(18,0),
	factura_nro numeric(18,0),
	publicacion_id numeric(18,0),
	PRIMARY KEY (id),
	FOREIGN KEY (factura_nro) REFERENCES LOS_SUPER_AMIGOS.Factura (nro),
	FOREIGN KEY (publicacion_id) REFERENCES LOS_SUPER_AMIGOS.Publicacion (id)
)

CREATE TABLE LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad
(
	usuario_id numeric(18,0),
	visibilidad_id numeric(18,0),
	contador_comisiones int,
	PRIMARY KEY (usuario_id, visibilidad_id),
	FOREIGN KEY (usuario_id) REFERENCES LOS_SUPER_AMIGOS.Usuario (id),
	FOREIGN KEY (visibilidad_id) REFERENCES LOS_SUPER_AMIGOS.Visibilidad (id),
)

-- FIN DE CREACION DE TABLAS



-- MIGRACION DE DATOS


-- INSERTAR Direcciones

-- INSERTAR Direcciones de Empresas
INSERT INTO LOS_SUPER_AMIGOS.Direccion
	([calle],[numero],[piso],[depto],[cod_postal],[localidad])
SELECT DISTINCT Publ_Empresa_Dom_Calle, Publ_Empresa_Nro_Calle, Publ_Empresa_Piso, Publ_Empresa_Depto, Publ_Empresa_Cod_Postal, 'localidadMigrada' FROM gd_esquema.Maestra WHERE ISNULL(Publ_Empresa_Dom_Calle,'')!=''


-- Direcciones de Clientes que vendieron
INSERT INTO LOS_SUPER_AMIGOS.Direccion
	([calle],[numero],[piso],[depto],[cod_postal],[localidad])
SELECT DISTINCT Publ_Cli_Dom_Calle, Publ_Cli_Nro_Calle, Publ_Cli_Piso, Publ_Cli_Depto, Publ_Cli_Cod_Postal, 'localidadMigrada' FROM gd_esquema.Maestra WHERE ISNULL(Publ_Cli_Dom_Calle,'')!=''


-- Direcciones de Clientes que compraron
INSERT INTO LOS_SUPER_AMIGOS.Direccion
	([calle],[numero],[piso],[depto],[cod_postal],[localidad])
SELECT DISTINCT Cli_Dom_Calle, Cli_Nro_Calle, Cli_Piso, Cli_Depto, Cli_Cod_Postal, 'localidadMigrada' FROM gd_esquema.Maestra WHERE ISNULL(Cli_Dom_Calle,'')!='' AND NOT exists (SELECT * FROM LOS_SUPER_AMIGOS.Direccion dir WHERE Cli_Dom_Calle = dir.calle AND Cli_Nro_Calle = dir.numero AND Cli_Piso = dir.piso AND Cli_Depto = dir.depto AND Cli_Cod_Postal = dir.cod_postal)

-- FIN INSERTAR Direcciones


-- INSERTAR Empresas

-- Migro todas las empresas
INSERT INTO LOS_SUPER_AMIGOS.Empresa
   ( [razon_social], [cuit], [fecha_creacion], [mail], [telefono], [ciudad], [direccion_id])
SELECT DISTINCT Publ_Empresa_Razon_Social, Publ_Empresa_Cuit, Publ_Empresa_Fecha_Creacion, Publ_Empresa_Mail, 0, 'ciudadMigrada', (SELECT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Publ_Empresa_Dom_Calle = d.calle AND Publ_Empresa_Nro_Calle = d.numero AND Publ_Empresa_Piso = d.piso AND Publ_Empresa_Depto = d.depto AND Publ_Empresa_Cod_Postal = d.cod_postal)) FROM gd_esquema.Maestra WHERE ISNULL(Publ_Empresa_Razon_Social, '') != ''

-- Le agrego un usuario a cada una de las empresas migradas
DECLARE mi_cursor CURSOR FOR
SELECT id FROM LOS_SUPER_AMIGOS.Empresa
DECLARE @id numeric(18,0)
OPEN mi_cursor
FETCH FROM mi_cursor INTO @id
WHILE  @@FETCH_STATUS = 0
BEGIN
	DECLARE @id_e numeric(18,0)
	EXEC LOS_SUPER_AMIGOS.crear_usuario @id_e OUTPUT
	UPDATE LOS_SUPER_AMIGOS.Empresa SET usuario_id = @id_e WHERE id = @id

	FETCH FROM mi_cursor INTO @id
END
CLOSE mi_cursor
DEALLOCATE mi_cursor

-- FIN INSERTAR Empresas


-- INSERTAR Tipo de documentos

INSERT INTO LOS_SUPER_AMIGOS.TipoDeDocumento 
	(nombre)
VALUES
	('DNI'),
	('OTRO')

-- FIN INSERTAR Tipo de documentos


-- INSERTAR Clientes

-- Todos los clientes que compraron
INSERT INTO LOS_SUPER_AMIGOS.Cliente
   ( [tipo_de_documento_id], [documento], [apellido], [nombre], [fecha_nacimiento], [mail], [telefono], [direccion_id])
SELECT DISTINCT 1, Cli_Dni, Cli_Apeliido, Cli_Nombre, Cli_Fecha_Nac, Cli_Mail, 0, (SELECT DISTINCT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Cli_Dom_Calle = d.calle AND Cli_Nro_Calle = d.numero AND Cli_Piso = d.piso AND Cli_Depto = d.depto AND Cli_Cod_Postal = d.cod_postal)) FROM gd_esquema.Maestra WHERE ISNULL(Cli_DNI, 0) != 0


-- Todos los clientes que vendieron
INSERT INTO LOS_SUPER_AMIGOS.Cliente
   ( [tipo_de_documento_id], [documento], [apellido], [nombre], [fecha_nacimiento], [mail], [telefono], [direccion_id])
SELECT DISTINCT 1, Publ_Cli_Dni, Publ_Cli_Apeliido, Publ_Cli_Nombre, Publ_Cli_Fecha_Nac, Publ_Cli_Mail, 0, (SELECT DISTINCT id FROM LOS_SUPER_AMIGOS.Direccion d WHERE (Publ_Cli_Dom_Calle = d.calle AND Publ_Cli_Nro_Calle = d.numero AND Publ_Cli_Piso = d.piso AND Publ_Cli_Depto = d.depto AND Publ_Cli_Cod_Postal = d.cod_postal)) FROM gd_esquema.Maestra as m WHERE ISNULL(Publ_Cli_DNI, 0) != 0 AND NOT exists (SELECT * FROM LOS_SUPER_AMIGOS.Cliente as c WHERE m.Publ_Cli_Dni = c.documento)

-- Le agrego un usuario a cada cliente
DECLARE mi_cursor CURSOR FOR
SELECT id FROM LOS_SUPER_AMIGOS.Cliente
OPEN mi_cursor
FETCH FROM mi_cursor INTO @id
WHILE  @@FETCH_STATUS = 0
BEGIN
	DECLARE @id_c numeric(18,0)
	EXEC LOS_SUPER_AMIGOS.crear_usuario @id_c OUTPUT
	UPDATE LOS_SUPER_AMIGOS.Cliente SET usuario_id = @id_c WHERE id = @id

	FETCH FROM mi_cursor INTO @id
END
CLOSE mi_cursor
DEALLOCATE mi_cursor

-- FIN INSERTAR Clientes


-- INSERTAR Roles

INSERT INTO LOS_SUPER_AMIGOS.Rol
	(nombre)
VALUES
	('Administrador'),
	('Cliente'),
	('Empresa')

-- FIN INSERTAR Roles


-- INSERTAR Roles_x_Usuario

-- A todas las empresas le setea el rol empresa
INSERT INTO LOS_SUPER_AMIGOS.Rol_x_Usuario
	([rol_id], [usuario_id])
SELECT (SELECT id FROM LOS_SUPER_AMIGOS.Rol WHERE nombre = 'Empresa'), usuario_id FROM LOS_SUPER_AMIGOS.Empresa

-- A todos los clientes le setea el rol cliente
INSERT INTO LOS_SUPER_AMIGOS.Rol_x_Usuario
	([rol_id], [usuario_id])
SELECT (SELECT id FROM LOS_SUPER_AMIGOS.Rol WHERE nombre = 'Cliente'), usuario_id FROM LOS_SUPER_AMIGOS.Cliente

-- FIN INSERTAR Roles_x_Usuario


-- INSERTAR Usuario

-- CREACION USUARIO ADMIN

-- La contraseña es la encriptacion de "w23e"
DECLARE @id_admin numeric(18,0)
exec LOS_SUPER_AMIGOS.crear_usuario_con_valores 'admin', 'e6b87050bfcb8143fcb8db0170a4dc9ed00d904ddd3e2a4ad1b1e8dc0fdc9be7', @id_admin output


-- Setea al nuevo admin, el rol Administrador que es el 1
INSERT INTO LOS_SUPER_AMIGOS.Rol_x_Usuario 
	(rol_id, usuario_id)
VALUES
	(1, @id_admin)
	
-- FIN CREACION USUARIO ADMIN


-- INSERTAR Funcionalidad

INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad
	(nombre)
VALUES 
	('Comprar / Ofertar'),
	('Generar publicacion'),
	('Editar publicacion'),
	('Calificar vendedor'),
	('Responder preguntas'),
	('Ver respuestas'),
	('Gestionar roles'),
	('Gestionar usuarios'),
	('Generar factura'),
	('Crear empresa'),
	('Editar empresa'),
	('Crear cliente'),
	('Editar cliente'),
	('Agregar visibilidad'),
	('Editar visibilidad'),
	('Agregar rubro'),
	('Editar rubro'),
	('Obtener estadisticas'),
	('Ver historial')

-- Fin INSERTAR Funcionalidad


-- INSERTAR Funcionalidad_por_Rol

-- Agrego a administrador todas las funcionalidades
DECLARE mi_cursor CURSOR FOR
SELECT id FROM LOS_SUPER_AMIGOS.Funcionalidad
OPEN mi_cursor
FETCH FROM mi_cursor INTO @id
WHILE  @@FETCH_STATUS = 0
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
		(funcionalidad_id,rol_id)
	VALUES(@id, 1);
	FETCH FROM mi_cursor INTO @id
END
CLOSE mi_cursor
DEALLOCATE mi_cursor

-- Le agrego a los demas roles sus funcionalidades
INSERT INTO LOS_SUPER_AMIGOS.Funcionalidad_x_Rol
	(funcionalidad_id,rol_id)
VALUES
	(1,2),
	(2,2),
	(3,2),
	(4,2),
	(5,2),
	(6,2),
	(9,2),
	(18,2),
	(19,2),
	(2,3),
	(3,3),
	(5,3),
	(9,3),
	(18,3)

-- FIN INSERTAR Funcionalidad_por_Rol


-- INSERTAR Visibilidad

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Visibilidad ON;

INSERT INTO LOS_SUPER_AMIGOS.Visibilidad
	([id],[descripcion],[porcentaje],[precio],[duracion])
SELECT DISTINCT Publicacion_Visibilidad_Cod, Publicacion_Visibilidad_Desc, Publicacion_Visibilidad_Porcentaje, Publicacion_Visibilidad_Precio, 7 FROM gd_esquema.Maestra

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Visibilidad OFF;

-- FIN INSERTAR Visibilidad


-- INSERTAR Rubro

INSERT INTO LOS_SUPER_AMIGOS.Rubro
	([descripcion])
SELECT DISTINCT Publicacion_Rubro_Descripcion FROM gd_esquema.Maestra WHERE ISNULL(Publicacion_Rubro_Descripcion, '') != ''

-- FIN INSERTAR Rubro


-- INSERTAR Publicacion

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Publicacion ON;

-- Carga todas las publicaciones
INSERT INTO LOS_SUPER_AMIGOS.Publicacion
	([id], [descripcion], [stock], [fecha_inicio], [fecha_vencimiento], [precio], [rubro_id], [visibilidad_id], [usuario_id], [estado], [tipo], [costo_pagado], [se_realizan_preguntas])
SELECT DISTINCT Publicacion_Cod, Publicacion_Descripcion, Publicacion_Stock, Publicacion_Fecha, Publicacion_Fecha_Venc, Publicacion_Precio, (SELECT id FROM LOS_SUPER_AMIGOS.Rubro r WHERE Publicacion_Rubro_Descripcion = r.descripcion), Publicacion_Visibilidad_Cod, LOS_SUPER_AMIGOS.agregar_id_publ(Publ_Cli_Dni, Publ_Empresa_Razon_Social),Publicacion_Estado,Publicacion_Tipo, 1, 1 FROM gd_esquema.Maestra WHERE ISNULL(Publicacion_Rubro_Descripcion, '') != ''

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Publicacion OFF;

-- Finaliza todas las publicaciones que tienen fecha de vencimiento anterior al dia de hoy
UPDATE LOS_SUPER_AMIGOS.Publicacion 
	SET estado = 'Finalizada'
	WHERE fecha_vencimiento <= GETDATE()
	
-- FIN INSERTAR Publicacion


-- INSERTAR Calificaciones

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Calificacion ON;

INSERT INTO LOS_SUPER_AMIGOS.Calificacion
	([id], [cantidad_estrellas], [descripcion])
SELECT DISTINCT Calificacion_Codigo, Calificacion_Cant_Estrellas, Calificacion_Descripcion FROM gd_esquema.Maestra WHERE ISNULL(Calificacion_Codigo, -1) != -1

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Calificacion OFF;

-- FIN INSERTAR Calificaciones


-- INSERTAR Ofertas

INSERT INTO LOS_SUPER_AMIGOS.Oferta
	([monto], [fecha], [usuario_id], [publicacion_id])
SELECT Oferta_Monto, Oferta_Fecha, (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = Cli_Dni), Publicacion_Cod FROM gd_esquema.Maestra WHERE ISNULL(Oferta_Monto, 0) != 0

-- FIN INSERTAR Ofertas


-- INSERTAR Compras

INSERT INTO LOS_SUPER_AMIGOS.Compra
	([cantidad], [fecha], [usuario_id], [publicacion_id], [Calificacion_id], [facturada])
SELECT Compra_Cantidad, Compra_Fecha, (SELECT usuario_id FROM LOS_SUPER_AMIGOS.Cliente WHERE documento = Cli_Dni), Publicacion_Cod, Calificacion_Codigo, 1 FROM gd_esquema.Maestra WHERE ISNULL(Compra_Cantidad, 0) != 0 AND ISNULL(Calificacion_Codigo,0) != 0

--  FIN INSERTAR Compras


-- INSERTAR Formas_Pago

INSERT INTO LOS_SUPER_AMIGOS.Forma_Pago
   ([descripcion])
VALUES
	('Efectivo'),
	('Tarjeta de credito')

-- FIN INSERTAR Formas_Pago


-- INSERTAR Facturas

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Factura ON;

INSERT INTO LOS_SUPER_AMIGOS.Factura
   ( [nro], [fecha], [total], [forma_pago_id])
SELECT DISTINCT Factura_Nro, Factura_Fecha, Factura_Total, (SELECT id FROM LOS_SUPER_AMIGOS.Forma_Pago WHERE descripcion = Forma_Pago_Desc) FROM gd_esquema.Maestra WHERE ISNULL(Factura_Nro,-1) != -1

SET IDENTITY_INSERT LOS_SUPER_AMIGOS.Factura OFF;

-- FIN INSERTAR Facturas


-- INSERTAR Items_Factura

INSERT INTO LOS_SUPER_AMIGOS.Item_Factura
	([monto], [cantidad], [factura_nro], [publicacion_id])
SELECT DISTINCT Item_Factura_Monto, Item_Factura_Cantidad, Factura_Nro, Publicacion_Cod FROM gd_esquema.Maestra WHERE ISNULL(Factura_Nro,-1) != -1

-- FIN INSERTAR Items_Factura


-- INSERTAR Comisiones_Usuario_x_Visibilidad

INSERT INTO LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad
	([usuario_id], [visibilidad_id], [contador_comisiones])
SELECT u.id id_usuario, v.id id_visibilidad, (COUNT(c.id)%10) ventas FROM LOS_SUPER_AMIGOS.Usuario u, LOS_SUPER_AMIGOS.Visibilidad v,  LOS_SUPER_AMIGOS.Compra c, LOS_SUPER_AMIGOS.Publicacion p WHERE p.usuario_id = u.id AND p.visibilidad_id = v.id AND c.publicacion_id = p.id GROUP BY u.id, v.id ORDER BY u.id

-- FIN INSERTAR Comisiones_Usuario_x_Visibilidad

-- FIN DE MIGRACION DE DATOS

GO

-- CREACION DE VISTAS

CREATE VIEW LOS_SUPER_AMIGOS.VistaOfertaMax
(
	precioMax,
	publicacion_id
)
AS
	SELECT MAX(o.monto), o.publicacion_id
	FROM LOS_SUPER_AMIGOS.Oferta o
	GROUP BY o.publicacion_id
GO

CREATE VIEW LOS_SUPER_AMIGOS.VistaCantidadVendida
(
	cant_vendida,
	publicacion_id
)
AS
	SELECT SUM(o.cantidad), o.publicacion_id
	FROM LOS_SUPER_AMIGOS.Compra o
	GROUP BY o.publicacion_id
GO

-- FIN DE CREACION DE VISTAS


-- CREACION DE TRIGGERS

CREATE TRIGGER finalizar_x_fin_stock ON LOS_SUPER_AMIGOS.Compra
FOR INSERT
AS
BEGIN
	IF((
			SELECT (p.stock - v.cant_vendida)
			FROM INSERTED i, LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.VistaCantidadVendida v
			WHERE i.publicacion_id = p.id 
				AND i.publicacion_id = v.publicacion_id
		) = 0)
	UPDATE LOS_SUPER_AMIGOS.Publicacion 
		SET estado = 'Finalizada'
		FROM INSERTED i, LOS_SUPER_AMIGOS.Publicacion p
		WHERE p.id = i.publicacion_id	
END
GO

CREATE TRIGGER agregar_valor_default_de_nueva_visiblidad_en_comisiones ON LOS_SUPER_AMIGOS.Visibilidad
FOR INSERT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad 
		([usuario_id], [visibilidad_id], [contador_comisiones])
	SELECT DISTINCT comision.usuario_id, inserted.id, 0 FROM LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad comision, INSERTED inserted 	
END
GO

CREATE TRIGGER agregar_valor_default_de_nuevo_usuario_en_comisiones ON LOS_SUPER_AMIGOS.Usuario
FOR INSERT
AS
BEGIN
	INSERT INTO LOS_SUPER_AMIGOS.Comisiones_Usuario_x_Visibilidad 
		([usuario_id], [visibilidad_id], [contador_comisiones])
	SELECT DISTINCT inserted.id, visibilidad.id, 0 FROM LOS_SUPER_AMIGOS.Visibilidad visibilidad, INSERTED inserted 	
END
GO

-- FIN DE CREACION DE TRIGGERS