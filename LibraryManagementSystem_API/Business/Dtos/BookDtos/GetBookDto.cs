using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Dtos.BookDtos
{
    public class GetBookDto
    {
        public Guid BookId { get; set; }
        public AuthorEntity Author { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public bool Availability { get; set; }
        public string Edition { get; set; }
        public string ImageUrl { get; set; }
    }
}
