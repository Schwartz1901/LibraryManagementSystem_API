using LibraryManagementSystem_API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem_API.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> dbContextOption) : base(dbContextOption) { }

        public DbSet<AuthorEntity> Author { get; set; }
        public DbSet<BookEntity> Book { get; set; }
        public DbSet<NotificationEntity> Notification { get; set; }
        public DbSet<RequestEntity> Request { get; set; }
        public DbSet<StaffEntity> Staff { get; set; }
        public DbSet<UserEntity> User { get; set; }

        
    }
}
