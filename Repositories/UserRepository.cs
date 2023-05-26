using Npgsql;
using NpgsqlTypes;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.HelperServices;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using System.Data;

namespace QuestionnaireManagerPOC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection;

        public UserRepository()
        {
            _connection = Database.Instance.GetConnection();
        }

        public int AddUser(BaseUserModel model)
        {
            int result = 0;
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }

                // Create a new Npgsql command
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "insert_user_with_role";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_name, @in_email, @in_created_by_user_id, @in_role_id)";
                    model.CreatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_name", model.FullName);
                    cmd.Parameters.AddWithValue("in_email", model.Email);
                    cmd.Parameters.AddWithValue("in_created_by_user_id", model.CreatedById);
                    cmd.Parameters.AddWithValue("in_role_id", model.RoleId);
                    cmd.Parameters.Add("rows_affected", NpgsqlDbType.Integer).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    int rowsAffected = (int)cmd.Parameters["rows_affected"].Value;
                    if (rowsAffected > 0)
                    {
                        result = rowsAffected;
                    }
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public UserModel GetUser(Guid id)
        {
            UserModel model = new UserModel();

            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_user_account_by_id";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT * FROM {functionName}(@parameter1)";

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("parameter1", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.UserId = reader.GetGuid(0);
                                model.FullName = reader.GetString(1);
                                model.Email = reader.GetString(2);
                                model.RoleId = reader.GetGuid(3);
                                model.Role = reader.GetString(4);
                                model.CreatedDate = reader.GetDateTime(5);
                                model.CreatedById = reader.GetGuid(7);
                            }
                        }
                    }
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            List<UserModel> model = new List<UserModel>();
            try
            {
               if( _connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                } 
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_user_accounts";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"SELECT * FROM {functionName}()";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.Add(new UserModel()
                                {
                                    UserId = reader.GetGuid(0),
                                    FullName = reader.GetString(1),
                                    Email = reader.GetString(2),
                                    RoleId = reader.GetGuid(3),
                                    Role = reader.GetString(4),
                                    CreatedDate = reader.GetDateTime(5),
                                    CreatedById = reader.GetGuid(7)
                                });
                            }
                        }
                    }
                }
                _connection?.Close();

            }
            catch (Exception)
            {

                throw;
            }
            return model;
        }

        public UserModel GetUser(string emaiIid)
        {
            UserModel model = new UserModel();

            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_user_account_by_email";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"SELECT * FROM {functionName}(@in_email)";

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_email", emaiIid);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.UserId = reader.GetGuid(0);
                                model.FullName = reader.GetString(1);
                                model.Email = reader.GetString(2);
                                model.RoleId = reader.GetGuid(3);
                                model.Role = reader.GetString(4);
                                model.CreatedDate = reader.GetDateTime(5);
                                model.CreatedById = reader.GetGuid(7);
                            }
                        }
                    }
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return model;
        }

        public int UpdateUser(BaseUserModel model, Guid id)
        {
            int result = 0;
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }

                // Create a new Npgsql command
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "update_user_account";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_id, @in_user_name, @in_email, @in_role_id, @in_updated_by_user_id)";
                    model.UpdatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_id", id);
                    cmd.Parameters.AddWithValue("in_user_name", model.FullName);
                    cmd.Parameters.AddWithValue("in_email", model.Email);
                    cmd.Parameters.AddWithValue("in_role_id", model.RoleId);
                    cmd.Parameters.AddWithValue("in_updated_by_user_id", model.UpdatedById);

                    cmd.Parameters.Add("num_user_account_rows_affected", NpgsqlDbType.Integer).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("num_user_role_rows_affected", NpgsqlDbType.Integer).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    object userAccountRowsAffectedValue = cmd.Parameters["num_user_account_rows_affected"].Value;
                    object userRoleRowsAffectedValue = cmd.Parameters["num_user_role_rows_affected"].Value;

                    int numUserRoleRowsAffected = 0;
                    int numUserAccountRowsAffected = 0;

                    if (userRoleRowsAffectedValue != null && (int)userRoleRowsAffectedValue > 0)
                    {
                        numUserRoleRowsAffected = (int)userRoleRowsAffectedValue;
                    }

                    if (userAccountRowsAffectedValue != null && (int)userAccountRowsAffectedValue > 0)
                    {
                        numUserAccountRowsAffected = (int)userAccountRowsAffectedValue;
                    }
                    
                    if (numUserRoleRowsAffected > 0 || numUserAccountRowsAffected > 0)
                    {
                        result = numUserRoleRowsAffected + numUserAccountRowsAffected;
                    }
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public int DeleteUser(Guid id)
        {
            int result = 0;
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }

                // Create a new Npgsql command
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "delete_user_account";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_id)";
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_id", id);

                    result = (int)cmd.ExecuteScalar();
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        ~UserRepository()
        {
            _connection?.Close();
        }
    }
}
