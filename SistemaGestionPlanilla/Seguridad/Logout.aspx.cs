using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionPlanilla.Seguridad
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CodUsuarioInterno"] != null) { Session["CodUsuarioInterno"] = null; }
            if (Session["UsuarioInterno"] != null) { Session["UsuarioInterno"] = null; }
            if (Session["NombreCompletoUsuarioInterno"] != null) { Session["NombreCompletoUsuarioInterno"] = null; }
            if (Session["CodPerfilAccesoUsuarioInterno"] != null) { Session["CodPerfilAccesoUsuarioInterno"] = null; }
            Response.Redirect("http://3.213.210.107/Dashboard.aspx");
        }
    }
}