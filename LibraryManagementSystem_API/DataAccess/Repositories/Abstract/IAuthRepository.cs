using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IAuthRepository
    {
        public string CreateAccessToken(UserEntity user);

        public string CreateRefreshToken();
    }
}
