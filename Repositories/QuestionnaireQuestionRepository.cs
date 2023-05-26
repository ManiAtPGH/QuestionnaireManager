using Newtonsoft.Json;
using Npgsql;
using NpgsqlTypes;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.HelperServices;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using System.Data;

namespace QuestionnaireManagerPOC.Repositories
{
    public class QuestionnaireQuestionRepository : IQuestionnaireQuestionRepository
    {
        private readonly NpgsqlConnection _connection;

        public QuestionnaireQuestionRepository()
        {
            _connection = Database.Instance.GetConnection();
        }

        public bool CheckForDuplication(Guid questionnaire_id, string question)
        {
            return false;
        }

        public int AddQuestionnaireQuestion(QuestionnaireQuestionDto model)
        {
            int result = 0;

            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var command = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "insert_questionnaire_question_and_answer";

                    command.Connection = _connection;

                    // Set the search path and function name
                    command.CommandText = $"SET search_path TO {schemaName};";
                    command.CommandText += $"SELECT {functionName}(@in_questionnaire_id, @in_question, @in_type_id, " +
                        $"@in_score, @in_created_by_user_id, @in_answers);";
                    model.CreatedById = Guid.Parse("26a19a40-8532-4a57-a263-f568f50a3804");

                    // Add parameters to the command
                    command.Parameters.AddWithValue("in_questionnaire_id", model.QuestionnaireId);
                    command.Parameters.AddWithValue("in_question", model.QuestionnaireQuestion);
                    command.Parameters.AddWithValue("in_type_id", model.TypeId);
                    command.Parameters.AddWithValue("in_score", model.Score);
                    command.Parameters.AddWithValue("in_created_by_user_id", model.CreatedById);

                    if (model.QuestionnaireQuestionAnswers != null && model.QuestionnaireQuestionAnswers.Count > 0)
                    {
                        var answerArray = model.QuestionnaireQuestionAnswers.Select(x =>
                        new
                        {
                            answer = x.AnswerValue,
                            created_by_id = model.CreatedById,
                            updated_by_id = model.UpdatedById
                        }).ToArray();

                        var value = JsonConvert.SerializeObject(answerArray);
                        command.Parameters.Add(new NpgsqlParameter("in_answers", NpgsqlDbType.Json)
                        {
                            Value = value
                        });
                    }
                    else
                    {
                        command.Parameters.AddWithValue("in_answers", DBNull.Value);
                    }
                    result = (int)command.ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                _connection?.Close();
            }
            return result;
        }

        public int DeleteQuestionnaireQuestion(Guid id)
        {
            throw new NotImplementedException();
        }

        public QuestionnaireQuestionDto GetQuestionnaireQuestion(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<QuestionnaireQuestionDto> GetQuestionnaireQuestions()
        {
            List<QuestionnaireQuestionDto> model = new List<QuestionnaireQuestionDto>();
            try
            {
                if (_connection.FullState != ConnectionState.Open)
                {
                    _connection?.Open();
                }
                using (var cmd = new NpgsqlCommand())
                {
                    string schemaName = "public";
                    string functionName = "get_questionnaire_Questions";

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
                                model.Add(new QuestionnaireQuestionDto()
                                {
                                    //UserId = reader.GetGuid(0),
                                    //FullName = reader.GetString(1),
                                    //Email = reader.GetString(2),
                                    //RoleId = reader.GetGuid(3),
                                    //Role = reader.GetString(4),
                                    //CreatedDate = reader.GetDateTime(5),
                                    //CreatedById = reader.GetGuid(7)
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

        public int UpdateQuestionnaireQuestion(QuestionnaireQuestionDto model, Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
