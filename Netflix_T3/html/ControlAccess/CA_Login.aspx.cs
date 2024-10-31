using Netflix_T3.C_.PyToC_;
using Netflix_T3.C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Netflix_T3.html.ControlAccess
{
    public partial class CA_Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserName"] != null)
            {
                Response.Redirect("Home_ControlAccess.aspx");
            }
            else
            {
                if (IsPostBack) //si la pagina esta recargando
                {
                    Control_BNT_LogIn(); // recrear controles de login
                }
                else
                {
                    btn_clean.Visible = false;
                    btn_nuevo1.Visible = false;
                    Button_Enviar.Visible = false;
                    btn_login_create.Visible = false;
                    btn_signup_create.Visible = false;
                    btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
                    btn_signup_create.Visible = false;
                    btn_login_create.Visible = true;

                    phSignUp.Controls.Clear();
                    Control_BNT_LogIn();
                }
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
        
        
        private void Control_BNT_LogIn(string Usuario = null, string Contrasena = null)
        {
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='UserN'>Nombre De Usuario</label>" });
            TextBox txtUserName = new TextBox { ID = "Txt_UserName_ID", CssClass = "form-control", Text = Usuario };
            phSignUp.Controls.Add(txtUserName);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='Password'>Contraseña</label>" });
            TextBox txtPassword = new TextBox { ID = "Txt_Password_ID", CssClass = "form-control", TextMode = TextBoxMode.Password, Text = Contrasena };
            phSignUp.Controls.Add(txtPassword);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });
        }

        protected void BTN_Login(object sender, EventArgs e)
        {            
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
            btn_login.Enabled = true; btn_login.CssClass = "button-asp";
            btn_signup_create.Visible = true;
            btn_login_create.Visible = false;

            /*TextBox txtUserName = (TextBox)phSignUp.FindControl("user_name");
            TextBox txtMail = (TextBox)phSignUp.FindControl("Correo_ID");
            TextBox txtPassword = (TextBox)phSignUp.FindControl("Password_ID");
            TextBox txtRepeatPassword = (TextBox)phSignUp.FindControl("Repeat_Password_ID");
            DropDownList Dropdown_Rank_Selected = (DropDownList)phSignUp.FindControl("Dropdown_Rank_ID");
            TextBox txtPago = (TextBox)phSignUp.FindControl("Pago_Por_Hora_ID");
            DropDownList Dropdown_Pago_Por_Hora_Selected = (DropDownList)phSignUp.FindControl("Dropdown_Pago_ID");//*/
            string txtUserName = ((TextBox)phSignUp.FindControl("user_name")).Text;
            string txtMail = ((TextBox)phSignUp.FindControl("Correo_ID")).Text;
            string txtPassword = ((TextBox)phSignUp.FindControl("Password_ID")).Text;
            string txtRepeatPassword = ((TextBox)phSignUp.FindControl("Repeat_Password_ID")).Text;
            string Dropdown_Rank_Selected = ((DropDownList)phSignUp.FindControl("Dropdown_Rank_ID")).SelectedValue;
            string txtPago = ((TextBox)phSignUp.FindControl("Pago_Por_Hora_ID")).Text;
            string Dropdown_Pago_Por_Hora_Selected = ((DropDownList)phSignUp.FindControl("Dropdown_Pago_ID")).SelectedValue;



            try
            {
                SQLQUERTYGETFROM sqlquertygetfrom = new SQLQUERTYGETFROM();

                //TextBox txtDate = (TextBox)phSignUp.FindControl("Date_ID");
                if (txtPassword == txtRepeatPassword)
                {
                    if (!string.IsNullOrWhiteSpace(txtUserName) &&
                        !string.IsNullOrWhiteSpace(txtMail) &&
                        !string.IsNullOrWhiteSpace(txtPassword) &&
                        !string.IsNullOrWhiteSpace(txtRepeatPassword) &&
                        !string.IsNullOrWhiteSpace(Dropdown_Rank_Selected) &&
                        !string.IsNullOrEmpty(txtPago) &&
                        !string.IsNullOrWhiteSpace(Dropdown_Pago_Por_Hora_Selected)
                        )
                    {
                        //Control_BNT_SignUp();
                        verificaciones v = new verificaciones();
                        if (v.User_NotExist(txtUserName) && v.Email_NotExist(txtMail) && v.MailExistOnList(txtMail) && verificaciones.ItsDoubleWithTwoDecimal(txtPago))
                        {
                            phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> {sqlquertygetfrom.SQL_CreateUser(txtUserName, txtPassword, txtMail, Dropdown_Rank_Selected, txtPago, Dropdown_Pago_Por_Hora_Selected)} </p> </div>" });
                            
                            btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
                            btn_signup_create.Visible = false;
                            btn_login_create.Visible = true;

                            phSignUp.Controls.Clear();
                            Control_BNT_LogIn();//*/
                        }
                        else
                        {
                            phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> {sqlquertygetfrom.SQL_CreateUser(txtUserName, txtPassword, txtMail, "yes")} </p> </div>" });
                            //btn_signup.Enabled = true;
                            //btn_signup.CssClass = "button-asp";
                        }
                    }
                    else
                    {
                        phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <p class=p-message> Ningun dato puede estar vacio </p> </div>" });
                        //btn_signup.Enabled = true;
                        //btn_signup.CssClass = "button-asp";
                    }
                }
                else
                {
                    phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <p class=p-message> Las contraseñas no concuerdan </p> </div>" });
                    //btn_signup.Enabled = true;
                    //btn_signup.CssClass = "button-asp";
                }

            }
            catch (Exception ex)
            {
                phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> Error, un dato no es correcto {ex.Message}</p> </div>" });
                //btn_signup.Enabled = true;
                //btn_signup.CssClass = "button-asp";
            }
        }
        protected void BTN_Login_Create(object sender, EventArgs e)
        {

            
            btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
            btn_signup_create.Visible = false;
            btn_login_create.Visible = true;
            TextBox txtUserName = (TextBox)phSignUp.FindControl("Txt_UserName_ID");
            TextBox txtPassword = (TextBox)phSignUp.FindControl("Txt_Password_ID");
            /*phSignUp.Controls.Clear();
            Control_BNT_LogIn();//*/
            try
            {
                verificaciones v = new verificaciones();
                SQLQUERTYGETFROM sqlquertygetfrom = new SQLQUERTYGETFROM();
                if (!string.IsNullOrWhiteSpace(txtUserName.Text) &&
                        !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    //Control_BNT_LogIn(txtUserName.ToString(), txtPassword.ToString());
                    string[] LoginResult = sqlquertygetfrom.SQL_Login(txtUserName.Text, txtPassword.Text);
                    if (LoginResult[1].ToLower()=="si".ToLower()) {
                        FormsAuthentication.SetAuthCookie(txtUserName.Text, false);
                        Session["UserName"] = v.NoSpaceSrting(v.Lower_Username(txtUserName.Text));
                        Response.Redirect("Home_ControlAccess.aspx");
                    }
                    else { 
                        phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> {LoginResult[0]} </p> </div>" });
                    }
                    
                }
                else
                {
                    phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <p class=p-message> Ningun dato puede estar vacio </p> </div>" });
                    //btn_login.Enabled = true;
                    //btn_login.CssClass = "button-asp";
                }
            }
            catch (Exception ex)
            {
                phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> Error, un dato no es correcto: {ex.Message} </p> </div>" });
                //btn_login.Enabled = true;
                //btn_login.CssClass = "button-asp";
            }
        }




    }
}

/*
 Solo para el logout
protected void btnLogout_Click(object sender, EventArgs e)
{
    Session.Clear();  // Limpiar todos los valores de la sesión
    Session.Abandon();  // Terminar la sesión
    Response.Redirect("Login.aspx");  // Redirigir al login
}
 */