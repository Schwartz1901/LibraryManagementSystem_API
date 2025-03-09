using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.Business.Dtos.BorrowDto;
using LibraryManagementSystem_API.Business.Dtos.NotificationDtos;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.DataAccess;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManagementSystem_API.Business.Services
{
    public class RequestServices : IRequestServices
    {

        protected readonly IRequestRepository _requestRepository;
        protected readonly IBookRepository _bookRepository;
        private readonly INotificationServices _notificationServices;
        private readonly IUserRepository _userRepository;
        private readonly LibraryDbContext _libraryDbContext;
        protected readonly IMapper _mapper;

        public RequestServices(IRequestRepository requestRepository, 
            IBookRepository bookRepository, 
            INotificationServices notificationServices,
            IUserRepository userRepository,
            LibraryDbContext libraryDbContext,
            IMapper mapper)
        {
            _requestRepository = requestRepository;
            _bookRepository = bookRepository;
            _notificationServices = notificationServices;
            _userRepository = userRepository;
            _libraryDbContext = libraryDbContext;
            _mapper = mapper;
        }

        

        public async Task<List<GetRequestDto>> GetRequests()
        {
            var listRequestEntities = await _requestRepository.GetRequests();

           List<GetRequestDto> listRequestDto = _mapper.Map<List<RequestEntity>,List<GetRequestDto>>(listRequestEntities);
            return listRequestDto;
        }

        public async Task<BaseResponse> PostRequest(GetBookDto bookDto, PostRequestDto postRequestDto)
        {
            RequestEntity request = _mapper.Map<RequestEntity>(postRequestDto);
/*            var book = await _bookRepository.GetById(id);*/


            if (bookDto == null)
            {
                return new BaseResponse { message = "failed", status = "false" };
            }
            else
            {
                request.BookId = bookDto.Id;
                request.BookName = bookDto.Name;

            }
            _requestRepository.PostRequest(request);
           
            var response = new BaseResponse { message = "success", status = "true" };

            var notificaition = new NotificationEntity { Text = $"Your request on {request.BookName} was sent!", 
                Read = false, CreateAt = DateTime.Now, Username = request.UserRequest };
            _notificationServices.AddNotification(notificaition);

            return (response);
        }

        public async Task<BaseResponse> PutRequest(string id, PutRequestDto putRequestDto)
        {
            var response = new BaseResponse { message = "", status = ""};

            if (id == null)
            {
                response.message = "Empty Id";
                response.status = "Failed";
            }
            else
            {
                var request = await _requestRepository.GetRequest(id);

                if (request != null)
                {
                    var bookEntity = await _bookRepository.GetById(request.BookId.ToString());

                    string accept = putRequestDto.Accept;
                    string reason = putRequestDto.Reason;

                    if (request.RequestType == "Borrow")
                    {
                        if (bookEntity.Stock > 0 && accept == "Yes")
                        {
                            bookEntity.Stock = bookEntity.Stock - 1;
                            request.ProcessedDate = DateTime.Now;
                            request.DueDate = DateTime.Now.AddDays(7);
                            request.Status = "Finished";
                            request.Reason = reason;

                            /* _notificationServices.ScheduleNotification("Borrow Book overdue", request.DueDate);*/

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You can borrow a book!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);

                            response =  new BaseResponse { message = "Accept the borrow", status = "true" };
                        }
                        else if (bookEntity.Stock <= 0 && accept == "Yes")
                        {

                            response = new BaseResponse { message = "Out of stock", status = "false" };
                        }
                        else
                        {
                            request.ProcessedDate = DateTime.Now;
                            request.Status = "Finished";
                            request.Reason = reason;

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You cannot borrow books!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);

                            response = new BaseResponse { message = "Failed to Borrow book", status = "false" };
                        }

                    }
                    else if (request.RequestType == "Return")
                    {

                        if (accept == "Yes")
                        {
                            bookEntity.Stock += 1;
                            request.ProcessedDate = DateTime.Now;
                            request.Status = "Finished";

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You Returned a book",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);
                            response = new BaseResponse { message = "Return book successfully", status = "true" };
                        }
                        else
                        {
                            request.ProcessedDate = DateTime.Now;
                            request.Status = "Finished";
                            request.Reason = reason;

                            var notificaition = new NotificationEntity
                            {
                                Text = $"No book was return!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };

                            var user = await _userRepository.GetUserByName(request.UserRequest);
                            user.UserScore -= 5;
                            _libraryDbContext.SaveChanges();
                            _notificationServices.AddNotification(notificaition);
                            response =  new BaseResponse { message = "No book was return", status = "true" };
                        }


                    }
                    else if (request.RequestType == "Hold")
                    {
                        if (accept == "Yes")
                        {
                            request.ProcessedDate = DateTime.Now;
                            request.DueDate = request.DueDate.AddDays(7);
                            request.Status = "Finished";
                            request.Reason = reason;

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You can Extend the Due Date!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);

                            response =  new BaseResponse { message = "Hold Book successfully", status = "true" };
                        }
                        else
                        {
                            request.ProcessedDate = DateTime.Now;
                            request.Status = "Finished";
                            request.Reason = reason;

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You cannot extend Due date a book!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);

                            response = new BaseResponse { message = "Fail to Hold book", status = "false" };
                        }
                    }

                    else if (request.RequestType == "Reserve")
                    {
                        if (accept == "Yes")
                        {
                            if (bookEntity.Stock <=5)
                            {
                                
                                response = new BaseResponse { message = "Cannot reserve due to low stock", status = "false" };
                            }

                        
                            else
                            {
                                request.ProcessedDate = DateTime.Now;
                                request.DueDate = DateTime.Now.AddDays(7);
                                request.Status = "Finished";
                                request.Reason = reason;
                                bookEntity.Stock -= 1;

                                var notificaition = new NotificationEntity
                                {
                                    Text = $"You have Reserved a book!",
                                    Read = false,
                                    CreateAt = DateTime.Now,
                                    Username = request.UserRequest
                                };
                                _notificationServices.AddNotification(notificaition);

                                response = new BaseResponse { message = "Reserve book successfully", status = "true" };
                            }
                            
                        }
                        else
                        {
                            request.ProcessedDate = DateTime.Now;
                            request.Status = "Finished";
                            request.Reason = reason;

                            var notificaition = new NotificationEntity
                            {
                                Text = $"You cannot Reserve books!",
                                Read = false,
                                CreateAt = DateTime.Now,
                                Username = request.UserRequest
                            };
                            _notificationServices.AddNotification(notificaition);

                            response = new BaseResponse { message = "Failed to reserve book", status = "false" };
                        }
                    }

                }
                var u = await _requestRepository.Update(request);
            }
   

            return response;

        }

        public async Task<List<BorrowDto>> GetBorrows(string username)
        {
            var results = await _requestRepository.GetBorrows(username);

            return _mapper.Map<List<BorrowDto>>(results);
        }

        public async Task<List<GetRequestDto>> GetRequestOfUser(string username)
        {
            var result = await _requestRepository.GetRequestOfUser(username);

            return _mapper.Map<List<GetRequestDto>>(result);
        }

        public async Task<List<PastTransactionDto>> GetPastTransaction(string username)
        {
            var result = await _requestRepository.GetPastTransaction(username);

            return _mapper.Map<List<PastTransactionDto>>(result) ;
        }


        public async Task<GetRequestDto> UserPutRequest(string id, UserPutRequestDto userPutRequestDto)
        {
            string username = userPutRequestDto.Username;
            string type = userPutRequestDto.Type;

            var request = await _requestRepository.GetRequest(id);

            if (request == null)
            {
                throw new Exception($"Request with ID {id} was not found.");
            }

            request.RequestType = type;
            request.Status = "Pending";

            var updatedRequest = await _requestRepository.Update(request);

            return _mapper.Map<GetRequestDto>(updatedRequest);
  

        }

        public void Delete(string id)
        {
            _requestRepository.Delete(id);

        }


        private BaseResponse HandleBookOfHoldRequest(bool accept)
        {
            if (accept)
            {
                return new BaseResponse { message = "Hold Book successfully", status = "true" };
            }
            else
            {
                return new BaseResponse { message = "Fail to Hold book", status = "false" };
            }
        }



/*        private BaseResponse HandleBookOfReserveRequest(bool accept, BookEntity book)
        {
            if (accept && book.Stock <= 0)
            {
                return 
            }
        }*/
    }
}
