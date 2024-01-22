using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class RequestEntity
    {
        [Key]
        public Guid RequestId { get; set; }
        public UserEntity User { get; set; }
        public BookEntity Book { get; set; }
    }
}
