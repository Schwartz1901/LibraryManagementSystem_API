using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.Business.Abstracts
{
    public interface IImageServices
    {
        public ImageEntity PostImage(IFormFile file, MemoryStream memoryStream);

        public ImageEntity GetImage(int id);
    }
}
