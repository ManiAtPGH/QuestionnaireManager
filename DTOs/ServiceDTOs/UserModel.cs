using System.Text.Json.Serialization;

namespace QuestionnaireManagerPOC.DTOs.ServiceDTOs
{
    public class BaseUserModel
    {
        [JsonIgnore]
        public Guid CreatedById { get; set; }

        [JsonIgnore]
        public Guid UpdatedById { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }
    }
    public class UserModel : BaseUserModel
    {

        public string? Role { get; set; }

        [JsonIgnore]
        public string? Password { get; set; }

        public Guid? UserId { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
