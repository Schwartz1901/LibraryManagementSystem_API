using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;

        public BookController(IBookServices bookServices) 
        {
            _bookServices = bookServices;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id) 
        {
            var result = await _bookServices.GetById(id);
            return Ok(result);
        }
        [HttpGet("name")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _bookServices.GetByName(name);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostBookDto postBookDto)
        {
            var result = await _bookServices.Post(postBookDto);
            return Ok(result);
        }
    }
}
