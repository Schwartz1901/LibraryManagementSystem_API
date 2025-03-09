using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.UserDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;

namespace LibraryManagementSystem_API.Business.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserServices(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> GetUserByName(string name)
        {
            var userEntity = await _userRepository.GetUserByName(name);
            return _mapper.Map<GetUserDto>(userEntity);
        }



        public async Task<List<GetUserInfoDto>> GetUserInfo()
        {
            var userList = await _userRepository.GetUsers();
            return _mapper.Map<List<GetUserInfoDto>>(userList);
        }

        public async Task<GetUserDto> PutUser(string username, GetUserDto userDto)
        {
            var user = await _userRepository.GetUserByName(username);

            if (user == null)
            {
                throw new Exception($"User {username} was not found");
            }
            else
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.PhoneNumber = userDto.PhoneNumber;
                user.Email = userDto.Email;
                user.Bio = userDto.Bio;

                var updatedUser = await _userRepository.PutUser(user);
                return _mapper.Map<GetUserDto>(updatedUser);
            }


        }

        public async Task<int> GetUserScore(string username)
        {
            var user = await _userRepository.GetUserByName(username);

            return user.UserScore;
        }


    }
}
