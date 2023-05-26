using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUser(Guid id);
        UserModel GetUser(string id);
        int AddUser(BaseUserModel model);
        int UpdateUser(BaseUserModel model, Guid id);
        int DeleteUser(Guid id);
    }
}
