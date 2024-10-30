using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Netflix_T3.C_;
using BCrypt.Net;
using iTextSharp.text.pdf.security;

namespace Netflix_T3.C_.PyToC_
{
    public class SQLQUERTYGETFROM
    {

        private string connectionString = ConfigurationManager.ConnectionStrings[DB_Configuration.DB_Configuration_Connection()].ConnectionString;
        ///////////////////////////////////SIGN UP       SIGN UP           SIGN UP/////////////////////////////////////////////////////  
        public string SQL_CreateUser(string User = null, string Password = null, string Email = null, string Rank = null, string Salario = null, string Tipodepago = null)//, string autorization = null)
        {
            string answer = "";
            verificaciones ver = new verificaciones();
            User = ver.NoSpaceSrting(ver.Lower_Username(User));
            if (!(string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Rank) || string.IsNullOrEmpty(Salario) || string.IsNullOrEmpty(Tipodepago) ))
                {//(autorization == null || autorization == "")))            
                try
                {
                    if (ver.User_NotExist(User) && ver.Email_NotExist(Email) && ver.MailExistOnList(Email) && verificaciones.ItsDoubleWithTwoDecimal(Salario))
                    {
                        using (SqlConnection cxnx = new SqlConnection(connectionString))
                        {
                            cxnx.Open();
                            Console.WriteLine("Conexión exitosa a la base de datos");
                            //string querty = "insert into test_user(username_test, password_test, email, autorization, password_test_hash) values (@username, @password, @mail, @autorization, @passwordhashed);";
                            /* information
                             SP_CrearUsuarioAndSalarioDeUsuario
                                @Usuario as varchar(40),
	                            @Password as VARBINARY(512),
	                            @Email as varchar(100),
	                            @Rank as varchar(40), --need dropdown 
	                            @SalarioPorHora as numeric(10,2), --only float of 0.00
	                            @TipoDePago as varchar(20) --dropdown
                             //*/
                            string querty = "exec SP_CrearUsuarioAndSalarioDeUsuario @username, @password , @email, @rank, @salario, @pagotipo;";
                            using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                            {
                                cmd.Parameters.AddWithValue("@username", User);
                                cmd.Parameters.AddWithValue("@password", verificaciones.HashearContrasena(Password));
                                cmd.Parameters.AddWithValue("@email", Email);
                                cmd.Parameters.AddWithValue("@rank", Rank);
                                cmd.Parameters.AddWithValue("@salario", verificaciones.TransforToMoney(Salario));
                                cmd.Parameters.AddWithValue("@pagotipo", Tipodepago);
                                //cmd.Parameters.AddWithValue("@autorization", autorization);
                                //cmd.BeginExecuteNonQuery(); //asincronica en segundo plano
                                cmd.ExecuteNonQuery(); //en plano principal
                                /*int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected > 0)
                                {
                                    answer = "Exito, Se enviaron los datos";
                                }
                                else
                                {
                                    answer = "Error, No se enviaron los datos";
                                }//*/
                            }

                            cxnx.Close();
                        }
                    }else if (!ver.User_NotExist(User))
                    {
                        answer = $"El username: [{User}] ya existe";
                    }
                    else if (!ver.Email_NotExist(Email))
                    {
                        answer = $"El email: [{Email}] ya existe";
                    }
                    else if (!ver.MailExistOnList(Email))
                    {
                        answer = $"El email \'[{Email}]\' no pudo ser corroborado";
                    }else if (!verificaciones.ItsDoubleWithTwoDecimal(Salario))
                    {
                        answer = $"El monto \'[{Salario}]\' no puede estar vacio contener mas de 2 numeros despues del punto";
                    }
                    else
                    {
                        answer = $"No se pudo procesar la solicitud";
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
        
///////////////////////////////////LOGIN       LOGIN           LOGIN/////////////////////////////////////////////////////
        
        public string[] SQL_Login(string User = null, string Password = null)
        {
            string [] answer = {"","no" };
            //string PasswordHashed = null;
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
                        //string query = "SELECT username_test, password_test_hash, email, autorization FROM test_user WHERE username_test = @username;";
                        string query = "select p.User_ControlGreg, p.Password_Control, p.Rank_Control, d.RankNumber from (personal as p inner join datos_pueden_ser_ranks as d on p.Rank_Control = d.Ranks) where p.User_ControlGreg = @username;";
                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", User);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    if (reader.Read())
                                    {
                                        if (!reader.IsDBNull(1)){
                                            byte[] hashedPasswordBytes = (byte[])reader.GetValue(1);
                                            string hashedPassword = System.Text.Encoding.UTF8.GetString(hashedPasswordBytes);

                                            if (BCrypt.Net.BCrypt.Verify(Password, hashedPassword) && (!reader.IsDBNull(3)))
                                            {
                                                int value = reader.GetInt32(3);//obtener el rango 
                                                    if (value >= 0 && value < 3)
                                                    {
                                                     answer[0] = "Bienvenido, tiene acceso";
                                                     answer[1] = "si".ToLower();
                                                    }
                                                    else
                                                    {
                                                        answer[0] = "Sin Acceso";
                                                        answer[1] = "no";
                                                    }
                                            }
                                            else
                                            {
                                                answer[0] = "Sin Acceso";
                                                answer[1] = "no";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    answer[0] = "Sin Acceso";
                                    answer[1] = "no";
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
                catch (Exception ex)
                {
                    answer[0] = "Error: " + ex.Message;
                    answer[1] = "no";
                }
            }
            else
            {
                answer[0] = "No Pueden haber espacios vacíos";
                answer[1] = "no";
            }
            return answer;
        }
        public List<string> DropDown(string querty = null)
        {
            List<string> answer = new List<string>();
            if (string.IsNullOrEmpty(querty))
            {
                answer.Add("No Hay Opciones Disponibles");
            }
            else
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        if (reader.GetString(0) != "ghost".ToLower())
                                        {
                                            answer.Add(reader.GetString(0));
                                        }
                                    }
                                }
                                else
                                {
                                    answer.Add("No Hay Datos Disponibles");
                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
                catch (Exception ex)
                {
                    answer.Add($"Error, No Se Cargaron los datos: {ex.Message}");
                }
            }
            return answer;
        }

        ////////////////SOLO EN EMERGENCIA CAMBIAR LA CLAVE
        public string ChangingPassword(string Password = null, string Username = null)
        {
            string answer = "";
            try
            {
                using (SqlConnection cxnx = new SqlConnection(connectionString))
                {
                    cxnx.Open();
                    string querty = "update personal set Password_Control = @password where User_ControlGreg = @username;";
                    using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                    {
                        cmd.Parameters.AddWithValue("@password", verificaciones.HashearContrasena(Password));
                        cmd.Parameters.AddWithValue("@username", Username);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            answer = "Exito, Se cambio la contraseña";
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
                answer = $"Error: {ex}";
            }
            return answer;
        }
    }
}