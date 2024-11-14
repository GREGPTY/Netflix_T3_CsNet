﻿using Netflix_T3.C_;
using Netflix_T3.C_.PyToC_;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Netflix_T3.html.ControlAccess
{
    public partial class Consulting : System.Web.UI.Page
    {
        verificaciones v = new verificaciones();
        SQLQUERTYGETFROM s = new SQLQUERTYGETFROM();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string Username = "greg";
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
            if (!IsPostBack)
            {
                LoadTextData(Username);
                Load_DGV(Username);
                Load_Fields_Select();
                ID_SeletedMode.Text = "None";
            }//*/
        }

        private void LoadTextData(string Username = null)
        {
            List<string> datalist = new List<string>(s.MyUserData(Username));
            ID_txt_0.Text = datalist.Count > 0 ? datalist[0].ToString() : "No Data";
            ID_txt_1.Text = datalist.Count > 1 ? datalist[1].ToString() : "No Data";
            ID_txt_1_1.Text = datalist.Count > 1 ? datalist[1].ToString() : "No Data";
            ID_txt_2.Text = datalist.Count > 2 ? datalist[2].ToString() : "No Data";
            ID_txt_3.Text = datalist.Count > 3 ? datalist[3].ToString() : "No Data";
            ID_txt_4.Text = datalist.Count > 4 ? datalist[4].ToString() : "No Data";
            ID_txt_5.Text = datalist.Count > 5 ? datalist[5].ToString() : "No Data";
            ID_txt_6.Text = datalist.Count > 6 ? "$"+datalist[6].ToString() : "No Data";
            if (datalist.Count > 0)
            {
                Literal_UserLogo.Text = (datalist[1] == "greg")
                                        ? "<img src='../../img/CoolCat_Greg.png' alt='Greg' />"
                                        : "<img src='../../img/userlogo.png' alt='User Logo' />";
            }
        }
        private void Load_Fields_Select()
        {
            string[] ID_DD_Filter_0_Data = { "Day", "Week" };
            int[] ID_DD_Filter_1_Data = {1,2,5,10,20};
            ID_DD_Filter_0.ClearSelection(); foreach(string data in ID_DD_Filter_0_Data){ ID_DD_Filter_0.Items.Add(data); }
            ID_DD_Filter_0.SelectedIndex = 0; 
            ID_DD_Filter_1.ClearSelection(); foreach (int data in ID_DD_Filter_1_Data) { ID_DD_Filter_1.Items.Add(data.ToString()); }
            ID_DD_Filter_1.SelectedIndex = 0;
        }
        private void Load_DGV(string Username = null)
        {
            ID_DGV_0.DataSource = Load_DGV_Weekly(Username,1); ID_DGV_0.DataBind();
            DateTime[] dateRange = s.GetDateRange(Username, s.Load_DGV_Weekly_RankDates());
            ID_DGV_1.DataSource = Load_DGV_Days(Username, 7, dateRange[0], dateRange[1]);
            ID_DGV_1.DataBind();    
            ID_DGV_Filter_2.DataBind();
        }

        private DataTable Load_DGV_Weekly(string Username = null, int Quantity = 0, DateTime DateStart = default, DateTime DateEnd = default)
        {
            if (Username == null || Quantity < 0)
            {
                return null;
            }
            List<List<string>> alldata = (DateStart==default||DateEnd==default)
                ? new List<List<string>>(s.DataGridView_Data(Username, s.Load_DGV_Last_Weekly_Querty(Quantity)))
                : new List<List<string>>(s.DataGridView_Data(Username, s.Load_DGV_Weekly_Between_Querty(Quantity),DateStart,DateEnd));
            DataTable table = new DataTable();
            if (alldata.Count > 0)
            {
                for (int j = 0; j < alldata[0].Count; j++)
                {
                    table.Columns.Add(alldata[0][j].ToString());
                }
                for (int i = 1; i < alldata.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    for (int j = 0; j < alldata[i].Count; j++)
                    {
                        dr[j] = alldata[i][j].ToString();
                    }
                    table.Rows.Add(dr);
                }
            }
            else
            {
                table.Columns.Add("No hay datos");
                DataRow dr = table.NewRow();
                dr[0] = "No hay datos disponibles";
                table.Rows.Add(dr);
            }
            return table;
        }
        private DataTable Load_DGV_Days(string Username = null, int Quantity = 0, DateTime DateStart = default, DateTime DateEnd = default)
        {
            if (Username == null || Quantity <= 0 || DateStart == default || DateEnd == default)
            {
                return null;
            }
            List<List<string>> alldata =  new List<List<string>>(s.DataGridView_Data(Username, 
                s.Load_DGV_Days_Between_Querty(Quantity),DateStart,DateEnd));
            DataTable table = new DataTable();
            if (alldata.Count > 0)
            {
                for (int j = 0; j < alldata[0].Count; j+=1)
                {
                    table.Columns.Add(alldata[0][j].ToString());
                }
                for (int i = 1; i < alldata.Count; i++)
                {
                    DataRow dr = table.NewRow();
                    for (int j = 0; j < alldata[i].Count; j++)
                    {
                        dr[j] = alldata[i][j].ToString();
                    }
                    table.Rows.Add(dr);
                }
            }
            else
            {
                table.Columns.Add("No hay datos");
                DataRow dr = table.NewRow();
                dr[0] = "No hay datos disponibles";
                table.Rows.Add(dr);
            }
            return table;
        }
        public void ID_BTN_Filter_Click(object sender, EventArgs e)
        {
            //string Username = "greg";//string Username = "greg";
            string Username = Session["Username"]?.ToString()?? "";
            string Mode = string.IsNullOrEmpty(ID_DD_Filter_0.SelectedValue) ? "": ID_DD_Filter_0.SelectedValue;
            string String_Quantity = string.IsNullOrEmpty(ID_DD_Filter_1.SelectedValue) ? "": ID_DD_Filter_1.SelectedValue;
            int Int_Quantity;
            Int_Quantity = int.TryParse(String_Quantity, out Int_Quantity) ? Int_Quantity: 0;
            ID_SeletedMode.Text = Mode;
            DateTime[] dateRange = s.GetDateRange(Username, s.Load_DGV_Weekly_RankDates());
            dateRange[0] = DateTime.TryParse(ID_TXT_Filter_2_DateStart.Text,out dateRange[0])? dateRange[0] : DateTime.Now;
            dateRange[1] = DateTime.TryParse(ID_TXT_Filter_3_DateEnd.Text,out dateRange[1])? dateRange[1] : DateTime.Now;
            ID_DGV_Filter_2.DataSource = null; ID_DGV_Filter_2.DataBind();
            if (dateRange[0] > dateRange[1])
            {
                return;
            }
            switch (Mode)
            {
                case "Day":
                    ID_DGV_Filter_2.DataSource = Load_DGV_Days(Username, Int_Quantity, dateRange[0], dateRange[1]);
                    break;
                case "Week":
                    //Load_DGV_Weekly
                    ID_DGV_Filter_2.DataSource = Load_DGV_Weekly(Username, Int_Quantity, dateRange[0], dateRange[1]);
                    break;
                case "Access":
                    break;
                default:
                    break;
            }            
            ID_DGV_Filter_2.DataBind();
        }


        ///SORTING
        /*
        /// <summary>
        /// SORTING/*
        /// </summary>
        private string SortExpression
        {
            get { return ViewState["SortExpression"] as string ?? string.Empty; }
            set { ViewState["SortExpression"] = value; }
        }

        private string SortDirection
        {
            get { return ViewState["SortDirection"] as string ?? "ASC"; }
            set { ViewState["SortDirection"] = value; }
        }

        protected void ID_DGV_0_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = Load_DGV_Weekly("greg", 1); // Usa tu función para cargar los datos
            // Cambiar el orden de la columna si se hace clic en la misma columna
            if (SortExpression == e.SortExpression)
            {
                SortDirection = SortDirection == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                SortExpression = e.SortExpression;
                SortDirection = "ASC";
            }
            DataView dv = dt.DefaultView;
            dv.Sort = SortExpression + " " + SortDirection;

            // Enlazar los datos ordenados al GridView
            ID_DGV_0.DataSource = dv;
            ID_DGV_0.DataBind();
        }
        //*/

    }
}