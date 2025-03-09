using Elastic.Clients.Elasticsearch;
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

        public Task<List<UserEntity>> GetUsers() 
        { 
            var userList = dbSet.ToListAsync();
            return userList;
        
        }


        public Task<UserEntity> GetById() 
        { 
            return null; 
        
        }

        public async Task<UserEntity> GetUserByRefreshToken(string refreshToken)
        {
            var user = await dbSet.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

            return user;
        }

        public async Task<UserEntity> PutUser(UserEntity user)
        {
            dbSet.Update(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> GetUserByName(string username) 
        {
            if (string.IsNullOrEmpty(username)) return null;
            var user = await dbSet.FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }

        public void Post(UserEntity userEntity)
        {
            var user = dbSet.AddAsync(userEntity);
            _dbContext.SaveChanges();
        }

    }
}
