using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Dtos.BookDtos
{
    public class PostBookDto
    {
        public Guid BookId { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }
        public string ImageUrl { get; set; }

        public string Categories { get; set; }

        public string Description { get; set; }

        public string Position { get; set; }
    }
}
