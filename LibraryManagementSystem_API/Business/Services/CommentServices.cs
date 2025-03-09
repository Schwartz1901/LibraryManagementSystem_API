using AutoMapper;
using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos.CommentDto;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;

namespace LibraryManagementSystem_API.Business.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CommentServices(ICommentRepository commentRepository, IBookRepository bookRepository,IMapper mapper) 
        {
            _commentRepository = commentRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<GetCommentDto>> GetComments(string bookId)
        {
            var results = await _commentRepository.GetComments(bookId);

            return _mapper.Map<List<GetCommentDto>>(results);
        }

        public void PostComment(string id, PostCommentDto postCommentDto)
        {
           _commentRepository.PostComment(id, _mapper.Map<CommentEntity>(postCommentDto));

            var rating = _commentRepository.AverageRatingOfBook(id);

            _bookRepository.UpdateAverageRating(id, rating);

        }

 /*       public void DeleteComment(string id, string username) { }*/
    }
}
