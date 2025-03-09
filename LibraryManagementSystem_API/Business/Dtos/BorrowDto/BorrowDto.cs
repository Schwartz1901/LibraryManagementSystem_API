namespace LibraryManagementSystem_API.Business.Dtos.BorrowDto
{
    public class BorrowDto
    {
        public Guid Id { get; set; } // BookId
        public string? BookName { get; set; }
        public DateTime BorrowDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
