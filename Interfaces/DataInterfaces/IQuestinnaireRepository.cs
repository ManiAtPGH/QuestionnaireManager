using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;

namespace QuestionnaireManagerPOC.Interfaces.DataInterfaces
{
    public interface IQuestionnaireRepository
    {
        List<QuestionnaireDto> GetQuestionnaire();
        QuestionnaireDto GetQuestionnaire(Guid id);
        int AddQuestionnaire(QuestionnaireDto model);
        int UpdateQuestionnaire(QuestionnaireDto model, Guid id);
        int DeleteQuestionnaire(Guid id);
    }
}
