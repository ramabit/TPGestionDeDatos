IF OBJECT_ID('Factura', 'U') IS NOT NULL
DROP TABLE Factura

create table Factura
(
Nro numeric(18,0),
Fecha DATETIME,
Total numeric(18,2),
Forma_Pago_Id int,
PRIMARY KEY (Nro),
FOREIGN KEY (Forma_Pago_Id) REFERENCES Forma_Pago (id)
)