using LibraryManagementSystem_API.DataAccess.Entities;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Abstract
{
    public interface IImageRepository
    {
        public void PostImage(ImageEntity image);

        public ImageEntity GetImage(int id);
    }
}
