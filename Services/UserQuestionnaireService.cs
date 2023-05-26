using AutoMapper;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

namespace QuestionnaireManagerPOC.Services
{
    public class UserQuestionnaireService : IUserQuestionnaireService
    {
        private readonly IUserQuestionnaireRepository _userQuestionnaireRepository;

        public UserQuestionnaireService(IUserQuestionnaireRepository userQuestionnaireRepository)
        {
            _userQuestionnaireRepository = userQuestionnaireRepository;
        }

        public int AddUserQuestionnaire(BasicUserQuestionnaireModel model)
        {
            var dmodel = new UserQuestionnaireDto()
            {
                QuestionnaireId = model.QuestionnaireId,
                UserId = model.UserId,
                StartDate = model.StartDate,
                ExpiryDate = model.ExpiryDate,
                DueDuration = model.DueDuration
            };

            var data = _userQuestionnaireRepository.AddUserQuestionnaire(dmodel);
            return data;
        }


        public UserQuestionnaireModel GetUserQuestionnaire(Guid UserQuestionnaireId)
        {
            var data = _userQuestionnaireRepository.GetUserQuestionnaire(UserQuestionnaireId);
            
            
                var userQuestionnaireRecord = new UserQuestionnaireModel()
                {
                    UserQuestionnaireId = data.UserQuestionnaireId,
                    UserId = data.UserId,
                    UserName = data.UserName,
                    QuestionnaireId = data.QuestionnaireId,
                    QuestionnaireName = data.QuestionnaireName,
                    DueDuration = data.DueDuration,
                    StartDate = data.StartDate,
                    ExpiryDate = data.ExpiryDate
                };
             

            return userQuestionnaireRecord;
        }

        public IEnumerable<UserQuestionnaireModel> GetUsersQuestionnaires()
        {
            var data = _userQuestionnaireRepository.GetUsersQuestionnaires();
            var convertedDataList = new List<UserQuestionnaireModel>();
            foreach (var userQuestionnaire in data)
            {
                var userQuestionnaireRecord = new UserQuestionnaireModel()
                {
                    UserQuestionnaireId = userQuestionnaire.UserQuestionnaireId,
                    UserId = userQuestionnaire.UserId,
                    UserName = userQuestionnaire.UserName,
                    QuestionnaireId = userQuestionnaire.QuestionnaireId,
                    QuestionnaireName = userQuestionnaire.QuestionnaireName,
                    DueDuration = userQuestionnaire.DueDuration,
                    StartDate = userQuestionnaire.StartDate,
                    ExpiryDate = userQuestionnaire.ExpiryDate
                };
                convertedDataList.Add(userQuestionnaireRecord);
            }

            return convertedDataList;
        }

        public IEnumerable<UserQuestionnaireModel> GetUserQuestionnaires(Guid UserId)
        {
            var data = _userQuestionnaireRepository.GetUserQuestionnaires(UserId);
            var convertedDataList = new List<UserQuestionnaireModel>();
            foreach (var userQuestionnaire in data)
            {
                var userQuestionnaireRecord = new UserQuestionnaireModel()
                {
                    UserQuestionnaireId = userQuestionnaire.UserQuestionnaireId,
                    UserId = userQuestionnaire.UserId,
                    UserName = userQuestionnaire.UserName,
                    QuestionnaireId = userQuestionnaire.QuestionnaireId,
                    QuestionnaireName = userQuestionnaire.QuestionnaireName,
                    DueDuration = userQuestionnaire.DueDuration,
                    StartDate = userQuestionnaire.StartDate,
                    ExpiryDate = userQuestionnaire.ExpiryDate
                };
                convertedDataList.Add(userQuestionnaireRecord);
            }
            return convertedDataList;


        }

        public int UpdateUserQuestionnaire(BasicUserQuestionnaireModel model, Guid id)
        {
            var dmodel = new UserQuestionnaireDto()
            {
                QuestionnaireId = model.QuestionnaireId,
                UserId = model.UserId,
                StartDate = model.StartDate,
                ExpiryDate = model.ExpiryDate,
                DueDuration = model.DueDuration
            };
            var data = _userQuestionnaireRepository.UpdateUserQuestionnaire(dmodel, id);
            return data;
        }


        public int DeleteUserQuestionnaire(Guid id)
        {
           int count = _userQuestionnaireRepository.DeleteUserQuestionnaire(id);
            return count;
        }
    }
}
