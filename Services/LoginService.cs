using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

namespace QuestionnaireManagerPOC.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public bool Login(string userName, string password)
        {
            bool IsValidated = false;
            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
            {
                IsValidated = _loginRepository.Login(userName, password);
            }
            return IsValidated;
        }

    }
}
