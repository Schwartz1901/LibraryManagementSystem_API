using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.Business.Dtos;
using LibraryManagementSystem_API.Business.Dtos.CommentDto;
using LibraryManagementSystem_API.Business.Dtos.RequestDtos;
using LibraryManagementSystem_API.Business.Dtos.UserDtos;
using LibraryManagementSystem_API.Business.Services;
using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IBookServices _bookServices;
        private readonly ICommentServices _commentServices;
        private readonly IRequestServices _requestServices;
        private readonly IBorrowServices _borrowServices;
        public UserController(IUserServices userServices, 
                                IBookServices bookServices,
                                ICommentServices commentServices,
                                IRequestServices requestServices,
                                IBorrowServices borrowServices)
        {
            _userServices = userServices;
            _bookServices = bookServices;
            _commentServices = commentServices;
            _requestServices = requestServices;
            _borrowServices = borrowServices;
        }
        /*
                [HttpGet("username")] 
                public async Task<IActionResult> GetUserByName(string username)
                {
                    var result = await _userServices.GetUserByName(username);
                    return Ok(result);
                }*/

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var result = await _bookServices.GetBooks();


            return Ok(result);
        }

        [HttpGet("view-book/{id}/{username}")]
        public async Task<IActionResult> GetById([FromRoute]string id, [FromRoute] string username)
        {
            var book = await _bookServices.GetById(id);
            book.Score = await _userServices.GetUserScore(username);

            return Ok(book);
        }

        [HttpGet("view-book/{id}/comments")]
        public async Task<IActionResult> GetComments(string id)
        {
            var result = await _commentServices.GetComments(id);

            return Ok(result);
        }
       
        [HttpPost("view-book/{id}")]
        public async Task<IActionResult> PostComment(string id, [FromBody] PostCommentDto postCommentDto)
        {
            try
            {
                 _commentServices.PostComment(id, postCommentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("current-request/{username}")]
        public async Task<IActionResult> GetRequestsById(string username)
        {
/*            string username = user.Username;*/
            var result = await _requestServices.GetRequestOfUser(username);

            if (result.IsNullOrEmpty()) return NotFound();

            return Ok(result);
        }

        [HttpPost("view-book/{id}/request")]
        public async Task<IActionResult> PostRequest(string id,[FromBody] PostRequestDto postRequestDto)
        {

            try
            {
                var bookDto = await _bookServices.GetById(id);

                if (bookDto == null) return NotFound();

                var response = _requestServices.PostRequest(bookDto, postRequestDto);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("view-book/request/{id}")]
        public async Task<ActionResult> PutRequest([FromRoute] string id, [FromForm]UserPutRequestDto userPutRequestDto)
        {
            if (id.IsNullOrEmpty() || userPutRequestDto.Username.IsNullOrEmpty()) return BadRequest();

            try
            {
                var response = await _requestServices.UserPutRequest(id, userPutRequestDto);
                return Ok(response);

            } catch (Exception ex) {

                return StatusCode(500, ex.Message);
            }

        }


        /*        // deleteRequest
                *//*        [HttpDelete("current-request/delete-request/{id}")]
                        public Task DeleteComment(string username)
                        {
                            _commentServices.DeleteComment(username);
                            return null;
                        }
                */

        [HttpDelete("current-request/delete-request/{id}")]
        public  ActionResult UserDeleteRequest([FromRoute] string id)
        {
            if (id == null) return NotFound();

            _requestServices.Delete(id);

            return Ok();
        }

        [HttpGet("current-borrow/{username}")]
        public async Task<IActionResult> GetBorrows(string username)
        {
            /*            var result = await _borrowServices.GetBorrows(username);
                        if (result.IsNullOrEmpty()) return NotFound();
                        return Ok(result);*/
            var result = await _requestServices.GetBorrows(username);
            if (result.IsNullOrEmpty()) return NotFound();
            return Ok(result);
        }


        [HttpPost("send-borrow")]
        public async Task<IActionResult> PostBorrow([FromBody] BorrowEntity borrow)
        {
            var response = await _borrowServices.PostBorrow(borrow);

            return Ok(response);
        }

        [HttpGet("past-transaction/{username}")]
        public async Task<IActionResult> GetPastTransaction(string username)
        {
            var response = await _requestServices.GetPastTransaction(username);

            return Ok(response);
        }

        [HttpGet("view-book/{id}/download")]
        public async Task<IActionResult> GetEpub([FromRoute]string id)
        {
            var response = await _bookServices.GetEpub(id);

            return Ok(response);
        }


        [HttpGet("recommend")]
        public async Task<IActionResult> GetRecommenedBookByRatings()
        {
            var response = await _bookServices.GetRecommendByRatings();

            return Ok(response);
        }

        [HttpGet("profile/{username}")]
        public async Task<IActionResult> GetUserByName([FromRoute] string username)
        {
            if (username == null) return NotFound("Username was Empty!");

            try
            {
                var response = await _userServices.GetUserByName(username);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("profile/{username}")]
        public async Task<IActionResult> PutUser([FromRoute] string username, [FromForm]GetUserDto getUserDto)
        {
            if (username == null) return NotFound("Username was Empty");

            try
            {
                var response = await _userServices.PutUser(username, getUserDto);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
