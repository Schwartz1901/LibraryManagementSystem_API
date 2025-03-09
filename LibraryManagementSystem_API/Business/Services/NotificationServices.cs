using Hangfire;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.DataAccess;
using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.AspNetCore.SignalR;
using LibraryManagementSystem_API.Hubs;
using LibraryManagementSystem_API.Business.Dtos.NotificationDtos;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
/*using Microsoft.AspNet.SignalR;*/
/*using Microsoft.AspNetCore.SignalR;*/

namespace LibraryManagementSystem_API.Business.Services
{
    public class NotificationServices : INotificationServices
    {
        private readonly LibraryDbContext _dbContext;
        private readonly IMapper _mapper;

        public NotificationServices(LibraryDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;

        }

        public async Task<List<NotificationDto>> GetNotification(string username)
        {
            var notifications = await _dbContext.Notification.Where(n => n.Username == username).ToListAsync();

            return _mapper.Map<List<NotificationDto>>(notifications);
        }

        public async Task<NotificationDto> PostNotification(string username, NotificationDto notificationDto)
        {
            NotificationEntity noti = new NotificationEntity {
                Text = notificationDto.Text, Username = username,
                Read = false, CreateAt = DateTime.Now
            };
            var notification = await _dbContext.Notification.AddAsync(noti);
            await _dbContext.SaveChangesAsync();

                return _mapper.Map<NotificationDto>(noti);
        }
        
        public void AddNotification(NotificationEntity notification)
        {
            _dbContext.Notification.Add(notification);
            _dbContext.SaveChanges();
        }

        public async Task<NotificationDto> ReadNotification(string username)
        {
            await _dbContext.Notification.Where(n => n.Username == username).ForEachAsync(notification =>
            {
                notification.Read = true;
            });

            await _dbContext.SaveChangesAsync();

            var noti = new NotificationDto { Text = "text" };

            return noti;

        }
    }
}
