using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class NotificationEntity
    {
        [Key]
        public Guid NotificationId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Content { get; set; }
        public UserEntity Receiver { get; set; }
    }
}
