using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.BookDtos;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using LibraryManagementSystem_API.DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LibraryManagementSystem_API.Business.Services
{
    public class BookServices : IBookServices
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookServices(IBookRepository bookRepository, IMapper mapper) 
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<GetBookDto> GetById(Guid guid)
        {
            var bookEntity = await _bookRepository.GetById(guid);
            return _mapper.Map<GetBookDto>(bookEntity);
        }
        public async Task<GetBookDto> GetByName(string name)
        {
            var bookEntity = await _bookRepository.GetByName(name);
            return _mapper.Map<GetBookDto>(bookEntity);
        }

        public async Task<PostBookDto> Post(PostBookDto postBookDto)
        {
            var result = await _bookRepository.Post();
            return results;
        }
    }
}
