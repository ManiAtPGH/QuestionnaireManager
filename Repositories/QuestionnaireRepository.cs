using Npgsql;
using NpgsqlTypes;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.HelperServices;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using System.Data;

namespace QuestionnaireManagerPOC.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {

        private readonly NpgsqlConnection _connection;

        public QuestionnaireRepository()
        {
            _connection = Database.Instance.GetConnection();
        }

        public int AddQuestionnaire(QuestionnaireDto model)
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
                    string functionName = "insert_questionnaire";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    if (Convert.ToBoolean(CheckDuplicateRecords(cmd, model.QuestionnaireName)))
                    {
                        return 0;
                    }
                    cmd.CommandText = $"SELECT {functionName}(@in_QuestionnaireName, @in_QuestionnaireDescription, @in_CreatedByUserId)";
                    model.CreatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_QuestionnaireName", model.QuestionnaireName);
                    cmd.Parameters.AddWithValue("in_QuestionnaireDescription", model.Description);
                    cmd.Parameters.AddWithValue("in_CreatedByUserId", model.CreatedById);
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

        public List<QuestionnaireDto> GetQuestionnaire()
        {
            List<QuestionnaireDto> model = new List<QuestionnaireDto>();
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_questionnaire";

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
                                model.Add(new QuestionnaireDto()
                                {
                                    QuestionnaireId = reader.GetGuid(0),
                                    QuestionnaireName = reader.GetString(1),
                                    Description = reader.GetString(2),
                                    CreatedById = reader.GetGuid(3),
                                    IsDeleted = reader.GetBoolean(5)
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

        public QuestionnaireDto GetQuestionnaire(Guid id)
        {
            QuestionnaireDto model = new QuestionnaireDto();

            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_questionnaire";

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
                                model.QuestionnaireId = reader.GetGuid(0);
                                model.QuestionnaireName = reader.GetString(1);
                                model.Description = reader.GetString(2);
                                model.CreatedById = reader.GetGuid(3);
                                model.IsDeleted = reader.GetBoolean(5);
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

        public int UpdateQuestionnaire(QuestionnaireDto model, Guid id)
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
                    string functionName = "update_questionnaire";

                    cmd.Connection = _connection;

                    cmd.CommandText = $"SET search_path TO {schemaName};";
                    cmd.CommandText = $"SELECT {functionName}(@in_questionnaire_id, @in_questionnaire_name" +
                        $", @in_description, @in_updated_by_user_id)";
                    model.UpdatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("in_questionnaire_id", id);
                    cmd.Parameters.AddWithValue("in_questionnaire_name", model.QuestionnaireName);
                    cmd.Parameters.AddWithValue("in_description", model.Description);
                    cmd.Parameters.AddWithValue("in_updated_by_user_id", model.UpdatedById);

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
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public int DeleteQuestionnaire(Guid id)
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
                    string functionName = "delete_Questionnaire";

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

        public int CheckDuplicateRecords(NpgsqlCommand command, string QuestionnaireName)
        {
            command.CommandText = $"SELECT check_for_duplicate_records(@in_QuestionnaireName)";
            command.Parameters.AddWithValue("in_QuestionnaireName", QuestionnaireName);

            int data = (int)command.ExecuteScalar();
            return data;
        }

        ~QuestionnaireRepository()
        {
            _connection?.Close();
        }
    }

}
