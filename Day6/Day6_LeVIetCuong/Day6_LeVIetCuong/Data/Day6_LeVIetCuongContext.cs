using Day6_LeVIetCuong.Models;
using Microsoft.EntityFrameworkCore;

namespace Day6_LeVIetCuong.Data
{
    public class Day6_LeVIetCuongContext(DbContextOptions<Day6_LeVIetCuongContext> options) : DbContext(options)
    {
        public DbSet<Genre> Genres { get; set; } = default!;
        public DbSet<Movie> Movies { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movie");
                entity.Property(m => m.Title).IsRequired().HasMaxLength(200);
                entity.HasOne(m => m.Genre)
                      .WithMany(g => g.Movies)
                      .HasForeignKey(m => m.GenreId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Hành động" },
                new Genre { Id = 2, Name = "Phiêu lưu" },
                new Genre { Id = 3, Name = "Hài kịch" },
                new Genre { Id = 4, Name = "Chính kịch" },
                new Genre { Id = 5, Name = "Kinh dị" },
                new Genre { Id = 6, Name = "Khoa học viễn tưởng" },
                new Genre { Id = 7, Name = "Giả tưởng" },
                new Genre { Id = 8, Name = "Lãng mạn" },
                new Genre { Id = 9, Name = "Giật gân" },
                new Genre { Id = 10, Name = "Hoạt hình" }
            );

            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Phim 1", ReleaseDate = new DateTime(2025, 1, 15), Duration = 100, GenreId = 1 },
                new Movie { Id = 2, Title = "Phim 2", ReleaseDate = new DateTime(2026, 5, 20), Duration = 120, GenreId = 3 }
            );
        }
    }
}
