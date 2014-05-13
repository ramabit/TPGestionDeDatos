IF OBJECT_ID('Usuario', 'U') IS NOT NULL
DROP TABLE Usuario

create table Usuario
(
username nvarchar(45) primary key,
password nvarchar(45),
habilitado bit default 1,
)