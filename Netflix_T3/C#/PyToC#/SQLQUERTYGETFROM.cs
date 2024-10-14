using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Netflix_T3.C_.PyToC_
{
    public class SQLQUERTYGETFROM
    {
        public void QuertysSQL()
        {
            string connectionString = "your_connection_string_here";  // Coloca aquí tu cadena de conexión
            string username = "input_username";  // Reemplaza con el valor del nombre de usuario
            string password = "input_password";  // Reemplaza con el valor de la contraseña
            bool answer = false;

            try
            {
                using (SqlConnection cnxn = new SqlConnection(connectionString))
                {
                    cnxn.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos");

                    string query = "SELECT User_AloAlo, Password_AloAlo, Rank_AloAlo FROM personal";
                    using (SqlCommand cmd = new SqlCommand(query, cnxn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string dbUsername = reader["User_AloAlo"].ToString();
                                string dbPassword = reader["Password_AloAlo"].ToString();
                                string dbRank = reader["Rank_AloAlo"].ToString();

                                if (dbUsername == username && dbPassword == password && dbRank == "master")
                                {
                                    answer = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {e.Message}");
                answer = false;
            }

            if (answer)
            {
                Console.WriteLine("Usuario validado como 'master'.");
            }
            else
            {
                Console.WriteLine("Usuario no encontrado o no tiene privilegios.");
            }
        }
    }
}