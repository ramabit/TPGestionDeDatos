#Tabla de usuario
- [ ] Agregar restriccion de username unico e informar
- [ ] Se debe enctriptar la password 

#Tabla de empresa
- [ ] Preguntar por nombre de contacto
- [ ] Preguntar por ciudad
- [ ] Preguntar por telefono
- [x] Agregar restriccion de razon social unico -- no se si esta bien esto. Me parece que hay que ponerle un atributo UNIQUE
- [x] Agregar restriccion de CUIT unico -- no se si esta bien esto
- [ ] Agregar restriccion de telefono unico e informar
- [x] Discutir cual deberia ser la PK -- La PK no es minima
- [x] Insertar username por defecto

#Tabla de cliente
- [ ] Preguntar por tipo de documento
- [ ] Preguntar por telefono
- [ ] Agregar restriccion de telefono unico
- [x] Discutir cual deberia ser la PK
- [x] Insertar username por defecto

#Tabla de rol

#Tabla de rol_x_usuario

#Tabla de funcionalidad

#Tabla de funcionalidad_por_rol

#Tabla de visibilidad

#Tabla de publicacion
- [ ] Agregar FK a rubro
- [ ] Separar los rubros y crear una tabla a partir de ellos
- [ ] Agregar funcionalidad de fecha_vencimiento automatica

#Tabla de pregunta
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de oferta
- [x] Agregar fecha en der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion en el der -> Una oferta no necesariamente tiene una calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de compra
- [x] Cambiar monto por cantidad en der
- [x] Agregar fecha en der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion en der-> Una compra tiene una calificacion pero no automaticamenteno, es decir, cuando se crea no tiene calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado
