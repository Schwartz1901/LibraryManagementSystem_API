using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.NotificationDtos;
using LibraryManagementSystem_API.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class NotificationController : ControllerBase
    {


        private readonly INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [HttpPost]


        [HttpGet("mail/{username}")] 
        public async Task<IActionResult> GetNotification([FromRoute] string username)
        {
            try 
            { 
                var response = await _notificationServices.GetNotification(username);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("mail/{username}")]
        public async Task<IActionResult> PostNotification([FromRoute] string username, [FromForm] NotificationDto notificationDto)
        {
            try
            {
                var response = await _notificationServices.PostNotification(username, notificationDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPut("mail/{username}")]
        public async Task<IActionResult> ReadNotification([FromRoute] string username)
        {
            try
            {
                var response = await _notificationServices.ReadNotification(username);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
