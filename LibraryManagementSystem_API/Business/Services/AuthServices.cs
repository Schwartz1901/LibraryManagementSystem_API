using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Text.Unicode;

namespace LibraryManagementSystem_API.Business.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthServices(IUserRepository userRepository, IMapper mapper) {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> Login(string username, string password) { 
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                var cres = _mapper.Map<LoginRequestDto>(user);

                if (username == cres.username && GetHashedPassword(password) == cres.password)
                {
                    return new LoginResponseDto { message = "Login Successfull.", success = true };
                }
                else
                {
                    return new LoginResponseDto { message = "Wrong Username of Password.", success = false };
                }

            }
            return new LoginResponseDto {message = "Account does not exist." , success = false };
            
        }

        public async Task<RegisterResponseDto> Register(string username, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                return new RegisterResponseDto { message = "The password does not match!", success = false };
            }
            
            var user = await _userRepository.GetByUsername(username);
            if (user != null)
            {
                return new RegisterResponseDto { message = "The account is already existed!", success = false };
            }
            else
            {
                string hashedPassword = GetHashedPassword(password);

                _userRepository.Post(new UserEntity { UserId = Guid.NewGuid(), UserName = username, Password = hashedPassword});
                return new RegisterResponseDto { message = "The account is successfully registered", success = true };
            }
        }

        private string GetHashedPassword(string password) 
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder stringBuilder = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return  stringBuilder.ToString();

            }
        }
    }

}
