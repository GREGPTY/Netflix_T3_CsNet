using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Netflix_T3.C_
{
    public class SQL_conection
    {
        readonly SqlConnection sql_cc = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        public void OpenConnection()
        {
            sql_cc.Open();
        }
        public DataTable Cargar_Tabla_Usuario()
        {
            SqlCommand cmd = new SqlCommand("SEE_DATA", sql_cc);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public bool User_Boss(string user_name, string password)
        {
            bool answer = false;
            SqlCommand cmd = new SqlCommand();
            return answer;
        }
        public void CloseConnection()
        {
            sql_cc.Close();
        }
    }
}