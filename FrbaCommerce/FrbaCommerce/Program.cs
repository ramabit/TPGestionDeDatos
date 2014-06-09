using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FrbaCommerce
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login.LoginForm());
            //Application.Run(new ABM_Cliente.AgregarCliente("USER101", "hola"));
            //Application.Run(new ABM_Empresa.AgregarEmpresa("USER102", "hola"));
            //Application.Run(new ABM_Visibilidad.AgregarVisibilidad());
            //Application.Run(new Generar_Publicacion.GenerarPublicacion());
        }
    }
}
