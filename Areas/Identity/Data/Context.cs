using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PageSage.Areas.Identity.Data;
using System.Reflection.Emit;
using static System.Reflection.Metadata.BlobBuilder;

namespace PageSage.Areas.Identity.Data;

public class Context : IdentityDbContext<AppUser>
{
    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }
	public DbSet<UserBook> UserBooks { get; set; }
	public DbSet<AppRole> AppRoles { get; set; }
    public async Task AddBookAsync(UserBook userBook)
	{
		await UserBooks.AddAsync(userBook);
		await SaveChangesAsync();
	}
	protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }

	public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(128);
			builder.Property(x => x.Surname).HasMaxLength(128);
		}
	}
}
