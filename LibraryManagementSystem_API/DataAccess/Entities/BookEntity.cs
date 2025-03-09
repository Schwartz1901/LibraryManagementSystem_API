using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class BookEntity
    {
        [Key]
        public Guid BookId { get; set; }
        public string? Name { get; set; }
        public string? Author { get; set; }
        public bool? Availability { get; set; }
        public int? Stock {  get; set; }
        public int? ImageId { get; set; }
        public ImageEntity? Image { get; set; }
        public string? Category {  get; set; }
        public string? Description { get; set; }
        public int? Length { get; set; }

        public Epub? EpubVersion { get; set; }
        public string Position { get; set; }
        public string? Publisher { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public float? Rating { get; set; }
    }

    public class Epub
    {
        public int Id { get; set; }
        public byte[] FileContent { get; set; }

        public string FileName { get; set; }
    }
        
}
