namespace LibraryManagementSystem_API.DataAccess.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public byte[]? Data { get; set; }
        public string? ContentType { get; set; }
    }
}
