using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IBookServices
    {
        public Task<GetBookDto> GetById(string id);
        public Task<StaffGetBookDto> StaffGetById(string id);
        public Task<GetBookDto> GetByName(string name);

        public BookResponse Post(PostBookDto postBookDto, ImageEntity image);

        public Task<PostBookDto> PutBook(string id, PostBookDto postBookDto, ImageEntity image, Epub epub);

        public Task<List<GetBookDto>> GetBooks();

        public BaseResponse DeleteBook(string id);

        public Task<EpubDto> GetEpub(string id);
        public void AddEpub(string id, Epub epub);

        public Task<List<GetBookDto>> GetRecommendByRatings();
    }
}
