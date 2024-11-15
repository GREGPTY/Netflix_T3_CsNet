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
using System.Data;
using System.Text;

namespace Netflix_T3.C_.PyToC_
{
    public class SQLQUERTYGETFROM
    {

        private string connectionString = ConfigurationManager.ConnectionStrings[DB_Configuration.DB_Configuration_Connection()].ConnectionString;
        verificaciones v = new verificaciones();
        ///////////////////////////////////SIGN UP       SIGN UP           SIGN UP/////////////////////////////////////////////////////  
        public string SQL_CreateUser(string User = null, string Password = null, string Email = null, string Rank = null, string Salario = null, string Tipodepago = null)//, string autorization = null)
        {
            string answer = "";
            verificaciones ver = new verificaciones();
            User = ver.NoSpaceSrting(ver.Lower_Username(User));
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(Email) ||
                    string.IsNullOrWhiteSpace(Rank) || string.IsNullOrWhiteSpace(Salario) || string.IsNullOrWhiteSpace(Tipodepago))
            {
                answer = $"Campos vacíos: {(string.IsNullOrEmpty(User) ? "User " : "")}" +
                         $"{(string.IsNullOrEmpty(Password) ? "Password " : "")}" +
                         $"{(string.IsNullOrEmpty(Email) ? "Email " : "")}" +
                         $"{(string.IsNullOrEmpty(Rank) ? "Rank " : "")}" +
                         $"{(string.IsNullOrEmpty(Salario) ? "Salario " : "")}" +
                         $"{(string.IsNullOrEmpty(Tipodepago) ? "Tipodepago" : "")}";
            }
            else
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
                                answer = $"Usuario {User} creado";
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

            return answer;
        }

        public string EditUser_General(string User_ControlGreg_Old = null, string User_ControlGreg_New = null, string Password_Control_New = null,
        int Password_Confirmation = 0, string Rank_New = null, string TipoDePago_New = null, string SalarioPorHora_New = null,
        string Email_New = null)
        {
            verificaciones v = new verificaciones();
            string answer = "";
            string query = "exec SP_EDICION_GENERAL @User_ControlGreg_Old, @User_ControlGreg_New, @Password_Control_New, @Password_Confirmation, " +
                "@Rank_New, @TipoDePago_New, @SalarioPorHora_New, @Email_New, @EdicionCompletada OUTPUT";
            //Console.WriteLine($"Password Confirmation: {Password_Confirmation}");
            bool PasswordConfirmation_Bool = false;
            if (( Password_Confirmation ==1 ) && !string.IsNullOrEmpty(Password_Control_New))
            {
                PasswordConfirmation_Bool = true;
            }else if(Password_Confirmation == 0)
            {
                PasswordConfirmation_Bool = false;
            }
            if (string.IsNullOrEmpty(User_ControlGreg_Old) || string.IsNullOrEmpty(User_ControlGreg_New) ||
                (PasswordConfirmation_Bool == true && string.IsNullOrEmpty(Password_Control_New)) || string.IsNullOrEmpty(Rank_New) ||
                string.IsNullOrEmpty(TipoDePago_New) || string.IsNullOrEmpty(SalarioPorHora_New) ||
                string.IsNullOrEmpty(Email_New))
            {
                answer = $"Error: Uno de los datos es vacío o nulo. Valores actuales: " +
                             $"User_ControlGreg_Old='{User_ControlGreg_Old}', " +
                             $"User_ControlGreg_New='{User_ControlGreg_New}', " +
                             $"PasswordConfirmation_Bool='{PasswordConfirmation_Bool}', " +
                             $"Rank_New='{Rank_New}', " +
                             $"TipoDePago_New='{TipoDePago_New}', " +
                             $"SalarioPorHora_New='{SalarioPorHora_New}', " +
                             $"Email_New='{Email_New}'";
            }
            else { 
                if (v.User_NotExist(User_ControlGreg_New) && v.Email_NotExist(Email_New) && v.MailExistOnList(Email_New) && verificaciones.ItsDoubleWithTwoDecimal(SalarioPorHora_New))
                {
                    try
                    {
                        Console.WriteLine("Procede a Enviar Los Datos");
                        using (SqlConnection cxnx = new SqlConnection(connectionString))
                        {
                            cxnx.Open();
                            using (SqlCommand cmd = new SqlCommand(query, cxnx))
                            {
                                cmd.Parameters.AddWithValue("@User_ControlGreg_Old", User_ControlGreg_Old);
                                cmd.Parameters.AddWithValue("@User_ControlGreg_New", User_ControlGreg_New);
                                cmd.Parameters.AddWithValue("@Password_Control_New", verificaciones.HashearContrasena(Password_Control_New));
                                cmd.Parameters.AddWithValue("@Password_Confirmation", Password_Confirmation);
                                cmd.Parameters.AddWithValue("@Rank_New", Rank_New);
                                cmd.Parameters.AddWithValue("@TipoDePago_New", TipoDePago_New);
                                cmd.Parameters.AddWithValue("@SalarioPorHora_New", SalarioPorHora_New);
                                cmd.Parameters.AddWithValue("@Email_New", Email_New);
                                SqlParameter outParameter = new SqlParameter("@EdicionCompletada", SqlDbType.VarChar, 50)
                                {
                                    Direction = ParameterDirection.Output
                                };
                                cmd.Parameters.Add(outParameter);

                                cmd.ExecuteNonQuery();

                                if (outParameter.Value != DBNull.Value)
                                {
                                    answer = outParameter.Value.ToString();
                                }
                            }
                            cxnx.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        answer = $"Error: {ex.ToString()}";
                    }
                }
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
                                                string value = reader.GetString(3);//obtener el rango 
                                                    if (value.Length >= 0 && value.Length <= 5)
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
        public List<string> DropDownsSingle(string querty = null)
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
        public string GetMyRankNumber(string Username = null)
        {
            string answer = "";
            if (string.IsNullOrEmpty(Username)) { return null; }
            using (SqlConnection cxnx = new SqlConnection(connectionString))
            {
                cxnx.Open();
                string querty = "select r.RankNumber from (personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks) where p.User_ControlGreg = @username";
                using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                {
                    cmd.Parameters.AddWithValue("@username", Username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            answer = reader.GetString(0);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            return answer;
        }
        public List<List<string>> DropDownsRanks(string RankNumber = null)
        {
            List<List<string>> answer = new List<List<string>>();
            if (string.IsNullOrEmpty(RankNumber))
            {
                answer.Add(new List<string> { "No Hay Opciones Disponibles" });
                return answer;
            }
            else
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        string querty = "select Ranks, RankNumber from datos_pueden_ser_ranks where RankNumber like @RankNumber+'%' order by RankNumber asc";
                        using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                        {
                            RankNumber = RankNumber == "0" ? "" : RankNumber;
                            cmd.Parameters.AddWithValue("@RankNumber", RankNumber);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        List<string> row = new List<string>();
                                        if (reader.GetString(1).Length > RankNumber.Length)
                                        {
                                            //0 String Rank Name, 1 String Rank Number
                                            if (reader.GetString(1) != "0")
                                            {
                                                row.Add(reader.GetString(0));
                                                row.Add(reader.GetString(1));
                                            }
                                        }
                                        answer.Add(row);
                                    }
                                }
                                else
                                {

                                }
                            }
                        }
                        cxnx.Close();
                    }
                }
                catch (Exception ex)
                {
                    answer.Add(new List<string> { $"Error, No Se Cargaron los datos: {ex.Message}" });
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
        public List<List<string>> UsersAndMeData(string User = null)
        {
            verificaciones v = new verificaciones();
            List<List<string>> answer = new List<List<string>>();
            bool b = false;
            if (!string.IsNullOrEmpty(User))
            {
                string[] querty = {
            "select r.RankNumber  from (personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks) where p.User_ControlGreg = @username",
                Search_UserMeAndData_Querty()
             };
                string UserRank = null;
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        using (SqlCommand cmd = new SqlCommand(querty[0], cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", User);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                {
                                    UserRank = reader.GetString(0);
                                    Console.WriteLine($"El Rango es: {UserRank}");
                                    b = true;
                                }
                                else
                                {
                                    b = false;
                                    answer.Add(new List<string> { "No Hay Datos" });
                                }//*/
                            }

                        }
                        cxnx.Close();
                        if (b == true)
                        {
                            cxnx.Open();
                            using (SqlCommand cmd = new SqlCommand(querty[1], cxnx))
                            {
                                answer.Clear();
                                cmd.Parameters.AddWithValue("@username", User);
                                UserRank = UserRank == "0"? "": UserRank;
                                cmd.Parameters.AddWithValue("@Rank", UserRank);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                            List<string> listT = new List<string>();
                                            string userData = "";
                                            int readerColumnsSize = reader.FieldCount;
                                            for (int i = 0; i < readerColumnsSize; i += 1)
                                            {
                                                Type fieldType = reader.GetFieldType(i);
                                                /*
                                                if (i == 0)
                                                {
                                                    userData = v.ValorDeLaTabla(reader, i).ToString();
                                                }
                                                else
                                                {
                                                    //userData += $",{v.ValorDeLaTabla(reader, i).ToString()}";
                                                    userData += v.ValorDeLaTabla(reader, i).ToString();
                                                }//*/
                                                Console.WriteLine(v.ValorDeLaTabla(reader, i).ToString());
                                                listT.Add(v.ValorDeLaTabla(reader, i).ToString());
                                                //Console.WriteLine(userData);
                                                //listT.Add(userData);
                                            }
                                            answer.Add(listT);
                                        }
                                    }
                                    else
                                    {
                                        return answer;
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                answer.Clear();
                answer.Add(new List<string> { "No Hay Datos" });
            }
            return answer;
        }
        public List<string> MyUserData(string Username = null)
        {
            List<string> answer = new List<string>();
            if (!string.IsNullOrEmpty(Username))
            {
                string querty = Search_My_Personal_Data();
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", Username);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                {
                                    string Data = null;
                                    int columnSize = reader.FieldCount;
                                    for (int i = 0; i < columnSize; i += 1)
                                    {
                                        Type type = reader.GetFieldType(i);
                                        answer.Add(v.ValorDeLaTabla(reader, i).ToString());
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    answer.Add("No Data");
                }
            }
            return answer;
        }
        public bool UserSessionIsBiggerThanUserEdit(string UserSession = null, string UserEdit = null)
        {
            bool answer = false;
            int esmayor = 0;
            if (UserSession == null || UserEdit == null)
            {
                return false;
            }
            string query = "exec SP_UserSessionIsBiggerThannUserEdit @usernameSession, @usernameEdit, @answer OUTPUT";
            try
            {
                using (SqlConnection cxnx = new SqlConnection(connectionString))
                {
                    cxnx.Open();
                    using (SqlCommand cmd = new SqlCommand(query, cxnx))
                    {
                        cmd.Parameters.AddWithValue("@usernameSession", UserSession);
                        cmd.Parameters.AddWithValue("@usernameEdit", UserEdit);
                        SqlParameter outParameter = new SqlParameter("@answer", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outParameter);
                        cmd.ExecuteNonQuery();
                        esmayor = outParameter.Value != DBNull.Value ? (int)outParameter.Value : 0;
                        //answer = esmayor == 1? true : false;
                        answer = esmayor == 1;
                    }
                    cxnx.Close();
                }
            }
            catch (Exception)
            {
                answer = false;
            }
            return answer;
        }
        public bool VerificadorDeContrasena(string Username = null, string Password = null)
        {
            string query = "select Password_Control from personal where User_ControlGreg = @username;";
            bool answer = false;

            if (Username == null || Password == null)
            {
                return false;
            }

            try
            {
                using (SqlConnection cxnx = new SqlConnection(connectionString))
                {
                    cxnx.Open();
                    using (SqlCommand cmd = new SqlCommand(query, cxnx))
                    {
                        cmd.Parameters.AddWithValue("@username", Username);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    byte[] PasswordHashedByte = (byte[])reader.GetValue(0);
                                    string PasswordHashed = Encoding.UTF8.GetString(PasswordHashedByte);
                                    if (BCrypt.Net.BCrypt.Verify(Password, PasswordHashed))
                                    {
                                        answer = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return answer;
        }
        public List<List<string>> DataGridView_Data(string Username = null, string querty = null, DateTime DateStart = default, DateTime DateEnd = default)
        {
            List<List<string>> answer = new List<List<string>>();
            if ((!string.IsNullOrEmpty(Username)) && (!string.IsNullOrEmpty(querty)))
            {
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", Username);
                            if (!(DateStart == default || DateEnd == default))
                            {
                                cmd.Parameters.AddWithValue("@DateStart", DateStart);
                                cmd.Parameters.AddWithValue("@DateEnd", DateEnd);
                            }
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    List<string> titlerow = new List<string>();
                                    for (int i = 0; i < reader.FieldCount; i += 1)
                                    {
                                        titlerow.Add(reader.GetName(i));
                                    }
                                    answer.Add(titlerow);
                                    while (reader.Read())
                                    {
                                        List<string> rowElement = new List<string>();
                                        for (int i = 0; i < reader.FieldCount; i += 1)
                                        {
                                            Type type = reader.GetFieldType(i);
                                            rowElement.Add(v.ValorDeLaTabla(reader, i));
                                        }
                                        answer.Add(rowElement);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    answer.Add(new List<string> { "No hay datos" });
                }
            }
            return answer;
        }
        public DateTime[] GetDateRange(string username = null, string queryDateRange = null)
        {
            //@"SELECT TOP 1 DATEFROMPARTS(AnoInicio, MesInicio, DiaIncio) AS DateStart, DATEFROMPARTS(AnoFinal, MesFinal, DiaFinal) AS DateEnd FROM salario_a_la_semana WHERE User_ControlGreg_Salario = @username ORDER BY Movimiento DESC"
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(queryDateRange)) { return null; }
            DateTime dateStart, dateEnd;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(queryDateRange, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dateStart = reader.GetDateTime(0);
                            dateEnd = reader.GetDateTime(1);
                            return new DateTime[] { dateStart, dateEnd };
                        }
                    }
                }
            }
            return null;
        }

        public string Get_RankName(string username = null)
        {
            string answer = null;
            try
            {
                if (string.IsNullOrEmpty(username)) { return null; }
                using (SqlConnection cxnx = new SqlConnection(connectionString))
                {
                    cxnx.Open();
                    string querty = "select Rank_Control from personal where User_ControlGreg = @username ;";
                    using (SqlCommand cmd = new SqlCommand(querty, cxnx))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                answer = reader.GetString(0);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }catch(Exception ex) { return null; }
            return answer;
        }
        public bool MeUpUserDown(string UserSession = null, string UserDown = null)
        {
            bool answer = false; string rank = "";
            if (string.IsNullOrEmpty(UserSession) || string.IsNullOrEmpty(UserDown)) { return false; }
            else if (UserSession == UserDown) { return true; }
            string[] querty = {@"select r.RankNumber from (personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks) 
                                    where p.User_ControlGreg = @usernameSession ;"
                            ,@"select 1 from (personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks) 
                                    where (p.User_ControlGreg = @usernameDown and r.RankNumber like @rank+'[0-9]%') ;" };
            using (SqlConnection cxnx = new SqlConnection(connectionString))
            {
                cxnx.Open();
                using (SqlCommand cmd = new SqlCommand(querty[0], cxnx))
                {
                    cmd.Parameters.AddWithValue("@usernameSession", UserSession);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            rank = reader.GetString(0);
                            if (rank == "0") { return true; }
                        }
                        else { cxnx.Close(); return false; }
                    }
                }
                cxnx.Close(); cxnx.Open();
                using (SqlCommand cmd = new SqlCommand(querty[1], cxnx))
                {
                    cmd.Parameters.AddWithValue("@usernameDown", UserDown);
                    cmd.Parameters.AddWithValue("@rank", rank);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            if (reader.GetInt32(0) == 1)
                            {
                                answer = true;
                            }
                        }
                        else { cxnx.Close(); return false; }
                    }
                }
                cxnx.Close();
            }
            return answer;
        }
        public List<string> Get_Names(string username = null)
        {
            verificaciones v = new verificaciones();
            List<string> answer = new List<string>();
            bool b = false;
            if (!string.IsNullOrEmpty(username))
            {
                string[] querty = {
            "select r.RankNumber  from (personal as p inner join datos_pueden_ser_ranks as r on p.Rank_Control = r.Ranks) where p.User_ControlGreg = @username",
                Search_UserMeAndData_Querty()
             };
                string UserRank = null;
                try
                {
                    using (SqlConnection cxnx = new SqlConnection(connectionString))
                    {
                        cxnx.Open();
                        using (SqlCommand cmd = new SqlCommand(querty[0], cxnx))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                {
                                    UserRank = reader.GetString(0);
                                    Console.WriteLine($"El Rango es: {UserRank}");
                                    b = true;
                                }
                                else
                                {
                                    b = false;
                                    answer.Add("No Hay Datos" );
                                }//*/
                            }

                        }
                        cxnx.Close();
                        if (b == true)
                        {
                            cxnx.Open();
                            using (SqlCommand cmd = new SqlCommand(querty[1], cxnx))
                            {
                                answer.Clear();
                                cmd.Parameters.AddWithValue("@username", username);
                                UserRank = UserRank == "0" ? "" : UserRank;
                                cmd.Parameters.AddWithValue("@Rank", UserRank);
                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.HasRows)
                                    {
                                        while (reader.Read())
                                        {
                                                answer.Add(reader.GetString(0));
                                        }
                                    }
                                    else
                                    {
                                        answer.Clear();
                                        answer.Add( "No Hay Datos" );
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                answer.Clear();
                answer.Add( "No Hay Datos" );
            }
            return answer;
        }

        //QUERY / QUERTYS

        public string Search_UserMeAndData_Querty()
        {
            return  @"SELECT p.User_ControlGreg, p.Rank_Control, p.email, s.SalarioPorHora, s.TipoDePago
                     FROM personal AS p
                     INNER JOIN salario_de_usuario_por_dia AS s ON p.User_ControlGreg = s.User_ControlGreg
                     INNER JOIN datos_pueden_ser_ranks AS r ON p.Rank_Control = r.Ranks
                     WHERE r.RankNumber LIKE @Rank + '[0-9]%' OR p.User_ControlGreg = @username;";
        }

        public string Search_My_Personal_Data()
        {
            return "SELECT top 1 p.ID, p.User_ControlGreg, p.Rank_Control, p.email, s.SalarioPorHora, s.TipoDePago, MontoPorSerPagado " +
                " FROM ((personal AS p\r\nINNER JOIN salario_de_usuario_por_dia AS s ON p.User_ControlGreg = s.User_ControlGreg) " +
                " INNER JOIN tabla_por_pagar_y_registro_de_pagados as pp on p.User_ControlGreg = pp.User_ControlGreg) " +
                " WHERE p.User_ControlGreg = @username order by pp.Movimiento desc;";
        }
        public string Load_DGV_Weekly_RankDates()
        {
            return "SELECT TOP 1 " +
                " CONVERT(DATE, DATEFROMPARTS(AnoInicio, MesInicio, DiaIncio)) AS DateStart, " +
                "CONVERT(DATE, DATEFROMPARTS(AnoFinal, MesFinal, DiaFinal)) AS DateEnd " +
                "FROM salario_a_la_semana \r\nWHERE User_ControlGreg_Salario = @username " +
                "ORDER BY Movimiento DESC;";
        }
        public string Load_DGV_Weekly_Between_Querty(int quantity = 0)
        {
            return $"select top {quantity} Movimiento, User_ControlGreg_Salario as Username, Monto as \"Amount\", HorasTrabajadas as \"Hours Worked\", MinutosTrabajados as \"Minutes Worked\", " +
            " CONVERT(DATE, DATEFROMPARTS(AnoInicio, MesInicio, DiaIncio)) AS \"Date Start\"," +
            " CONVERT(DATE, DATEFROMPARTS(AnoFinal, MesFinal, DiaFinal)) AS \"Date End\" " +
            " from salario_a_la_semana where User_ControlGreg_Salario = @username and " +
            " DATEFROMPARTS(AnoFinal, MesFinal, DiaFinal) BETWEEN @DateStart AND @DateEnd " +
            " order by Movimiento desc";
        }
        public string Load_DGV_Last_Weekly_Querty(int quantity = 0)
        {
            return $"select top {quantity} Movimiento, User_ControlGreg_Salario as Username, Monto as \"Amount\", HorasTrabajadas as \"Hours Worked\", MinutosTrabajados as \"Minutes Worked\", " +
            "CONVERT(DATE, DATEFROMPARTS(AnoInicio, MesInicio, DiaIncio)) AS \"Date Start\", " +
            "CONVERT(DATE, DATEFROMPARTS(AnoFinal, MesFinal, DiaFinal)) AS \"Date End\" " +
            "from salario_a_la_semana where User_ControlGreg_Salario = @username " +
            " order by Movimiento desc";
        }
        public string Load_DGV_Days_Between_Querty(int top = 0)
        {
            return $"SELECT TOP {top}" +
                    $"User_ControlGreg_Salario AS \"Username\", DATEFROMPARTS(Ano, Mes, Dia) AS \"Date\", Monto as \"Amount\", " +
                    $"RIGHT('00' + CAST(HoraInicio AS VARCHAR), 2) + ':' + RIGHT('00' + CAST(MinutoInicio AS VARCHAR), 2) AS \"Check-in Time\", " +
                    $"RIGHT('00' + CAST(HoraFinal AS VARCHAR), 2) + ':' + RIGHT('00' + CAST(MinutoFinal AS VARCHAR), 2) AS \"Check-out Time\", " +
                    $"RIGHT('00' + CAST(HorasTrabajadas AS VARCHAR), 2) + ':' + RIGHT('00' + CAST(MinutosTrabajados AS VARCHAR), 2) AS \"Hours Worked\" " +
                    $"FROM salario_al_dia " +
                    $"WHERE User_ControlGreg_Salario = @username " +
                    $"AND DATEFROMPARTS(Ano, Mes, Dia) BETWEEN @DateStart AND @DateEnd order by DATEFROMPARTS(Ano, Mes, Dia) asc;";
        }
    }
}