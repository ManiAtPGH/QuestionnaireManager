using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;

namespace QuestionnaireManagerPOC.Interfaces.DataInterfaces
{
    public interface ILoginRepository
    {
        bool Login(string username, string password);
    }
}
