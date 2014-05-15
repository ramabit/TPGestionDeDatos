#Tabla de usuario

#Tabla de empresa
- [ ] Preguntar por nombre de contacto
- [ ] Preguntar por ciudad
- [ ] Preguntar por telefono
- [ ] Discutir cual deberia ser la PK -- La PK no es minima
- [ ] Insertar username por defecto
- [x] Agregar restriccion de razon social unico -- no se si esta bien esto. Me parece que hay que ponerle un atributo UNIQUE
- [x] Agregar restriccion de CUIT unico -- no se si esta bien esto

#Tabla de cliente
- [ ] Agregar restriccion de telefono unico
- [ ] Agregar tipo de documento
- [ ] Preguntar por telefono
- [ ] Discutir cual deberia ser la PK
- [ ] Insertar username por defecto

#Tabla de rol

#Tabla de rol_x_usuario

#Tabla de funcionalidad

#Tabla de funcionalidad_por_rol

#Tabla de visibilidad

#Tabla de publicacion
- [ ] Agregar FK a rubro
- [ ] Separar los rubros y crear una tabla a partir de ellos

#Tabla de pregunta
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de oferta
- [ ] Agregar fecha en der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion en el der -> Una oferta no necesariamente tiene una calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado

#Tabla de compra
- [ ] Cambiar monto por cantidad en der
- [ ] Agregar fecha en der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion en der-> Una compra tiene una calificacion pero no automaticamenteno, es decir, cuando se crea no tiene calificacion
- [ ] Deliberar acerca de si puede tener un campo habilitado