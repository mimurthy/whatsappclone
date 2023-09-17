using UserService.Models;
using System.Data.SqlClient;
using System.Data;

namespace UserService.Data
{
    public class SQLServerDatabaseHandler : IDatabaseAdapter, IDisposable
    {
        SqlConnection? _connection;

        string _connectionString = "Data Source=KNOR-LAPTOP08;Initial Catalog=WhatsApp;Integrated Security=True;";

        public SQLServerDatabaseHandler()
        {
            try
            {
                _connection = new SqlConnection(_connectionString);

                _connection.Open();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateProfile(User userinfo)
        {

        }

        public void SignUp(User userinfo)
        {
            string insertQuery = "INSERT INTO Users (UserId, UserName, PhoneNumber, Email, ProfilePicture, DeviceId, NotificationsReqd, Password, Location)" +
                "VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9)";

            using (SqlCommand command = new SqlCommand(insertQuery, _connection)) 
            {
                command.Parameters.AddWithValue("@Value1", userinfo.UserId);
                command.Parameters.AddWithValue("@Value2", userinfo.Username);
                command.Parameters.AddWithValue("@Value3", userinfo.PhoneNumber);
                command.Parameters.AddWithValue("@Value4", userinfo.Email);
                command.Parameters.AddWithValue("@Value5", "");
                command.Parameters.AddWithValue("@Value6", userinfo.DeviceId);
                command.Parameters.AddWithValue("@Value7", 0);
                command.Parameters.AddWithValue("@Value8", userinfo.Password);
                command.Parameters.AddWithValue("@Value9", "Norway");

                int rowsAffected = command.ExecuteNonQuery();

                Console.WriteLine($"Rows affected: {rowsAffected}");
            }
        }

        public string Login(string userName, string password)
        {
            string pwd = string.Empty;
            string userid = string.Empty;

            string sqlQuery = "SELECT Password, UserId FROM dbo.users WHERE UserName = @paramValue";

            using (SqlCommand command = new SqlCommand(sqlQuery, _connection))
            {
                command.Parameters.AddWithValue("@paramValue", userName);

                using (SqlDataReader reader = command.ExecuteReader()) 
                {
                    while(reader.Read()) 
                    {
                        pwd = reader["Password"].ToString();
                        userid = reader["UserId"].ToString();
                    }
                }
            }

            if (password == pwd)
            {
                return GetWebsocketConnection(userid);
            }
            else
                return "";
        }

        public string GetWebsocketConnection(string userid) 
        {
            int insertedPortNumber;

            using (SqlCommand command = new SqlCommand("InsertRecordWithLeastAvailablePort", _connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter userIdParameter = new SqlParameter("@UserId", SqlDbType.Int);
                userIdParameter.Value = userid;
                command.Parameters.Add(userIdParameter);

                SqlParameter insertedPortParameter = new SqlParameter("@InsertedPortNumber", SqlDbType.Int);
                insertedPortParameter.Direction = ParameterDirection.Output;
                command.Parameters.Add(insertedPortParameter);

                command.ExecuteNonQuery();

                insertedPortNumber = (int)insertedPortParameter.Value;
                Console.WriteLine("Inserted Port Number: " + insertedPortNumber);
            }

            return insertedPortNumber.ToString();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
