using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetList();

        public Task<UserEntity> GetById();

        public Task<UserEntity> GetByUsername(string username);

        public void Post(UserEntity entity);
    }
}
