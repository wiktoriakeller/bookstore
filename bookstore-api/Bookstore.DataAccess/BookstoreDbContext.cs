using Bookstore.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DataAccess
{
    public class BookstoreDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public BookstoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(eb =>
            {
                eb.Property(c => c.Title)
                    .HasMaxLength(200);

                eb.Property(c => c.Author)
                    .HasMaxLength(500);

                eb.Property(c => c.Genre)
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Publisher>(eb =>
            {
                eb.Property(c => c.Name)
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Book>()
                .HasIndex(c => c.Title)
                .IsUnique();

            modelBuilder.Entity<Publisher>()
                .HasIndex(c => c.Name)
                .IsUnique();
        }
    }
}
