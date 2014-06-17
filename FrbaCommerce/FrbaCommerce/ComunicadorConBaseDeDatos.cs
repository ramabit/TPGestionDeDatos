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
            query = objeto.GetQueryCrear();
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

        public Boolean Modificar(Decimal id, Comunicable objeto)
        {
            query = objeto.GetQueryModificar();
            parametros.Clear();
            parametros = objeto.GetParametros();
            parametros.Add(new SqlParameter("@id", id));
            int filasAfectadas = builderDeComandos.Crear(query, parametros).ExecuteNonQuery();
            if (filasAfectadas == 1) return true;
            return false;
        }

        public Comunicable Obtener(Decimal id, Type clase)
        {
            Comunicable objeto = (Comunicable)Activator.CreateInstance(clase);
            query = objeto.GetQueryObtener();
            parametros.Clear();
            parametros.Add(new SqlParameter("@id", id));
            SqlDataReader reader = builderDeComandos.Crear(query, parametros).ExecuteReader();
            if (reader.Read())
            {
                objeto.CargarInformacion(reader);
                return objeto;
            }
            return objeto;
        }

        public Decimal CrearCliente(Cliente cliente)
        {
            if (!pasoControlDeRegistro(cliente.GetIdTipoDeDocumento(), cliente.GetNumeroDeDocumento()))
                throw new ClienteYaExisteException();

            if (!pasoControlDeUnicidad(cliente.GetTelefono(), "telefono", "Cliente"))
                throw new TelefonoYaExisteException();

            return this.Crear(cliente);
        }

        public Decimal CrearEmpresa(Empresa empresa)
        {
            if (!pasoControlDeRegistroDeCuit(empresa.GetCuit()))
                throw new CuitYaExisteException();

            if (!pasoControlDeUnicidad(empresa.GetTelefono(), "telefono", "Empresa"))
                throw new TelefonoYaExisteException();

            if (!pasoControlDeRegistroDeRazonSocial(empresa.GetRazonSocial()))
                throw new RazonSocialYaExisteException();

            return this.Crear(empresa);
        }

        public Decimal CrearDireccion(Direccion direccion)
        {
            return this.Crear(direccion);
        }

        public Decimal CrearPublicacion(Publicacion publicacion)
        {
            return this.Crear(publicacion);
        }

        public Decimal CrearVisibilidad(Visibilidad visibilidad)
        {
            if (!pasoControlDeUnicidad(visibilidad.GetDescripcion(), "descripcion", "Visibilidad"))
                throw new VisibilidadYaExisteException();

            return this.Crear(visibilidad);
        }

        public Cliente ObtenerCliente(Decimal idCliente)
        {
            Cliente objeto = new Cliente();
            Type clase = objeto.GetType();
            return (Cliente) this.Obtener(idCliente, clase);
        }

        public Empresa ObtenerEmpresa(Decimal idEmpresa)
        {
            Empresa objeto = new Empresa();
            Type clase = objeto.GetType();
            return (Empresa)this.Obtener(idEmpresa, clase);
        }

        public Direccion ObtenerDireccion(Decimal idDireccion)
        {
            Direccion objeto = new Direccion();
            Type clase = objeto.GetType();
            return (Direccion)this.Obtener(idDireccion, clase);
        }

        public Visibilidad ObtenerVisibilidad(Decimal idVisibilidad)
        {
            Visibilidad objeto = new Visibilidad();
            Type clase = objeto.GetType();
            return (Visibilidad)this.Obtener(idVisibilidad, clase);
        }

        public Publicacion ObtenerPublicacion(Decimal idPublicacion)
        {
            Publicacion objeto = new Publicacion();
            Type clase = objeto.GetType();
            return (Publicacion)this.Obtener(idPublicacion, clase);
        }

        public Object SelectFromWhere(String que, String deDonde, String param1, String param2)
        {
            query = "SELECT " + que + " FROM LOS_SUPER_AMIGOS." + deDonde + " WHERE " + param1 + " = @" + param1;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + param1, param2));
            return builderDeComandos.Crear(query, parametros).ExecuteScalar();
        }

        public Object SelectFromWhere(String que, String deDonde, String param1, Decimal param2)
        {
            query = "SELECT " + que + " FROM LOS_SUPER_AMIGOS." + deDonde + " WHERE " + param1 + " = @" + param1;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + param1, param2));
            return builderDeComandos.Crear(query, parametros).ExecuteScalar();
        }

        public DataTable SelectDataTable(String que, String deDonde)
        {
            return this.SelectDataTable(que, deDonde, "true = true");
        }

        public DataTable SelectDataTable(String que, String deDonde, String condiciones)
        {
            return this.SelectDataTableConUsuario(que, deDonde, condiciones);
        }

        public DataTable SelectDataTableConUsuario(String que, String deDonde, String condiciones)
        {
            parametros.Clear();
            parametros.Add(new SqlParameter("@idUsuario", UsuarioSesion.Usuario.id));
            command = builderDeComandos.Crear("SELECT " + que + " FROM " + deDonde + " WHERE " + condiciones, parametros);
            DataSet datos = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(datos);
            return datos.Tables[0];
        }

        public DataTable SelectClientesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("c.id, u.username Username, c.nombre Nombre, c.apellido Apellido, td.nombre 'Tipo de Documento', c.documento Documento, c.fecha_nacimiento 'Fecha de Nacimiento', c.mail Mail, c.telefono Telefono, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo postal', d.localidad Localidad"
                , "LOS_SUPER_AMIGOS.Cliente c, LOS_SUPER_AMIGOS.TipoDeDocumento td, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u"
                , "c.tipo_de_documento_id = td.id AND c.direccion_id = d.id AND c.usuario_id = u.id " + filtro);
        }

        public DataTable SelectClientesParaFiltro()
        {
            return this.SelectClientesParaFiltroConFiltro("");
        }

        public DataTable SelectEmpresasParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("e.id, u.username Username, e.razon_social 'Razon Social', e.nombre_de_contacto 'Nombre de contacto', e.cuit 'CUIT', e.fecha_creacion 'Fecha de creacion', e.mail 'Mail', e.telefono 'Telefono', e.ciudad Ciudad, d.calle Calle, d.numero Numero, d.piso Piso, d.depto Departamento, d.cod_postal 'Codigo Postal', d.localidad Localidad"
                , "LOS_SUPER_AMIGOS.Empresa e, LOS_SUPER_AMIGOS.Direccion d, LOS_SUPER_AMIGOS.Usuario u"
                , "e.direccion_id = d.id AND e.usuario_id = u.id " + filtro);
        }

        public DataTable SelectEmpresasParaFiltro()
        {
            return this.SelectEmpresasParaFiltroConFiltro("");
        }

        public DataTable SelectVisibilidadesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTable("v.id, v.descripcion Descripcion, v.precio Precio, v.porcentaje Porcentaje, v.duracion Duracion"
                , "LOS_SUPER_AMIGOS.Visibilidad v"
                , filtro);
        }

        public DataTable SelectVisibilidadesParaFiltro()
        {
            return this.SelectDataTable("v.id, v.descripcion Descripcion, v.precio Precio, v.porcentaje Porcentaje, v.duracion Duracion"
                , "LOS_SUPER_AMIGOS.Visibilidad v");
        }

        public DataTable SelectPublicacionesParaFiltroConFiltro(String filtro)
        {
            return this.SelectDataTableConUsuario("p.id, u.username Username, p.tipo 'Tipo de publicacion', p.estado Estado, p.descripcion Descripcion, p.fecha_inicio 'Fecha de inicio', p.fecha_vencimiento 'Fecha de vencimiento', r.descripcion Rubro, v.descripcion Visibilidad, p.se_realizan_preguntas 'Permite preguntas', p.stock Stock, p.precio Precio"
                , "LOS_SUPER_AMIGOS.Publicacion p, LOS_SUPER_AMIGOS.Rubro r, LOS_SUPER_AMIGOS.Visibilidad v, LOS_SUPER_AMIGOS.Usuario u"
                , "p.rubro_id = r.id AND p.visibilidad_id = v.id AND p.usuario_id = u.id AND p.usuario_id = @idUsuario" + filtro);
        }

        public DataTable SelectPublicacionesParaFiltro()
        {
            return this.SelectPublicacionesParaFiltroConFiltro("");
        }

        private bool ControlDeUnicidad(String query, IList<SqlParameter> parametros)
        {
            int cantidad = (int)builderDeComandos.Crear(query, parametros).ExecuteScalar();
            if (cantidad > 0)
            {
                return false;
            }
            return true;
        }

        private bool pasoControlDeUnicidad(String que, String aQue, String enDonde)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS." + enDonde + " WHERE " + aQue + " = @" + aQue;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + aQue, que));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeUnicidad(String que, String aQue, String enDonde, Decimal id)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS." + enDonde + " WHERE " + aQue + " = @" + aQue + " AND id != " + id;
            parametros.Clear();
            parametros.Add(new SqlParameter("@" + aQue, que));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistro(Decimal tipoDeDocumento, String numeroDeDocumento)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE tipo_de_documento_id = @tipoDeDocumento AND documento = @numeroDeDocumento";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", Convert.ToDecimal(numeroDeDocumento)));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistro(Decimal tipoDeDocumento, String numeroDeDocumento, Decimal idCliente)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Cliente WHERE tipo_de_documento_id = @tipoDeDocumento AND documento = @numeroDeDocumento AND id != @idCliente";
            parametros.Clear();
            parametros.Add(new SqlParameter("@tipoDeDocumento", tipoDeDocumento));
            parametros.Add(new SqlParameter("@numeroDeDocumento", numeroDeDocumento));
            parametros.Add(new SqlParameter("@idCliente", idCliente));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistroDeRazonSocial(String razonSocial)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razonSocial";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistroDeRazonSocial(String razonSocial, Decimal idEmpresa)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE razon_social = @razonSocial AND id != @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@razonSocial", razonSocial));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistroDeCuit(String cuit)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE cuit = @cuit";
            parametros.Clear();
            parametros.Add(new SqlParameter("@cuit", cuit));
            return ControlDeUnicidad(query, parametros);
        }

        private bool pasoControlDeRegistroDeCuit(String cuit, Decimal idEmpresa)
        {
            query = "SELECT COUNT(*) FROM LOS_SUPER_AMIGOS.Empresa WHERE cuit = @cuit AND id != @idEmpresa";
            parametros.Clear();
            parametros.Add(new SqlParameter("@cuit", cuit));
            parametros.Add(new SqlParameter("@idEmpresa", idEmpresa));
            return ControlDeUnicidad(query, parametros);
        }

    }
}
