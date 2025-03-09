using LibraryManagementSystem_API.DataAccess.Entities;
using Nest;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IELasticSearchServices
    {
        public Task IndexDataAsync();
        public Task<ISearchResponse<BookEntity>> SearchAsync(string query);
    }
}
