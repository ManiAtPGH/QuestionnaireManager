﻿namespace QuestionnaireManagerPOC.DTOs.RepositoryDTOs
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string? Role { get; set; }
        public string Password { get; set; }
    }
}
