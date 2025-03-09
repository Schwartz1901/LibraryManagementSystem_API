using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos;
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
        public async Task<GetBookDto> GetById(string id)
        {
            var bookEntity = await _bookRepository.GetById(id);
            return _mapper.Map<GetBookDto>(bookEntity);
        }
        public async Task<StaffGetBookDto> StaffGetById(string id)
        {
            var bookEntity = await _bookRepository.GetById(id);
            return _mapper.Map<StaffGetBookDto>(bookEntity);
        }
        public async Task<GetBookDto> GetByName(string name)
        {
            var bookEntity = await _bookRepository.GetByName(name);
            return _mapper.Map<GetBookDto>(bookEntity);
        }

        public BookResponse Post(PostBookDto postBookDto, ImageEntity image)
        {
            _bookRepository.Post(_mapper.Map<BookEntity>(postBookDto), image);

            var response = new BookResponse { message = "Added Successfully", status = "true" };
            return response;
        }
        public async Task<List<GetBookDto>> GetBooks()
        {
            var listBookEntities = await _bookRepository.GetBooks();

            List<GetBookDto> listBookDto = _mapper.Map<List<BookEntity>, List<GetBookDto>>(listBookEntities);
            return listBookDto;
        }

        public async Task<PostBookDto> PutBook(string id, PostBookDto postBookDto, ImageEntity image, Epub epub)
        {
            var result = await _bookRepository.PutBook(id, postBookDto, image, epub);

            return _mapper.Map<PostBookDto>(result);
        }

        public BaseResponse DeleteBook(string id)
        {
            _bookRepository.Delete(id);

            return new BaseResponse { message = "Delete successfully", status = "true" };
        }

        public async Task<EpubDto> GetEpub(string id)
        {
            var result = await _bookRepository.GetEpub(id);
            EpubDto epub = new EpubDto { Id = result.Id, 
                                        Data = Convert.ToBase64String(result.FileContent), 
                                        Name = result.FileName };

            return epub;
        }

        public void AddEpub(string id, Epub epub)
        {
            _bookRepository.AddEpub(id, epub);
        }

        public async Task<List<GetBookDto>> GetRecommendByRatings()
        {
            var books = await _bookRepository.GetRecommendByRatings();

            return _mapper.Map<List<GetBookDto>>(books);
        }
    }
}
