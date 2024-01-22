using LibraryManagementSystem_API.Business.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorServices _authorServices;

        public AuthorController(IAuthorServices authorServices) {
            _authorServices = authorServices;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _authorServices.Get();
            return Ok();
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetList(Guid id)
        {
            _authorServices.GetList();
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> Post(Guid id)
        {
            _authorServices.Post();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Put(Guid id)
        {
            _authorServices.Put();
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            _authorServices.Delete();
            return Ok();
        }
    }
}
