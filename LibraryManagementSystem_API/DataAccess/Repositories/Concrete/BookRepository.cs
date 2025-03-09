using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class BookRepository : BookEntity , IBookRepository
    {
        protected DbSet<BookEntity> dbSet;
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext) 
        {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<BookEntity>();
      
        }

        public async Task<BookEntity> GetById(string id)
        {
            var bookEntity = await dbSet.Include(b => b.Image).FirstOrDefaultAsync(book => book.BookId.ToString() == id);
            return bookEntity;
        }
        public Task<BookEntity> GetByName(string name)
        {
            var bookEntity = dbSet.FirstOrDefaultAsync(book => book.Name == name);
            return bookEntity;
        }
        public void Post(BookEntity bookEntity, ImageEntity image)
        {
            bookEntity.Image = image;
            var book = dbSet.AddAsync(bookEntity);
            _dbContext.SaveChanges();

        }

        public Task<List<BookEntity>> GetBooks()
        {
            var ListBookEntities = dbSet.Include(b => b.Image).ToListAsync();
            return ListBookEntities;
        }

        public async Task<BookEntity> PutBook(string id, PostBookDto postBookDto, ImageEntity image, Epub epub)
        {
            var book = await dbSet.FirstOrDefaultAsync(b => b.BookId.ToString() == id);

            book.Name = postBookDto.Name;
            book.Author = postBookDto.Author;
            book.Description = postBookDto.Synopsis;
            book.Category = postBookDto.Category;
            book.Image = image;
            book.Stock = postBookDto.Stock;
            book.Position = postBookDto.Position;
            book.Publisher = postBookDto.Publisher;
            book.Description = postBookDto.Synopsis;
            book.EpubVersion = epub;

            
            
            _dbContext.SaveChanges();

            return book;
        }

        public void Delete(string id)
        {
            var book = dbSet.FirstOrDefault(b => b.BookId.ToString() == id);
            if (book != null)
            {
                _dbContext.Remove(book);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateAverageRating(string id, float rating)
        {
            var book = dbSet.FirstOrDefault(b => b.BookId.ToString() == id);
            book.Rating = rating;
            _dbContext.SaveChanges();
        }

        /*public Task<List<BookEntity>> FilterBook(BookEntity bookEntity) { }*/


        public async Task<Epub> GetEpub(string id)
        {
            var book = await dbSet.Include(b => b.EpubVersion).FirstOrDefaultAsync(b => b.BookId.ToString() == id);
            var epub = book.EpubVersion;
            return epub;
        }

        public void AddEpub(string id, Epub epub)
        {
            var book = dbSet.Include(b => b.EpubVersion).FirstOrDefault(b => b.BookId.ToString() == id);

            book.EpubVersion = epub;

            _dbContext.SaveChanges();
        }

        public async Task<List<BookEntity>> GetRecommendByRatings()
        {
            var books = await dbSet.Include(b => b.Image)
                                    .OrderByDescending(b => b.Rating)
                                    .Take(6)
                                    .ToListAsync();
            return books;
        }
    }
}
