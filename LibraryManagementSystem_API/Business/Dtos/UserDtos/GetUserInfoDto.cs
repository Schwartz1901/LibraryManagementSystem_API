namespace LibraryManagementSystem_API.Business.Dtos.UserDtos
{
    public class GetUserInfoDto
    {
        public string UserName { get; set; }
        public string Id{ get; set; }

        public string Email { get; set; }

        public string SignUpDate { get; set; }
    }
}
