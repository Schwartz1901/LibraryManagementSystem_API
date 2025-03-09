using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOption) : base(dbContextOption) { }

        public DbSet<BookEntity> Book { get; set; }
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<RequestEntity> Request { get; set; }
        public DbSet<StaffEntity> Staff { get; set; }
        public DbSet<UserEntity> User { get; set; }

        public DbSet<CommentEntity> Comment { get; set; }

        public DbSet<BorrowEntity> Borrow { get; set; }

        public DbSet<ImageEntity> Image { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
/*            modelBuilder.Entity<BookEntity>().HasData(
                new BookEntity { BookId = Guid.NewGuid(), Author = "Author" , Name = "Test Book", Availability = true, Category = "Category", Description = "Description", ImageUrl = "URL"},
                new BookEntity { BookId = Guid.NewGuid(), Author = "Author 2", Name = "Test Book 2", Availability = true, Category = "Category", Description = "Description", ImageUrl = "URL 2" }
             );

            modelBuilder.Entity<RequestEntity>().HasData(
                new RequestEntity { RequestId = Guid.NewGuid(), BookId = Guid.NewGuid(), BookName = "Test Book", RequestType = "Hold", Status = "Pending", UserRequest = "User" }

                );*/

        }
    }
}
