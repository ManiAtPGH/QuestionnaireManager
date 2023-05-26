using Npgsql;

namespace QuestionnaireManagerPOC.HelperServices
{
    public sealed class Database
    {

        private static Database _instance;

        private Database() { }

        public static Database Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            var connectionbuilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = connectionbuilder.Build();

            var connectionString = _configuration.GetConnectionString("DB_CONNECTION_STRING");

            NpgsqlConnection connection = new NpgsqlConnection(connectionString);

            return connection;
        }
    }
}

