using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.Business.Dtos.BorrowDto;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IRequestServices
    {
        public Task<List<GetRequestDto>> GetRequests();
        public Task<BaseResponse> PostRequest(GetBookDto bookDto, PostRequestDto postRequestDto);

        public Task<BaseResponse> PutRequest(string id, PutRequestDto putRequestDto);


        public Task<List<GetRequestDto>> GetRequestOfUser(string username);

        public void Delete(string id);

        public Task<List<BorrowDto>> GetBorrows(string username);

        public Task<List<PastTransactionDto>> GetPastTransaction(string username);


        public Task<GetRequestDto> UserPutRequest(string id, UserPutRequestDto userPutRequestDto);
    }
}
