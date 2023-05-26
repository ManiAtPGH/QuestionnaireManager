using QuestionnaireManagerPOC.DTOs.ServiceDTOs;

namespace QuestionnaireManagerPOC.Interfaces.ServicesInterfaces
{
    public interface ILoginService
    {
       bool Login(string userName, string password);
    }
}
