using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BCrypt.Net;
using iTextSharp.text;
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
        public static bool ItsDoubleWithTwoDecimal(string Money = null)
        {
            bool answer = false;
            Money = Money.Replace(" ", "");
            if (!string.IsNullOrEmpty(Money))
            {
                if (Money.Contains("."))
                {
                    string[] MoneySplit = Money.Split('.');
                    int entero_p = 0, decimal_p = 0;
                    if (MoneySplit.Length == 2)
                    {
                        if (int.TryParse(MoneySplit[0], out entero_p) && int.TryParse(MoneySplit[1], out decimal_p))
                        {
                            if (MoneySplit[1].Length < 3)
                            {
                                answer = true;
                            }
                        }
                        else
                        {
                            int i = 0;
                            answer = !int.TryParse(Money, out i);
                        }
                    }
                }
                else
                {
                    int i = 0;
                    answer = int.TryParse(Money, out i);
                }
            }
            return answer;
        }
        public static double TransforToMoney(string Money = null)
        {
            double answer = 0;
            Money = Money.Replace(" ", "");
            if (!string.IsNullOrEmpty(Money))
            {
                answer = double.Parse(Money);
            }
            else
            {
                answer = 0.00;
            }
            return answer;
        }
        public bool ItsHighRank(string Username = null) //0,1
        {
            bool answer = false;
            if (!string.IsNullOrEmpty(Username))
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        string query = "select p.User_ControlGreg, p.Rank_Control, r.RankNumber from personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks " +
                                       "where p.User_ControlGreg = @username";
                        using (SqlCommand cmd = new SqlCommand(query, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", Username);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int RankNumber = Convert.ToInt32(reader.GetInt32(2));
                                    //Console.WriteLine($"El Rango Obtenido fue: {RankNumber} para el usuario [{Username}]");
                                    if (RankNumber >= 0 && RankNumber < 2)
                                    {
                                        answer = true;
                                    }
                                    else
                                    {
                                        answer = false;
                                    }
                                }
                                else
                                {
                                    answer = false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    answer = false;
                }
            }
            else
            {
                answer = false;
            }
            return answer;
        }
        public string ValorDeLaTabla(SqlDataReader reader = null, int column = 0)
        {
            string answer = "";
            if (reader.IsDBNull(column))
            {
                return "NULL";
            }
            Type fieldType = reader.GetFieldType(column);
            if (fieldType == typeof(string))
            {
                return reader.GetString(column);
            }
            else if (fieldType == typeof(int))
            {
                return reader.GetInt32(column).ToString();
            }
            else if (fieldType == typeof(double))
            {
                return reader.GetDouble(column).ToString("F2");
            }
            else if (fieldType == typeof(bool))
            {
                return reader.GetBoolean(column).ToString();
            }
            else if (fieldType == typeof(DateTime))
            {
                return reader.GetDateTime(column).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return reader.GetValue(column).ToString();
            }
            return answer;
        }
        public static ITemplate CreateItemTemplate()
        {
            return new CompiledBindableTemplateBuilder(
                container =>
                {
                    if (container is DataListItem dataListItem)
                    {
                        Literal itemLiteral = new Literal();
                        itemLiteral.DataBinding += (sender, args) =>
                        {
                            Literal currentItem = (Literal)sender;
                            if (dataListItem.DataItem != null)
                            {
                                object dataValue = DataBinder.Eval(dataListItem.DataItem, string.Empty);
                                currentItem.Text = dataValue != null ? dataValue.ToString() : "N/A"; // Maneja valores nulos
                            }
                            else
                            {
                                currentItem.Text = "N/A";
                            }
                        };
                        container.Controls.Add(itemLiteral);
                    }
                },
                null
            );
        }
    }
    public class transformdata{
        //este metodo esta diseñado para agarrar los datos de una o datos en general y devolver una respuesta con su operacion
        //despues explico mejor esto

        public List<string> DataUser(List<List<string>> Usuarios = null, string Username = null)
        { //Esta funcion se dedica a leer una lista que contiene una lista y devolver una lista especifica,, ej lista 1, 2, 3, quiero que devuelva los valores de la 3era lista solamente
            List<string> answer = new List<string>();
            answer.Add("No Hay Datos");
            if (Usuarios.Any())
            {
                for (int i = 0; i < Usuarios.Count; i += 1)
                {
                    //if (Usuarios[i][0].Contains(Username))
                    if (Usuarios[i][0].Equals(Username, StringComparison.OrdinalIgnoreCase))
                    {
                        answer.Clear();
                        foreach (string data in Usuarios[i])
                        {
                            answer.Add(data);
                        }
                        return answer;
                    }
                }
            }
            return answer;
        }
    }
}