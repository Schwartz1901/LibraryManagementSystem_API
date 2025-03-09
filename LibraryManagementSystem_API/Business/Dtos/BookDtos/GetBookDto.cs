using LibraryManagementSystem_API.DataAccess.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem_API.Business.Dtos.BookDtos
{
    public class GetBookDto
    {   
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public string? Category { get; set; }

        public int? Stock {  get; set; }

        public string? Position { get; set; }
        public ImageDto? Image { get; set; }
        public int? Length { get; set; }
        public string? Synopsis { get; set; }

        public bool? Availability { get; set; }
        public string? Publisher { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public int? Rating { get; set; }

        public int Score { get; set; }
    }
}
