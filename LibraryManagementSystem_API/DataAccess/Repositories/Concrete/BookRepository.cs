using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class BookRepository : BookEntity , IBookRepository
    {
        protected DbSet<BookEntity> dbSet;

        public BookRepository(LibraryDbContext dbContext) 
        {
            dbSet = dbContext.Set<BookEntity>();
        }

        public Task<BookEntity> GetById(Guid id)
        {
            var bookEntity = dbSet.FirstOrDefaultAsync(book => book.BookId == id);
            return bookEntity;
        }
        public Task<BookEntity> GetByName(string name)
        {
            var bookEntity = dbSet.FirstOrDefaultAsync(book => book.Title == name);
            return bookEntity;
        }
        public async Task<BookEntity> Post()
        {
            BookEntity bookEntity = new BookEntity { Title = "book" };

            return bookEntity;
        }
    }
}
