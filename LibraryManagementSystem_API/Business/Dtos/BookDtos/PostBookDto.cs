using LibraryManagementSystem_API.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem_API.Business.Dtos.BookDtos
{
    public class PostBookDto
    {
        public string Name { get; set; }

        public string? Author { get; set; }

        public string? Category { get; set; }

        public int? Stock {  get; set; }

        public string? Position { get; set; }

        public IFormFile? Image { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Length { get; set; }

        public string Publisher { get; set; }

        public string? Synopsis { get; set; }

        public IFormFile Ebook { get; set; }

    }
}
