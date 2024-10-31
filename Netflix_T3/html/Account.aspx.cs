using Netflix_T3.C_;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Netflix_T3.html
{
    public partial class Account1 : System.Web.UI.Page
    {

        //readonly SqlConnection sql_cc = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL_connection"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Control_BNT_SignUp();
                Control_BNT_LogIn();
                //SQL_conection pql_dat = new SQL_conection();

            }
            else
            {
                btn_clean.Visible = false;
                btn_nuevo1.Visible = false;
                Button_Enviar.Visible = false;
                btn_login_create.Visible = false;
                btn_signup_create.Visible = false;
            }
        }

        protected void Btn_nuevo(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            int counter = 0;
            string message = "No te duermas intento: ";
            do
            {
                sb.Append("<label>" + message + " [" + counter + "] " + "</label>");

                counter += 1;
            } while (counter <= 10);
            LiteralHTML.Text = sb.ToString();
            //message_id_rory.Text = sb.ToString();

        }
        protected void Btn_Read(object sender, EventArgs e)
        {

        }
        protected void Enviar(object sender, EventArgs e)
        {
            SQL_conection jeje = new SQL_conection();
            jeje.OpenConnection();
            ID_Gridview_admin.DataSource = jeje.Cargar_Tabla_Usuario();
            ID_Gridview_admin.DataBind();
            jeje.CloseConnection();
        }
        protected void Btn_Clean(object sender, EventArgs e)
        {
            ID_Gridview_admin.DataSource = null;
            ID_Gridview_admin.DataBind();
        }
        //Creando los botones paar que se guarden los datos
        private void Control_BNT_SignUp()
        {
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><h1>Crear Usuario</h1></div>" });

            //User
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='user_name'> Nombre de Usuario</label>" });
            TextBox txtUserName = new TextBox { ID = "user_name", CssClass = "form-control" };
            phSignUp.Controls.Add(txtUserName);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            // Mail
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='email'>Correo</label>" });
            TextBox txtEmail = new TextBox { ID = "Correo_ID", CssClass = "form-control" };
            phSignUp.Controls.Add(txtEmail);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            // Password
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='pass-w'>Contraseña</label>" });
            TextBox txtPassword = new TextBox { ID = "Password_ID", TextMode = TextBoxMode.Password, CssClass = "form-control" };
            phSignUp.Controls.Add(txtPassword);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            // Repeat Password
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='pass-w-repeat'>Repita la Contraseña</label>" });
            TextBox txtRepeatPassword = new TextBox { ID = "Repeat_Password_ID", TextMode = TextBoxMode.Password, CssClass = "form-control" };
            phSignUp.Controls.Add(txtRepeatPassword);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            /*// Born Date
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='date-n'>Fecha de nacimiento</label>" });
            TextBox txtDate = new TextBox { ID = "Date_ID", TextMode = TextBoxMode.Date, CssClass = "form-control" };
            phSignUp.Controls.Add(txtDate);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });*/
        }
        protected void BTN_SignUp(object sender, EventArgs e)
        {
            btn_signup.Enabled = false; btn_signup.CssClass = "button-asp-press";
            btn_login.Enabled = true; btn_login.CssClass = "button-asp";
            btn_signup_create.Visible = true;
            btn_login_create.Visible = false;

            phSignUp.Controls.Clear();
            Control_BNT_SignUp();
            /*StringBuilder signup = new StringBuilder();
            signup.Append("<div class=\"form-group\"><h1>Crear Usuario</h1></div><div class=\"form-group\"><label for=\"user_name\"> Nombre de Usuario</label>" +
                            "<asp:TextBox runat=\"server\" ID=\"user_name\"> </asp:TextBox></div>" +
                            "<div class=\"form-group\"><label for=\"email\">Correo</label><asp:TextBox runat=\"server\" ID=\"Correo_ID\"> </asp:TextBox></div>" +
                            "<div class=\"form-group\"><label for=\"pass-w\">Contraseña</label><asp:TextBox runat=\"server\" ID=\"Password_ID\"> </asp:TextBox></div>" +
                            "<div class=\"form-group\"><label for=\"pass-w-repeat\">Repita la Contraseña</label><asp:TextBox runat=\"server\" ID=\"Repeat_Password_ID\"></asp:TextBox></div>" +
                            "<div class=\"form-group\"><label for=\"date-n\">Fecha de nacimiento</label><asp:TextBox runat=\"server\" ID=\"Date_ID\" TextMode=\"Date\"> </asp:TextBox></div>");
            //Literal_Sign_Log.Text = signup.ToString();*/


            //Button Iniciar Sesion (Enviar)
            /*phSignUp.Controls.Add(new Literal{ Text= "<div class='form-group'> <div class='div-button-horizontal'>" });
            Button btn_Send_SignUp = new Button {Text = "Crear Usuario", ID= "Send_SignUp_ID", CssClass = "button-asp" };
            phSignUp.Controls.Add(btn_Send_SignUp);
            phSignUp.Controls.Add(new Literal { Text = "</div></div>" });*/
            //Control_BNT_SignUp();

        }
        private void Control_BNT_LogIn(string Usuario = null, string Contrasena = null)
        {
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='UserN'>Nombre De Usuario</label>" });
            TextBox txtUserName = new TextBox { ID = "Txt_UserName_ID", CssClass = "form-control", Text = Usuario };
            phSignUp.Controls.Add(txtUserName);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'<label for='Password'>Contraseña</label>" });
            TextBox txtPassword = new TextBox { ID = "Txt_Password_ID", CssClass = "form-control", TextMode = TextBoxMode.Password, Text = Contrasena };
            phSignUp.Controls.Add(txtPassword);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });
        }

        protected void BTN_Login(object sender, EventArgs e)
        {
            btn_signup.Enabled = true; btn_signup.CssClass = "button-asp";
            btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
            btn_signup_create.Visible = false;
            btn_login_create.Visible = true;

            phSignUp.Controls.Clear();
            Control_BNT_LogIn();

            //Button LogIn
            /*phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <div class='div-button-horizontal'>" });
            Button btn_Send_LogIn = new Button { Text = "Iniciar Sesion", ID = "Send_SignUp_ID", CssClass = "button-asp" };
            phSignUp.Controls.Add(btn_Send_LogIn);
            phSignUp.Controls.Add(new Literal { Text = "</div></div>" });*/
            //Control_BNT_LogIn();
        }

        protected void BTN_SignUp_Create(object sender, EventArgs e)
        {
            btn_signup.Enabled = false; btn_signup.CssClass = "button-asp-press";
            btn_login.Enabled = true; btn_login.CssClass = "button-asp";
            btn_signup_create.Visible = true;
            btn_login_create.Visible = false;


            phSignUp.Controls.Clear();
            try
            {
                TextBox txtUserName = (TextBox)phSignUp.FindControl("user_name");
                TextBox txtMail = (TextBox)phSignUp.FindControl("Correo_ID");
                TextBox txtPassword = (TextBox)phSignUp.FindControl("Password_ID");
                TextBox txtRepeatPassword = (TextBox)phSignUp.FindControl("Repeat_Password_ID");
                //TextBox txtDate = (TextBox)phSignUp.FindControl("Date_ID");                
                if (txtUserName != null && txtMail != null && txtPassword != null && txtRepeatPassword != null)
                {
                    Control_BNT_SignUp();
                }
                else
                {
                    phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <h1 class=p-separate> Ningun dato puede estar vacio </h1> </div>" });
                    btn_signup.Enabled = true;
                    btn_signup.CssClass = "button-asp";
                }

            }
            catch (Exception ex)
            {
                phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <h1 class=p-separate> Error, un dato no es correcto {ex.Message}</h1> </div>" });
                btn_signup.Enabled = true;
                btn_signup.CssClass = "button-asp";
            }
        }
        protected void BTN_Login_Create(object sender, EventArgs e)
        {
            btn_signup.Enabled = true; btn_signup.CssClass = "button-asp";
            btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
            btn_signup_create.Visible = false;
            btn_login_create.Visible = true;

            phSignUp.Controls.Clear();
            try
            {
                TextBox txtUserName = (TextBox)phSignUp.FindControl("Txt_UserName_ID");
                TextBox txtPassword = (TextBox)phSignUp.FindControl("Txt_Password_ID");
                if ((txtUserName != null) && txtPassword != null)
                {
                    Control_BNT_LogIn(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <h1 class=p-separate> Ningun dato puede estar vacio </h1> </div>" });
                    btn_login.Enabled = true;
                    btn_login.CssClass = "button-asp";
                }
            }
            catch (Exception ex)
            {
                phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <h1 class=p-separate> Error, un dato no es correcto {ex.Message}</h1> </div>" });
                btn_login.Enabled = true;
                btn_login.CssClass = "button-asp";

            }
        }



    }
}