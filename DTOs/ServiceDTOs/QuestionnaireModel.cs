namespace QuestionnaireManagerPOC.DTOs.ServiceDTOs
{
    public class BaseQuestionnaireModel
    {
        public string QuestionnaireName { get; set; }
        public string? Description { get; set; }
    }
    public class QuestionnaireModel : BaseQuestionnaireModel
    {
        public Guid QuestionnaireId { get; set; }
        public int AssignedCount { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
