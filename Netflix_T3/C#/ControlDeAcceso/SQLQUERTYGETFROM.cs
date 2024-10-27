﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Netflix_T3.C_;
using BCrypt.Net;

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
        
        public string SQL_Login(string User = null, string Password = null)
        {
            string answer = "";
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
                        string query = "SELECT username_test, password_test_hash, email, autorization FROM test_user WHERE username_test = @username;";
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
    }
}