using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        public Task<List<UserEntity>> GetUsers();

        public Task<UserEntity> GetById();

        public Task<UserEntity> GetUserByName(string username);

        public Task<UserEntity> GetUserByRefreshToken(string refreshToken);

        public Task<UserEntity> PutUser(UserEntity user);

        public void Post(UserEntity entity);

    }
}
