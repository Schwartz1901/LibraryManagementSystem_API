using LibraryManagementSystem_API.Business.Dtos.NotificationDtos;
using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface INotificationServices
    {
        /*        public void ScheduleNotification(string message, DateTime scheduleAt);

                public Task SendNotification(int notificationId);*/

        public Task<List<NotificationDto>> GetNotification(string username);

        public Task<NotificationDto> PostNotification(string username, NotificationDto notificationDto);

        public void AddNotification(NotificationEntity notificationEntity);



        public Task<NotificationDto> ReadNotification(string username);
    }
}
