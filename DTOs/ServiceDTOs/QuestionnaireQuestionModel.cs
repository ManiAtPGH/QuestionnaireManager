namespace QuestionnaireManagerPOC.DTOs.ServiceDTOs
{
    public class AddQuestionnaireQuestionModel
    {
        public Guid QuestionnaireId { get; set; }
        public string QuestionnaireQuestion { get; set; }
        public int Score { get; set; }
        public Guid TypeId { get; set; }
        public List<AddQuestionnaireQuestionAnswerModel>? AddQuestionnaireQuestionAnswers { get; set; }
    }

    public class AddQuestionnaireQuestionAnswerModel
    {
        public string AnswerValue { get; set; }
    }

    public class QuestionnaireQuestionModel : AddQuestionnaireQuestionModel
    {
        public Guid QuestionnaireQuestionId { get; set; }
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<QuestionnaireQuestionAnswerModel>? QuestionnaireQuestionAnswers { get; set; }
    }

    public class QuestionnaireQuestionAnswerModel : AddQuestionnaireQuestionAnswerModel
    {
        public Guid AnswerId { get; set; }
    }
}
