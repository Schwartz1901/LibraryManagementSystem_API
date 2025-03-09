using LibraryManagementSystem_API.Business.Dtos.BorrowDto;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IBorrowServices
    {
        public Task<List<BorrowDto>> GetBorrows(string username);

        public Task<BorrowEntity> PostBorrow(BorrowEntity borrow);
    }
}
