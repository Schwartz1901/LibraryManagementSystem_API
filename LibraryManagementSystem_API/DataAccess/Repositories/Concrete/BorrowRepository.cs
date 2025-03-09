using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class BorrowRepository : BorrowEntity, IBorrowRepository
    {
        protected DbSet<BorrowEntity> dbSet;
        private readonly LibraryDbContext _dbContext;

        public BorrowRepository(LibraryDbContext dbContext)
        {
            dbSet = dbContext.Set<BorrowEntity>();
            _dbContext = dbContext;
        }

        public async Task<List<BorrowEntity>> GetBorrows(string username)
        {
            var result = await dbSet.Where(b => b.Username == username).ToListAsync<BorrowEntity>();

            return result;
        }

        public async Task<BorrowEntity> PostBorrow(BorrowEntity borrow)
        {
            var result = await dbSet.AddAsync(borrow);
            _dbContext.SaveChanges();

            return borrow;
        }
    }
}
