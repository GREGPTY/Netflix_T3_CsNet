using Netflix_T3.C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Netflix_T3.C_;
using Netflix_T3.C_.PyToC_;
using System.Runtime.ConstrainedExecution;

namespace Netflix_T3.html.ControlAccess
{
    public partial class CA_AccountLoginSignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            verificaciones v = new verificaciones();
            if (!IsPostBack)
            {
                // Verifica si el usuario está autenticado usando FormsAuthentication                
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("CA_Login.aspx");
                }
                else if (Session["UserName"] != null) //Pues ya hizo login, ahora verificamos si el rango es el adecuado
                {
                    if (v.ItsHighRank(Session["UserName"].ToString()))
                    {
                        btn_clean.Visible = false;
                        btn_nuevo1.Visible = false;
                        Button_Enviar.Visible = false;
                        btn_login_create.Visible = false;
                        btn_signup_create.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("Home_ControlAccess.aspx");
                    }
                }
                else
                {
                    // Solo Por si acaso
                    btn_clean.Visible = false;
                    btn_nuevo1.Visible = false;
                    Button_Enviar.Visible = false;
                    btn_login_create.Visible = false;
                    btn_signup_create.Visible = false;
                }
            }
            else
            {
                // Si es un postback, recrear los controles según sea necesario
                if (btn_login_create.Visible)
                {
                    Control_BNT_LogIn();  // Recrear controles de login
                }
                else if (btn_signup_create.Visible)
                {
                    Control_BNT_SignUp();  // Recrear controles de signup
                }
                if (!User.Identity.IsAuthenticated)
                {
                    Response.Redirect("CA_Login.aspx");
                }
                else if (Session["UserName"] != null) //Pues ya hizo login, ahora verificamos si el rango es el adecuado
                {
                    if (v.ItsHighRank(Session["UserName"].ToString()))
                    {
                        btn_clean.Visible = false;
                        btn_nuevo1.Visible = false;
                        Button_Enviar.Visible = false;
                        btn_login_create.Visible = false;
                        btn_signup_create.Visible = false;
                    }
                    else
                    {
                        Response.Redirect("Home_ControlAccess.aspx");
                    }
                }
            }
        }

        public void VerificarLogin()
        {
            verificaciones v = new verificaciones();
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("CA_Login.aspx");
            }
            else if (Session["UserName"] != null) //Pues ya hizo login, ahora verificamos si el rango es el adecuado
            {
                if (v.ItsHighRank(Session["UserName"].ToString()))
                {
                    btn_clean.Visible = false;
                    btn_nuevo1.Visible = false;
                    Button_Enviar.Visible = false;
                    btn_login_create.Visible = false;
                    btn_signup_create.Visible = false;
                }
                else
                {
                    Response.Redirect("Home_ControlAccess.aspx");
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
        private void Control_BNT_SignUp()
        {
            SQLQUERTYGETFROM s = new SQLQUERTYGETFROM();
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
            
            // Rank
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='rank'>Selecciona el Rango</label>" });
            DropDownList dropdown_rank = new DropDownList
            {
                ID = "Dropdown_Rank_ID",
                CssClass = "ddClass"
            };
            string querty_rank = "select Ranks from datos_pueden_ser_ranks;";
            foreach (string data in s.DropDown(querty_rank))
            {
                dropdown_rank.Items.Add(new ListItem(data, data));
            }
            phSignUp.Controls.Add(dropdown_rank);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            // Repeat Password
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='salario'>Cuanto Gana Por Hora</label>" });
            TextBox txtSalario = new TextBox { ID = "Pago_Por_Hora_ID", CssClass = "form-control" };
            phSignUp.Controls.Add(txtSalario);
            phSignUp.Controls.Add(new Literal { Text = "</div>" });

            // Rank
            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='rank'>Selecciona Cuando Se le Paga</label>" });
            DropDownList dropdown_pago = new DropDownList
            {
                ID = "Dropdown_Pago_ID",
                CssClass = "ddClass"
            };
            string querty_pago = "select TipoDePago from datos_pueden_ser_tipodepago;";
            foreach (string data in s.DropDown(querty_pago))
            {
                dropdown_pago.Items.Add(new ListItem(data, data));
            }
            phSignUp.Controls.Add(dropdown_pago);
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

            phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'><label for='Password'>Contraseña</label>" });
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
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("CA_Login.aspx");
            }
            btn_signup.Enabled = false; btn_signup.CssClass = "button-asp-press";
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
                        !string.IsNullOrWhiteSpace(Dropdown_Rank_Selected)&&
                        !string.IsNullOrEmpty(txtPago)&&
                        !string.IsNullOrWhiteSpace(Dropdown_Pago_Por_Hora_Selected)
                        )
                    {
                        //Control_BNT_SignUp();
                        verificaciones v = new verificaciones();
                        if (v.User_NotExist(txtUserName) && v.Email_NotExist(txtMail) && v.MailExistOnList(txtMail) && verificaciones.ItsDoubleWithTwoDecimal(txtPago)) {
                            phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> {sqlquertygetfrom.SQL_CreateUser(txtUserName, txtPassword, txtMail, Dropdown_Rank_Selected,txtPago,Dropdown_Pago_Por_Hora_Selected)} </p> </div>" });
                            btn_signup.Enabled = true; btn_signup.CssClass = "button-asp";
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
                    else {
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

            btn_signup.Enabled = true; btn_signup.CssClass = "button-asp";
            btn_login.Enabled = false; btn_login.CssClass = "button-asp-press";
            btn_signup_create.Visible = false;
            btn_login_create.Visible = true;
            TextBox txtUserName = (TextBox)phSignUp.FindControl("Txt_UserName_ID");
            TextBox txtPassword = (TextBox)phSignUp.FindControl("Txt_Password_ID");
            /*phSignUp.Controls.Clear();
            Control_BNT_LogIn();//*/
            try
            {
                SQLQUERTYGETFROM sqlquertygetfrom = new SQLQUERTYGETFROM();
                if (!string.IsNullOrWhiteSpace(txtUserName.Text) &&
                        !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    //Control_BNT_LogIn(txtUserName.ToString(), txtPassword.ToString());
                    phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> {sqlquertygetfrom.SQL_Login(txtUserName.Text, txtPassword.Text)} </p> </div>" });
                    
                }
                else
                {
                    phSignUp.Controls.Add(new Literal { Text = "<div class='form-group'> <p class=p-message> Ningun dato puede estar vacio </p> </div>" });
                    //btn_login.Enabled = true;
                    //btn_login.CssClass = "button-asp";
                }
            }catch (Exception ex) {
                phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> Error, un dato no es correcto: {ex.Message}</p> </div>" });
                //btn_login.Enabled = true;
                //btn_login.CssClass = "button-asp";
            }
        }
        



    }
}
