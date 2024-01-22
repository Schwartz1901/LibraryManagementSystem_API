using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authServices;

        public AuthController(IAuthServices authServices) 
        {
            _authServices = authServices;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            
            var result = await _authServices.Login(loginRequestDto.username, loginRequestDto.password);
            if (result.success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
           
            var result = await _authServices.Register(registerRequestDto.username, registerRequestDto.password, registerRequestDto.confirmPassword);

            if (result.success)
            {
                return Ok(result);
            }
            else 
            { return BadRequest(result); }
        }
    }
}
