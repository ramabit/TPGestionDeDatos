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

        public Decimal ObtenerIdDe(TipoDeDocumento tipoDeDocumento)
        {
            if (tipoDeDocumento.GetNombre() == "") 
                throw new FaltanDefinirAtributosException();
            query = "SELECT id FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE nombre = @tipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento.GetNombre()));
            Decimal idTipoDeDocumento = (Decimal)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            tipoDeDocumento.SetId(idTipoDeDocumento);
            return idTipoDeDocumento;
        }

        public String ObtenerNombreDe(TipoDeDocumento tipoDeDocumento)
        {
            if (tipoDeDocumento.GetId() == 0)
                throw new FaltanDefinirAtributosException();
            query = "SELECT nombre FROM LOS_SUPER_AMIGOS.TipoDeDocumento WHERE id = @idTipoDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@idTipoDeDocumento", tipoDeDocumento.GetId()));
            String nombreTipoDeDocumento = (String)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            tipoDeDocumento.SetNombre(nombreTipoDeDocumento);
            return nombreTipoDeDocumento;
        }

        public Decimal CrearDireccion(Direccion direccion)
        {
            query = "LOS_SUPER_AMIGOS.crear_direccion";
            parametros.Clear();
            parametroOutput = new SqlParameter("@direccion_id", SqlDbType.Decimal);
            parametroOutput.Direction = ParameterDirection.Output;
            parametros.Add(new SqlParameter("@calle", direccion.GetCalle()));
            parametros.Add(new SqlParameter("@numero", direccion.GetNumero()));
            parametros.Add(new SqlParameter("@piso", direccion.GetPiso()));
            parametros.Add(new SqlParameter("@depto", direccion.GetDepartamento()));
            parametros.Add(new SqlParameter("@cod_postal", direccion.GetCodigoPostal()));
            parametros.Add(new SqlParameter("@localidad", direccion.GetLocalidad()));
            parametros.Add(parametroOutput);
            command = builderDeComandos.Crear(query, parametros);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            Decimal idDireccionNueva = (Decimal)parametroOutput.Value;
            direccion.SetId(idDireccionNueva);
            return idDireccionNueva;
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
            return (Decimal) parametroOutput.Value;
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
            return (Decimal) parametroOutput.Value;
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
    }
}
