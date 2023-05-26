namespace QuestionnaireManagerPOC.DTOs.RepositoryDTOs
{
    public class QuestionnaireDto
    {
        public Guid QuestionnaireId { get; set; }
        public string QuestionnaireName { get; set; }
        public string Status { get; set; }
        public Guid StatusId { get; set; }
        public string? Description { get; set; }
        public int AssignedCount { get; set; }
        public Guid CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
