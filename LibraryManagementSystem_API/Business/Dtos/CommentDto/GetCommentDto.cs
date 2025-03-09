namespace LibraryManagementSystem_API.Business.Dtos.CommentDto
{
    public class GetCommentDto
    {
        public string? Username { get; set; }
        public int? Star { get; set; }
        public string? Comment { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
