namespace LibraryManagementSystem_API.Business.Dtos.AuthDtos
{
    public class RefreshDto
    {
        public string message { get; set; }

        public bool success { get; set; }

        public string refreshToken { get; set; }
    }
}
