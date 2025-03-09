using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IBorrowRepository
    {
        public Task<List<BorrowEntity>> GetBorrows(string id);

        public Task<BorrowEntity> PostBorrow(BorrowEntity borrow);
    }
}
