using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using FrbaCommerce.Objetos;
using FrbaCommerce.Exceptions;
using System.Data;

namespace FrbaCommerce
{
    class ComunicadorConBaseDeDatos
    {
        private String query;
        private IList<SqlParameter> parametros = new List<SqlParameter>();
        private SqlParameter parametroOutput;
        private SqlCommand command;
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();

        public Decimal CrearUsuario()
        {
            query = "LOS_SUPER_AMIGOS.crear_usuario";
            parametros.Clear();
            parametroOutput = new SqlParameter("@usuario_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            return (Decimal)parametroOutput.Value;
        }

        public Decimal CrearUsuarioConValores(String username, String password)
        {
            query = "LOS_SUPER_AMIGOS.crear_usuario_con_valores";
            parametros.Clear();
            parametroOutput = new SqlParameter("@usuario_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@username", username));
            parametros.Add(new SqlParameter("@password", HashSha256.getHash(password)));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            return (Decimal)parametroOutput.Value;
        }

        public Decimal Crear(Comunicable objeto)
        {
            query = objeto.GetQuery();
            parametros.Clear();
            parametros = objeto.GetParametros();
            parametroOutput = new SqlParameter("@id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            return (Decimal)parametroOutput.Value;
        }

        public Decimal CrearDireccion(Direccion direccion)
        {
            return this.Crear(direccion);
        }

        public Boolean ModificarDireccion(Decimal idDireccion, Direccion direccion)
        {
            query = "UPDATE LOS_SUPER_AMIGOS.Direccion SET calle = @calle, numero = @numero, piso = @piso, depto = @departamento, cod_postal = @codigoPostal, localidad = @localidad WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@calle", direccion.GetCalle()));
            parametros.Add(new SqlParameter("@numero", direccion.GetNumero()));
            parametros.Add(new SqlParameter("@piso", direccion.GetPiso()));
            parametros.Add(new SqlParameter("@departamento", direccion.GetDepartamento()));
            parametros.Add(new SqlParameter("@codigoPostal", direccion.GetCodigoPostal()));
            parametros.Add(new SqlParameter("@localidad", direccion.GetLocalidad()));
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }

        public Direccion ObtenerDireccion(Decimal idDireccion)
        {
            Direccion nuevaDireccion = new Direccion();
            query = "SELECT calle, numero, piso, depto, cod_postal, localidad FROM LOS_SUPER_AMIGOS.Direccion WHERE id = @idDireccion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idDireccion", idDireccion));
            SqlDataReader readerDireccion = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerDireccion.Read())
            {
                nuevaDireccion.SetCalle(Convert.ToString(readerDireccion["calle"]));
                nuevaDireccion.SetNumero(Convert.ToString(readerDireccion["numero"]));
                nuevaDireccion.SetPiso(Convert.ToString(readerDireccion["piso"]));
                nuevaDireccion.SetDepartamento(Convert.ToString(readerDireccion["depto"]));
                nuevaDireccion.SetCodigoPostal(Convert.ToString(readerDireccion["cod_postal"]));
                nuevaDireccion.SetLocalidad(Convert.ToString(readerDireccion["localidad"]));
                return nuevaDireccion;
            }
            return nuevaDireccion;
        }

        public Decimal CrearClienteNuevo(Cliente cliente)
        {
            if (!pasoControlDeRegistro(cliente.GetIdTipoDeDocumento(), cliente.GetNumeroDeDocumento()))
                throw new ClienteYaExisteException();

            if (!pasoControlDeUnicidad(cliente.GetTelefono(), "telefono", "Cliente"))
                throw new TelefonoYaExisteException();

            return this.Crear(cliente);
        }

        public Decimal CrearCliente(Cliente cliente)
        {
            if (!pasoControlDeRegistro(cliente.GetIdTipoDeDocumento(), cliente.GetNumeroDeDocumento()))
                throw new ClienteYaExisteException();

            if (!pasoControlDeUnicidad(cliente.GetTelefono(), "telefono", "Cliente"))
                throw new TelefonoYaExisteException();

            query = "LOS_SUPER_AMIGOS.crear_cliente";
            parametros.Clear();
            parametroOutput = new SqlParameter("@cliente_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@nombre", cliente.GetNombre()));
            parametros.Add(new SqlParameter("@apellido", cliente.GetApellido()));
            parametros.Add(new SqlParameter("@tipo_de_documento_id", cliente.GetIdTipoDeDocumento()));
            parametros.Add(new SqlParameter("@documento", cliente.GetNumeroDeDocumento()));
            parametros.Add(new SqlParameter("@fecha_nacimiento", cliente.GetFechaDeNacimiento()));
            parametros.Add(new SqlParameter("@mail", cliente.GetMail()));
            parametros.Add(new SqlParameter("@telefono", cliente.GetTelefono()));
            parametros.Add(new SqlParameter("@direccion_id", cliente.GetIdDireccion()));
            parametros.Add(new SqlParameter("@usuario_id", cliente.GetIdUsuario()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idClienteNuevo = (Decimal)parametroOutput.Value;
            cliente.SetId(idClienteNuevo);
            return idClienteNuevo;
        }

        public Decimal CrearEmpresa(Empresa empresa)
        {
            if (!pasoControlDeRegistroDeCuit(empresa.GetCuit()))
                throw new CuitYaExisteException();

            if (!pasoControlDeUnicidad(empresa.GetTelefono(), "telefono", "Empresa"))
                throw new TelefonoYaExisteException();

            if (!pasoControlDeRegistroDeRazonSocial(empresa.GetRazonSocial()))
                throw new RazonSocialYaExisteException();

            query = "LOS_SUPER_AMIGOS.crear_empresa";
            parametros.Clear();
            parametroOutput = new SqlParameter("@empresa_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@razon_social", empresa.GetRazonSocial()));
            parametros.Add(new SqlParameter("@nombre_de_contacto", empresa.GetNombreDeContacto()));
            parametros.Add(new SqlParameter("@cuit", empresa.GetCuit()));
            parametros.Add(new SqlParameter("@fecha_creacion", empresa.GetFechaDeCreacion()));
            parametros.Add(new SqlParameter("@mail", empresa.GetMail()));
            parametros.Add(new SqlParameter("@telefono", empresa.GetTelefono()));
            parametros.Add(new SqlParameter("@ciudad", empresa.GetCiudad()));
            parametros.Add(new SqlParameter("@direccion_id", empresa.GetIdDireccion()));
            parametros.Add(new SqlParameter("@usuario_id", empresa.GetIdUsuario()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idEmpresaNuevo = (Decimal)parametroOutput.Value;
            empresa.SetId(idEmpresaNuevo);
            return idEmpresaNuevo;
        }

        public Boolean ModificarCliente(Decimal idCliente, Cliente cliente)
        {
            if (!pasoControlDeRegistro(cliente.GetIdTipoDeDocumento(), cliente.GetNumeroDeDocumento(), idCliente))
                throw new ClienteYaExisteException();

            if (!pasoControlDeUnicidad(cliente.GetTelefono(), "telefono", "Cliente", idCliente))
                throw new TelefonoYaExisteException();

            query = "UPDATE LOS_SUPER_AMIGOS.Cliente SET nombre = @nombre, apellido = @apellido, tipo_de_documento_id = @tipo_de_documento_id, documento = @documento, fecha_nacimiento = @fecha_nacimiento, mail = @mail, telefono = @telefono WHERE id = @idCliente";
            parametros.Clear();
            parametros.Add(new SqlParameter("@nombre", cliente.GetNombre()));
            parametros.Add(new SqlParameter("@apellido", cliente.GetApellido()));
            parametros.Add(new SqlParameter("@tipo_de_documento_id", cliente.GetIdTipoDeDocumento()));
            parametros.Add(new SqlParameter("@documento", cliente.GetNumeroDeDocumento()));
            parametros.Add(new SqlParameter("@fecha_nacimiento", cliente.GetFechaDeNacimiento()));
            parametros.Add(new SqlParameter("@mail", cliente.GetMail()));
            parametros.Add(new SqlParameter("@telefono", cliente.GetTelefono()));
            parametros.Add(new SqlParameter("@idCliente", idCliente));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }

        public Boolean ModificarEmpresa(Decimal idEmpresa, Empresa empresa)
        {
            if (!pasoControlDeRegistroDeCuit(empresa.GetCuit(), idEmpresa))
                throw new CuitYaExisteException();

            if (!pasoControlDeUnicidad(empresa.GetTelefono(), "telefono", "Empresa", idEmpresa))
                throw new TelefonoYaExisteException();

            if (!pasoControlDeRegistroDeRazonSocial(empresa.GetRazonSocial(), idEmpresa))
                throw new RazonSocialYaExisteException();

            query = "UPDATE LOS_SUPER_AMIGOS.Empresa SET razon_social = @razon_social, nombre_de_contacto = @nombre_de_contacto, cuit = @cuit, fecha_creacion = @fecha_creacion, mail = @mail, telefono = @telefono, ciudad = @ciudad  WHERE id = @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razon_social", empresa.GetRazonSocial()));
            parametros.Add(new SqlParameter("@nombre_de_contacto", empresa.GetNombreDeContacto()));
            parametros.Add(new SqlParameter("@cuit", empresa.GetCuit()));
            parametros.Add(new SqlParameter("@fecha_creacion", empresa.GetFechaDeCreacion()));
            parametros.Add(new SqlParameter("@mail", empresa.GetMail()));
            parametros.Add(new SqlParameter("@telefono", empresa.GetTelefono()));
            parametros.Add(new SqlParameter("@ciudad", empresa.GetCiudad()));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }

        public Cliente ObtenerCliente(Decimal idCliente)
        {
            Cliente nuevoCliente = new Cliente();
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Cliente WHERE id = @idCliente";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            SqlDataReader readerCliente = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerCliente.Read())
            {
                nuevoCliente.SetNombre(Convert.ToString(readerCliente["nombre"]));
                nuevoCliente.SetApellido(Convert.ToString(readerCliente["apellido"]));
                nuevoCliente.SetFechaDeNacimiento(Convert.ToString(readerCliente["fecha_nacimiento"]));
                nuevoCliente.SetMail(Convert.ToString(readerCliente["mail"]));
                nuevoCliente.SetTelefono(Convert.ToString(readerCliente["telefono"]));
                nuevoCliente.SetIdTipoDeDocumento(Convert.ToDecimal(readerCliente["tipo_de_documento_id"]));
                nuevoCliente.SetNumeroDeDocumento(Convert.ToString(readerCliente["documento"]));
                nuevoCliente.SetIdDireccion(Convert.ToDecimal(readerCliente["direccion_id"]));
                nuevoCliente.SetIdUsuario(Convert.ToDecimal(readerCliente["usuario_id"]));
                return nuevoCliente;
            }
            return nuevoCliente;
        }

        public Empresa ObtenerEmpresa(Decimal idEmpresa)
        {
            Empresa nuevoEmpresa = new Empresa();
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Empresa WHERE id = @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            SqlDataReader readerEmpresa = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerEmpresa.Read())
            {
                nuevoEmpresa.SetRazonSocial(Convert.ToString(readerEmpresa["razon_social"]));
                nuevoEmpresa.SetNombreDeContacto(Convert.ToString(readerEmpresa["nombre_de_contacto"]));
                nuevoEmpresa.SetFechaDeCreacion(Convert.ToString(readerEmpresa["fecha_creacion"]));
                nuevoEmpresa.SetCuit(Convert.ToString(readerEmpresa["cuit"]));
                nuevoEmpresa.SetMail(Convert.ToString(readerEmpresa["mail"]));
                nuevoEmpresa.SetTelefono(Convert.ToString(readerEmpresa["telefono"]));
                nuevoEmpresa.SetCiudad(Convert.ToString(readerEmpresa["ciudad"]));
                nuevoEmpresa.SetIdDireccion(Convert.ToDecimal(readerEmpresa["direccion_id"]));
                nuevoEmpresa.SetIdUsuario(Convert.ToDecimal(readerEmpresa["usuario_id"]));
                return nuevoEmpresa;
            }
            return nuevoEmpresa;
        }

        public Decimal CrearVisibilidad(Visibilidad visibilidad)
        {
            if (!pasoControlDeUnicidad(visibilidad.GetDescripcion(), "descripcion", "Visibilidad"))
                throw new VisibilidadYaExisteException();

            query = "LOS_SUPER_AMIGOS.crear_visibilidad";
            parametros.Clear();
            parametroOutput = new SqlParameter("@visibilidad_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@descripcion", visibilidad.GetDescripcion()));
            parametros.Add(new SqlParameter("@precio", visibilidad.GetPrecioPorPublicar()));
            parametros.Add(new SqlParameter("@porcentaje", visibilidad.GetPorcentajePorVenta()));
            parametros.Add(new SqlParameter("@duracion", visibilidad.GetDuracion()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idVisibilidadNueva = (Decimal)parametroOutput.Value;
            visibilidad.SetId(idVisibilidadNueva);
            return idVisibilidadNueva;
        }

        public Boolean ModificarVisibilidad(Decimal idVisibilidad, Visibilidad visibilidad)
        {
            if (!pasoControlDeUnicidad(visibilidad.GetDescripcion(), "descripcion", "Visibilidad", idVisibilidad))
                throw new VisibilidadYaExisteException();

            query = "UPDATE LOS_SUPER_AMIGOS.Visibilidad SET descripcion = @descripcion, precio = @precioporPublicar, porcentaje = @porcentajeDeVenta, duracion = @duracion WHERE id = @idVisibilidad";
            parametros.Clear();
            parametros.Add(new SqlParameter("@descripcion", visibilidad.GetDescripcion()));
            parametros.Add(new SqlParameter("@precioporPublicar", visibilidad.GetPrecioPorPublicar()));
            parametros.Add(new SqlParameter("@porcentajeDeVenta", visibilidad.GetPorcentajePorVenta()));
            parametros.Add(new SqlParameter("@duracion", visibilidad.GetDuracion()));
            parametros.Add(new SqlParameter("@idVisibilidad", idVisibilidad));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }

        public Visibilidad ObtenerVisibilidad(Decimal idVisibilidad)
        {
            Visibilidad nuevoVisibilidad = new Visibilidad();
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Visibilidad WHERE id = @idVisibilidad";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idVisibilidad", idVisibilidad));
            SqlDataReader readerVisibilidad = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerVisibilidad.Read())
            {
                nuevoVisibilidad.SetDescripcion(Convert.ToString(readerVisibilidad["descripcion"]));
                nuevoVisibilidad.SetPrecioPorPublicar(Convert.ToString(readerVisibilidad["precio"]));
                nuevoVisibilidad.SetPorcentajePorVenta(Convert.ToString(readerVisibilidad["porcentaje"]));
                nuevoVisibilidad.SetDuracion(Convert.ToString(readerVisibilidad["duracion"]));
                return nuevoVisibilidad;
            }
            return nuevoVisibilidad;
        }

        public Object SelectFromWhere(String que, String deDonde, String param1, String param2)
        {
            query = "SELECT " + que + " FROM LOS_SUPER_AMIGOS." + deDonde + " WHERE " + param1 + " = @" + param1;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + param1, param2));
            return builderDeComandos.Crear(query, parametros).ExecuteScalar();
        }

        public Decimal CrearPublicacion(Publicacion publicacion)
        {
            query = "LOS_SUPER_AMIGOS.crear_publicacion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@publicacion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@tipo", publicacion.GetTipo()));
            parametros.Add(new SqlParameter("@estado", publicacion.GetEstado()));
            parametros.Add(new SqlParameter("@descripcion", publicacion.GetDescripcion()));
            parametros.Add(new SqlParameter("@fecha_inicio", publicacion.GetFechaDeInicio()));
            parametros.Add(new SqlParameter("@fecha_vencimiento", publicacion.GetFechaDeVencimiento()));
            parametros.Add(new SqlParameter("@stock", publicacion.GetStock()));
            parametros.Add(new SqlParameter("@precio", publicacion.GetPrecio()));
            parametros.Add(new SqlParameter("@rubro_id", publicacion.GetIdRubro()));
            parametros.Add(new SqlParameter("@visibilidad_id", publicacion.GetIdVisibilidad()));
            parametros.Add(new SqlParameter("@usuario_id", publicacion.GetIdUsuario()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idPublicacionNueva = (Decimal)parametroOutput.Value;
            publicacion.SetId(idPublicacionNueva);
            return idPublicacionNueva;
        }

        public Boolean ModificarPublicacion(Decimal idPublicacion, Publicacion publicacion)
        {
            query = "UPDATE LOS_SUPER_AMIGOS.Publicacion SET estado = @estado, descripcion = @descripcion, fecha_inicio = @fecha_inicio, fecha_vencimiento = @fecha_vencimiento, rubro_id = @rubro_id, visibilidad_id = @visibilidad_id, stock = @stock, precio = @precio WHERE id = @idPublicacion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@estado", publicacion.GetEstado()));
            parametros.Add(new SqlParameter("@descripcion", publicacion.GetDescripcion()));
            parametros.Add(new SqlParameter("@fecha_inicio", publicacion.GetFechaDeInicio()));
            parametros.Add(new SqlParameter("@fecha_vencimiento", publicacion.GetFechaDeVencimiento()));
            parametros.Add(new SqlParameter("@stock", publicacion.GetStock()));
            parametros.Add(new SqlParameter("@precio", publicacion.GetPrecio()));
            parametros.Add(new SqlParameter("@rubro_id", publicacion.GetIdRubro()));
            parametros.Add(new SqlParameter("@visibilidad_id", publicacion.GetIdVisibilidad()));
            parametros.Add(new SqlParameter("@idPublicacion", idPublicacion));

            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();

            if (filasAfectadas == 1) return true;
            return false;
        }

        public Publicacion ObtenerPublicacion(Decimal idPublicacion)
        {
            Publicacion nuevoPublicacion = new Publicacion();
            query = "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE id = @idPublicacion";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idPublicacion", idPublicacion));
            SqlDataReader readerPublicacion = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (readerPublicacion.Read())
            {
                nuevoPublicacion.SetTipo(Convert.ToString(readerPublicacion["tipo"]));
                nuevoPublicacion.SetEstado(Convert.ToString(readerPublicacion["estado"]));
                nuevoPublicacion.SetDescripcion(Convert.ToString(readerPublicacion["descripcion"]));
                nuevoPublicacion.SetFechaDeInicio(Convert.ToString(readerPublicacion["fecha_inicio"]));
                nuevoPublicacion.SetFechaDeVencimiento(Convert.ToString(readerPublicacion["fecha_vencimiento"]));
                nuevoPublicacion.SetStock(Convert.ToString(readerPublicacion["stock"]));
                nuevoPublicacion.SetPrecio(Convert.ToString(readerPublicacion["precio"]));
                nuevoPublicacion.SetIdRubro(Convert.ToDecimal(readerPublicacion["rubro_id"]));
                nuevoPublicacion.SetIdVisibilidad(Convert.ToDecimal(readerPublicacion["visibilidad_id"]));
                nuevoPublicacion.SetIdUsuario(Convert.ToDecimal(readerPublicacion["usuario_id"]));
                return nuevoPublicacion;
            }
            return nuevoPublicacion;
        }

        public DataTable SelectDataTable(String que, String deDonde)
        {
            parametros.Clear();
            command = builderDeComandos.Crear("SELECT " + que + " FROM " + deDonde, parametros);
            DataSet datos = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(datos);
            return datos.Tables[0];
        }

        public DataTable SelectDataTable(String que, String deDonde, String condiciones)
        {
            parametros.Clear();
            command = builderDeComandos.Crear("SELECT " + que + " FROM " + deDonde + " WHERE " + condiciones, parametros);
            DataSet datos = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(datos);
            return datos.Tables[0];
        }

        public DataTable SelectDataTableConUsuario(String que, String deDonde, String condiciones)
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@idUsuario", 2));//UsuarioSesion.Usuario.id));
            command = builderDeComandos.Crear("SELECT " + que + " FROM " + deDonde + " WHERE " + condiciones, parametros);
            DataSet datos = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(datos);
            return datos.Tables[0];
        }

        public DataTable SelectClientesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("c.id, u.username Username, c.nombre Nombre, c.apellido Apellido, td.nombre 'Tipo de Documento', c.documento Documento, c.fecha_nacimiento 'Fecha de Nacimiento', c.mail Mail, c.telefono Telefono, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo postal', d.localidad Localidad", "LOS_SUPER_AMIGOS.Cliente c, LOS_SUPER_AMIGOS.TipoDeDocumento td, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u", "c.tipo_de_documento_id = td.id AND c.direccion_id = d.id AND c.usuario_id = u.id " + filtro);
        }

        public DataTable SelectClientesParaFiltro()
        {
            return this.SelectClientesParaFiltroConFiltro("");
        }

        public DataTable SelectEmpresasParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("e.id, u.username Username, e.razon_social 'Razon Social', e.nombre_de_contacto 'Nombre de contacto', e.cuit 'CUIT', e.fecha_creacion 'Fecha de creacion', e.mail 'Mail', e.telefono 'Telefono', e.ciudad Ciudad, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo Postal', d.localidad Localidad", "LOS_SUPER_AMIGOS.Empresa e, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u", "e.direccion_id = d.id AND e.usuario_id = u.id " + filtro);
        }

        public DataTable SelectEmpresasParaFiltro()
        {
            return this.SelectEmpresasParaFiltroConFiltro("");
        }

        public DataTable SelectVisibilidadesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("v.id, v.descripcion Descripcion, v.precio Precio, v.porcentaje Porcentaje, v.duracion Duracion", "LOS_SUPER_AMIGOS.Visibilidad v", filtro);
        }

        public DataTable SelectVisibilidadesParaFiltro()
        {
            return this.SelectDataTable("v.id, v.descripcion Descripcion, v.precio Precio, v.porcentaje Porcentaje, v.duracion Duracion", "LOS_SUPER_AMIGOS.Visibilidad v");
        }

        public DataTable SelectPublicacionesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTableConUsuario("p.id, u.username Username, p.tipo 'Tipo de publicacion', p.estado Estado, p.descripcion Descripcion, p.fecha_inicio 'Fecha de inicio', p.fecha_vencimiento 'Fecha de vencimiento', r.descripcion Rubro, v.descripcion Visibilidad, p.se_realizan_preguntas 'Permite preguntas', p.stock Stock, p.precio Precio", "LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Rubro r, LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u", "p.rubro_id = r.id AND p.visibilidad_id = v.id AND p.usuario_id = u.id AND p.usuario_id = @idUsuario" + filtro);
        }

        public DataTable SelectPublicacionesParaFiltro()
        {
            return this.SelectPublicacionesParaFiltroConFiltro("");
        }

        private bool pasoControlDeUnicidad(String que, String aQue, String enDonde)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS." + enDonde + " WHERE " + aQue + " = @" + aQue;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + aQue, que));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeUnicidad(String que, String aQue, String enDonde, Decimal id)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS." + enDonde + " WHERE " + aQue + " = @" + aQue + " AND id != " + id;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + aQue, que));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistro(Decimal tipoDeDocumento, String numeroDeDocumento)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE tipo_de_documento_id = @tipoDeDocumento AND documento = @numeroDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", Convert.ToDecimal(numeroDeDocumento)));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistro(Decimal tipoDeDocumento, String numeroDeDocumento, Decimal idCliente)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE tipo_de_documento_id = @tipoDeDocumento AND documento = @numeroDeDocumento AND id != @idCliente";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", numeroDeDocumento));
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeRazonSocial(String razonSocial)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razonSocial";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeRazonSocial(String razonSocial, Decimal idEmpresa)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razonSocial AND id != @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeCuit(String cuit)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE cuit = @cuit";
            parametros.Clear();
            parametros.Add(new SqlParameter("@cuit", cuit));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeRegistroDeCuit(String cuit, Decimal idEmpresa)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE cuit = @cuit AND id != @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@cuit", cuit));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

    }
}
