using Npgsql;
using NpgsqlTypes;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.HelperServices;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using System.Data;

namespace QuestionnaireManagerPOC.Repositories
{
    public class UserQuestionnaireRepository : IUserQuestionnaireRepository
    {
        private readonly NpgsqlConnection _connection;

        public UserQuestionnaireRepository()
        {
            _connection = Database.Instance.GetConnection();
        }
        public int AddUserQuestionnaire(UserQuestionnaireDto model)
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
                    string functionName = "insert_user_questionnaire_association";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_id, @in_questionnaire_id, " +
                        $"@in_start_date, @in_expiry_date, @in_due_duration, @in_created_by_id)";
                    model.CreatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_id", model.UserId);
                    cmd.Parameters.AddWithValue("in_questionnaire_id", model.QuestionnaireId);
                    cmd.Parameters.AddWithValue("in_start_date", model.StartDate);
                    cmd.Parameters.AddWithValue("in_expiry_date", model.ExpiryDate);
                    cmd.Parameters.AddWithValue("in_created_by_id", model.CreatedById);
                    cmd.Parameters.AddWithValue("in_due_duration", model.DueDuration);

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
        public UserQuestionnaireDto GetUserQuestionnaire(Guid UserQuestionnaireId)
        {
            UserQuestionnaireDto model = new UserQuestionnaireDto();

            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_user_questionnaire";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT * FROM {functionName}(@in_user_questionnaire_id)";

                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_questionnaire_id", UserQuestionnaireId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.UserQuestionnaireId = reader.GetGuid(0);
                                model.UserName = reader.GetString(1);
                                model.QuestionnaireName = reader.GetString(2);
                                model.StartDate = reader.GetDateTime(3);
                                model.ExpiryDate = reader.GetDateTime(4);
                                model.DueDuration = reader.GetInt32(5);
                                model.UserId = reader.GetGuid(6);
                                model.QuestionnaireId = reader.GetGuid(7);
                                model.CreatedDate = reader.GetDateTime(8);
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
        public IEnumerable<UserQuestionnaireDto> GetUserQuestionnaires(Guid UserId)
        {
            List<UserQuestionnaireDto> model = new List<UserQuestionnaireDto>();
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_user_questionnaires";
                    cmd.Connection = _connection;
                    cmd.CommandText = $"SET search_path To {schemaName};";
                    cmd.CommandText = $"SELECT * FROM {functionName}(@in_user_id)";
                    cmd.Parameters.AddWithValue("in_user_id", UserId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                model.Add(new UserQuestionnaireDto()
                                {
                                    UserQuestionnaireId = reader.GetGuid(0),
                                    UserName = reader.GetString(1),
                                    QuestionnaireName = reader.GetString(2),
                                    StartDate = reader.GetDateTime(3),
                                    ExpiryDate = reader.GetDateTime(4),
                                    DueDuration = reader.GetInt32(5),
                                    UserId = reader.GetGuid(6),
                                    QuestionnaireId = reader.GetGuid(7),
                                    CreatedDate = reader.GetDateTime(8)
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
        public IEnumerable<UserQuestionnaireDto> GetUsersQuestionnaires()
        {
            List<UserQuestionnaireDto> model = new List<UserQuestionnaireDto>();
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_users_questionnaires";

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
                                model.Add(new UserQuestionnaireDto()
                                {
                                    UserQuestionnaireId = reader.GetGuid(0),
                                    UserName = reader.GetString(1),
                                    QuestionnaireName = reader.GetString(2),
                                    StartDate = reader.GetDateTime(3),
                                    ExpiryDate = reader.GetDateTime(4),
                                    DueDuration = reader.GetInt32(5),
                                    UserId = reader.GetGuid(6),
                                    QuestionnaireId = reader.GetGuid(7),
                                    CreatedDate = reader.GetDateTime(8)
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
        public int UpdateUserQuestionnaire(UserQuestionnaireDto model, Guid id)
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
                    string functionName = "update_user_questionnaire_association";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_questionnaire_id,@in_user_id, @in_questionnaire_id, " +
                        $"@in_start_date, @in_expiry_date, @in_due_duration, @in_updated_by_id)";
                    model.UpdatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_questionnaire_id", model.UserQuestionnaireId);
                    cmd.Parameters.AddWithValue("in_user_id", model.UserId);
                    cmd.Parameters.AddWithValue("in_questionnaire_id", model.QuestionnaireId);
                    cmd.Parameters.AddWithValue("in_start_date", model.StartDate);
                    cmd.Parameters.AddWithValue("in_expiry_date", model.ExpiryDate);
                    cmd.Parameters.AddWithValue("in_due_duration", model.DueDuration);
                    cmd.Parameters.AddWithValue("in_updated_by_id", model.UpdatedById);

                    cmd.Parameters.Add("rows_affected", NpgsqlDbType.Integer).Direction = ParameterDirection.Output;
                    cmd.ExecuteScalar();

                    object RowsAffectedValue = cmd.Parameters["rows_affected"].Value;

                    int RowsAffected = 0;

                    RowsAffected = Convert.ToInt32(RowsAffectedValue);

                    if (RowsAffectedValue != null && RowsAffectedValue is object[] AffectedArray)
                    {
                        RowsAffected = (int)AffectedArray[0];
                    }

                    result = RowsAffected > 0 ? RowsAffected : 0;
                }
                _connection?.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public int DeleteUserQuestionnaire(Guid id)
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
                    string functionName = "delete_user_questionnaire";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_user_questionnaire_id)";
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_user_questionnaire_id", id);

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
    }
}
