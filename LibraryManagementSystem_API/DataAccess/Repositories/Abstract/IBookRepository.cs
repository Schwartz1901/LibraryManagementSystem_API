using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IBookRepository
    {
        public Task<BookEntity> GetById(string id);
        public Task<BookEntity> GetByName(string name);

        public void Post(BookEntity bookEntity, ImageEntity image);
        public Task<BookEntity> PutBook(string id, PostBookDto postBookDto, ImageEntity image, Epub epub);
        public Task<List<BookEntity>> GetBooks();

        public void Delete(string id);

        public void UpdateAverageRating(string id, float rating);

        public Task<Epub> GetEpub(string id);

        public void AddEpub(string id, Epub epub);

        public Task<List<BookEntity>> GetRecommendByRatings();
        
    }
}
