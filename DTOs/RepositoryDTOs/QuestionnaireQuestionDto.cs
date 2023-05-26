namespace QuestionnaireManagerPOC.DTOs.RepositoryDTOs
{
    public class QuestionnaireQuestionDto
    {
        public Guid QuestionnaireId { get; set; }
        public Guid QuestionnaireQuestionId { get; set; }
        public string QuestionnaireQuestion { get; set; }
        public int Score { get; set; }
        public Guid TypeId { get; set; }
        public List<QuestionnaireQuestionAnswerDto>? QuestionnaireQuestionAnswers { get; set; }

        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }

    public class QuestionnaireQuestionAnswerDto
    {
        public string AnswerValue { get; set; }
        public Guid AnswerId { get; set; }
        
        public Guid CreatedById { get; set; }
        public Guid UpdatedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
