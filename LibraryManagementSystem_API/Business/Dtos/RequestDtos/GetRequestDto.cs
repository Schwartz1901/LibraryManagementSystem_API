namespace LibraryManagementSystem_API.Business.Dtos.RequestDtos
{
    public class GetRequestDto
    {
        public Guid Id { get; set; }

        public string? Username { get; set; }

        public Guid? BookId { get; set; }

        public string? BookName { get; set; }

        public string? Type { get; set; }

        public string? Email { get; set; }

        public DateTime? Date {  get; set; }

        public string? Status { get; set; }
    }
}
