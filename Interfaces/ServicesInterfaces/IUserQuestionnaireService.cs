using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface IUserQuestionnaireService
    {
        IEnumerable<UserQuestionnaireModel> GetUsersQuestionnaires();
        IEnumerable<UserQuestionnaireModel> GetUserQuestionnaires(Guid UserId);
        UserQuestionnaireModel GetUserQuestionnaire(Guid UserQuestionnaireId);
        int AddUserQuestionnaire(BasicUserQuestionnaireModel model);
        int UpdateUserQuestionnaire(BasicUserQuestionnaireModel model, Guid id);
        int DeleteUserQuestionnaire(Guid id);
    }
}
