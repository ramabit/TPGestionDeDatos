#Tabla de cliente
- [ ] Agregar restriccion de telefono unico
- [ ] Agregar tipo de documento
- [ ] Preguntar por telefono
- [ ] Agregar PK

#Tabla de empresa
- [ ] Preguntar por nombre de contacto
- [ ] Preguntar por ciudad
- [ ] Preguntar por telefono
- [ ] Agregar restriccion de razon social unico
- [ ] Agregar restriccion de CUIT unico
- [ ] Agregar PK

#Tabla de publicacion
- [ ] Agregar FK a usuario
- [x] Agregar FK a visibilidad
- [ ] Agregar FK a rubro
- [ ] Separar los rubros y crear una tabla a partir de ellos

#Tabla de usuario
- [ ] Crear tabla

#Tabla de visibilidad
- [x] Crear tabla

#Tabla de pregunta
- [x] Crear tabla

#Tabla de oferta
- [ ] Agregar FK a usuario
- [ ] Agregar FK a calificacion
- [ ] Agregar fecha a der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion -> Una oferta no necesariamente tiene una calificacion

#Tabla de compra
- [ ] Agregar PK
- [ ] Agregar FK a usuario
- [ ] Agregar FK a calificacion
- [ ] Cambiar monto por cantidad en der
- [ ] Agregar fecha a der
- [ ] Cambiar la cardinalidad de la relacion con Calificacion -> Una compra tiene una calificacion pero no automaticamenteno, es decir, cuando se crea no tiene calificacion

#Tabla de calificacion
- [x] Crear tabla
