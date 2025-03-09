using LibraryManagementSystem_API.DataAccess.Entities;
using LibraryManagementSystem_API.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess.Repositories.Concrete
{
    public class ImageRepository : ImageEntity, IImageRepository
    {
        protected DbSet<ImageEntity> dbSet;
        private readonly LibraryDbContext _dbContext;
        public ImageRepository(LibraryDbContext dbContext) {
            _dbContext = dbContext;
            dbSet = _dbContext.Set<ImageEntity>();
        }

        public void PostImage(ImageEntity image)
        {
            dbSet.Add(image);
            _dbContext.SaveChanges();
        }

        public ImageEntity GetImage(int id)
        {
            var image = dbSet.Find(id);

            return image;
        }
    }
}
