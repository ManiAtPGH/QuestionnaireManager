using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface IQuestionnaireService
    {
        IEnumerable<QuestionnaireModel> GetQuestionnaire();
        QuestionnaireModel GetQuestionnaire(Guid id);
        int AddQuestionnaire(BaseQuestionnaireModel model);
        int UpdateQuestionnaire(BaseQuestionnaireModel model, Guid id);
        int DeleteQuestionnaire(Guid id);
    }
}
