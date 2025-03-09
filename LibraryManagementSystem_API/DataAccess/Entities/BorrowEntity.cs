using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class BorrowEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? BookName { get; set; }
        public Guid BookId { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }

 
    }
}
