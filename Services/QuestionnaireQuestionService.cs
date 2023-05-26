using QuestionnaireManagerPOC.DTOs.RepositoryDTOs;
using QuestionnaireManagerPOC.DTOs.ServiceDTOs;
using QuestionnaireManagerPOC.Interfaces.DataInterfaces;
using QuestionnaireManagerPOC.Interfaces.ServicesInterfaces;

namespace QuestionnaireManagerPOC.Services
{
    public class QuestionnaireQuestionService : IQuestionnaireQuestionService
    {
        private readonly IQuestionnaireQuestionRepository _questionnaireQuestionRepository;

        public QuestionnaireQuestionService(IQuestionnaireQuestionRepository questionnaireQuestionRepository)
        {
            _questionnaireQuestionRepository = questionnaireQuestionRepository;
        }

        public int AddQuestionnaireQuestion(AddQuestionnaireQuestionModel model)
        {
            var answerList = new List<QuestionnaireQuestionAnswerDto>();

            if (model.AddQuestionnaireQuestionAnswers != null)
            {
                foreach (var item in model.AddQuestionnaireQuestionAnswers)
                {
                    answerList.Add(new QuestionnaireQuestionAnswerDto { AnswerValue = item.AnswerValue });
                }
            }
            var dModel = new QuestionnaireQuestionDto() 
            {
                QuestionnaireId = model.QuestionnaireId,
                QuestionnaireQuestion = model.QuestionnaireQuestion,
                Score = model.Score,
                TypeId = model.TypeId,
                QuestionnaireQuestionAnswers = answerList
            };

          var result = _questionnaireQuestionRepository.AddQuestionnaireQuestion(dModel);
            return result;
        }

        public QuestionnaireQuestionModel GetQuestionnaireQuestion(Guid id)
        {
            var data = _questionnaireQuestionRepository.GetQuestionnaireQuestion(id);
            var ddata = new QuestionnaireQuestionModel() { };

            return ddata;
        }

        public IEnumerable<QuestionnaireQuestionModel> GetQuestionnaireQuestions()
        {
            var data = _questionnaireQuestionRepository.GetQuestionnaireQuestions();
            var ddata = new List<QuestionnaireQuestionModel>() { };

            return ddata;
        }

        public int UpdateQuestionnaireQuestion(AddQuestionnaireQuestionModel model, Guid id)
        {
            var dModel = new QuestionnaireQuestionDto() { };

            var data = _questionnaireQuestionRepository.UpdateQuestionnaireQuestion(dModel, id);
            return data;
        }

        public int DeleteQuestionnaireQuestion(Guid id)
        {
            var data = _questionnaireQuestionRepository.DeleteQuestionnaireQuestion(id);
            return data;
        }

        public bool CheckForDuplication(Guid questionnaire_id, string question)
        {
           return _questionnaireQuestionRepository.CheckForDuplication(questionnaire_id, question);
        }
    }
}
