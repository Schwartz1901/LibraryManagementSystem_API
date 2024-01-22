using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class AuthorEntity
    {
        [Key]
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }
    }
}
