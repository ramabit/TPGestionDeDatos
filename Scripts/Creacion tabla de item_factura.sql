IF OBJECT_ID('Item_Factura', 'U') IS NOT NULL
DROP TABLE Item_Factura

create table Item_Factura
(
id int,
monto numeric(18,2),
cantidad numeric(18,0),
Factura_Nro numeric(18,0),
Publicacio_Cod numeric,
PRIMARY KEY (id),
FOREIGN KEY (Factura_Nro) REFERENCES Factura (Nro),
FOREIGN KEY (Publicacion_Nro) REFERENCES Publicacion (Cod)
)