using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.DataInterfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUser(Guid id);
        UserModel GetUser(string emaiIid);
        int AddUser(BaseUserModel model);
        int UpdateUser(BaseUserModel model, Guid id);
        int DeleteUser(Guid id);

    }
}
