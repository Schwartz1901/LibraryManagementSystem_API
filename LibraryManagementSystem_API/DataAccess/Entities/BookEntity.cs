using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class BookEntity
    {
        [Key]
        public Guid BookId { get; set; }
        public AuthorEntity Author { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }
        public string ImageUrl { get; set; }

        public string Categories {  get; set; }

        public string Description { get; set; }

        public string Position { get; set; }
    }
}
