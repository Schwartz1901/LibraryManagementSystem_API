using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class RequestEntity
    {
        [Key]
        public Guid RequestId { get; set; }

        public string RequestType { get; set; }

        public string? BookName { get; set; }

        public Guid? BookId { get; set; }

        public string? UserRequest { get; set; }

        public string Status { get; set; }

        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ProcessedDate { get; set; }
        public DateTime DueDate { get; set; }

        public string? Reason { get; set; }


    }
}
