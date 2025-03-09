namespace LibraryManagementSystem_API.Business.Dtos.UserDtos
{
    public class GetUserDto
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Bio {  get; set; }
    }
}
