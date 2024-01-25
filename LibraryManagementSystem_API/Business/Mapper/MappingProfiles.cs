using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using AutoMapper;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Routing.Constraints;

namespace LibraryManagementSystem_API.Business.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            //Auth Mapping Profile
            CreateMap<UserEntity, LoginRequestDto>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.role, opt => opt.MapFrom(src => src.Role)).ReverseMap();
            // Book Mapping Profile
            CreateMap<GetBookDto, BookEntity>().ReverseMap();
        }
    } 
}
