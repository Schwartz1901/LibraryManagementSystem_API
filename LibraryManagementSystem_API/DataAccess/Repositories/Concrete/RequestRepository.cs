using Elastic.Clients.Elasticsearch;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class RequestRepository : RequestEntity , IRequestRepository
    {
        private readonly DbSet<RequestEntity> _dbSet;
        private readonly LibraryDbContext _dbContext;

        public RequestRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<RequestEntity>();
        }

        public async Task<RequestEntity> GetRequest(string id)
        {
            var request = await _dbSet.FirstOrDefaultAsync(r => (r.RequestId.ToString() == id));
            
            return request;
        }

        public async Task<List<RequestEntity>> GetRequests()
        {
            var requests = await _dbSet.ToListAsync();

            return requests;
        }

        public void PostRequest(RequestEntity request)
        {
            _dbSet.Add(request);
            _dbContext.SaveChanges();
        }

        public void PutRequest(RequestEntity request, bool accept, string reason)
        {

            if(request != null)
            {
                request.Status = "Finished";
                request.Reason = reason;
                request.ProcessedDate = DateTime.Now;
                request.DueDate = DateTime.Now.AddDays(7);
            }
            _dbContext.SaveChanges();
        }

        public async Task<List<RequestEntity>> GetBorrows(string username)
        {
            var result = await _dbSet.Where(b => (b.UserRequest == username &&
                                                    (DateTime.Compare(DateTime.Now, (DateTime)b.DueDate) <= 0 )
                                                    && b.Status == "Finished" && 
                                                    b.RequestType != "Return"))
                .ToListAsync<RequestEntity>();
            

            return result;
        }

        public async Task<List<RequestEntity>> GetRequestOfUser(string username)
        {
            var result = await _dbSet.Where(r => (r.UserRequest == username && r.Status == "Pending")).ToListAsync<RequestEntity>();

            return result;
        }

        public async Task<List<RequestEntity>> GetPastTransaction(string username)
        {
            var pastTransactions = await _dbSet.Where(r => (r.UserRequest == username && (r.Status == "Finished")))
                .ToListAsync();

            return pastTransactions;
        }


        public async Task<RequestEntity> GetRequestByUsernameAndBookId(string id, string username)
        {
            var request = await _dbSet.FirstOrDefaultAsync(
                r => (r.BookId.ToString() == id && r.UserRequest == username)
                );


            return request;
        }

        public async void UserPutRequest(string id, UserPutRequestDto dto)
        {
            string username = dto.Username;
            string type = dto.Type;
            var request = await _dbSet.FirstOrDefaultAsync(r => (r.BookId.ToString() == id && r.UserRequest == username));
            if (request != null)
            {
                request.RequestType = type;
                request.Status = "Pending";
            }
            else
            {
                Console.WriteLine("Request was Null");
            }
            _dbContext.SaveChanges();


        }

        public async Task<RequestEntity> Update(RequestEntity request)
        {
            _dbSet.Update(request);
            await _dbContext.SaveChangesAsync();

            return request;
        }

        public void Delete(string id)
        {
            var request = _dbSet.FirstOrDefault(r => r.RequestId.ToString() == id);

            _dbSet.Remove(request);

            _dbContext.SaveChanges();
        }

        public async void SaveChangeAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
