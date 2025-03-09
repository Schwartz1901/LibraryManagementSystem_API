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
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AuthServices(IUserRepository userRepository, IAuthRepository authRepository,IMapper mapper) {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<LoginResponseDto> Login(string username, string password) { 
            var user = await _userRepository.GetUserByName(username);
            if (user != null)
            {
                var cres = _mapper.Map<LoginRequestDto>(user);

                if (username == cres.username && GetHashedPassword(password) == cres.password)
                {
                    var accessToken = _authRepository.CreateAccessToken(user);
                    var refreshToken = _authRepository.CreateRefreshToken();

                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpireTime = DateTime.Now.AddHours(1);

                    var updateUser = await _userRepository.PutUser(user);
                    

                    return new LoginResponseDto { message = "Login Successfull.", 
                        success = true, 
                        role= user.Role.ToLower(), 
                        Accesstoken = accessToken, 
                        Refreshtoken = refreshToken };
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
            
            var user = await _userRepository.GetUserByName(username);
            if (user != null)
            {
                return new RegisterResponseDto { message = "The account is already existed!", success = false };
            }
            else
            {
                string hashedPassword = GetHashedPassword(password);

                _userRepository.Post(new UserEntity { UserId = Guid.NewGuid(), UserName = username, Password = hashedPassword, Role="User", CreationDate = DateTime.Now});
                return new RegisterResponseDto { message = "The account is successfully registered", success = true };
            }
        }

        public async Task<RefreshDto> Refresh(RefreshToken refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshToken(refreshToken.Refreshtoken);

            if (user != null || user.RefreshTokenExpireTime <= DateTime.Now) 
            {
                throw new UnauthorizedAccessException("Invalid Refresh Token!");
            }

            var accessToken = _authRepository.CreateAccessToken(user);
            var newRefreshToken = _authRepository.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(1);

            var updateUser = _userRepository.PutUser(user);


            return new RefreshDto
            {
                message = "Refresh",
                success = true,
                refreshToken = newRefreshToken
            };
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
