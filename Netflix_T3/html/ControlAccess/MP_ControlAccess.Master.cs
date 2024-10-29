using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Netflix_T3.html.ControlAccess
{
    public partial class MP_ControlAccess : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si se está solicitando cerrar sesión (logout)
                if (Request.QueryString["logout"] == "true")
                {
                    // Cerrar sesión
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    Session.Abandon();

                    // Redirigir a la página de login
                    Response.Redirect("CA_Login.aspx");
                }

                // Configuración dinámica del enlace de login/logout
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Si el usuario está autenticado, mostrar "Logout"
                    HyperLink_LoginLogout.Text = "Logout";
                    HyperLink_LoginLogout.NavigateUrl = "~/html/ControlAccess/Home_ControlAccess.aspx?logout=true"; // Agregar el parámetro para logout
                }
                else
                {
                    // Si no está autenticado, mostrar "Log-in"
                    HyperLink_LoginLogout.Text = "Log-in";
                    HyperLink_LoginLogout.NavigateUrl = "~/html/ControlAccess/CA_Login.aspx";  // Redirigir al formulario de login
                }
            }
        }        
    }
}