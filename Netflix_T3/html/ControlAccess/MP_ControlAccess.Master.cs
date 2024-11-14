using Netflix_T3.C_;
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
                
                if (Request.QueryString["logout"] == "true")// Verificar si se está solicitando cerrar sesión (logout)
                {
                    // Cerrar sesión
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    Session.Abandon();

                    // Redirigir a la página de login
                    Response.Redirect("CA_Login.aspx");
                }
                Literal_Menu_AC.Controls.Clear();
                // Configuración dinámica del enlace de login/logout
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Si el usuario está autenticado, mostrar "Logout"
                    HyperLink_LoginLogout.Text = $"Logout [{Session["UserName"]}]";
                    HyperLink_LoginLogout.NavigateUrl = "~/html/ControlAccess/Home_ControlAccess.aspx?logout=true"; // Agregar el parámetro para logout
                    verificaciones v = new verificaciones();
                    Literal_Menu_AC.Text = MenuLoged(v.ItsHighRank(Session["UserName"].ToString())).ToString();
                }
                else
                {
                    // Si no está autenticado, mostrar "Log-in"
                    HyperLink_LoginLogout.Text = "Log-in";
                    HyperLink_LoginLogout.NavigateUrl = "~/html/ControlAccess/CA_Login.aspx";  // Redirigir al formulario de login
                    Literal_Menu_AC.Text = MenuNotLoged().ToString();
                }
            }
        }
        private string MenuLoged(bool highrank = false)
        {
            string answer = null;
            if (highrank == true) {
                answer = "<li> " +
                    "<a href=\"#\">Programs</a> " +
                    "<ul class=\"dropdown\">       " +
                            "<li><a href=\"../PyToCs.aspx\">Py to C#</a></li>" +
                            "<li><a href=\"Home_ControlAccess.aspx\">Control-Salary</a></li>" +
                    "</ul></li>" +
                    "<li> " +
                    "<a href=\"#\">Users</a> " +
                    "<ul class=\"dropdown\">" +
                            "<li><a href=\"CA_AccountLoginSignUp.aspx\">Create User</a></li>" +
                            "<li><a href=\"EditarUsuarios.aspx\">Edit User</a></li>" +
                    "</ul></li>" +
                    "<li><a href=\"Consulting.aspx\">Consulting</a></li>";// +
                    //"</li>";
            }
            else
            {
                answer = "<li> " +
                    "<a href=\"#\">Programs</a> " +
                    "<ul class=\"dropdown\">       " +
                            "<li><a href=\"../PyToCs.aspx\">Py to C#</a></li>" +
                            "<li><a href=\"Home_ControlAccess.aspx\">Control-Salary</a></li>" +
                    "</ul>" +
                    "<li><a href=\"EditarUsuarios.aspx\">Edit User</a></li>" +
                    "<li><a href='Consulting.aspx'>Consulting</a></li>" +
                    "</li>";
            }
            return answer;
        }
        private string MenuNotLoged()
        {
            string answer = null;
            answer = "<li> " +
                "<a href=\"#\">Programs</a> " +
                "<ul class=\"dropdown\">       " +
                    "<li>" +
                        "<a href=\"../PyToCs.aspx\">Py to C#</a></li> <li>" +
                        "<a href=\"Home_ControlAccess.aspx\">Control-Salary</a>" +
                    "</li>" +
                "</ul>" +
                "</li>";
            return answer;
        }
    }
}