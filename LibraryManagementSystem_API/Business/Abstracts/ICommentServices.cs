using LibraryManagementSystem_API.Business.Dtos.CommentDto;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface ICommentServices
    {
        public Task<List<GetCommentDto>> GetComments(string bookId);

        public void PostComment(string id, PostCommentDto postCommentDto);

/*        public void DeleteComment(string username);*/
    }
}
