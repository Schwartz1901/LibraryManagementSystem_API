using LibraryManagementSystem_API.Business.Dtos.BookDtos;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IBookServices
    {
        public Task<GetBookDto> GetById(Guid guid);
        public Task<GetBookDto> GetByName(string name);

        public Task<PostBookDto> Post(PostBookDto postBookDto);
    }
}
