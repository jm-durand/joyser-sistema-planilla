using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SistemaGestionPlanilla
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Session["CodUsuarioInterno"] = "1";
                //Session["UsuarioInterno"] = "jdurand";
                //Session["NombreCompletoUsuarioInterno"] = "Jose mari";
                //Session["CodPerfilAccesoUsuarioInterno"] = "1";

                if (Session["CodUsuarioInterno"] != null)
                {
                    NombreCompletoUsuarioInterno.Text = Session["NombreCompletoUsuarioInterno"].ToString();
                }
                else
                {
                    Response.Redirect(ConfigurationManager.AppSettings["AssetsUrl"] + "/Seguridad/Logout.aspx");
                }
            }
        }
    }
}