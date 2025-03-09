using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.BorrowDto;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;

namespace LibraryManagementSystem_API.Business.Services
{
    public class BorrowServices : IBorrowServices
    {
        private readonly IBorrowRepository _borrowRepository;
        private readonly IMapper _mapper;
        public BorrowServices(IBorrowRepository borrowRepository, IMapper mapper) 
        {
            _borrowRepository = borrowRepository;
            _mapper = mapper;
        }
        public async Task<List<BorrowDto>> GetBorrows(string id)
        {
            var results = await _borrowRepository.GetBorrows(id);
            return _mapper.Map<List<BorrowDto>>(results);
        }

        public async Task<BorrowEntity> PostBorrow(BorrowEntity borrowEntity)
        {
            var result = await _borrowRepository.PostBorrow(borrowEntity);
            return result;
        }
    }
}
