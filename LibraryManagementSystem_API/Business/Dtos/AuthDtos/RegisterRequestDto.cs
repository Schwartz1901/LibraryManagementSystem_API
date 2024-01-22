namespace LibraryManagementSystem_API.Business.Dtos.AuthDtos
{
    public class RegisterRequestDto
    {
        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword {  get; set; }
    }
}
