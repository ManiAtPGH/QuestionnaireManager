using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;

namespace QuestionnaireManagerPOC.Interfaces.DataInterfaces
{
    public interface IUserQuestionnaireRepository
    {
        IEnumerable<UserQuestionnaireDto> GetUsersQuestionnaires();
        IEnumerable<UserQuestionnaireDto> GetUserQuestionnaires(Guid UserId);
        UserQuestionnaireDto GetUserQuestionnaire(Guid UserQuestionnaireId);
        int AddUserQuestionnaire(UserQuestionnaireDto model);
        int UpdateUserQuestionnaire(UserQuestionnaireDto model, Guid id);
        int DeleteUserQuestionnaire(Guid id);
    }
}
