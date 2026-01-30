using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio;

namespace SistemaGestionPlanilla
{
    public partial class Inicio : System.Web.UI.Page
    {
        ReporteNegocio procesoReportes = new ReporteNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            DataTable dt = procesoReportes.ObtenerDatosGenerales();

            lblCantidadTrabajadores.Text = dt.Rows[0][0] + " nuevos trabajadores.";
            lblCantidadUsuarios.Text = dt.Rows[0][1] + " usuario(s) con acceso al sistema.";
            lblCantidadPerfilesPlanilla.Text = dt.Rows[0][2] + " perfiles planilla.";
            lblCantidadPlanilla.Text = dt.Rows[0][3] + " planillas generadas en el día.";
        }
    }
}