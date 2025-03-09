using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DoB { get; set; }
        public string? Address { get; set; }

        public int UserScore { get; set; }

        public string? Role {  get; set; }

        public DateTime? CreationDate { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpireTime { get; set; }

        public string? Bio { get; set; }
    }
}
