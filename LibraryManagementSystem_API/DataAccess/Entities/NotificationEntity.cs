using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class NotificationEntity
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Text { get; set; }

        public DateTime CreateAt { get; set; }

        public bool Read { get; set; }
    }
}
