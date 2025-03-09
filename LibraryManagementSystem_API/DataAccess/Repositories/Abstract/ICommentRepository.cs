namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface ICommentRepository
    {
        public Task<List<CommentEntity>> GetComments(string bookId);

        public void PostComment( string id, CommentEntity comment);

        public float AverageRatingOfBook(string id);
    }
}
