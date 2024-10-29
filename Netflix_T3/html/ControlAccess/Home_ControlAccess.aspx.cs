using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Netflix_T3.html.ControlAccess
{
    public partial class Home_ControlAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                //Por si necesito un mensaje personalizado                
            }
            else
            {
                //Todos los que no hagan login lo veran              
            }
        }
    }
}