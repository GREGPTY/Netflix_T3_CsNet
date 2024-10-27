using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using BCrypt.Net;
using iTextSharp.text.pdf.security;
using Netflix_T3.C_;
using Netflix_T3.C_.PyToC_;


namespace Netflix_T3.C_
{    
    public class verificaciones
    {
        private string connectionString = ConfigurationManager.ConnectionStrings[DB_Configuration.DB_Configuration_Connection()].ConnectionString;
        public string NoSpaceSrting(string String = null)
        {
            string answer = "";
            String = String.Replace(" ", "");
            answer = String;
            return answer;
        }
        public string Lower_Username(string String = null)
        {
            string answer = "";
            String = String.ToLower();
            answer = String;
            return answer;
        }
        public static byte[] HashearContrasena(string contraseña)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(contraseña);
            byte[] hashedbyte = Encoding.UTF8.GetBytes(hashed);
            return hashedbyte;
        }
        public bool MailExistOnList(string UserEmail = null)
        {
            bool answer = false;
            List<string> emailList = new List<string>();
            try
            {
                if (!string.IsNullOrEmpty(UserEmail))
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        string query = "select correo from correo_puede_ser;";

                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (!reader.IsDBNull(0))
                                        {
                                            emailList.Add(reader.GetString(0));
                                        }
                                    }
                                    foreach (string s in emailList)
                                    {
                                        if (UserEmail.EndsWith(s))
                                        {
                                            answer = true;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return answer;
        }
        public bool Email_NotExist(string Mail = null)
        {
            bool answer = false;
            Mail = NoSpaceSrting(Lower_Username(Mail));
            try
            {
                if (!string.IsNullOrEmpty(Mail)) //Mail
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        string query = "select 1 from test_user where email = @email;";

                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@email", Mail);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!(reader.HasRows))
                                {
                                    answer = true;
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return answer;
        }
        public bool User_NotExist(string User = null)
        {
            bool answer = false;
            User = NoSpaceSrting(Lower_Username(User));
            try
            {
                if (!string.IsNullOrEmpty(User)) //si ninguno es null perfecto
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        string query = "select 1 from test_user where username_test = @username;";

                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", User);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (!(reader.HasRows))
                                {
                                    answer = true;
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return answer;
        }

    }
}