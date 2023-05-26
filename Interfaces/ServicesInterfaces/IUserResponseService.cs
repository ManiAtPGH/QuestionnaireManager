using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface IUserResponseService
    {
        IEnumerable<UserResponseModel> GetUsersResponses();
        IEnumerable<UserResponseModel> GetUserResponses(string UserId);
        UserResponseModel GetUserResponse(string UserResponseId);
        UserResponseModel AddUserResponse(UserResponseModel model);
    }
}
