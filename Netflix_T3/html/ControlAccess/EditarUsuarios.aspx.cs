using Netflix_T3.C_.PyToC_;
using Netflix_T3.C_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;
using System.Web.Security;

namespace Netflix_T3.html.ControlAccess
{
    public partial class EditarUsuarios : System.Web.UI.Page
    {
        readonly SQLQUERTYGETFROM s = new SQLQUERTYGETFROM();
        readonly verificaciones v = new verificaciones();
        readonly transformdata t = new transformdata();
        
        //private List<string>datoslist = new List<string>();//UnDatoDePersona
        private readonly int cantidadDeDatos = 10;
        //private string selectedValue = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            /*
            string Username = null;
            if (!User.Identity.IsAuthenticated || Session["UserName"] == null)
            {
                Response.Redirect("CA_Login.aspx");
                return;
            }
            else
            {
                Username = Session["UserName"]?.ToString() ?? null;
            }
            if ((!v.ItsHighRank(Session["UserName"].ToString())) || (Username == null))
            {
                Response.Redirect("Home_ControlAccess.aspx");
                return;
            }//*/
            string Username = Session["UserName"]?.ToString() ?? "greg"; //Temporal
            if (!IsPostBack)
            {
                //LoadUsernames(Username);
                LoadUsernames(Username);
                ViewState["UsernameSelectedTrueFalse"] = false;
                ViewState["UserToChange"] = "";
                ViewState["UserData"] = new List<string>();
                Literal_Message.Visible = false;                
            }
            else
            {

            }
        }

        private void LoadUsernames(string userName = null)
        {
            List<List<string>> datoslistTotal = new List<List<string>>();
            List<string> Usernames_List = new List<string>();
            datoslistTotal = s.UsersAndMeData(userName);
            string UsernameHTML = "";
            foreach (var item in datoslistTotal)
            {
                Usernames_List.Add(item[0]);
                UsernameHTML+=$"<option value='{item[0]}'/>";                
            }    
            Literal_Usernames_List.Text = UsernameHTML;
            ID_Usernames_List_datalist.Text = "";
            inizialiceTextBox();
            inizialiceBotton();
            inizialiceDropDownList();
            inizialiceChecked();
        }
        private void inizialiceTextBox()
        {
            ID_txt_0.Enabled = false;
            ID_ddl_1.Enabled = false;
            ID_txt_2.Enabled = false;
            ID_txt_3.Enabled = false;
            ID_ddl_4.Enabled = false;
            ID_txt_5.Enabled = false; ID_txt_6.Enabled = false; ID_txt_7.Enabled = false;//Password
            ID_txt_0.Text = "";
            ID_ddl_1.SelectedIndex = 0;
            ID_txt_2.Text = "";
            ID_txt_3.Text = "";
            ID_ddl_4.SelectedIndex = 0;
            ID_txt_5.Text = "";ID_txt_6.Text = "";ID_txt_7.Text = "";
        }
        private void inizialiceBotton()
        {
            btn_edituser.Enabled = false;btn_edituser.CssClass = "button-asp-press";
            btn_edituser_create.Enabled = false;btn_edituser_create.CssClass = "button-asp-press";
        }
        private void inizialiceChecked() {

            ID_chk_0.Enabled = false; ID_chk_1.Enabled = false; ID_chk_2.Enabled = false; ID_chk_3.Enabled = false; ID_chk_4.Enabled = false; ID_chk_5.Enabled = false;
            ID_chk_0.Checked = false; ID_chk_1.Checked = false; ID_chk_2.Checked = false; ID_chk_3.Checked = false; ID_chk_4.Checked = false; ID_chk_5.Checked = false;
        }
        private void inizialiceDropDownList()
        {
            string querty_rank = "select Ranks from datos_pueden_ser_ranks;";
            foreach (string data in s.DropDown(querty_rank))
            {
                ID_ddl_1.Items.Add(new ListItem(data, data));
            }
            string querty_pago = "select TipoDePago from datos_pueden_ser_tipodepago;";
            foreach (string data in s.DropDown(querty_pago))
            {
                ID_ddl_4.Items.Add(new ListItem(data, data));
            }



        }
        private void SetControlVisibilityForHighRankUser()
        {
            
        }



        //Creando los botones paar que se guarden los datos

        
        protected void listUsernames_TextChanged(object sender, EventArgs e)
        {
            inizialiceChecked();
            inizialiceTextBox();
            Literal_Message.Visible = false;
            List<string> datoslist = new List<string>();
            //TextBox txtUsername_Selected = (TextBox)sender;
            if (sender is TextBox txtUsername_Selected)
            {
                List<List<string>> datoslistTotal = new List<List<string>>();
                List<string> Usernames_List = new List<string>();
                //datoslistTotal = s.UsersAndMeData(Session["UserName"]?.ToString()??"");
                datoslistTotal = s.UsersAndMeData("greg");
                foreach (var item in datoslistTotal)
                {
                    Usernames_List.Add(item[0]);
                }
                /*LiteralUsernameShow.Text = selectedValue;
                ID_txt_0.Text = selectedValue;//*/
                for(int i = 0; i< Usernames_List.Count; i += 1) {
                    if (Usernames_List[i].Equals(txtUsername_Selected.Text.ToString(), StringComparison.OrdinalIgnoreCase))
                    {
                        datoslist = t.DataUser(datoslistTotal, txtUsername_Selected.Text.ToString());
                        ViewState["UserData"] = datoslist;
                        ViewState["UsernameSelectedTrueFalse"] = true;
                        LiteralShow_0.Text = !string.IsNullOrEmpty(datoslist[0])? datoslist[0]: "No Hay Datos";
                        ID_txt_0.Text = !string.IsNullOrEmpty(datoslist[0]) ? datoslist[0] : "No Hay Datos";
                        LiteralShow_1.Text = !string.IsNullOrEmpty(datoslist[1]) ? datoslist[1] : "No Hay Datos";
                        if (datoslist[1] != "ghost") {
                            ID_ddl_1.Text = !string.IsNullOrEmpty(datoslist[1]) ? datoslist[1] : "No Hay Datos";
                            ID_chk_1.Enabled = true;
                        }
                        else
                        {
                            ID_chk_1.Enabled= false;
                        }
                        LiteralShow_2.Text = !string.IsNullOrEmpty(datoslist[2]) ? datoslist[2] : "No Hay Datos";
                        ID_txt_2.Text = !string.IsNullOrEmpty(datoslist[2]) ? datoslist[2] : "No Hay Datos";
                        LiteralShow_3.Text = !string.IsNullOrEmpty(datoslist[3]) ? datoslist[3] : "No Hay Datos";
                        ID_txt_3.Text = !string.IsNullOrEmpty(datoslist[3]) ? datoslist[3] : "No Hay Datos";
                        LiteralShow_4.Text = !string.IsNullOrEmpty(datoslist[4]) ? datoslist[4] : "No Hay Datos";
                        ID_ddl_4.Text = !string.IsNullOrEmpty(datoslist[4]) ? datoslist[4] : "No Hay Datos";
                        ID_txt_5.Text = ""; ID_txt_6.Text = ""; ID_txt_7.Text = "";
                        ViewState["UserToChange"] = txtUsername_Selected.Text.ToString();
                        if (((string)Session["UserName"] == (string)ViewState["UserToChange"]) && v.ItsSuperHighRank((string)Session["UserName"]))
                        { ID_chk_1.Enabled = false; ID_chk_3.Enabled = true; ID_chk_4.Enabled = true; ID_chk_0.Enabled = true; ID_chk_2.Enabled = true; ID_chk_5.Enabled = true; }
                        else if ((string)Session["UserName"] == (string)ViewState["UserToChange"])
                        { ID_chk_1.Enabled = false; ID_chk_3.Enabled = false; ID_chk_4.Enabled = false; ID_chk_0.Enabled = true; ID_chk_2.Enabled = true; ID_chk_5.Enabled = true; }
                        else if ((v.ItsSuperHighRank(datoslist[0]) && !v.ItsSuperHighRank((string)Session["UserName"])) || !s.UserSessionIsBiggerThanUserEdit((string)Session["UserName"], datoslist[0]))
                        { ID_chk_1.Enabled = false; ID_chk_3.Enabled = false; ID_chk_4.Enabled = false; ID_chk_0.Enabled = false; ID_chk_2.Enabled = false; ID_chk_5.Enabled = false; }
                        else
                        { ID_chk_1.Enabled = true; ID_chk_3.Enabled = true; ID_chk_4.Enabled = true; ID_chk_0.Enabled = true; ID_chk_2.Enabled = true; ID_chk_5.Enabled = true; }
                        break;
                    }
                    else
                    {
                        datoslist = t.DataUser(datoslistTotal, txtUsername_Selected.Text.ToString());
                        ViewState["UsernameSelectedTrueFalse"] = false;
                    }
                }
                //ID_txt_0.Text = datoslist[0];


                /*
                if (datoslist.Count>0)
                {
                    for (int i = 0; i < datoslist.Count; i++)
                    {
                        TextBox txtBox = (TextBox)FindControl("ID_txt_"+i);
                        if (txtBox != null)
                        {
                            txtBox.Text = datoslist[i];
                        }
                    }
                }//*/
            }
        }

        private void UserDataForTextBox(List<string> DatosList = null)
        {
            
        }

        protected void Chk_Changed(object sender, EventArgs e)
        {
            Literal_Message.Visible = false;
            ID_txt_0.Enabled = ID_chk_0.Checked? true: false;
            ID_ddl_1.Enabled = ID_chk_1.Checked? true: false;
            ID_txt_2.Enabled = ID_chk_2.Checked? true: false;
            ID_txt_3.Enabled = ID_chk_3.Checked? true: false;
            ID_ddl_4.Enabled = ID_chk_4.Checked? true: false;            
            if (ID_chk_5.Checked)
            {ID_txt_5.Enabled = true;ID_txt_6.Enabled = true;ID_txt_7.Enabled = true;}
            else
            { ID_txt_5.Enabled = false;ID_txt_6.Enabled = false;ID_txt_7.Enabled = false;}

            if ((ID_chk_5.Checked || ID_chk_4.Checked || ID_chk_3.Checked || ID_chk_2.Checked || ID_chk_1.Checked || ID_chk_0.Checked) && (bool)ViewState["UsernameSelectedTrueFalse"] == true)
            {
                btn_edituser_create.Enabled = true;btn_edituser_create.CssClass = "button-asp";
            }
            else
            {
                btn_edituser_create.Enabled = false; btn_edituser_create.CssClass = "button-asp-press";
            }
        }
        
        protected void BTN_EditUser(object sender, EventArgs e)
        {
            btn_edituser.Enabled = false; btn_edituser.CssClass = "button-asp-press";
            //btn_login.Enabled = true; //btn_login.CssClass = "button-asp";                       

        }
        
        protected void BTN_EditUser_Create(object sender, EventArgs e)
        {
            string querty = "exec SP_EDICION_GENERAL @User_ControlGreg_Old, @User_ControlGreg_New, @Password_Control_New, @Password_Confirmation, " +
                "@Rank_New,@TipoDePago_New, @SalarioPorHora_New, @Email_New, @EdicionCompletada OUTPUT";
            /*
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("CA_Login.aspx");
            }//*/
            //para usar el usar el output
            /*SqlParameter outputParam = new SqlParameter("@EdicionCompletada", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);
            cmd.ExecuteNonQuery();
            bool edicionCompletada = (bool)cmd.Parameters["@EdicionCompletada"].Value;
            //*/

            //string UserSelected = ((TextBox)FindControl("ID_Usernames_List_datalist")).Text;
            List<string> datalist = new List<string>((List<string>)ViewState["UserData"]);
            string txtUserName = ID_chk_0.Checked?((TextBox)ID_Contenedor.FindControl("ID_txt_0")).Text.ToString(): datalist[0];
            string txtMail = ID_chk_2.Checked ? ((TextBox)ID_Contenedor.FindControl("ID_txt_2")).Text.ToString(): datalist[2];            
            string Dropdown_Rank_Selected = ID_chk_1.Checked ? ((DropDownList)ID_Contenedor.FindControl("ID_ddl_1")).SelectedValue.ToString(): datalist[1];
            string txtPago = ID_chk_3.Checked ? ((TextBox)ID_Contenedor.FindControl("ID_txt_3")).Text.ToString(): datalist[3];
            string Dropdown_Pago_Por_Hora_Selected = ID_chk_4.Checked ? ((DropDownList)ID_Contenedor.FindControl("ID_ddl_4")).SelectedValue.ToString(): datalist[4];
            //string txtPassword = ((TextBox)FindControl("Password_ID")).Text;
            //string txtRepeatPassword = ((TextBox)FindControl("Repeat_Password_ID")).Text;

            try
            {
                Literal_Message.Visible = true;
                if (((ID_chk_4.Checked || ID_chk_3.Checked || ID_chk_2.Checked || ID_chk_1.Checked || ID_chk_0.Checked) && (bool)ViewState["UsernameSelectedTrueFalse"] == true)&&(
                    (!string.IsNullOrEmpty(txtUserName) && !string.IsNullOrEmpty(txtMail) && !string.IsNullOrEmpty(Dropdown_Rank_Selected) &&
                      !string.IsNullOrEmpty(txtPago) && !string.IsNullOrEmpty(Dropdown_Pago_Por_Hora_Selected) //&& !string.IsNullOrEmpty(UserSelected) 
                      && !string.IsNullOrEmpty((string)ViewState["UserToChange"]))))
                {                    
                        //Literal_Message.Text = $"<h1>Message: [Exito, Usuario {(string)ViewState["UserToChange"]} Editado]</h1>";
                    Literal_Message.Text = $"<h1>Message: [Éxito, Usuario {(string)ViewState["UserToChange"]} Editado]</h1>" +
                                            $"<p>Nombre de usuario: {txtUserName}</p>" +
                                            $"<p>Email: {txtMail}</p>" +
                                            $"<p>Rango seleccionado: {Dropdown_Rank_Selected}</p>" +
                                            $"<p>Pago: {txtPago}</p>" +
                                            $"<p>Pago por hora seleccionado: {Dropdown_Pago_Por_Hora_Selected}</p>";

                    //Primero Hay que Realizar los Cambios
                    if ((string)Session["UserName"] == (string)ViewState["UserToChange"])
                    {
                        FormsAuthentication.SignOut();
                        Session.Clear();
                        Session.Abandon();
                        Response.Redirect("CA_Login.aspx");
                    }
                }
                else
                {
                    Literal_Message.Text = "<h1>Message: [Error, Algun Check o Dato Esta vacio]</h1>";
                }
            }
            catch (Exception ex)
            {
                //phSignUp.Controls.Add(new Literal { Text = $"<div class='form-group'> <p class=p-message> Error, un dato no es correcto {ex.Message}</p> </div>" });
                
            }
            //*/
        }        
        
    }
}
