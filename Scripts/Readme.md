#Tabla de usuario
- [ ] Cambiar la forma de username automatic
- [ ] Cambiar int a otra cosa

#Tabla de empresa
- [x] Preguntar por nombre de contacto
- [x] Preguntar por ciudad
- [x] Preguntar por telefono => Es 0 por default
- [x] Agregar restriccion de razon social unico 
- [x] Agregar restriccion de CUIT unico
- [x] Agregar restriccion de telefono unico e informar
- [x] Discutir cual deberia ser la PK -- La PK no es minima
- [x] Insertar username por defecto

#Tabla de cliente
- [x] Preguntar por tipo de documento
- [x] Preguntar por telefono
- [x] Agregar restriccion de telefono unico
- [x] Discutir cual deberia ser la PK
- [x] Insertar username por defecto

#Tabla de rol

#Tabla de rol_x_usuario

#Tabla de funcionalidad

#Tabla de funcionalidad_por_rol

#Tabla de visibilidad
- [x] Ojo que puede faltar el campo codigo

#Tabla de publicacion
- [x] Agregar FK a rubro
- [x] Separar los rubros y crear una tabla a partir de ellos
- [ ] Agregar funcionalidad de fecha_vencimiento automatica
- [x] Hay que fijarse el tema de las preguntas

#Tabla de pregunta
- [x] Deliberar acerca de si puede tener un campo habilitado

#Tabla de calificacion
- [x] Deliberar acerca de si puede tener un campo habilitado

#Tabla de oferta
- [x] Agregar fecha en der
- [x] Cambiar la cardinalidad de la relacion con Calificacion en el der -> Una oferta no necesariamente tiene una calificacion
- [x] Deliberar acerca de si puede tener un campo habilitado

#Tabla de compra
- [x] Cambiar monto por cantidad en der
- [x] Agregar fecha en der
- [x] Cambiar la cardinalidad de la relacion con Calificacion en der-> Una compra tiene una calificacion pero no automaticamenteno, es decir, cuando se crea no tiene calificacion
- [x] Deliberar acerca de si puede tener un campo habilitado

#Tabla de Rubro
- [x] Fijarse si es necesario un campo habilitado
- [x] Ojo que puede faltar el campo codigo
- [x] Cambiar nombre por descripcion
