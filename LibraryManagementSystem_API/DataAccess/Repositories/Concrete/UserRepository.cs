using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class UserRepository : UserEntity , IUserRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly DbSet<UserEntity> dbSet;

        public UserRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            dbSet = dbContext.Set<UserEntity>();
            
        }

        public Task<UserEntity> GetList() 
        { 
            return null;
        
        }

        public Task<UserEntity> GetById() 
        { 
            return null; 
        
        }

        public async Task<UserEntity> GetByUsername(string username) 
        {
            if (string.IsNullOrEmpty(username)) return null;
            var user = await dbSet.FirstOrDefaultAsync(u => username == u.UserName);
            return user;
        }

        public void Post(UserEntity userEntity)
        {
            var user = dbSet.AddAsync(userEntity);
            _dbContext.SaveChanges();
        }

    }
}
