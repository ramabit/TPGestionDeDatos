using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FrbaCommerce
{
    public partial class MenuPrincipal : Form
    {

        Dictionary<String, Form> funcionalidades = new Dictionary<String, Form>();
            
        public MenuPrincipal()
        {
            InitializeComponent();

            funcionalidades.Add("Comprar / Ofertar", new Comprar_Ofertar.ComprarOfertar());
            funcionalidades.Add("Generar publicacion", new Generar_Publicacion.GenerarPublicacion());
            funcionalidades.Add("Editar publicacion", new Editar_Publicacion.EditarPublicacion());
            funcionalidades.Add("Calificar vendedor", new Calificar_Vendedor.Calificar());
            funcionalidades.Add("Preguntar", new Gestion_de_Preguntas.Preguntar());
            funcionalidades.Add("Responder preguntas", new Gestion_de_Preguntas.ResponderPreguntas());
            funcionalidades.Add("Gestionar roles", new ABM_Rol.RolForm());
            funcionalidades.Add("Gestionar usuarios", null);
            funcionalidades.Add("Generar factura", new Facturar_Publicaciones.Facturar());
            funcionalidades.Add("Crear empresa", new ABM_Empresa.AgregarEmpresa());
            funcionalidades.Add("Editar empresa", new ABM_Empresa.FiltroEmpresa());
            funcionalidades.Add("Crear cliente", new ABM_Cliente.AgregarCliente("clienteCreadoPorAdmin", "A"));
            funcionalidades.Add("Editar cliente", new ABM_Cliente.FiltroCliente());
            funcionalidades.Add("Crear visibilidad", new ABM_Visibilidad.AgregarVisibilidad());
            funcionalidades.Add("Editar visibilidad", new ABM_Visibilidad.FiltroVisibilidad());
            funcionalidades.Add("Crear rubro", new ABM_Rubro.AgregarRubro());
            //funcionalidades.Add("Editar rubro", new ABM_Rubro.FiltroRubros());
            funcionalidades.Add("Obtener estadisticas", new Listado_Estadistico.Estadisticas());
            funcionalidades.Add("Ver historial", new Historial_Cliente.Historial());
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            
        }
    }
}
