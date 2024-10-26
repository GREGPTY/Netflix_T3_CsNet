using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Netflix_T3.C_;

namespace Netflix_T3.C_.PyToC_
{
    public class SQLQUERTYGETFROM
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conexion_ControlDeAcceso_Greg"].ConnectionString;
        
        public string SQL_CreateUser(string User = null, string Password = null, string Email = null, string autorization = null)
        {
            string answer = "";
            verificaciones ver = new verificaciones();
            User = ver.NoSpaceSrting(ver.Lower_Username(User));
            if (!((User == null || User == "") || (Password == null || Password == "") ||
                (Email == null || Email == "") || (autorization == null || autorization == "")))
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        Console.WriteLine("Conexión exitosa a la base de datos");
                        string querty = "insert into test_user(username_test, password_test, email, autorization) values (@username, @password, @mail, @autorization);";
                        using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                        { 
                            cmd.Parameters.AddWithValue("@username", User);
                            cmd.Parameters.AddWithValue ("@password", Password);
                            cmd.Parameters.AddWithValue ("@mail", Email);
                            cmd.Parameters.AddWithValue("@autorization", autorization);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                answer = "Exito, Se enviaron los datos";
                            }
                            else
                            {
                                answer = "Error, No se enviaron los datos";
                            }
                        }
                        cxnx.Close();
                    }                        
                }
                catch (Exception ex)
                {
                    answer = "Error: " + ex.Message;
                }
            }
            else
            {
                answer = "No Pueden haber espacios vacios";
            }

            return answer;
        }
        public string SQL_Login(string User = null, string Password = null)
        {
            string answer = "";
            verificaciones ver = new verificaciones();
            User = ver.NoSpaceSrting(ver.Lower_Username(User));

            if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        Console.WriteLine("Conexión exitosa a la base de datos");
                        string query = "SELECT username_test, password_test, email, autorization FROM test_user WHERE username_test = @username AND password_test = @password;";
                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", User);
                            cmd.Parameters.AddWithValue("@password", Password);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        if (!reader.IsDBNull(3))
                                        {
                                            string value = reader.GetString(3); // Obtener el valor de la columna "autorization"
                                            if (value == "yes")
                                            {
                                                answer = "Bienvenido, tiene acceso";
                                            }
                                            else
                                            {
                                                answer = "Sin Acceso";
                                            }
                                        }
                                        else
                                        {
                                            answer = "Sin Acceso";
                                        }
                                    }
                                }
                                else
                                {
                                    answer = "Sin Acceso";
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
                catch (Exception ex)
                {
                    answer = "Error: " + ex.Message;
                }
            }
            else
            {
                answer = "No Pueden haber espacios vacíos";
            }
            return answer;
        }
    }
}