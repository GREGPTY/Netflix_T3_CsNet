using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Netflix_T3.C_.PyToC_
{
    public class SQLQUERTYGETFROM
    {
        public void QuertysSQL(string username, string password)
        {
            string connectionString = "your_connection_string_here"; // Usa un gestor de secretos para esta cadena
            bool answer = false;

            try
            {
                using (SqlConnection cnxn = new SqlConnection(connectionString))
                {
                    cnxn.Open();
                    Console.WriteLine("Conexión exitosa a la base de datos");

                    // Consulta con parámetros
                    string query = "SELECT User_AloAlo, Password_AloAlo, Rank_AloAlo FROM personal WHERE User_AloAlo = @username AND Password_AloAlo = @password";

                    using (SqlCommand cmd = new SqlCommand(query, cnxn))
                    {
                        // Asignar valores a los parámetros
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string dbRank = reader["Rank_AloAlo"].ToString();

                                if (dbRank == "master")
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