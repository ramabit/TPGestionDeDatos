using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace FrbaCommerce
{
    public partial class MenuPrincipal : Form
    {
        private SqlCommand comando { get; set; }
        private Dictionary<String, Form> funcionalidades = new Dictionary<String, Form>();
        private BuilderDeComandos builderDeComandos = new BuilderDeComandos();
    
        public MenuPrincipal()
        {
            InitializeComponent();

            funcionalidades.Add("Comprar / Ofertar", new Comprar_Ofertar.BuscadorPublicaciones());
            funcionalidades.Add("Generar publicacion", new Generar_Publicacion.GenerarPublicacion());
            funcionalidades.Add("Editar publicacion", new Editar_Publicacion.FiltrarPublicacion());
            funcionalidades.Add("Calificar vendedor", new Calificar_Vendedor.Listado());            
            funcionalidades.Add("Responder preguntas", new Gestion_de_Preguntas.VerPreguntas());
            funcionalidades.Add("Ver respuestas", new Gestion_de_Preguntas.VerRespuestas());
            funcionalidades.Add("Gestionar roles", new ABM_Rol.RolForm());
            funcionalidades.Add("Generar factura", new Facturar_Publicaciones.Facturar());
            funcionalidades.Add("Crear empresa", new ABM_Empresa.AgregarEmpresa("empresaCreadoPorAdmin", "password"));
            funcionalidades.Add("Editar empresa", new ABM_Empresa.FiltroEmpresa());
            funcionalidades.Add("Crear cliente", new ABM_Cliente.AgregarCliente("clienteCreadoPorAdmin", "password"));
            funcionalidades.Add("Editar cliente", new ABM_Cliente.FiltroCliente());
            funcionalidades.Add("Crear visibilidad", new ABM_Visibilidad.AgregarVisibilidad());
            funcionalidades.Add("Editar visibilidad", new ABM_Visibilidad.FiltroVisibilidad());
            funcionalidades.Add("Crear rubro", new ABM_Rubro.AgregarRubro());
            funcionalidades.Add("Editar rubro", new ABM_Rubro.FiltroRubros());
            funcionalidades.Add("Obtener estadisticas", new Listado_Estadistico.Estadisticas());
            funcionalidades.Add("Ver historial", new Historial_Cliente.Historial());
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            DataSet acciones = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            
            String funcionalidadesUsuario = "select f.nombre from LOS_SUPER_AMIGOS.Rol r, LOS_SUPER_AMIGOS.Funcionalidad_x_Rol fr,LOS_SUPER_AMIGOS.Funcionalidad f where r.id = fr.rol_id and f.id = fr.funcionalidad_id and r.nombre = @unRol";
            IList<SqlParameter> parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@unRol", UsuarioSesion.Usuario.rol));
            comando = builderDeComandos.Crear(funcionalidadesUsuario, parametros);

            adapter.SelectCommand = comando;
            adapter.Fill(acciones, "Funcionalidad");
            comboBoxAccion.DataSource = acciones.Tables[0].DefaultView;
            comboBoxAccion.ValueMember = "nombre";

        }

        private void botonAceptar_Click(object sender, EventArgs e)
        {
            String accionElegida = comboBoxAccion.SelectedValue.ToString();
            
            this.Hide();
            funcionalidades[accionElegida].ShowDialog();
            this.Close();
          
        }

        private void botonCerrarSesion_Click(object sender, EventArgs e)
        {
            UsuarioSesion.Usuario.id = 0;
            UsuarioSesion.Usuario.nombre = null;
            UsuarioSesion.Usuario.rol = null;

            this.Hide();
            new Login.LoginForm().ShowDialog();
            this.Close();

        }
    }
}
