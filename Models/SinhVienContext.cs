using Microsoft.EntityFrameworkCore;

namespace SinhVienApi.Models;

public class SinhVienContext : DbContext
{
	public SinhVienContext(DbContextOptions<SinhVienContext> options)
		: base(options)
	{
	}

	public DbSet<SinhVienItem> SinhVienItems { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;
}