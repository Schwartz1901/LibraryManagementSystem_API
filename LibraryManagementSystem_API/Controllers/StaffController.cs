using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.Business.Services;
using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api/staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IBookServices _bookServices;
        private readonly IUserServices _userServices;
        private readonly IRequestServices _requestServices;
        private readonly IImageServices _imageServices;
        private readonly INotificationServices _notificationServices;
        public StaffController(IBookServices bookServices, 
            IUserServices userServices, 
            IRequestServices requestServices, 
            IImageServices imageServices,
            INotificationServices notificationServices
            ) 
        { 
            _bookServices = bookServices;
            _userServices = userServices;
            _requestServices = requestServices;
            _imageServices = imageServices;
            _notificationServices = notificationServices;
        }

        [HttpGet("manage-book")]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookServices.GetBooks();

            return Ok(result);
        }

        [HttpGet("manage-book/{id}")]
        public async Task<IActionResult> GetBookById(string id)
        {
            if (id == null) return BadRequest("Id is empty");
            else
            {
                var result = await _bookServices.StaffGetById(id);
                return Ok(result);
            }
      
        }

        [HttpPost("manage-book/add-book")]
        public async Task<IActionResult> PostBook([FromForm] PostBookDto postBookDto)
        {
            if (postBookDto.Image == null || postBookDto.Image.Length == 0)
            {
                return BadRequest("No file Uploaded");
            }

            using var memoryStream = new MemoryStream();
            await postBookDto.Image.CopyToAsync(memoryStream);

            var image = _imageServices.PostImage(postBookDto.Image, memoryStream);

            var result = _bookServices.Post(postBookDto, image);
            return Ok(result);
        }

        [HttpPut("manage-book/edit-book/{id}")]
        public async Task<IActionResult> PutBook([FromRoute]string id, [FromForm] PostBookDto postBookDto)
        {
            if (postBookDto.Image == null || postBookDto.Image.Length == 0)
            {
                return BadRequest("No file Uploaded");
            }

            using var memoryStream = new MemoryStream();
            await postBookDto.Image.CopyToAsync(memoryStream);

            var image = _imageServices.PostImage(postBookDto.Image, memoryStream);

            var Ebook = postBookDto.Ebook;

            using var EbookMemoryStream = new MemoryStream();
            await Ebook.CopyToAsync(EbookMemoryStream);
            var epub = new Epub
            {
                FileContent = EbookMemoryStream.ToArray(),
                FileName = Ebook.FileName
            };

            var result = await _bookServices.PutBook(id, postBookDto, image, epub);




            return Ok(result);
        }

        [HttpDelete("manage-book/delete-book/{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] string id)
        {
            var result = _bookServices.DeleteBook(id);

            return Ok(new {message = "Delete Successfully"});
        }

        [HttpGet("user-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var result = await _userServices.GetUserInfo();
            return Ok(result);
        }

        [HttpGet("user-request")]
        public async Task<IActionResult> GetUserRequests()
        {
            var result = await _requestServices.GetRequests();

            return Ok(result);
        }

        [HttpPut("user-request/{id}")]
        public async Task<IActionResult> PutRequest([FromRoute]string id, [FromForm] PutRequestDto putRequestDto)
        {
            var result = await _requestServices.PutRequest(id, putRequestDto);
            
            return Ok(result);
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> PostImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file Uploaded");
            }

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            var image = _imageServices.PostImage(file, memoryStream);

            return Ok(new { image.Id });
            
        }

        [HttpGet("get-image/{id}")]
        public IActionResult GetImage(int id)
        {
            var image = _imageServices.GetImage(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image.Data, image.ContentType);
        }

        [HttpPost("post-epub/{id}")]
        public async Task<IActionResult> PostEpub([FromRoute] string id, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded");
            }
            using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var epub = new Epub
                {
                    FileContent = memoryStream.ToArray(),
                    FileName = file.FileName
                };

                _bookServices.AddEpub(id, epub);

                return Ok();
            
        }
    }
}
