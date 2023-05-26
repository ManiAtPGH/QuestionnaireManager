namespace QuestionnaireManagerPOC.DTOs.ServiceDTOs
{
    public class BasicUserQuestionnaireModel
    {
        public Guid UserId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int DueDuration { get; set; }
    }

    public class UserQuestionnaireModel : BasicUserQuestionnaireModel
    {
        public Guid UserQuestionnaireId { get; set; }
        public string? UserName { get; set; }
        public string? QuestionnaireName { get; set; }
        public string? Status { get; set; }
        public int Score { get; set; }
    }

}
