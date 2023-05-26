using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface IQuestionnaireQuestionService
    {
        IEnumerable<QuestionnaireQuestionModel> GetQuestionnaireQuestions();
        QuestionnaireQuestionModel GetQuestionnaireQuestion(Guid id);
        int AddQuestionnaireQuestion(AddQuestionnaireQuestionModel model);
        int UpdateQuestionnaireQuestion(AddQuestionnaireQuestionModel model, Guid id);
        int DeleteQuestionnaireQuestion(Guid id);
        bool CheckForDuplication(Guid questionnaire_id, string question);
    }
}
