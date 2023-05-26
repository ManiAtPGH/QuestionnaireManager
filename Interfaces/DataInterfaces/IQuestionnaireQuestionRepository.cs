using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;

namespace QuestionnaireManagerPOC.Interfaces.DataInterfaces
{
    public interface IQuestionnaireQuestionRepository
    {
        IEnumerable<QuestionnaireQuestionDto> GetQuestionnaireQuestions();
        QuestionnaireQuestionDto GetQuestionnaireQuestion(Guid id);
        int AddQuestionnaireQuestion(QuestionnaireQuestionDto model);
        int UpdateQuestionnaireQuestion(QuestionnaireQuestionDto model, Guid id);
        int DeleteQuestionnaireQuestion(Guid id);
        bool CheckForDuplication(Guid questionnaire_id, string question);

    }
}
