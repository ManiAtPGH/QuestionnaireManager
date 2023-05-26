using AutoMapper;
using Npgsql;
using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

namespace QuestionnaireManagerPOC.Services
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;
        private readonly IMapper _mapper;

        public QuestionnaireService(IQuestionnaireRepository questionnaireRepository, IMapper mapper = null)
        {
            _questionnaireRepository = questionnaireRepository;
            _mapper = mapper;
        }

        public int AddQuestionnaire(BaseQuestionnaireModel model)
        {
            var destinationModel = _mapper.Map<BaseQuestionnaireModel, QuestionnaireDto>(model);
            return _questionnaireRepository.AddQuestionnaire(destinationModel);
        }

        public int DeleteQuestionnaire(Guid id)
        {
            return _questionnaireRepository.DeleteQuestionnaire(id); 
        }

        public IEnumerable<QuestionnaireModel> GetQuestionnaire()
        {
            var data = _questionnaireRepository.GetQuestionnaire();
            List<QuestionnaireModel> result = new List<QuestionnaireModel>();
            foreach (var item in data)
            {
                result.Add(new QuestionnaireModel()
                {
                    QuestionnaireId = item.QuestionnaireId,
                    QuestionnaireName = item.QuestionnaireName,
                    Description = item.Description,
                    IsDeleted = item.IsDeleted,
                    CreatedById = item.CreatedById,
                    UpdatedById = item.UpdatedById
                });

            }
            return result;
        }

        public QuestionnaireModel GetQuestionnaire(Guid id)
        {
            var data = _questionnaireRepository.GetQuestionnaire(id);
            QuestionnaireModel result = new QuestionnaireModel();
            result.QuestionnaireId = data.QuestionnaireId;
            result.QuestionnaireName = data.QuestionnaireName;
            result.Description = data.Description;
            result.IsDeleted = data.IsDeleted;
            result.CreatedById = data.CreatedById;
            result.UpdatedById = data.UpdatedById;
            return result;
        }

        public int UpdateQuestionnaire(BaseQuestionnaireModel model, Guid id)
        {
            var destinationModel = _mapper.Map<BaseQuestionnaireModel, QuestionnaireDto>(model);

            return _questionnaireRepository.UpdateQuestionnaire(destinationModel, id);
        }
    }
}
