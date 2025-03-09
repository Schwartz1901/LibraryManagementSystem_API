using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using AutoMapper;
using LibraryManagementSystem_API.Business.Dtos.AuthDtos;
using Microsoft.AspNetCore.Routing.Constraints;
using LibraryManagementSystem_API.Business.Dtos.UserDtos;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.Business.Dtos.CommentDto;
using LibraryManagementSystem_API.Business.Dtos.BorrowDto;
using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.NotificationDtos;

namespace LibraryManagementSystem_API.Business.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            //Auth Mapping Profile
            CreateMap<UserEntity, LoginRequestDto>()
                .ForMember(dest => dest.username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.password, opt => opt.MapFrom(src => src.Password)).ReverseMap();
            // Image Mapping
            CreateMap<ImageEntity, ImageDto>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => Convert.ToBase64String(src.Data)))
                .ReverseMap();

            //Epub mapping
/*            CreateMap<Epub, EpubDto>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => Convert.ToBase64String(src.FileContent)))
                .ReverseMap();*/

            // Book Mapping Profile
            CreateMap<BookEntity, GetBookDto>()
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            CreateMap<PostBookDto, BookEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Synopsis))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(dest => dest.EpubVersion, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Ebook, opt => opt.Ignore());
            CreateMap<BookEntity, StaffGetBookDto>()
                .ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image));

            // UserEntity Mapping
            CreateMap<GetUserDto, UserEntity>()
/*                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))*/
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ReverseMap();

            CreateMap<UserEntity, GetUserInfoDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.SignUpDate, opt => opt.MapFrom(src => src.CreationDate))
                .ReverseMap();

            // RequestEntity Mapping
            CreateMap<GetRequestDto, RequestEntity>()
                .ForMember(dest => dest.UserRequest, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.RequestId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.Date))
                .ReverseMap();
            CreateMap<PostRequestDto, RequestEntity>()
                .ForMember(dest => dest.UserRequest, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.RequestType, opt => opt.MapFrom(src => src.Type))
                .AfterMap((src, dest) =>
                {
                    dest.RequestId = Guid.NewGuid();
                    dest.Status = "Pending";
                    dest.CreatedDate = DateTime.Now;
                });

            CreateMap<RequestEntity, PastTransactionDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.BookName))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.RequestType))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.ProcessedDate, opt => opt.MapFrom(src => src.ProcessedDate))
                .ReverseMap();

            CreateMap<RequestEntity, BorrowDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequestId))
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.BookName))
                .ForMember(dest => dest.BorrowDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate));


            CreateMap<RequestEntity, UserPutRequestDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserRequest))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.RequestType))
                .ReverseMap();
            // CommentEntity Mapping
            CreateMap<GetCommentDto, CommentEntity>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Star))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ReverseMap();

            CreateMap<PostCommentDto, CommentEntity>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Star))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment))
                .AfterMap((src, dest) =>
                {
                    dest.CreatedDate = DateTime.Now;
                });
   /*             .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))*/
/*                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))*/


            // BorrowEntity Mapping
            CreateMap<BorrowDto, BorrowEntity>()
                .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            // Notification Mapping
            CreateMap<NotificationEntity, NotificationDto>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CreateAt))
                .ForMember(dest => dest.Read, opt => opt.MapFrom(src => src.Read))
                .ReverseMap()
                .ForMember(dest => dest.Username, opt => opt.Ignore());

        }
    } 
}
