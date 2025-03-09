namespace LibraryManagementSystem_API.Business.Dtos.RequestDtos
{
    public class PastTransactionDto
    {
        public string Id { get; set; } // Request ID

        public string? BookName { get; set; }

        public string? Type { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ProcessedDate { get; set; }
    }
}
