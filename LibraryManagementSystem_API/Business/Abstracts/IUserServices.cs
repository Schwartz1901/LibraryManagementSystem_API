using LibraryManagementSystem_API.Business.Dtos.UserDtos;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IUserServices
    {
        public Task<GetUserDto> GetUserByName(string userName);

        public Task<List<GetUserInfoDto>> GetUserInfo();

        public Task<GetUserDto> PutUser(string username, GetUserDto userDto);

        public Task<int> GetUserScore(string username);
    }
}
