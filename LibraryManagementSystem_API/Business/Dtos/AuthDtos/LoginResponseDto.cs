namespace LibraryManagementSystem_API.Business.Dtos.AuthDtos
{
    public class LoginResponseDto
    {
        public bool success { get; set; }
        public string message {  get; set; }
        public string role { get; set; }
        public string Accesstoken {  get; set; }

        public string Refreshtoken { get; set; }
    }
}
