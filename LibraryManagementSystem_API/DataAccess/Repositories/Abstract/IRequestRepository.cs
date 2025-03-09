using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IRequestRepository
    {
        public Task<RequestEntity> GetRequest(string id);
        public Task<List<RequestEntity>> GetRequests();

        public Task<List<RequestEntity>> GetRequestOfUser(string username);
        public void PostRequest(RequestEntity requestEntity);

        public void Delete(string id);
        public void PutRequest(RequestEntity request, bool accept, string reason);

        public Task<RequestEntity> GetRequestByUsernameAndBookId(string id, string username);

        public void UserPutRequest(string id, UserPutRequestDto dto);

        public Task<RequestEntity> Update(RequestEntity request);

        public Task<List<RequestEntity>> GetBorrows(string username);
        public Task<List<RequestEntity>> GetPastTransaction(string username);

        public void SaveChangeAsync();
    }
}
