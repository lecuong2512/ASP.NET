using Microsoft.EntityFrameworkCore;

public class Day6_LeVIetCuongContext(DbContextOptions<Day6_LeVIetCuongContext> options) : DbContext(options)
{
    public DbSet<Day6_LeVIetCuong.Models.Product> Product { get; set; } = default!;
}
