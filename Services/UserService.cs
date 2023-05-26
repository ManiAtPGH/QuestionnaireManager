using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

namespace QuestionnaireManagerPOC.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int AddUser(BaseUserModel model)
        {
            int result = _userRepository.AddUser(model);
            return result;
        }

        public int DeleteUser(Guid id)
        {
            int result = _userRepository.DeleteUser(id);
            return result;
        }

        public UserModel GetUser(Guid id)
        {
            UserModel user = _userRepository.GetUser(id);
            return user;
        }

        public UserModel GetUser(string emailId)
        {
            return _userRepository.GetUser(emailId);
        }

        public IEnumerable<UserModel> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public int UpdateUser(BaseUserModel model, Guid id)
        {
            int result = _userRepository.UpdateUser(model, id);
            return result;
        }
    }
}
