using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrbaCommerce.Exceptions;
using System.Data.SqlClient;

namespace FrbaCommerce.Objetos
{
    class Publicacion : Objeto, Comunicable
    {
        private Decimal id;
        private String tipo;
        private String estado;
        private String descripcion;
        private DateTime fechaDeInicio;
        private DateTime fechaDeVencimiento;
        private Decimal idRubro;
        private Decimal idVisibilidad;
        private Decimal idUsuario;
        private String stock;
        private String precio;
        private Boolean pregunta;
        private Boolean habilitado;

        public void SetId(Decimal id)
        {
            this.id = id;
        }

        public Decimal GetId()
        {
            return this.id;
        }

        public void SetTipo(String tipo)
        {
            if (tipo == "")
                throw new CampoVacioException("Tipo");
            this.tipo = tipo;
        }

        public String GetTipo()
        {
            return this.tipo;
        }

        public void SetEstado(String estado)
        {
            if (estado == "")
                throw new CampoVacioException("Estado");
            this.estado = estado;
        }

        public String GetEstado()
        {
            return this.estado;
        }

        public void SetDescripcion(String descripcion)
        {
            if (descripcion == "")
                throw new CampoVacioException("Descripcion");
            this.descripcion = descripcion;
        }

        public String GetDescripcion()
        {
            return this.descripcion;
        }

        public void SetFechaDeInicio(DateTime fechaDeInicio)
        {
            this.fechaDeInicio = fechaDeInicio;
        }

        public DateTime GetFechaDeInicio()
        {
            return this.fechaDeInicio;
        }

        public void SetFechaDeVencimiento(DateTime fechaDeVencimiento)
        {
            this.fechaDeVencimiento = fechaDeVencimiento;
        }

        public DateTime GetFechaDeVencimiento()
        {
            return this.fechaDeVencimiento;
        }

        public void SetIdRubro(Decimal idRubro)
        {
            if (idRubro == 0)
                throw new CampoVacioException("Rubro");
            this.idRubro = idRubro;
        }

        public Decimal GetIdRubro()
        {
            return this.idRubro;
        }

        public void SetIdUsuario(Decimal idUsuario)
        {
            if (idUsuario == 0)
                throw new CampoVacioException("Usuario");
            this.idUsuario = idUsuario;
        }

        public Decimal GetIdUsuario()
        {
            return this.idUsuario;
        }

        public void SetIdVisibilidad(Decimal idVisibilidad)
        {
            if (idVisibilidad == 0)
                throw new CampoVacioException("Visibilidad");
            this.idVisibilidad = idVisibilidad;
        }

        public Decimal GetIdVisibilidad()
        {
            return this.idVisibilidad;
        }

        public void SetStock(String stock)
        {
            if (stock == "")
                throw new CampoVacioException("Stock");

            if (!esNumero(stock))
                throw new FormatoInvalidoException();

            this.stock = stock;
        }

        public String GetStock()
        {
            return this.stock;
        }

        public void SetPrecio(String precio)
        {
            if (precio == "")
                throw new CampoVacioException("Precio");

            if (!esNumero(precio))
                throw new FechaPasadaException();

            this.precio = precio;
        }

        public String GetPrecio()
        {
            return this.precio;
        }

        public void SetPregunta(Boolean pregunta)
        {
            this.pregunta = pregunta;
        }

        public Boolean GetPregunta()
        {
            return this.pregunta;
        }

        public void SetHabilitado(Boolean habilitado)
        {
            this.habilitado = habilitado;
        }

        public Boolean GetHabilitado()
        {
            return this.habilitado;
        }


        #region Miembros de Comunicable

        string Comunicable.GetQueryCrear()
        {
            return "LOS_SUPER_AMIGOS.crear_publicacion";
        }

        string Comunicable.GetQueryModificar()
        {
            return "UPDATE LOS_SUPER_AMIGOS.Publicacion SET tipo = @tipo, estado = @estado, descripcion = @descripcion, fecha_inicio = @fecha_inicio, fecha_vencimiento = @fecha_vencimiento, rubro_id = @rubro_id, visibilidad_id = @visibilidad_id, stock = @stock, precio = @precio, se_realizan_preguntas = @se_realizan_preguntas, habilitado = @habilitado WHERE id = @id";
        }

        string Comunicable.GetQueryObtener()
        {
            return "SELECT * FROM LOS_SUPER_AMIGOS.Publicacion WHERE id = @id";
        }

        IList<System.Data.SqlClient.SqlParameter> Comunicable.GetParametros()
        {
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@tipo", this.tipo));
            parametros.Add(new SqlParameter("@estado", this.estado));
            parametros.Add(new SqlParameter("@descripcion", this.descripcion));
            parametros.Add(new SqlParameter("@fecha_inicio", this.fechaDeInicio));
            parametros.Add(new SqlParameter("@fecha_vencimiento", this.fechaDeVencimiento));
            parametros.Add(new SqlParameter("@stock", this.stock));
            parametros.Add(new SqlParameter("@precio", this.precio));
            parametros.Add(new SqlParameter("@rubro_id", this.idRubro));
            parametros.Add(new SqlParameter("@visibilidad_id", this.idVisibilidad));
            parametros.Add(new SqlParameter("@usuario_id", this.idUsuario));
            parametros.Add(new SqlParameter("@se_realizan_preguntas", this.pregunta));
            parametros.Add(new SqlParameter("@habilitado", this.habilitado));
            return parametros;
        }

        void Comunicable.CargarInformacion(SqlDataReader reader)
        {
            this.tipo = Convert.ToString(reader["tipo"]);
            this.estado = Convert.ToString(reader["estado"]);
            this.descripcion = Convert.ToString(reader["descripcion"]);
            this.fechaDeInicio = Convert.ToDateTime(reader["fecha_inicio"]);
            this.fechaDeVencimiento = Convert.ToDateTime(reader["fecha_vencimiento"]);
            this.stock = Convert.ToString(reader["stock"]);
            this.precio = Convert.ToString(reader["precio"]);
            this.idRubro = Convert.ToDecimal(reader["rubro_id"]);
            this.idVisibilidad = Convert.ToDecimal(reader["visibilidad_id"]);
            this.idUsuario = Convert.ToDecimal(reader["usuario_id"]);
            this.pregunta = Convert.ToBoolean(reader["se_realizan_preguntas"]);
        }

        #endregion
    }
}
