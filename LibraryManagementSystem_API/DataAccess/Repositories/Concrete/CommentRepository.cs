using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class CommentRepository : CommentEntity, ICommentRepository
    {
        protected DbSet<CommentEntity> dbSet;
        private readonly LibraryDbContext _dbContext;

        public CommentRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<CommentEntity>();
        }

        public async Task<List<CommentEntity>> GetComments(string bookId)
        {
            var comments = await dbSet.Where(c => c.BookId.ToString() == bookId).ToListAsync();

            return comments;
        }

        public void PostComment(string id, CommentEntity comment)
        {
            var userComment = dbSet.FirstOrDefault(c => (c.Username == comment.Username && id == c.BookId.ToString()));
            if (userComment == null)
            {
                comment.BookId = new Guid(id);
                dbSet.Add(comment);

            } else
            {
                userComment.Content = comment.Content;
                userComment.Rating = comment.Rating;
            }
            _dbContext.SaveChanges();

        }

        public float AverageRatingOfBook(string id)
        {
            var averageRating = dbSet.Where(b => b.BookId.ToString() == id).Average(c => c.Rating);
            return averageRating;
        }
    }
}
