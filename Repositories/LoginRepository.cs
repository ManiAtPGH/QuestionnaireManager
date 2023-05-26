using Npgsql;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;

namespace QuestionnaireManagerPOC.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly NpgsqlConnection _connection;

        public LoginRepository()
        {
            _connection = GetConnection();
        }

        public bool Login(string username, string password)
        {
            bool IsVerified = false;

            try
            {
                username = username.Trim();
                password = password.Trim();
                _connection.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "login_validation";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"SELECT {functionName}(@email, @password)";

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("email", username);
                    cmd.Parameters.AddWithValue("password", password);
                    IsVerified = (bool)cmd.ExecuteScalar();
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return IsVerified;
        }

        private NpgsqlConnection GetConnection()
        {
            var connectionbuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = connectionbuilder.Build();

            var connectionString = _configuration.GetConnectionString("DB_CONNECTION_STRING");

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            return connection;
        }

        ~LoginRepository()
        {
            _connection?.Close();
        }
    }
}
