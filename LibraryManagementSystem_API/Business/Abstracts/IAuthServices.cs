using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IAuthServices
    {
        public Task<LoginResponseDto> Login(string username, string password);

        public Task<RegisterResponseDto> Register(string username, string password, string confirmPassword);
    }
}