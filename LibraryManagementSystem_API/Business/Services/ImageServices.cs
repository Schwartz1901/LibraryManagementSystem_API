using LibraryManagementSystem_API.Business.Abstracts;
using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;

namespace LibraryManagementSystem_API.Business.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IImageRepository _imageRepository;

        public ImageServices(IImageRepository imageRepository) 
        {
            _imageRepository = imageRepository;
        }
        public ImageEntity PostImage(IFormFile formFile, MemoryStream memoryStream)
        {
            var image = new ImageEntity
            {
                Name = formFile.FileName,
                Data = memoryStream.ToArray(),
                ContentType = formFile.ContentType
            };

/*            _imageRepository.PostImage(image);*/

            return image;

        }

        public ImageEntity GetImage(int id)
        {
            var image = _imageRepository.GetImage(id);

            return image;
        }

    }
}
