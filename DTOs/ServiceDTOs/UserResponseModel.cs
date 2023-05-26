namespace QuestionnaireManagerPOC.DTOs.ServiceDTOs
{
    public class UserResponseModel
    {
        public Guid UserResponseId { get; set; }
        public Guid UeserQuestionnaireId { get; set; }
        public Guid QuestionnaireQestionId { get; set; }
        public object? Answer { get; set; }

        public List<QuestionnaireResponseAnswer>? QuestionnaireResponseAnswers { get; set; }
    }
    public class QuestionnaireResponseAnswer
    {
        public int AnswerId { get; set; }
        public string AnswerValue { get; set; }
    }
}
