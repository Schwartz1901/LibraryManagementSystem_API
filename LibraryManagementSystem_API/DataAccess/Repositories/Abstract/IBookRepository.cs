using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IBookRepository
    {
        public Task<BookEntity> GetById(Guid id);
        public Task<BookEntity> GetByName(string name);

        public Task<BookEntity> Post();
    }
}
